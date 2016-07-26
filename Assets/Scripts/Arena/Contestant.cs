using UnityEngine;
using System.Collections;

public class Contestant : MonoBehaviour {

	[Header("Contestant Settings")]
	[Tooltip("The name of this contestant")]
	/// <summary>
	/// The name of the contestant.
	/// </summary>
	public string contestantName;
	[Tooltip("Their maximum health value")]
	/// <summary>
	/// The maximum health of the contestant.
	/// </summary>
	public int maxHealth;
	[Tooltip("Ture if they are a player, false if they are an AI. PLEASE make sure the correct controller is attached :)")]
	/// <summary>
	/// True if the contestant is the player, false if they're the AI.
	/// </summary>
	public bool isPlayer;
	[Tooltip("Where the anchor point for an item is")]
	/// <summary>
	/// The anchor.
	/// </summary>
	public Transform anchor_rifle, anchor_pistol;
	[Tooltip("How much time in seconds after unequipping a weapon must the contestant wait")]
	/// <summary>
	/// How much time in seconds after unequipping a weapon must the contestant wait
	/// </summary>
	public float pickupCooldown;
	[Tooltip("The movement speed of this contestant")]
	/// <summary>
	/// The movespeed of this contestant.
	/// </summary>
	public float movespeed;
    
	public GameObject bloodSplatter;
	public Transform backpack;

	//[Header("SPRITES")]
	//public Sprite unarmedSprite;
	//public Sprite rifleSprite;
	//public Sprite pistolSprite;
	//public Sprite corpse;

	[Header("Runtime Only")]
	/// <summary>
	/// The active health of the contestant.
	/// </summary>
	public int health;
	/// <summary>
	/// True if the contestant is alive, false if they are a corpse.
	/// </summary>
	public bool isAlive = true;
	/// <summary>
	/// The item currently equipped by this contestant.
	/// </summary>
	public Item equipped;
	/// <summary>
	/// Who killed this contestant.
	/// </summary>
	public Contestant killer;
	/// <summary>
	/// Internal counter for equip cooldown
	/// </summary>
	public float cooldownCounter;
	public Item inventory;
    float baseSpeed;

    Rigidbody body;
	Collider coll;
	//SpriteRenderer spr;
	public Transform anchor;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		coll = GetComponent<Collider>();
		//spr = GetComponent<SpriteRenderer>();
		health = maxHealth;
        baseSpeed = movespeed;
        //temp change color for enemies
        if(!isPlayer){
        	GetComponent<MeshRenderer>().material.color = Color.blue;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(isAlive){
			if (equipped != null){
				switch(equipped.stance){
					case Item.Stance.Pistol:
						//spr.sprite = pistolSprite;
						anchor = anchor_pistol;
						break;
					case Item.Stance.Rifle:
						//spr.sprite = rifleSprite;
						anchor = anchor_rifle;
						break;
				}
			}
			else{
				//spr.sprite = unarmedSprite;
				if(cooldownCounter > 0){
					cooldownCounter -= Time.deltaTime;
				}
			}
            //degrade our movespeed
            if (movespeed > baseSpeed) {
                movespeed -= Time.deltaTime * 0.3f;
            }
		}
		else{
			//spr.sprite = corpse;
		}
		if (health > maxHealth){
			health = maxHealth;
		}
	}

	/// <summary>
	/// Call this to take damage, from a source. You must use a DAMAGEPARAMS class in order to parse the informaiton properly!!
	/// </summary>
	/// <param name="damage"></param>
	public void TakeDamage(DamageParams damage){
		if(isAlive){
			health -= damage.damage;
			body.AddForce(damage.knockback, ForceMode.Impulse);
				
			if (health <= 0){
				killer = damage.owner;
				Die();
			}	
			if (damage.damage > 0){
				GameObject blood = (GameObject)Instantiate(bloodSplatter,new Vector3(damage.location.x,0.1f,damage.location.z),Quaternion.Euler(90,Random.Range(0f,360f),0));
				float scale = Random.Range(0.08f,0.2f);
				blood.transform.localScale = new Vector3(scale,scale,1);
				if(isPlayer){
					FindObjectOfType<InterfaceManager>().noise.grainIntensityMax = 2.0f;
				}
				else{
					GetComponent<AIController>().confidence -= (0.01f * damage.damage);
				}
			}
		}
	
	}

	/// <summary>
	/// Turns this contestant into a corpse.
	/// </summary>
	public void Die(){
		if (!isPlayer){
			GetComponent<AIController>().enabled = false;
			GetComponent<NavMeshAgent>().enabled = false;
			FindObjectOfType<RoundManager>().Death();
		}
		else{
			GetComponent<PlayerController>().enabled = false;
			if (Time.timeSinceLevelLoad < FindObjectOfType<RoundManager>().govtime){
				FindObjectOfType<StaticGameStats>().Influence(0, 10.0f);
			}else{
				FindObjectOfType<StaticGameStats>().Influence(0, -10.0f);
			}
		}
		body.isKinematic = true;
		coll.enabled = false;
		isAlive = false;
		if (equipped != null){
			equipped.Unequip();
		}
		equipped = null;
		if (killer != FindObjectOfType<PlayerController>().GetComponent<Contestant>()){
			GetComponent<MeshRenderer>().material.color = Color.white;
		}
		else{
			GetComponent<MeshRenderer>().material.color = Color.yellow;
		}
		
	}

	public bool UseEquipped(bool held){
		if (equipped != null){
			equipped.Use(held);
			return true;
		}
		else{
			return false;
		}
	}

	public bool ThrowEquipped(){
		if (equipped != null){
			equipped.Throw();
			return true;
		}
		else{
			return false;
		}
	}

	public int GetAmmo(){
		if (equipped != null){
			if (equipped.type == Item.ItemType.Ranged){
				return equipped.GetComponent<RangedWeapon>().ammo;
			}
			return -1;
		}
		return -1;
	}

	public void swap(){
		Item temp = equipped;
		equipped = inventory;
		inventory = temp;
	}

	/// <summary>
	/// This class stores information about incoming damage, including who is dealing the damage.
	/// </summary>
	public class DamageParams{
		public int damage;
		public Contestant owner;
		public Vector3 knockback;
		public Vector3 location;

		//Constructor
		public DamageParams(int damage, Contestant owner, Vector3 knockback, Vector3 location){
			this.damage = damage;
			this.owner = owner;
			this.location = location;
			this.knockback = knockback;
		}
	}

}
