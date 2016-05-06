using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public enum ItemType{Ranged, Melee, Other};
	public enum Stance{Rifle, Pistol, Unarmed};

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
	public Stance stance;
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
	public Texture2D cursor;
	public SpriteRenderer selectBox;	
	[Header("Runtime Only")]
	/// <summary>
	/// The contestant that is equipping this item.
	/// </summary>
	public Contestant equipper;



	Rigidbody2D body;
	Collider2D coll;
	float impactVelocityMin = 10;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//If i'm equipped, make sure i stick to my equipper's anchor point.
		if (equipper != null){
			transform.position = equipper.anchor.position;
			transform.rotation = equipper.anchor.rotation;
			selectBox.enabled = false;
		}
		else if (GetComponent<RangedWeapon>().ammo > 0){
			selectBox.enabled = true;
		}
	}

	/// <summary>
	/// To use this item, call this.
	/// </summary>
	public void Use(bool held){
		//Check what item type i am, then find my associated item script and call the correct function.
		switch (type){
			case ItemType.Ranged:
				GetComponent<RangedWeapon>().Fire(held);
				break;
			case ItemType.Melee:
				GetComponent<MeleeWeapon>().Attack(held);
				break;
			case ItemType.Other:
				GetComponent<OtherItem>().Use(held);
				break;
		}
	}

	public void Throw(){
		Unequip();
		body.AddForce(transform.right.normalized * 10, ForceMode2D.Impulse);
		body.AddTorque(1, ForceMode2D.Impulse);
	}

	public void Equip(Contestant contestant){
		equipper = contestant;
		contestant.equipped = this;
		coll.enabled = false;
		body.isKinematic = true;
		//TODO: Cursor should only change if the contestant is a player!!
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
	}

	public void Unequip(){
		equipper.equipped = null;
		equipper = null;
		coll.enabled = true;
		body.isKinematic = false;
		//TODO: Cursor should only change if the contestant is a player!!
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
	}

	void OnCollisionEnter2D(Collision2D c){
		if (equipper == null){
			if (body.velocity.magnitude > impactVelocityMin){
				//TODO need some way to preserve who has thrown the item, so damage can be dealt correctly.
				//At the moment, you technically unequip the item when you throw it, so if it deals damage it has no owner.
				c.gameObject.SendMessage("TakeDamage", new Contestant.DamageParams(10, null, Vector3.zero, c.contacts[0].point), SendMessageOptions.DontRequireReceiver);
			}
			else{
				if (c.gameObject.tag == "Contestant"){
					Contestant grabber = c.gameObject.GetComponent<Contestant>();
					if (grabber.equipped == null && grabber.cooldownCounter <= 0){
						Equip(grabber);
					}
				}
			}
		}
	}
}
