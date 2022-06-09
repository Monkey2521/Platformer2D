using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItemSlot : ItemSlot {

	private Inventory inventory;

	private void Start () { 
		GetReferences ();
	}

	public void AddToInventory () {
		inventory.Add (item);

		HideSlot ();

		GetComponentInParent<ChestItemController> ().DecreaseLenght ();
	}

	private void GetReferences () {
		inventory = Inventory.instance;
	}
}
