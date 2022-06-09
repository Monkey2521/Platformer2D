using UnityEngine;

[CreateAssetMenu(fileName = "New note", menuName = "Note")]
public class Note : ScriptableObject {

	public string _name;
	public string content;
}
