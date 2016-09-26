using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public enum ItemType{Ranged, Melee, Other};
	public enum Stance{Rifle, Pistol, RPG, Shotgun, Sniper, Unarmed, RPGDouble, Gattling, SniperPistol, LaserRifle};

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
	[Tooltip("The Amount of Knockback the enemy will recieve when hit by this weapon")]
	/// <summary>
	/// What itm type this is.
	/// </summary>
	public int KnockbackAmount;
	[Tooltip("The Amount of Damage this item will do when thrown at an enemy")]
	/// <summary>
	/// What itm type this is.
	/// </summary>
	public int ThrownDamage;
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

	[Header("Runtime Only")]
	/// <summary>
	/// The contestant that is equipping this item.
	/// </summary>
	public Contestant equipper;
	public Contestant player;

	[Range(0,1)]
	public float threat;
	public GameObject UI_Card;

	Contestant thrower;
	Rigidbody body;
	Collider coll;
	float impactVelocityMin = 10;
	public UI_WeaponTracker tracker;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		coll = GetComponent<Collider>();
		GameObject spawned = (GameObject)Instantiate(UI_Card);
		spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		tracker = spawned.GetComponent<UI_WeaponTracker>();
		tracker.item = this;
	}
	
	// Update is called once per frame
	void Update () {
		//If i'm equipped, make sure i stick to my equipper's anchor point.
		if (equipper != null){
			if (equipper.inventory == this){
				transform.position = equipper.backpack.position;
				transform.rotation = equipper.backpack.rotation;
			}
			else{
				transform.position = equipper.anchor.position;
				transform.rotation = equipper.anchor.rotation;
			}
			if(tracker != null){
				tracker.gameObject.SetActive(false);
			}

		}
		else{
			if(tracker != null){
				tracker.gameObject.SetActive(true);
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
		Debug.Log("throwing");
		thrower = equipper;
		Unequip();
		body.AddForce(transform.forward.normalized * 30, ForceMode.Impulse);
		body.AddTorque(Random.insideUnitSphere, ForceMode.Impulse);
	}

	public void Equip(Contestant contestant){
		thrower = null;
		equipper = contestant;
		contestant.cooldownCounter = contestant.pickupCooldown;
		contestant.equipped = this;
		coll.enabled = false;
		body.useGravity = false;
		body.isKinematic = true;
		if(equipper.type == Contestant.ContestantType.Player){
			Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
		}
	}

	public void Unequip(){
		if(equipper.type == Contestant.ContestantType.Player){
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}
		equipper.equipped = null;
		equipper = null;
		coll.enabled = true;
		body.useGravity = true;
		body.isKinematic = false;
	}

	void OnCollisionEnter(Collision c){
		if (equipper == null){
			if (body.velocity.magnitude > impactVelocityMin){
				if(c.gameObject.tag == "Contestant"){
					if(c.gameObject.GetComponent<Contestant>() != thrower){
						c.gameObject.SendMessage("TakeDamage", new Contestant.DamageParams(ThrownDamage, thrower, Vector3.zero, c.contacts[0].point), SendMessageOptions.DontRequireReceiver);
					}
					if(StaticGameStats.TierTwoUpgrades[2]){
						if (type == ItemType.Ranged) {
							c.rigidbody.AddForce (transform.forward.normalized * KnockbackAmount, ForceMode.Impulse);
						}
					}
				}
				else{
					c.gameObject.SendMessage("TakeDamage", new Contestant.DamageParams(ThrownDamage, thrower, Vector3.zero, c.contacts[0].point), SendMessageOptions.DontRequireReceiver);
				}
			}
			else{
				if (c.gameObject.tag == "Contestant"){
					Contestant grabber = c.gameObject.GetComponent<Contestant>();
					if (grabber.equipped == null && grabber.cooldownCounter <= 0 && grabber.type != Contestant.ContestantType.Medic && grabber.isAlive){
						Equip(grabber);
					}
				}
			}
		}
	}

	public float GetRangeHint(bool min){
		if(min){
			if(type == ItemType.Ranged){
				return GetComponent<RangedWeapon>().RangeHintMin;
			}
			else{
				return 0;
			}
		}
		else{
			if(type == ItemType.Ranged){
				return GetComponent<RangedWeapon>().RangeHintMax;
			}
			else{
				return 100;
			}
		}
	}

	public int GetAmmo(){
		if(type == ItemType.Ranged){
			return GetComponent<RangedWeapon>().ammo;
		}
		else{
			return GetComponent<OtherItem>().ammo;
		}
	}
}
