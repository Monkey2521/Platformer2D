using UnityEngine;
using UnityEngine.UI;

public class HUDHelper : MonoBehaviour {

	[Header("Debug settings")]
	public bool isDebug;

	[Header("Elements")]
	private Animator _animator;

	public Image buttonImage;
	public Sprite showSprite;
	public Sprite hideSprite;
	private bool _onHelper = false;

	private void Awake () {
		GetReferences ();
	}

	public void ChangeHelper () {
		if (_onHelper) {
			_animator.SetTrigger ("Hide");
			_onHelper = false;
			buttonImage.sprite = showSprite;
		} 
		else {
			_animator.SetTrigger ("Show");
			_onHelper = true;
			buttonImage.sprite = hideSprite;
		}
			
		if (isDebug) {
			Debug.Log (_onHelper);
		}
	}

	private void GetReferences () {
		_animator = GetComponent<Animator> ();
	}
}
