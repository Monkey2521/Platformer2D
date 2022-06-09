using UnityEngine;

public class CameraMoveController : MonoBehaviour {

	[Header("Debug Settings")]
	public bool isDebug;
	public Color areaColor;

	[Header("Camera Settings")]
	public Collider2D target;
	public Vector2 focusAreaSize;
	public float verticalOffset;
	public float lookAheadDistanceX;
	public float lookSmoothX;
	[SerializeField] private float _offsetZ;

	public Transform _inHousePosition;
	private bool _inHouse = false;

	[Header("Settings")]
	public GameObject gameOverMenu;
	public GameObject player;

	#region Moving settings
	private float _currentLookAheadX;
	private float _targetLookAheadX;
	private float _smoothLookVelocityX;
	private Vector2 _focusPosition;
	private FocusArea _focusArea;

	private bool _gameOver = false;
	#endregion

	private void Start (){
		_focusArea = new FocusArea (target.bounds, focusAreaSize);
		FindObjectOfType<PlayerController> ().OnPlayerDieCallback += GameOver;
	}

	private void LateUpdate (){
		if (!_gameOver && !_inHouse) {
			_focusArea.Update (target.bounds);
			_focusPosition = _focusArea.center + Vector2.up * verticalOffset;

			_targetLookAheadX = lookAheadDistanceX;
			_currentLookAheadX = Mathf.SmoothDamp (_currentLookAheadX, _targetLookAheadX, ref _smoothLookVelocityX, lookSmoothX);

			_focusPosition += Vector2.right * _currentLookAheadX;

			transform.position = (Vector3)_focusPosition + Vector3.forward * _offsetZ;
		}
	}

	private void GameOver () {
		_gameOver = true;

		gameOverMenu.SetActive (true);

		if (isDebug) {
			Debug.Log ("Game over!"); 
		}
	}

	public void Restart () {
		if (isDebug) {
			Debug.Log ("Restart");
		}

		player.SetActive (true);
		player.GetComponent<PlayerController> ().Restart ();

		SetNormal ();

		_gameOver = false;
		gameOverMenu.SetActive (false);
	}

	public void MainMenu () {
		if (isDebug) {
			Debug.Log ("Go to main menu");
		}
	}

	public void SetInHouse () {
		_inHouse = true;
		transform.position = _inHousePosition.position;
	}

	public void SetNormal () {
		_inHouse = false;
	}

	public void SetInLastHouse (Transform position) {
		_inHouse = true;
		transform.position = position.position;
	}

	void OnDrawGizmosSelected (){
		if (isDebug) {
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireCube (_focusArea.center, focusAreaSize);
		}
	}
}
