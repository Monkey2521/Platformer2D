using UnityEngine;

public class EnemyAttackController : AttackController {

	public string Tag;

	private void Awake () {
		GetReferences ();
		damage = defaultDamage;
	}

	private void Update () {
		_timer -= Time.deltaTime;
	}

	public virtual void Attack () {
		if (_timer <= 0f) {
			if (isDebug) {
				Debug.Log ("Attacks!");
			}

			if (_animator != null) {
				AttackAnimation ();
			}
		}
	}

	private void GetReferences () {
		_animator = GetComponentInParent<Animator> ();

		_targetTag = targetTag.ToString ();

		Tag = _targetTag;
	}
}
