using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	private MusicManager _manager;

	public GameObject adventureBox;

	private void Awake() {
		GetReferences ();
	}

	private void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == "Player") {
			switch (this.gameObject.name) {
			case "MusicController0":
				if (_manager._currentMusic != _manager.adventureMusic) {
					_manager.PlayAdventureMusic ();
				}
				break;
			case "MusicController1":

				break;
			case "MusicController2":

				break;
			case "MusicController3":

				break;
			case "MusicController4":
				if (_manager._currentMusic != _manager.battleMusic) {
					_manager.PlayBattleMusic ();
				}
				break;
			case "MusicController5":
				if (_manager._currentMusic != _manager.battleMusic) {
					_manager.PlayBattleMusic ();
				}
				break;
			case "MusicController6":
				if (_manager._currentMusic != _manager.lastBattleMusic) {
					_manager.PlayLastBattleMusic ();
				}
				break;
			case "MusicController7":
				if (_manager._currentMusic != _manager.gamePassMusic) {
					_manager.PlayGamePassMusic ();
				}
				break;
			case "MusicController8":
				if (_manager._currentMusic != _manager.houseMusic) {
					_manager.PlayHouseMusic ();
				}
				break;
			}

			GetComponent<BoxCollider2D> ().enabled = false;
			if (adventureBox != null) {
				adventureBox.SetActive (true);
			}
		}
	}

	private void GetReferences () {
		_manager = FindObjectOfType<MusicManager> ();
	}
}
