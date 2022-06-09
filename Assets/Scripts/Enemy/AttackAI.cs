using UnityEngine;

public class AttackAI : MonoBehaviour {

	private PlantAI _plantAI;
	private BeeAI _beeAI;
	private SlugAI _slugAI;

	private void Awake () {
		GetReferences ();
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		if (_plantAI != null)
			_plantAI.ColliderEnter (collider);
		else if (_beeAI != null)
			_beeAI.ColliderEnter (collider);
		else
			_slugAI.ColliderEnter (collider);
	}

	private void OnTriggerStay2D (Collider2D collider) {
		if (_plantAI != null)
			_plantAI.ColliderStay (collider);
		else if (_beeAI != null)
			_beeAI.ColliderStay (collider);
		else
			_slugAI.ColliderStay (collider);
	}

	private void GetReferences () {
		_plantAI = GetComponentInParent<PlantAI> ();
		_beeAI = GetComponentInParent<BeeAI> ();
		_slugAI = GetComponentInParent<SlugAI> ();
	}
}
