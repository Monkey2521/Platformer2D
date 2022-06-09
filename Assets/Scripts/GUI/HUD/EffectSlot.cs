using UnityEngine;
using UnityEngine.UI;

public class EffectSlot : MonoBehaviour {

	public GameObject effectSlot;

	[SerializeField] private Image icon;
	[SerializeField] private Text timer;
	[SerializeField] private Text effectName;

	public Effect effect = null;

	private float _timer = 10f;

	private void DestroySlot () {
		GetComponentInParent<EffectsController> ().RemoveSlot (this.gameObject);
	}

	private void Update () {
		if (effect != null) {
			_timer -= Time.deltaTime;
			if (_timer < 0f) {
				effect.RevealEffect ();
				GetComponentInParent<EffectsController> ().RemoveSlot (this.gameObject);
				Debug.Log ("Destroy slot");
			}

			timer.text = SetTimer ();
		}
	}

	private string SetTimer () {
		return ((int)_timer / 60).ToString () + ":" + ((int)_timer % 60).ToString ();
	}

	public void SetSlot () {
		effectName.text = effect._name;
		_timer = effect.effectTime;
		timer.text = _timer.ToString ();
		icon.sprite = effect.icon;

		_timer = effect.effectTime;
	}
}
