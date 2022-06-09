using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAI : EnemyAttackController {

	private Collider2D _collider;

	private void OnTriggerStay2D (Collider2D collider) {
		if (collider.tag == _targetTag) {
			_collider = collider;
			Attack ();
		}
	}

	public override void Attack ()
	{
		if (_timer <= 0f) {
			if (isDebug) {
				Debug.Log ("Attacks!");
			}

			MakeDamage (_collider);

			_timer = timeBetweenAttack;
		}
	}

}
