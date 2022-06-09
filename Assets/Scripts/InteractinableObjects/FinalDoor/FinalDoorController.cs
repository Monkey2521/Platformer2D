using UnityEngine;
using UnityEngine.UI;

public class FinalDoorController : InteractionableObject {

	public Item[] keys;
	public GameObject messageBox;
	public GameObject door;

	private void Awake () {
		GetReferences ();
		SetKeyText (GameObject.FindObjectOfType<PlayerController> ().actionKey.ToString ());
		SetPromptText ("OTKPbITb");
		HideAction ();

	}

	public override GameObject Action () {
		ActionAnimation ();

		if (CheckKeys ()) {
			OpenDoor ();
			ShowMessage ("OTKPbITO");

			for (int i = 0; i < keys.Length; i++) {
				Inventory.instance.Remove (keys[i]);
			}

			Destroy (door);

			GetComponent<BoxCollider2D> ().enabled = false;
		} else {
			string text = "HE XBATAET KJII04EU !!!\nHEO6XODUMO:\nKPACHbIU U CUHUU";
			ShowMessage (text);
		}

		return null;
	}

	private bool CheckKeys () {
		for (int i = 0; i < keys.Length; i++) {
			if (Inventory.instance.FindItemIndex (keys [i]) < 0)
				return false;
		}
		return true;
	}

	private void ShowMessage (string text) {
		messageBox.SetActive (true);
		messageBox.GetComponentInChildren<Text> ().text = text;
	}

	private void OpenDoor () {

	}

	protected override void ActionAnimation () {
		_onAction = true;
	}

	public override void StopAction () {
		_onAction = false;
	}

	private void GetReferences () {
		_animator = GetComponent<Animator> ();

		_actionTag = actionObjectTag.ToString ();
	}
}
