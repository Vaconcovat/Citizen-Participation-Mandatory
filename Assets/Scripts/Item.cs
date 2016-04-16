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

	Rigidbody2D body;
	float impactVelocityMin = 10;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//If i'm equipped, make sure i stick to my equipper's anchor point.
		if (equipper != null){
			transform.position = equipper.anchor.position;
			transform.rotation = equipper.anchor.rotation;
		}
	}

	/// <summary>
	/// To use this item, call this.
	/// </summary>
	public void Use(){
		//Check what item type i am, then find my associated item script and call the correct function.
		switch (type){
			case ItemType.Ranged:
				GetComponent<RangedWeapon>().Fire();
				break;
			case ItemType.Melee:
				GetComponent<MeleeWeapon>().Attack();
				break;
			case ItemType.Other:
				GetComponent<OtherItem>().Use();
				break;
		}
	}

	public void Throw(){
		Unequip();
		body.AddForce(transform.rotation.eulerAngles.normalized * 10, ForceMode2D.Impulse); //look into this, the rotation might not be correct.
		body.AddTorque(10, ForceMode2D.Impulse);
	}

	public void Equip(){
		//todo
	}

	public void Unequip(){
		//todo
	}

	void OnCollisionEnter2D(Collision2D coll){
		//todo
		if (equipper == null){
			if (body.velocity.magnitude > impactVelocityMin){
				object[] info;
				int damage = 10;
				info[0] = damage;
				info[1] = equipper;
				coll.gameObject.SendMessage("TakeDamage", info, SendMessageOptions.DontRequireReceiver);
			}
			else{
				if (coll.gameObject.tag == "Contestant"){
					Contestant grabber = coll.gameObject.GetComponent<Contestant>();
					if (grabber.equipped == null && grabber.cooldownCounter <= 0){
						Equip();
					}
				}
			}
		}
	}
}
