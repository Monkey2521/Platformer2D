using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryItemInfo : MonoBehaviour {

	public GameObject infoPanel;

	private Item _item;

	public Text itemName;
	public Text description;

	public GameObject craftPanel;
	public GameObject useButton;

	public GameObject craftPrefab;
	public RectTransform craftParent;

	public GameObject messageBox;

	private GameObject[] _craftSlots;

	public Item[] items;
	private Dictionary<string, Item> craftItems = new Dictionary<string, Item> ();

	private void Awake () {
		SetDictionary ();
	}

	private void Start () {
		infoPanel.SetActive (false);
	}

	private void SetDictionary () {
		for (int i = 0; i < items.Length; i++) {
			craftItems.Add (items[i].name, items[i]);
		}
	}

	private void SetInfo () {
		itemName.text = _item._name;
		description.text = _item.description;
	}

	public void ShowInfo (ItemInventory itemInventory) {
		_item = itemInventory.item; 

		infoPanel.SetActive (true);

		SetInfo ();

		ShowCrafts ();
	}

	public void HideInfo () {
		_item = null;

		itemName.text = "ItemName";
		description.text = "Item description";

		craftPanel.SetActive (false);
		infoPanel.SetActive (false);
	}

	private void ShowCrafts () {
		if (_item.craftable) {
			craftPanel.SetActive (true);
			useButton.SetActive (false);

			SetCrafts (_item);
		} 

		else {
			craftPanel.SetActive (false);
			useButton.SetActive (true);
		}
	}

	public void SetCrafts (Item item) {
		DestroyCrafts ();
		_craftSlots = new GameObject[item.crafts.Length];

		for (int i = 0; i < item.crafts.Length; i++) {
			GameObject craftSlot = (GameObject)Instantiate (craftPrefab);
			craftSlot.GetComponent<RectTransform> ().SetParent (craftParent);

			craftSlot.transform.localScale = new Vector3 (1f, 1f, 1f);

			craftSlot.GetComponent<CraftSlot> ().messageBox = this.messageBox;

			Item dictItem;

			if (craftItems.TryGetValue (item.crafts [i].ToString (), out dictItem))
				craftSlot.GetComponent<CraftSlot> ().SetCraft (dictItem);

			_craftSlots [i] = craftSlot;
		}
	}

	public void DestroyCrafts () {
		if (_craftSlots == null) {
			return;
		}

		for (int i = _craftSlots.Length - 1; i >= 0; i--) {
			Destroy (_craftSlots[i]);
		}
	}

	public void HideMessageBox () {
		messageBox.SetActive (false);
	}

	public void UseItem () {
		_item.Use ();
		if (_item.name != "RedKey" && _item.name != "BlueKey") { 
			Inventory.instance.Remove (_item);
		}
	}
}
