using UnityEngine;

public class ChestItemController : MonoBehaviour {

	private ChestItemSlot[] itemSlots;

	private Item[] items;

	public Item[] level1Items;
	public Item[] level2Items;
	public Item[] level3Items;
	public Item[] level4Items;

	public int level;

	public GameObject emptyPanel;

	private ItemInventory[] _itemInventory;

	private int _itemCount;

	private void Start () {
		GetReferences ();

		switch (level) {
		case 1:
			items = level1Items;
			break;
		case 2:
			items = level2Items;
			break;
		case 3:
			items = level3Items;
			break;
		case 4:
			items = level4Items;
			break;
		}

		_itemInventory = new ItemInventory[itemSlots.Length];

		_itemCount = itemSlots.Length;

		emptyPanel.SetActive (false);

		SetItems ();
	}

	public void LoadItems (int level) {
		if (itemSlots == null) {
			GetReferences ();
		}

		this.level = level;

		switch (level) {
		case 1:
			items = level1Items;
			break;
		case 2:
			items = level2Items;
			break;
		case 3:
			items = level3Items;
			break;
		}

		_itemInventory = new ItemInventory[items.Length];

		_itemCount = items.Length;

		emptyPanel.SetActive (false);

		SetItems ();
	}
		
	private void SetItems () {
		for (int i = 0; i < items.Length; i++) {
			_itemInventory [i] = new ItemInventory (items [i]);

			_itemInventory [i].SetCount ();

			itemSlots [i].SetSlot (_itemInventory[i]);
		}

		for (int i = items.Length; i < itemSlots.Length; i++) {
			itemSlots [i].HideSlot ();
		}
	}

	public void DecreaseLenght () {
		_itemCount -= 1;

		if (_itemCount == 0) {
			emptyPanel.SetActive (true);
		}
	}

	private void GetReferences () {
		itemSlots = GetComponentsInChildren<ChestItemSlot> ();
	}
}
