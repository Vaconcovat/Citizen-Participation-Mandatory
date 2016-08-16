using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Contestant : MonoBehaviour {
	public enum Trait{Sick, Strong, Scared, Fearless, Merciful, Relentless};
	public enum ContestantType{Player, AI, Guard, Medic, Target};

	public List<Trait> traits;

	[Header("Contestant Settings")]
	public ContestantType type;
	[Tooltip("The name of this contestant")]
	/// <summary>
	/// The name of the contestant.
	/// </summary>
	public string contestantName;
	[Tooltip("This contestant's tidbit")]
	/// <summary>
	/// The name of the contestant.
	/// </summary>
	public string contestantTidBit;
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
    
	public GameObject bloodSplatter, deathCard;
	public Transform backpack;
	public GameObject UI_Card;
	public Contestant player;

	public Material hologram;


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
	public bool beaconActive = false;
    float baseSpeed;

    Rigidbody body;
	Collider coll;
	//SpriteRenderer spr;
	public Transform anchor;

	GameObject currentTalkCard;
	/* Comment: Tried to integrate same system used Overhead display from SkillCoolDown to no avail*/
	//public GameObject contestantTrackerUI;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		coll = GetComponent<Collider>();
        baseSpeed = movespeed;
        //temp change color for enemies
        if(type == ContestantType.AI){
        	GetComponent<SkinnedMeshRenderer>().material.color = Color.blue;
        }
		if(type == ContestantType.Target){
			GetComponent<SkinnedMeshRenderer>().material.color = Color.magenta;
		}

        ContestantGenerator gen = FindObjectOfType<ContestantGenerator>();
        if(contestantName == ""){
        	contestantName = gen.GetFirstName() + " " + gen.GetLastName();
        }
        if(contestantTidBit == ""){
        	contestantTidBit = gen.GetTidBit();
        }

        //----------------------------------------------------
        //GENERATE TRAITS

        //20% chance to be sick, 20% chance to be strong
        float random = Random.value;
        if (random < 0.2f){
        	traits.Add(Trait.Sick);
        }
        else if (random > 0.8f){
        	traits.Add(Trait.Strong);
        }

        //20% chance to be scared, 20% chance to be fearless
		random = Random.value;
		if (random < 0.2f){
        	traits.Add(Trait.Scared);
        }
        else if (random > 0.8f){
        	traits.Add(Trait.Fearless);
        }

		//20% chance to be merciful, 20% chance to be relentless
		random = Random.value;
		if (random < 0.2f){
        	traits.Add(Trait.Merciful);
        }
        else if (random > 0.8f){
        	traits.Add(Trait.Relentless);
        }

        //
        //------------------------------------------------------


        if(traits.Contains(Trait.Sick)){
        	maxHealth = 80;
        }
		if(traits.Contains(Trait.Strong)){
        	maxHealth = 120;
        }

		health = maxHealth;
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


		if (StaticGameStats.TierTwoUpgrades [3]) {
			if(equipped != null){
				if (equipped.type == Item.ItemType.Ranged) {
					if (player.equipped.GetComponent<RangedWeapon> ().ammo == 0) {
						player.movespeed = StaticGameStats.Upgrade8MovementSpeedBuff;
					}
				}
			} 
			else {
				player.movespeed = StaticGameStats.Upgrade8NormalSpeed;
			}
		}

		if (StaticGameStats.TierTwoUpgrades [2]) {
			if (equipped != null) {
				if (equipped.type == Item.ItemType.Ranged) {
					if ((player.equipped.GetComponent<RangedWeapon> ().ammo == 0) && (StaticGameStats.Upgrade7AlreadyTriggered == false)) { //only triggers once when the health hits 0
						StaticGameStats.Upgrade7AlreadyTriggered = true;
						player.health = player.health + 20;
					}
					if (player.equipped.GetComponent<RangedWeapon> ().ammo > 0) { //resets AlreadyTriggered if the ammo goes above 0
						StaticGameStats.Upgrade7AlreadyTriggered = false;
					}
				}
			}
		}

		if (StaticGameStats.TierThreeUpgrades [2]) {
			if (equipped != null) {
				if (equipped.type == Item.ItemType.Ranged) {
					if ((player.equipped.GetComponent<RangedWeapon> ().ammo == 0) && (StaticGameStats.Upgrade7AlreadyTriggered == false)) { //only triggers once when the ammo hits 0
						StaticGameStats.Upgrade7AlreadyTriggered = true;
						player.ThrowEquipped ();

					}
					if (player.equipped.GetComponent<RangedWeapon> ().ammo > 0) { //resets AlreadyTriggered if the ammo goes above 0
						StaticGameStats.Upgrade7AlreadyTriggered = false;
					}
				}
			}
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
				if (killer.equipped.isSponsored) 
				{
					FindObjectOfType<StaticGameStats>().Influence(1, StaticGameStats.CorSponsorWeaponKillIncrease);
				}
			}	
			if (damage.damage > 0){
				GameObject blood = (GameObject)Instantiate(bloodSplatter,new Vector3(damage.location.x,0.1f,damage.location.z),Quaternion.Euler(90,Random.Range(0f,360f),0));
				float scale = Random.Range(0.08f,0.2f);
				blood.transform.localScale = new Vector3(scale,scale,1);
				if(type == ContestantType.Player){
					FindObjectOfType<InterfaceManager>().noise.grainIntensityMax = 2.0f;
				}
				else if(type == ContestantType.AI){
					GetComponent<AIController>().confidence -= (0.01f * damage.damage);
				}
				else if(type == ContestantType.Guard){
					AI_GuardController[] guards = FindObjectsOfType<AI_GuardController>();
					foreach(AI_GuardController guard in guards){
						guard.endStatus  = AI_GuardController.endRoundStatus.Fight;
					}
				} else if (type == ContestantType.Target) {
					Debug.Log ("Target took damage");
				}
			}
		}
	
	}

	/// <summary>
	/// Turns this contestant into a corpse.
	/// </summary>
	public void Die(){
		if (type == ContestantType.AI){
			GetComponent<AIController>().enabled = false;
			GetComponent<NavMeshAgent>().enabled = false;
			if(GetComponent<AIController>().state == AIController.AIState.Beacon || GetComponent<AIController>().state == AIController.AIState.Evacuating){
				GetComponent<AIController>().beacon.gameObject.SetActive(false);
			}
			GetComponent<AIController>().state = AIController.AIState.Dead;
			FindObjectOfType<RoundManager>().Death();
		}
		else if(type == ContestantType.Guard){
			FindObjectOfType<StaticGameStats>().Influence(0, StaticGameStats.GovKillGuardsDecrease);
			FindObjectOfType<StaticGameStats>().Influence(2, StaticGameStats.RebKillGuardsIncrease);
			GetComponent<AI_GuardController>().enabled = false;
			GetComponent<NavMeshAgent>().enabled = false;
		}
		else if(type == ContestantType.Player){
			GetComponent<PlayerController>().enabled = false;
			if(equipped != null){
				if (equipped.isSponsored) {
					FindObjectOfType<StaticGameStats>().Influence(1, StaticGameStats.CorSponsorWeaponDeathDecrease);
				}
			}
		} else if(type == ContestantType.Target){
			//GetComponent<AI_GuardController>().enabled = false;
			GetComponent<NavMeshAgent>().enabled = false;
			print ("Target died a horrible death!");
		}

		else{
			GetComponent<AI_MedicController>().enabled = false;
			GetComponent<NavMeshAgent>().enabled = false;
		}
		//body.isKinematic = true;
		//coll.enabled = false;
		isAlive = false;
		if (equipped != null){
			equipped.Unequip();
		}
		equipped = null;
		GetComponent<SkinnedMeshRenderer>().material = hologram;
		if (killer != FindObjectOfType<PlayerController>().GetComponent<Contestant>()){
			GetComponent<SkinnedMeshRenderer>().material.color = Color.white;
		}
		else{
			GetComponent<SkinnedMeshRenderer>().material.color = Color.yellow;
		}
		GameObject spawned = (GameObject)Instantiate(deathCard);
		spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		UI_DeathCard tracker = spawned.GetComponent<UI_DeathCard>();
		tracker.contest = this;
		if(currentTalkCard != null){
			Destroy(currentTalkCard);
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

	public void Say(string words){
		if(currentTalkCard != null){
			Destroy(currentTalkCard);
		}
		currentTalkCard = (GameObject)Instantiate(UI_Card);
		currentTalkCard.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		UI_GenericCard card = currentTalkCard.GetComponent<UI_GenericCard>();
		card.text = words;
		card.lifetime = 3.0f;
		card.target = transform;
	}
}
