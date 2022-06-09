using UnityEngine;

public class WeaponController : MonoBehaviour {

	public string targetTag;

	public float throwForce;

	public float timeBeforeDestroy;
	private float _timer = 0f;

	private void Update () {
		if (_timer >= timeBeforeDestroy) {
			Destroy (this.gameObject);
		}
		_timer += Time.deltaTime;
	}

	private void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == targetTag) {
			FindObjectOfType<PlayerAttackController> ().OnWeaponCollision (collider);
		}
		if (collider.tag != "Player" && !collider.isTrigger) {
			Destroy (this.gameObject);
		}
	}
}
