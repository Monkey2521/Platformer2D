using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastHouseController : MonoBehaviour {

	public Transform cameraPosition;

	private void OnTriggerEnter2D (Collider2D collider) {
		FindObjectOfType<CameraMoveController> ().SetInLastHouse (cameraPosition);
	}
}
