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


	Contestant thrower;
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
			if (equipper.inventory == this){
				transform.position = equipper.backpack.position;
				transform.rotation = equipper.backpack.rotation;
				selectBox.enabled = false;
			}
			else{
				transform.position = equipper.anchor.position;
				transform.rotation = equipper.anchor.rotation;
				selectBox.enabled = false;
			}

		}
		else if (type == ItemType.Ranged){
			if (GetComponent<RangedWeapon>().ammo > 0){
				selectBox.enabled = true;
			}
			else{
				selectBox.enabled = false;
			}
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
		thrower = equipper;
		Unequip();
		body.AddForce(transform.right.normalized * 10, ForceMode2D.Impulse);
		body.AddTorque(1, ForceMode2D.Impulse);
	}

	public void Equip(Contestant contestant){
		thrower = null;
		equipper = contestant;
		contestant.cooldownCounter = contestant.pickupCooldown;
		contestant.equipped = this;
		coll.enabled = false;
		body.isKinematic = true;
		if(equipper.isPlayer){
			Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
		}
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
				if(c.gameObject.tag == "Contestant"){
					if(c.gameObject.GetComponent<Contestant>() != thrower){
						c.gameObject.SendMessage("TakeDamage", new Contestant.DamageParams(10, thrower, Vector3.zero, c.contacts[0].point), SendMessageOptions.DontRequireReceiver);
					}
				}
				else{
					c.gameObject.SendMessage("TakeDamage", new Contestant.DamageParams(10, thrower, Vector3.zero, c.contacts[0].point), SendMessageOptions.DontRequireReceiver);
				}
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
