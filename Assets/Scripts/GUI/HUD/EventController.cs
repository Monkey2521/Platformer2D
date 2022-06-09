using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EventController : MonoBehaviour {

	public Text takedItem;

	private bool _isShow;
	private float _time = 3f;
	private float _timer = 0f;

	private List<string> queue = new List<string> ();

	private Animator _animator;

	public static EventController instance;

	private void Awake () {
		GetReferences ();

		if (instance != null) {
			Debug.Log ("Event controller error");
			_isShow = false;
			return;
		}

		instance = this;

		_isShow = false;	
	}

	private void Update () {
		if (_isShow) {
			_timer += Time.deltaTime;
		}
	}

	public void Hide () {
		queue.Remove (queue[0]);
		
		_isShow = false;

		_timer = 0f;

		_animator.SetTrigger ("Hide");
	}

	public IEnumerator Show (string text) {
		queue.Add (text);
		int index = queue.Count - 1;

		if (!_isShow) {
			yield return StartCoroutine (CoroutineShow (text));
			Hide ();
		} else {
			yield return Await (index - 1);
			yield return StartCoroutine (CoroutineShow (queue[0]));
			Hide ();

		}
		yield return null;
	}

	private IEnumerator CoroutineShow (string text) {
		_isShow = true;

		takedItem.text = text;

		_animator.SetTrigger ("Show");

		yield return new WaitForSeconds (_time);
	}

	private IEnumerator Await (int multiplier) {
		yield return new WaitForSeconds (_time - _timer + 0.2f + 0.2f * multiplier + _time * multiplier);
	}

	private void GetReferences () {
		_animator = GetComponent<Animator> ();
	}
}
