using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Item")]
public class Item : ScriptableObject {
	[Header("Debug settings")]
	public bool isDebug;

	[Header("Settings")]
	public Sprite icon;

	public string _name;
	public string description;

	public bool haveCount;

	public int minCount;
	public int maxCount;

	public bool craftable;

	public int craftCount;

	public Crafts[] crafts;

	public Item[] itemsForCraft;
	public int[] itemsCount;

	public Effect effect;

	public virtual void Use () {
		if (isDebug) {
			Debug.Log ("Using " + _name);
		}
		if (effect != null)
			effect.TakeEffect ();
	}
}

public enum Crafts 
{
	Ciggarete
};
