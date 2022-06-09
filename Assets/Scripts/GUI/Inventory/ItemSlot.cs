using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {

	public GameObject itemSlot;
	public Image icon;
	public Text count;

	public ItemInventory item = null;

	public bool setted;

	private InventoryItemInfo _inventoryItemInfo;

	private void Awake () {
		_inventoryItemInfo = FindObjectOfType<InventoryItemInfo> ();
	}

	public void SetSlot (ItemInventory item) {
		itemSlot.SetActive (true);

		this.item = item;

		icon.sprite = item.item.icon;
		count.text = item.count.ToString ();

		setted = true;
	}

	public void HideSlot () {
		itemSlot.SetActive (false);

		item = null;

		icon.sprite = null;
		count.text = "";

		setted = false;
	}

	public virtual void ThrowItem () {
		Inventory.instance.Remove (item.item, all: true);
	}

	public void ShowItemInfo () {
		Debug.Log(item.item._name);
		_inventoryItemInfo.ShowInfo (item);
	}
}
