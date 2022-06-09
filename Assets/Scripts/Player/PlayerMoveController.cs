using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveController : MoveController {
	private PlayerAudioManager _audioManager;

	#region Ground Raycast
	private bool _onGround;
	private RaycastHit2D _groundRaycast;
	public float onGroundRaycastDistance;
	public float isJumpingRaycastDistance;
	public LayerMask groundLayerMask;
	#endregion

	#region More moving
	public float jumpForce;
	private bool _isJumping = false;
	private bool _isFalling = false;

	public Tags stairsTag;
	public float climbSpeed;
	private bool _isClimbing = false;
	private bool _canClimb = false;
	private string _stairsTag;
	private Vector2 _verticalVelocity;

	public float effectSpeedMutliplier = 1f;
	#endregion

	#region Keys for moving
	private KeyCode _left;
	private KeyCode _right;
	private KeyCode _up;
	private KeyCode _down;
	private KeyCode _jump;
	private KeyCode _run;
	#endregion

	public Text speedStat;

	private void Awake () {
		GetReferences ();

		ChangeStat ();

		FindObjectOfType<PlayerController> ().OnSpeedChangedCallback += ChangeStat;
	}

	private void ChangeStat () {
		//Debug.Log ("Change speed");

		float speed = defaultSpeed * effectSpeedMutliplier;

		if (_isRunning) {
			speed *= runSpeedMultiplier;
		}

		//Debug.Log (speed);

		speedStat.text = speed.ToString ();
	}

	protected override void StopMoving (bool forcibly)
	{
		base.StopMoving (forcibly);

		ChangeStat ();
	}

	private void Update () {
		if (!PlayerController._onUI && !PlayerController._onPause) {
			CheckOnGround ();

			if (!_canClimb) {
				CheckFalling ();
			} else {
				_isFalling = false;
			}

			StopMoving (false);

			if (_onGround) {
				_isJumping = false;
				_isFalling = false;
				_isClimbing = false;

				_animator.SetBool ("Falling", false);
				_animator.SetBool ("Climbing", false);
			}

			if (!_onGround && _isFalling) {
				_animator.SetBool ("Falling", true);
				_isFalling = true;
			}
				
			if (_isFalling) {
				_isJumping = false;
				_animator.SetBool ("Jumping", false);
			}

			_animator.SetBool ("Jumping", _isJumping);
			_animator.SetBool ("Falling", _isFalling);
			_animator.SetBool ("Climbing", _isClimbing);

			if (!_isClimbing && PlayerController.smoking) {
				FindObjectOfType<PlayerController> ().ShowCiggarete ();
			} else {
				FindObjectOfType<PlayerController> ().HideCiggarete ();
			}
		}
	}

	private void FixedUpdate () {
		if (FindObjectOfType<PlayerController> ().isAlive && !PlayerController._onUI && !PlayerController._onPause) {
			#region Run
			if (_isMoving) {
				if (Input.GetKey (_run)) {
					_isRunning = true;
					_animator.SetBool ("Run", true);

					if (FindObjectOfType<PlayerController> ().OnSpeedChangedCallback != null) {
						FindObjectOfType<PlayerController> ().OnSpeedChangedCallback.Invoke ();
					}
				}

				if (Input.GetKeyUp (_run)) {
					_isRunning = false;
					_animator.SetBool ("Run", false);

					if (FindObjectOfType<PlayerController> ().OnSpeedChangedCallback != null) {
						FindObjectOfType<PlayerController> ().OnSpeedChangedCallback.Invoke ();
					}
				}
			}
			#endregion

			#region Move right
			if (Input.GetKey (_right)) {
				_isMoving = true;
				Move (Vector2.right);
			}
			if (Input.GetKeyUp (_right)) {
				_isMoving = false;
				_isRunning = false;
				StopMoving (true);
			}
			#endregion
		
			#region Move left
			if (Input.GetKey (_left)) {
				_isMoving = true;
				Move (Vector2.left);
			}
			if (Input.GetKeyUp (_left)) {
				_isMoving = false;
				_isRunning = false;
				StopMoving (true);
			}
			#endregion

			#region Jump
			if (Input.GetKey (_jump)) {
				Jump ();
			}
			#endregion

			Climb ();
		} else {
			if (isDebug) {
				Debug.Log ("Can't move" + FindObjectOfType<PlayerController> ().isAlive);
			}
		}
	}

	private void CheckOnGround () {
		_groundRaycast = Physics2D.Raycast (
			_transform.position,
			Vector2.down,
			onGroundRaycastDistance,
			groundLayerMask);
		_onGround = _groundRaycast;

		/*if (isDebug) {
			Debug.Log ("OnGround " + _onGround);
		}*/
	}

	private void CheckFalling () {
		_groundRaycast = Physics2D.Raycast (
			_transform.position,
			Vector2.down,
			isJumpingRaycastDistance,
			groundLayerMask);
		_isFalling = !_groundRaycast;

		/*if (isDebug) {
			Debug.Log ("Fall " + _isFalling);
		}*/
	}

	public void Jump () {
		if (_onGround && _rigidbody.velocity.y == 0f) {
			_animator.SetTrigger ("Jump");
			_rigidbody.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
			_isJumping = true;

			_audioManager.JumpSound ();

			if (isDebug) {
				Debug.Log ("Jump!");
			}
		} else {
			if (isDebug) {
				Debug.Log ("Can't jump!");
			}
		}
	}

	#region Climbing
	private void Climb () {
		if (_canClimb) {
			#region Climp up
			if (Input.GetKey (_up)) {
				_verticalVelocity.Set (_rigidbody.velocity.x, climbSpeed);
				_rigidbody.velocity = _verticalVelocity;

				_isClimbing = true;
				_animator.SetBool ("Climbing", true);

				if (isDebug) {
					Debug.Log ("Climbing up!");
				}
			}
			if (Input.GetKeyUp (_up)) {
				_verticalVelocity.Set (_rigidbody.velocity.x, 0f);
				_rigidbody.velocity = _verticalVelocity;

				_isClimbing = false;
				_animator.SetBool ("Climbing", false);
			}
			#endregion

			#region Climb down
			if (Input.GetKey (_down)) {
				_verticalVelocity.Set (_rigidbody.velocity.x, -climbSpeed);
				_rigidbody.velocity = _verticalVelocity;

				_isClimbing = true;
				_animator.SetBool ("Climbing", true);

				if (isDebug) {
					Debug.Log ("Climbing down!");
				}
			}
			if (Input.GetKeyUp (_down)) {
				_verticalVelocity.Set (_rigidbody.velocity.x, 0f);
				_rigidbody.velocity = _verticalVelocity;
				 
				_isClimbing = false;
				_animator.SetBool ("Climbing", false);

			}
			#endregion
		}

		if (_canClimb && !_isClimbing) {
			_rigidbody.velocity = new Vector2 (_rigidbody.velocity.x, 0f);
		}
	}

	private void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == _stairsTag) {
			_canClimb = true;
			_animator.SetBool ("CanClimb", true);

			if (isDebug) {
				Debug.Log ("Can climb!");
			}
		}
	}

	private void OnTriggerExit2D (Collider2D collider) {
		if (collider.tag == _stairsTag) {
			_canClimb = false;

			_animator.SetBool ("CanClimb", false);

			if (isDebug) {
				Debug.Log ("Can't climb now");
			}
		}
	}
	#endregion

	protected override float SpeedMultiprier ()
	{
		float speedMultiplier = 1f;

		if (_isRunning) {
			speedMultiplier *= runSpeedMultiplier;
		}

		speedMultiplier *= effectSpeedMutliplier;

		//Debug.Log (speedMultiplier);

		return speedMultiplier;
	}

	public Vector2 GetViewSide () {
		return _viewSide;
	}

	private void GetReferences () {
		_rigidbody = GetComponent<Rigidbody2D> ();
		_transform = GetComponent<Transform> ();
		_animator = GetComponent<Animator> ();

		_stairsTag = stairsTag.ToString ();

		_left = GetComponent<PlayerController> ().moveLeftKey;
		_right = GetComponent<PlayerController> ().moveRightKey;
		_up = GetComponent<PlayerController> ().moveUpOnStairsKey;
		_down = GetComponent<PlayerController> ().moveDownOnStairsKey;
		_run = GetComponent<PlayerController> ().runKey;
		_jump = GetComponent<PlayerController> ().jumpKey;

		_audioManager = GetComponent<PlayerAudioManager> ();

		_viewSide = defaultViewSide;
	}
}
