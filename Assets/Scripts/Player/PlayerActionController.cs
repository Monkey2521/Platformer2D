using UnityEngine;

public class PlayerActionController : MonoBehaviour {
	[Header("Debug settings")]
	public bool isDebug;

	[Header("Settings")]
	[SerializeField] private Tags interactionableObjectTag;
	private string _actionTag;

	public bool _canAction;
	public bool _onAction;

	private Collider2D _actionCollider;

	private void Awake () {
		GetReferences ();
	}

	public GameObject Action () {
		_onAction = true;

		if (isDebug) {
			Debug.Log ("Action with " + _actionCollider.gameObject.name);
		}

		return _actionCollider.GetComponent<InteractionableObject> ().Action ();
	}

	private void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == _actionTag) {
			_canAction = true;
			_actionCollider = collider;

			if (isDebug) {
				Debug.Log ("Can action with " + collider.gameObject.name);
			}
		}
	}

	private void OnTriggerExit2D (Collider2D collider) {
		if (collider.tag == _actionTag) {
			_canAction = false;

			StopAction ();

			_actionCollider = null;

			if (isDebug) {
				Debug.Log ("Can't action with " + collider.gameObject.name);
			}
		}
	}

	public void StopAction () {
		_onAction = false;

		if (_actionCollider != null) {
			_actionCollider.GetComponent<InteractionableObject> ().StopAction ();
		}
	}

	private void GetReferences () {
		_actionTag = interactionableObjectTag.ToString ();
	}
}
