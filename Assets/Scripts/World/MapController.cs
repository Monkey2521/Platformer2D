using UnityEngine;

public class MapController : MonoBehaviour {

	public GameObject map;

	private void Start () {
		if (this.gameObject.name != "0") {
			map.SetActive (false);
		}
	}

	private void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == "Player") {
			map.SetActive (true);
		}
	}

	private void OnTriggerExit2D (Collider2D collider) {
		if (collider.tag == "Player") {
			map.SetActive (false);
		}
	}
}
