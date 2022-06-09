using UnityEngine;

public class MoveController : MonoBehaviour {
	#region Components
	protected Transform _transform;
	protected Rigidbody2D _rigidbody;
	protected Animator _animator;
	#endregion

	[Header("Debug settings")]
	public bool isDebug;

	[Header("Settings")]
	public float defaultSpeed;
	public float runSpeedMultiplier;
	public Vector2 defaultViewSide;

	#region Move
	protected Vector2 _viewSide;
	protected Vector2 _horizontalVelocity;
	protected Vector3 _horizontalRotation = new Vector3 (0f, 180f, 0f);

	protected bool _isMoving;
	protected bool _isRunning;
	#endregion

	#region Wall Raycast
	protected bool _onWall;
	private RaycastHit2D _wallRaycast;
	public float onWallRaycastDistance;
	public LayerMask wallLayerMask;
	#endregion

	public void Move (Vector2 side) {
		float speed;

		SetViewSide (side);
		CheckOnWall ();

		if (_onWall) {
			speed = 0f;
		} else {
			speed = defaultSpeed * SpeedMultiprier () * _viewSide.x;
		}

		if (_isMoving) {
			_horizontalVelocity.Set (speed, _rigidbody.velocity.y);
			_rigidbody.velocity = _horizontalVelocity;
		}

		if (_isMoving && _isRunning) {
			RunAnimation ();
		} else if (_isMoving) {
			MoveAnimation ();
		}
	}

	protected virtual void StopMoving (bool forcibly) {
		if (forcibly || Mathf.Abs (_rigidbody.velocity.x) < defaultSpeed * SpeedMultiprier () * 0.85f) {
			_horizontalVelocity.Set (0f, _rigidbody.velocity.y);
			_rigidbody.velocity = _horizontalVelocity;

			_isMoving = false;
			_isRunning = false;

			_animator.SetBool ("Move", false);
			_animator.SetBool ("Run", false);
		}
	}

	protected virtual float SpeedMultiprier () {
		return 1f;
	}

	protected void SetViewSide (Vector2 side) {
		#region Rotate to right side
		if (!_viewSide.Equals (Vector2.right) && side.Equals (Vector2.right)) {
			_viewSide = Vector2.right;
			_transform.Rotate (_horizontalRotation);

			if (isDebug) {
				Debug.Log ("Rotate to right side");
			}
		}
		#endregion

		#region Rotate to left side
		if (!_viewSide.Equals (Vector2.left) && side.Equals (Vector2.left)) {
			_viewSide = Vector2.left;
			_transform.Rotate (_horizontalRotation);

			if (isDebug) {
				Debug.Log ("Rotate to left side");
			}
		}
		#endregion
	}

	protected void CheckOnWall () {
		_wallRaycast = Physics2D.Raycast (
			_transform.position,
			_viewSide,
			onWallRaycastDistance,
			wallLayerMask);
		_onWall = _wallRaycast;

		/*if (isDebug) {
			Debug.Log ("Wall " + _onWall);
		}*/
	}

	protected virtual void MoveAnimation () {
		_animator.SetBool ("Move", true);
	}

	protected virtual void RunAnimation () {
		_animator.SetBool ("Run", true);
	}
}
