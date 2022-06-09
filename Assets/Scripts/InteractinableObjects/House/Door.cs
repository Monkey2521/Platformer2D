using UnityEngine;

public class Door : InteractionableObject {

	private void Start () {
		GetReferences ();
		SetKeyText (GameObject.FindObjectOfType<PlayerController> ().actionKey.ToString ());
		SetPromptText ("BbIUTU");
		HideAction ();
	}

	public override GameObject Action () {
		StopAction ();

		FindObjectOfType<PlayerController> ().SetPositionInCheckpoint ();

		FindObjectOfType<CameraMoveController> ().SetNormal ();

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
