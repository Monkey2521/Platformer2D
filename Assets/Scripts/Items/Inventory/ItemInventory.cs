using UnityEngine;

public class ItemInventory {
	[Header("Ddebug settings")]
	public bool isDebug;

	[Header("Settings")]
	private Inventory inventory;

	public Item item;

	public int count;

	public ItemInventory (Item item) {
		this.item = item;
	}

	public void SetCount () {
		count = UnityEngine.Random.Range (item.minCount, item.maxCount);
	}

	public void PickUp () {
		if (inventory == null) {
			inventory = Inventory.instance;
		}

		if (isDebug) {
			Debug.Log ("Try to pick up " + item._name);
		}

		inventory.Add (this);
	}
}
