using UnityEngine;

public class InventoryItemContoller : MonoBehaviour {

	private Inventory inventory;

	private ItemSlot[] itemSlots;

	public GameObject emptyPanel;
	public GameObject inventoryPanel;

	private void Start () {
		GetReferences ();
		HideAll ();

		inventory.OnInventoryChangedCallback += SetSlots;

		emptyPanel.SetActive (false);
		inventoryPanel.SetActive (false);
	}

	public void UpdateUI () {
		SetSlots ();
	}

	private void SetSlots () {

		inventory = Inventory.instance;

		for (int i = 0; i < inventory.items.Count; i++) {
			itemSlots [i].SetSlot (inventory.items [i]);
		}

		for (int i = inventory.items.Count; i < itemSlots.Length; i++) {
			itemSlots [i].HideSlot ();
		}

		if (inventory.items.Count == 0) {
			emptyPanel.SetActive (true);
		} else {
			emptyPanel.SetActive (false);
		}
	}

	private void HideAll () {
		for (int i = 0; i < itemSlots.Length; i++) {
			itemSlots [i].HideSlot ();
		}
	}

	public int GetSlotCount () {
		return itemSlots.Length;
	}

	private void GetReferences () {
		inventory = Inventory.instance;

		itemSlots = GetComponentsInChildren<ItemSlot> ();
	}

}
