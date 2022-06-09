using UnityEngine;
using UnityEngine.UI;

public abstract class InteractionableObject : MonoBehaviour {

	[Header("Debug settings")]
	public bool isDebug;

	[Header("Settings")]
	[SerializeField] protected GameObject _keyForAction;
	[SerializeField] protected GameObject _promptPanel;

	private Text _keyText;
	private Text _promptText;

	protected bool _onAction;

	[SerializeField] protected Tags actionObjectTag;
	protected string _actionTag;

	protected Animator _animator;

	protected void ShowAction () {
		_keyForAction.SetActive (true);
		_promptPanel.SetActive (true);

		if (isDebug) {
			Debug.Log ("Action is available");
		}
	}

	protected void HideAction () {
		_keyForAction.SetActive (false);
		_promptPanel.SetActive (false);

		if (isDebug) {
			Debug.Log ("Can't action");
		}
	}

	protected void SetKeyText (string text) {
		_keyText = _keyForAction.GetComponentInChildren<Text> ();
		_keyText.text = text;
	}

	protected virtual void SetPromptText (string text) {
		_promptText = _promptPanel.GetComponentInChildren<Text> ();
		_promptText.text = text;
	}

	private void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == _actionTag) {
			ShowAction ();
		}
	}

	private void OnTriggerExit2D (Collider2D collider) {
		if (collider.tag == _actionTag) {
			HideAction ();
		}
	}

	protected virtual void ActionAnimation () {
		_animator.SetBool ("Action", true);
	}

	public abstract GameObject Action ();

	public virtual void StopAction () {
		_onAction = false;

		if (_animator != null) {
			_animator.SetBool ("Action", false);
		}
	}

}
