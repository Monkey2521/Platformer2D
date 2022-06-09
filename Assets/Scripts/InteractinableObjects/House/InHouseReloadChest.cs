using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHouseReloadChest : MonoBehaviour {

	public GameObject chest;

	public void Reload (int level) {
		chest.GetComponent<ChestItemController> ().LoadItems (level);
	}
}
