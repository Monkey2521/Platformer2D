using UnityEngine;

public class SlugMoveAI : MoveController {

	private float _waitTimer = 0f;
	public float _time = 3f;

	private void Awake () {
		GetReferences ();

		_isMoving = true;
	}

	private void Update () {
		_waitTimer += Time.deltaTime;
		if (_waitTimer >= _time) {
			_waitTimer = 0f;
			_isMoving = !_isMoving;
			if (_isMoving) {
				SetViewSide (-_viewSide);
			}
		}
	}

	private void FixedUpdate () {
		if (_isMoving) {
			Move (_viewSide);
		}
	}
	private void GetReferences () {
		_rigidbody = GetComponent<Rigidbody2D> ();
		_transform = GetComponent<Transform> ();
		_animator = GetComponent<Animator> ();

		_viewSide = defaultViewSide;
	}

	protected override void MoveAnimation ()
	{
		// null
	}
}

