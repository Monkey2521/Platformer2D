using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public delegate void OnInventoryChanged ();
	public OnInventoryChanged OnInventoryChangedCallback;

	[Header("Debug settings")]
	public bool isDebug;

	[Header("Settings")]
	public static Inventory instance;

	public GameObject eventPanel;

	private int _room = 18;
	public List<ItemInventory> items = new List<ItemInventory>();

	private void Awake () {

		if (instance != null) {
			if (isDebug) {
				Debug.LogWarning ("Inventory instance is already setted!");
			}
			return;
		}

		instance = this;

	}

	public bool Add (ItemInventory item) {
		if (items.Count >= _room) {
			if (isDebug) {
				Debug.LogWarning ("Not enough room in inventory!");
			}
			return false;
		}

		if (!InInventory (item) || !item.item.haveCount) {
			items.Add (item);
			if (isDebug) {
				Debug.Log ("Add new item to inventory");
			}
		} 
		else {
			items [FindIndex (item)].count += item.count;
			if (isDebug) {
				Debug.Log ("Add count for item in inventory");
			}
		}

		if (isDebug) {
			Debug.Log ("Pick up " + item.item._name);	
		}

		if (OnInventoryChangedCallback != null) {
			OnInventoryChangedCallback.Invoke ();
		}

		if (isDebug) {
			Debug.Log ("Count of " + items [FindIndex (item)].item._name + " = " + items [FindIndex (item)].count);
		}

		Debug.Log (EventController.instance);

		StartCoroutine(EventController.instance.Show ((item.count > 1 ? (item.count + " ") : "") + item.item._name));

		GetComponent<SoundManager> ().PickUpItemSound ();

		return true;
	}

	public void Remove (Item item, bool all = false, int count = 1) {
		if (all) {
			items.Remove (items[FindItemIndex (item)]);
		} else {
			items [FindItemIndex (item)].count -= count;
		}

		if (items [FindItemIndex (item)].count == 0) {
			items.Remove (items [FindItemIndex (item)]);
		}

		if (OnInventoryChangedCallback != null) {
			OnInventoryChangedCallback.Invoke ();
		}
	}

	public void CheckItem(ItemInventory item) {
		if (item.count == 0) {
			Remove (item.item, true);
		}
	}

	private bool InInventory (ItemInventory item) {
		for (int i = 0; i < items.Count; i++) {
			if (item.item._name == items [i].item._name) {
				return true;
			}
		}

		return false;
	}

	private int FindIndex (ItemInventory item) {
		for (int i = 0; i < items.Count; i++) {
			if (item.item._name == items [i].item._name) {
				return i;
			}
		}

		return -1;
	}

	public int FindItemIndex (Item item) {
		for (int i = 0; i < items.Count; i++) {
			if (item._name == items [i].item._name) {
				return i;
			}
		}

		return -1;
	} 
}
