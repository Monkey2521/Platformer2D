using UnityEngine;

public class SecretRoomController : MonoBehaviour {

	public GameObject HideTileSet;
	public GameObject SecretRoom;

	private void Awake () {
		SecretRoom.SetActive (false);
	}

	private void OnTriggerEnter2D (Collider2D collider) {
		OpenRoom ();
	}

	public void OpenRoom () {
		HideTileSet.SetActive (false);
		SecretRoom.SetActive (true);
	}
}
