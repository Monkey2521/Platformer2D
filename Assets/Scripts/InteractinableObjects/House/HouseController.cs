using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : InteractionableObject {

	public Transform housePosition;

	private bool _entered = false;

	public GameObject reloadableChest;
	public int chestLevel;

	private void Start () {
		GetReferences ();
		SetKeyText (GameObject.FindObjectOfType<PlayerController> ().actionKey.ToString ());
		SetPromptText ("BOUTU");
		HideAction ();
	}

	public override GameObject Action () {
		StopAction ();

		FindObjectOfType<PlayerController> ().SetCheckpointPosition (
			GameObject.FindGameObjectWithTag ("Player").transform.position);
		
		FindObjectOfType<PlayerController> ().SetPositionInHouse (housePosition.position);

		FindObjectOfType<CameraMoveController> ().SetInHouse ();

		if (!_entered) {
			reloadableChest.GetComponent<InHouseReloadChest> ().Reload (chestLevel);
			_entered = true;
		}

		return null;
	}

	protected override void ActionAnimation () {
		_onAction = true;
	}

	public override void StopAction () {
		_onAction = false;
	}

	private void GetReferences () {
		_actionTag = actionObjectTag.ToString ();
	}
}
