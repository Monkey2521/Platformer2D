using UnityEngine;
using UnityEngine.UI;

public class NoteSlot : MonoBehaviour {

	public Note note;

	public Text noteName;
	public Image icon;
	public Sprite showNote;
	public Sprite hideNote;

	public GameObject notePanel;
	public Text notePanelName;
	public Text notePanelContent;

	public void SetSlot (Note note) {
		this.note = note;
		noteName.text = note._name;
	}

	public void ShowNote () {

		notePanel.SetActive (true);

		notePanelName.text = note._name;
		notePanelContent.text = note.content;

		icon.sprite = showNote;
		icon.color = Color.white;
	}

	public void HideNote () {
		notePanel.SetActive (false);

		notePanelName.text = string.Empty;
		notePanelContent.text = string.Empty;

		icon.sprite = hideNote;
	}

}
