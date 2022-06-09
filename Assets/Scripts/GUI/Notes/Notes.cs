using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Notes : MonoBehaviour {

	public GameObject notesPanel;

	public List<Note> notes = new List<Note> ();

	public GameObject noteSlotPrefab;

	public GameObject notePanel;
	public Transform parentTransform;

	public Text noteName;
	public Text noteContent;

	public static Notes instance;

	private List<GameObject> noteSlots = new List<GameObject> ();

	private void Awake () {
		if (instance != null) {
			Debug.Log ("Notes error");
			notesPanel.SetActive (false);
			return;
		}

		instance = this;

		notesPanel.SetActive (false);
	}

	public void AddNote (Note note) {
		notes.Add (note);
		SetNote (note);

		StartCoroutine (EventController.instance.Show (note._name));

		FindObjectOfType<SoundManager> ().PickUpNoteSound ();
	}

	private void SetNote (Note note) {
		GameObject noteSlot = (GameObject)Instantiate (noteSlotPrefab);

		noteSlot.transform.SetParent (parentTransform);
		noteSlot.transform.localScale = new Vector3 (1f, 1f, 1f);

		noteSlot.GetComponent<NoteSlot> ().notePanel = notePanel;
		noteSlot.GetComponent<NoteSlot> ().notePanelName = noteName;
		noteSlot.GetComponent<NoteSlot> ().notePanelContent = noteContent;
	
		noteSlot.GetComponent<NoteSlot> ().SetSlot (note);

		noteSlots.Add (noteSlot);
	}

	public void HideNote () {
		for (int i = 0; i < noteSlots.Count; i++) {
			noteSlots [i].GetComponent<NoteSlot> ().HideNote ();
		}
	}
}
