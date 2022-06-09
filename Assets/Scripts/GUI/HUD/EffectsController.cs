using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour {

	public GameObject effectSlotPrefab;
	public Transform parentTransform;

	private List<GameObject> effectSlots = new List<GameObject> ();

	public void Awake () {
		FindObjectOfType<PlayerController> ().OnEffectChangedCallback += SetEffects;
	}

	public void SetEffects () {
		Debug.Log ("Set effects!");

		Effect[] effects = FindObjectOfType<PlayerController> ().effects.ToArray ();
		if (effects.Length == 0) {
			ClearEffects ();
		}

		for (int i = 0; i < effects.Length; i++){
			for (int j = 0; j < effectSlots.Count; j++) {
				if (effectSlots [j].GetComponent<EffectSlot> ().effect == effects [i]) {
					RemoveSlot (effectSlots [j]);
				}
			}

			GameObject effectSlot = (GameObject)Instantiate (effectSlotPrefab);
			effectSlot.transform.SetParent (parentTransform);

			effectSlot.GetComponent<EffectSlot> ().effect = effects [i];
			effectSlot.GetComponent<EffectSlot> ().SetSlot ();

			effectSlots.Add (effectSlot);
		}
	}

	private void ClearEffects () {
		for (int i = 0; i < effectSlots.Count;) {
			RemoveSlot (effectSlots[0]);
		}
		effectSlots.Clear ();

		Debug.Log ("Slots = " + effectSlots.Count);
	}

	public void RemoveSlot (GameObject slot) {
		effectSlots.Remove (slot);
		Destroy (slot);
	}
}
