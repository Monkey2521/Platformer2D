using UnityEngine;

public abstract class DamageableObject : MonoBehaviour {

	[Header("Debug Settings")]
	public bool isDebug;

	[Header("Settings")]
	[SerializeField] protected float _maxHP;
	protected float _HP = 0f;
	public bool isAlive;

	protected Animator _animator;

	public void TakeDamage (float damage) {
		_HP -= damage;

		TakeDamageAnimation ();

		ChangeHeatlBar ();

		if (_HP <= 0f) {
			isAlive = false;
			DieAnimation ();
		}
	}
		
	protected virtual void TakeDamageAnimation () {
		_animator.SetTrigger ("TakeDamage");
	}

	protected virtual void DieAnimation () {
		_animator.SetTrigger ("Die");
	}

	public void Heal (float heal) {
		_HP += heal;

		if (_HP > _maxHP) {
			_HP = _maxHP;
		}

		if (_HP <= 0f) {
			_HP = 1f;
		}

		ChangeHeatlBar ();
	}

	protected virtual void ChangeHeatlBar () {
		// Someting
	}

	public virtual void Destroy () {
		if (isDebug) {
			Debug.Log ("Destroy " + this.gameObject.name);
		}
	}
}
