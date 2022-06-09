using UnityEngine;
using UnityEngine.UI;

public class ChestController : InteractionableObject {

	public GameObject chestPanel;
	public Button closeButton;

	private void Awake () {
		GetReferences ();
		SetKeyText (GameObject.FindObjectOfType<PlayerController> ().actionKey.ToString ());
		SetPromptText ("OTKPbITb");
		HideAction ();

		chestPanel.SetActive (false);

		closeButton.onClick.AddListener (FindObjectOfType<PlayerController> ().CloseActiveUI);
	}

	public override GameObject Action () {
		ActionAnimation ();
		return chestPanel;
	}

	private void GetReferences () {
		_animator = GetComponent<Animator> ();

		_actionTag = actionObjectTag.ToString ();
	}
}
