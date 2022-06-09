using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteItem : InteractionableObject {

	Inventory inventory;
	public Item dropItem;
	private ItemInventory _itemInventory;

	private Notes notes;
	public Note note;


	private void Start () {
		GetReferences ();
		SetKeyText (GameObject.FindObjectOfType<PlayerController> ().actionKey.ToString ());
		SetPromptText ("B39ITb");
		HideAction ();
	}

	public override GameObject Action () {
		notes.AddNote (note);
		inventory.Add (_itemInventory);
		ActionAnimation ();
		return null;
	}

	protected override void ActionAnimation () {
		if (_animator != null)
			_animator.SetTrigger ("Action");
		else {
			DestroyObject ();
		}
		_onAction = true;
	}

	public override void StopAction () {
		_onAction = false;
	}

	public void DestroyObject () {
		Destroy (this.gameObject);
	}

	private void GetReferences () {
		_animator = GetComponent<Animator> ();

		_actionTag = actionObjectTag.ToString ();

		inventory = Inventory.instance;

		_itemInventory = new ItemInventory (dropItem);
		_itemInventory.count = 1;

		notes = Notes.instance;
	}

}
