using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour {

	public ItemInventory item;

	public Text itemName;
	public Image icon;

	public GameObject messageBox;

	public void SetCraft (Item item) {
		this.item = new ItemInventory (item);
		
		itemName.text = item._name;
		icon.sprite = item.icon;
	}

	public void Craft () {
		bool enoughMaterials = false;
		Inventory inventory = Inventory.instance;

		#region Check materials for craft
		for (int i = 0; i < item.item.itemsCount.Length; i++) {
			int index = inventory.FindItemIndex (item.item.itemsForCraft[i]);
			if (index >= 0) {
				if (inventory.items [index].count < item.item.itemsCount [i]) {
					enoughMaterials = false;
					break;
				}
			} else {
				enoughMaterials = false;
				break;
			}

			enoughMaterials = true;
		}
		#endregion

		#region Craft
		if (enoughMaterials) {
			ShowMessageBox ("Получено ", true);
			Debug.Log ("Crafting...");

			item.count = item.item.craftCount;

			for (int i = 0; i < item.item.itemsCount.Length; i++) {
				inventory.Remove (inventory.items[inventory.FindItemIndex (item.item.itemsForCraft[i])].item, count: item.item.itemsCount[i]);
			}

			Inventory.instance.Add (item);

		}
		#endregion

		#region Show error
		else {
			string requireMaterials = "";

			for (int i = 0; i < item.item.itemsCount.Length; i++) {
				requireMaterials += (item.item.itemsForCraft[i]._name + ": " + item.item.itemsCount[i].ToString ());
				if (i != item.item.itemsCount.Length - 1) {
					requireMaterials += "\n";
				}
			}

			ShowMessageBox ("Не хватает ресурсов!\n" + requireMaterials, false);
		}
		#endregion
	}

	private void ShowMessageBox(string text, bool positive) {
		messageBox.SetActive (true);

		Text messageText = messageBox.GetComponentInChildren<Text> ();

		if (positive) {
			messageText.color = Color.green;
		} else {
			messageText.color = Color.red;
		}

		messageText.text = text;
	}

	public void HideMessageBox () {
		messageBox.SetActive (false);
	}
}
