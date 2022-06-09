using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackController : AttackController {

	private PlayerAudioManager _audioManager;

	public GameObject weaponPrefab;
	public Transform parentTransform;

	public Text attackStat;

	private void Awake () {
		GetReferences ();

		ChangeStat ();

		FindObjectOfType<PlayerController> ().OnDamageChangedCallback += ChangeStat;
	}

	private void Update () {
		_timer -= Time.deltaTime;
	}

	private void ChangeStat () {
		damage = defaultDamage * effectDamageMultiplier;
		attackStat.text = damage.ToString ();
	}

	public void Attack () {
		if (_timer <= 0f) {
			if (isDebug) {
				Debug.Log ("Attack");
			}

			AttackAnimation ();
			ThrowWeapon ();

			_audioManager.AttackSound ();
		} else {
			if (isDebug) {
				Debug.Log ("Not time yet!"); 
			}
		}
	}

	protected override void AttackAnimation ()
	{
		_timer = timeBetweenAttack;
	}

	public void OnWeaponCollision (Collider2D collider) {
		MakeDamage (collider);
	}

	private void ThrowWeapon () {
		GameObject weapon = (GameObject)Instantiate (weaponPrefab);
		weapon.transform.position = parentTransform.position;

		weapon.GetComponent<WeaponController> ().targetTag = _targetTag;

		weapon.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 5f, ForceMode2D.Impulse);
		weapon.GetComponent<Rigidbody2D> ().AddForce (
			FindObjectOfType<PlayerMoveController> ().GetViewSide () * weapon.GetComponent<WeaponController> ().throwForce, 
			ForceMode2D.Impulse
		);
	}

	private void GetReferences () { 
		_animator = GetComponentInParent<Animator> ();
		_audioManager = GetComponentInParent <PlayerAudioManager> ();

		Debug.Log (_audioManager);

		_targetTag = targetTag.ToString ();
	}
}
