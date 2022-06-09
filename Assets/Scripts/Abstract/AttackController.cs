using UnityEngine;

public abstract class AttackController : MonoBehaviour {

	protected Animator _animator;

	[Header("Debug settings")]
	public bool isDebug;

	[Header("Settings")]
	public float defaultDamage;
	protected float damage;
	public float timeBetweenAttack;
	[SerializeField] protected Tags targetTag;

	public float effectDamageMultiplier = 1f;

	protected string _targetTag;
	protected float _timer = 0f;

	protected void MakeDamage (Collider2D collider) {
		collider.GetComponent<DamageableObject> ().TakeDamage (damage * effectDamageMultiplier);

		if (isDebug) {
			Debug.Log (collider.gameObject.name + " take " + damage + " damage");
		}
	}

	protected virtual void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == _targetTag) {
			MakeDamage (collider);
		}
	}

	protected virtual void AttackAnimation () {
		if (_animator != null) {
			_animator.SetTrigger ("Attack");
		}
		_timer = timeBetweenAttack;
	}
}
