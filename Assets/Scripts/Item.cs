using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public enum ItemType{Ranged, Melee, Other};

	[Header("Item Settings")]
	[Tooltip("The name of the Item.")]
	/// <summary>
	/// The name of the item.
	/// </summary>
	public string itemName;
	[Tooltip("The type of item. If it doesn't fit in ranged or melee weapons, it's other.")]
	/// <summary>
	/// What itm type this is.
	/// </summary>
	public ItemType type;
	[Tooltip("Was this item brought to us by a sponsor?")]
	/// <summary>
	/// True if the item is sponsored.
	/// </summary>
	public bool isSponsored;
	[Header("Sprites")]
	[Tooltip("The sprite that is rendered on HUD when this item is equipped")]
	/// <summary>
	/// The sprite that is rendered on HUD when this item is equipped
	/// </summary>
	public Sprite logo;
	[Tooltip("The sprite that the cursor changes to while this item is equipped")]
	/// <summary>
	/// The sprite that the cursor changes to while this item is equipped
	/// </summary>
	public Sprite cursor;	
	[Header("Runtime Only")]
	/// <summary>
	/// The contestant that is equipping this item.
	/// </summary>
	public Contestant equipper;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
