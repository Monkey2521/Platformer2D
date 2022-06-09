using UnityEngine;

public class PlacementObject : InteractionableObject {

	Inventory inventory;

	public Item dropItem;
	private ItemInventory _itemInventory;

	public void Start () {
		GetReferences ();
		SetKeyText (GameObject.FindObjectOfType<PlayerController> ().actionKey.ToString ());
		SetPromptText ("B39ITb");
		HideAction ();
	}

	public override GameObject Action () {
		ActionAnimation ();
		inventory.Add (_itemInventory);
		return null;
	}

	protected override void ActionAnimation () {
		_animator.SetTrigger ("Action");

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
	}

}
