﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class Contestant : MonoBehaviour {
	public enum Trait{Sick, Strong, Scared, Fearless, Merciful, Relentless};
	public enum ContestantType{Player, AI, Guard, Medic, Target};
    Animator animator;
	public bool SwapOnInteract;

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
	[Tooltip("True if they are a player, false if they are an AI. PLEASE make sure the correct controller is attached :)")]
	/// <summary>
	/// True if the contestant is the player, false if they're the AI.
	/// </summary>
	public bool isPlayer;
	[Tooltip("Where the anchor point for an item is")]
	/// <summary>
	/// The anchor.
	/// </summary>
	public Transform anchor_rifle, anchor_pistol, anchor_rpg, anchor_shotgun, anchor_sniper, anchor_rpgdouble, anchor_pistolsniper, anchor_gattling, anchor_laserrifle;
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
	public GameObject HealthBar;
	public Transform head;
	public SkinnedMeshRenderer corpseRenderer;
	public Material hologram;

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
	public float ContestantDamageModifier;
	public float ContestantRepModifier;
	public bool IsDummy;
	public bool moving;
    Rigidbody body;
	public Component[] bones;
	public GameObject Rig;
	public Transform anchor;
	public SkinnedMeshRenderer aliveRenderer;

	public GameObject currentTalkCard;

	public List<Arena_Camera> onCameras = new List<Arena_Camera>();

	// Use this for initialization
	void Start () {
		bones = GetComponentsInChildren<Rigidbody> ();
		body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        baseSpeed = movespeed;
        anchor = anchor_pistol;
        //temp change color for enemies
        switch(type){
        	case ContestantType.AI:
				aliveRenderer.material.color = new Color(255,0,0);
        		break;
        	case ContestantType.Guard:
				aliveRenderer.material.color = new Color(0,0,0.5f);
        		break;
        	case ContestantType.Medic:
				aliveRenderer.material.color = Color.yellow;
        		break;
        	case ContestantType.Target:
				aliveRenderer.material.color = new Color(0.5f,0,0);
        		break;
        }

        ContestantGenerator gen = FindObjectOfType<ContestantGenerator>();
        if(contestantName == ""){
        	if(type == ContestantType.Target && StaticGameStats.instance.RebelName != ""){
				if (StaticGameStats.instance.FirstRun) {
					contestantName = StaticGameStats.instance.RebelName;
				}	
        	}
        	else{
				contestantName = gen.GetFirstName() + " " + gen.GetLastName();
        	}
        }
        if(contestantTidBit == ""){
        	contestantTidBit = gen.GetTidBit();
        }

        //----------------------------------------------------
        //GENERATE TRAITS
		if (type == ContestantType.AI) {
			if (StaticGameStats.instance.TierTwoUpgrades [2]) {
				//20% chance to be sick, 20% chance to be strong
				float random = Random.value;
				if (random < 0.2f) {
					traits.Add (Trait.Sick);
				} else if (random > 0.8f) {
					traits.Add (Trait.Strong);
				}

				//20% chance to be scared, 20% chance to be fearless
				random = Random.value;
				if (random < 0.2f) {
					traits.Add (Trait.Scared);
				} else if (random > 0.8f) {
					traits.Add (Trait.Fearless);
				}

				//Always Applies the Merciful Trait
				traits.Add (Trait.Merciful);
			} else {
				//20% chance to be sick, 20% chance to be strong
				float random = Random.value;
				if (random < 0.2f) {
					traits.Add (Trait.Sick);
				} else if (random > 0.8f) {
					traits.Add (Trait.Strong);
				}

				//20% chance to be scared, 20% chance to be fearless
				random = Random.value;
				if (random < 0.2f) {
					traits.Add (Trait.Scared);
				} else if (random > 0.8f) {
					traits.Add (Trait.Fearless);
				}

				//20% chance to be merciful, 20% chance to be relentless
				random = Random.value;
				if (random < 0.2f) {
					traits.Add (Trait.Merciful);
				} else if (random > 0.8f) {
					traits.Add (Trait.Relentless);
				}
			}
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

		//StartCoroutine("CheckCameraDelay", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(isAlive){
			if (equipped != null){
				switch(equipped.stance){
					case Item.Stance.Pistol:
                        //call animation Pistol_Run
                        animator.Play((moving)?"Pistol_Run":"Pistol_Idle");
                        anchor = anchor_pistol;
						break;
					case Item.Stance.Rifle:
                        //call animation Rifle_Run
						animator.Play((moving)?"Rifle_Run":"Rifle_Idle");
                        anchor = anchor_rifle;
						break;
                    case Item.Stance.RPGDouble:
                        //call animation Rifle_Run
						animator.Play((moving)?"Rifle_Run":"Rifle_Idle");
                        anchor = anchor_rpgdouble;
                        break;
                    case Item.Stance.Shotgun:
						//call animation Rifle_Run
						animator.Play((moving)?"Rifle_Run":"Rifle_Idle");
                        anchor = anchor_shotgun;
                        break;
                    case Item.Stance.Sniper:
						//call animation Rifle_Run
						animator.Play((moving)?"Rifle_Run":"Rifle_Idle");
                        anchor = anchor_sniper;
                        break;
                    case Item.Stance.RPG:
						//call animation Rifle_Run
						animator.Play((moving)?"Rifle_Run":"Rifle_Idle");
                        anchor = anchor_rpg;
                        break;
					case Item.Stance.SniperPistol:
						//call animation Pistol_Run
						animator.Play((moving)?"Pistol_Run":"Pistol_Idle");
						anchor = anchor_pistolsniper;
						break;
					case Item.Stance.LaserRifle:
						//call animation Rifle_Run
						animator.Play((moving)?"Rifle_Run":"Rifle_Idle");
						anchor = anchor_laserrifle;
						break;
					case Item.Stance.Gattling:
						//call animation Pistol_Run
						animator.Play ((moving)?"Rifle_Run":"Rifle_Idle");
						anchor = anchor_gattling;
						break;
                }
			}
			else{
                animator.Play((moving)?"Run_Impulse":"Idle_Neutral");
                if (cooldownCounter > 0){
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
			
		CheckCameras();

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
				Die(null);
				if(killer.equipped != null){
					if (killer.equipped.isSponsored) {
						if(onCameras.Count > 0){
							if (!StaticGameStats.instance.FirstRun) {
								StaticGameStats.instance.Influence (StaticGameStats.InfluenceTrigger.SponsorWeaponKill, 0);
							}
							CameraInfluence(1, true);
						}
					}
				}

			}	
			if (damage.damage > 0){
				FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().hurt, transform.position, 0.5f, true);
				if(damage.owner.isPlayer){
					FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().hit, transform.position, 1f, false);
				}
				GameObject blood = (GameObject)Instantiate(bloodSplatter,new Vector3(damage.location.x,0.1f,damage.location.z),Quaternion.Euler(90,Random.Range(0f,360f),0));
				float scale = Random.Range(0.08f,0.2f);
				blood.transform.localScale = new Vector3(scale,scale,1);
				if(type == ContestantType.Player){
					InterfaceManager im = FindObjectOfType<InterfaceManager>();
					if (im != null){
						im.noise.grainIntensityMax = 2.0f;
					}

				}
				else if(type == ContestantType.AI){
					GetComponent<AIController>().confidence -= (0.01f * damage.damage);
				}
				else if(type == ContestantType.Guard){
					RoundManager rm = FindObjectOfType<RoundManager> ();
					if(rm!=null){
						rm.noGuardDamage = false;
					}
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
	public void Die(string title){
		if(!isAlive){
			return;
		}
		isAlive = false;
		foreach (Rigidbody ragdoll in bones) 
		{
			ragdoll.isKinematic = false;
		}
		Rig.SetActive (true);
		animator.enabled = false;

		switch(type){
			case ContestantType.Player:
				StaticGameStats.instance.AbilitiesActive = false;
				if(equipped != null){
					if (equipped.isSponsored) {
						if(onCameras.Count > 0){
							if (!StaticGameStats.instance.FirstRun) {
								StaticGameStats.instance.Influence (StaticGameStats.InfluenceTrigger.SponsorWeaponDeath, 0);
							}
							CameraInfluence(1, false);
						}
					}
				}
				GetComponent<PlayerController>().enabled = false;


				//Analytics
				//------------------------------
				if(PlayerPrefs.GetInt("Analytics") == 1){
					RoundManager rm = FindObjectOfType<RoundManager>();
					if(rm != null){
						int currentRound = rm.GetRound();
						int remainingContestants = rm.aliveContestants;
						float roundTime = Time.timeSinceLevelLoad;
						Debug.Log("Sending player death analytics!");
						Analytics.CustomEvent("PlayerArenaDeath", new Dictionary<string, object>{
							{"Round", currentRound},
							{"RemainingContestants", remainingContestants},
							{"RoundTime", roundTime}
						});
					}
					else{
						Analytics.CustomEvent("PlayerTutorialDeath");
						Debug.Log("Sending player death analytics!");
					}
				}
				//------------------------------
				break;

			case ContestantType.AI:
				if(equipped != null){
					if (equipped.isSponsored) {
						if(onCameras.Count > 0){
							if (!StaticGameStats.instance.FirstRun) {
								StaticGameStats.instance.Influence (StaticGameStats.InfluenceTrigger.SponsorWeaponDeath, 0);
							}
							CameraInfluence(1, false);
						}
					}
				}
				if(onCameras.Count > 0){
					if(title == null){
						FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().death, transform.position, 0.7f, true);
						title = "KILLED ON CAMERA";
					}
					if (!StaticGameStats.instance.FirstRun) {
					StaticGameStats.instance.Influence (StaticGameStats.InfluenceTrigger.OnCameraKill, 0);
					}
					CameraInfluence(0, true);
					CameraInfluence(2, false);
				}
				else{
					if(title == null){
						FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().death, transform.position, 0.7f, true);
						title = "DECEASED";
					}
				}
				GetComponent<AIController>().enabled = false;
				GetComponent<NavMeshAgent>().enabled = false;
				if(GetComponent<AIController>().state == AIController.AIState.Beacon || GetComponent<AIController>().state == AIController.AIState.Evacuating){
					GameObject _beacon = GetComponent<AIController> ().beacon.gameObject;
					if (_beacon != null) {
						_beacon.SetActive (false);
					}
				}
				GetComponent<AIController>().state = AIController.AIState.Dead;
				FindObjectOfType<RoundManager>().Death();
				break;

			case ContestantType.Guard:
				Debug.Log ("Switch stage");
				FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().death, transform.position, 0.7f, true);
				if(onCameras.Count > 0){
					if(title == null){
						title = "KILLED ON CAMERA";
					}
					if (!StaticGameStats.instance.FirstRun) {
						StaticGameStats.instance.Influence (StaticGameStats.InfluenceTrigger.KillGuard, 0);
					}
				}
				else{
					title = "DECEASED";
				}
				GetComponent<AI_GuardController>().enabled = false;
				GetComponent<NavMeshAgent>().enabled = false;
				break;

			case ContestantType.Medic:
				FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().death, transform.position, 0.7f, true);
				GetComponent<AI_MedicController>().enabled = false;
				if(GetComponent<AI_MedicController>().tracker_card != null){
					Destroy(GetComponent<AI_MedicController>().tracker_card.gameObject);
				}
				GetComponent<NavMeshAgent>().enabled = false;
				break;

			case ContestantType.Target:
				title = "DECEASED";
				FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().death, transform.position, 0.7f, true);
				break;
		}
		if (equipped != null){
			equipped.Unequip();
		}
		equipped = null;
		corpseRenderer.material = hologram;
		GetComponent<Animator>().enabled = false;
		GetComponent<Collider>().enabled = false;
		GetComponent<Rigidbody>().useGravity = false;
		GameObject spawned = (GameObject)Instantiate(deathCard);
		spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		UI_DeathCard tracker = spawned.GetComponent<UI_DeathCard>();
		tracker.contest = this;
		tracker.title = title;
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
			FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().throw_item, transform.position, 0.7f, true);
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
			if(equipped.type == Item.ItemType.Other){
				return equipped.GetComponent<OtherItem>().ammo;
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

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.GetComponent<Item>() == null){
			return;
		}
		if (SwapOnInteract)	{
			if (equipped != null) { //does the contestant have something equipped
				if (equipped.type == Item.ItemType.Ranged) { //is their equipped item a ranged weapon
					if (equipped.GetComponent<RangedWeapon> ().ammo == 0) { //does their equipped ranged weapon have 0 ammo
						if (col.gameObject.GetComponent<Item> ().itemName == equipped.GetComponent<Item> ().itemName) { //does their equipped ranged weapon with 0 ammo have the same name as the colliding object
							if (col.gameObject.GetComponent<RangedWeapon> ().ammo != 0) { //does the colliding object with the same name have more than 0 ammo
								equipped.GetComponent<Item>().Throw();
								col.gameObject.GetComponent<Item> ().Equip (GetComponent<Contestant> ());

								//Swap ammo with the item on the floor
								//equipped.GetComponent<RangedWeapon> ().AddAmmo (col.gameObject.GetComponent<RangedWeapon> ().ammo);
								//col.gameObject.GetComponent<RangedWeapon> ().ammo = 0;
							}	
						}
					}
				}

				if (equipped.type == Item.ItemType.Other) { //is their equipped item a ranged weapon
					if (equipped.GetComponent<OtherItem>().ammo == 0) { //does their equipped ranged weapon have 0 ammo
						if (col.gameObject.GetComponent<Item> ().itemName == equipped.GetComponent<Item> ().itemName) { //does their equipped ranged weapon with 0 ammo have the same name as the colliding object
							if (col.gameObject.GetComponent<OtherItem> ().ammo != 0) { //does the colliding object with the same name have more than 0 ammo
								equipped.GetComponent<Item>().Throw();
								col.gameObject.GetComponent<Item> ().Equip (GetComponent<Contestant> ());

								//Swap ammo with the item on the floor
								//equipped.GetComponent<RangedWeapon> ().AddAmmo (col.gameObject.GetComponent<RangedWeapon> ().ammo);
								//col.gameObject.GetComponent<RangedWeapon> ().ammo = 0;
							}	
						}
					}
				}

			}

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

	public void Say(string words, int size){
		if(currentTalkCard != null){
			Destroy(currentTalkCard);
		}
		currentTalkCard = (GameObject)Instantiate(UI_Card);
		currentTalkCard.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		UI_GenericCard card = currentTalkCard.GetComponent<UI_GenericCard>();
		card.text = words;
		card.lifetime = 3.0f;
		card.target = transform;
		card.textSize = size;
	}

	public void Say(string words, int size, float lifetime){
		if(currentTalkCard != null){
			Destroy(currentTalkCard);
		}
		currentTalkCard = (GameObject)Instantiate(UI_Card);
		currentTalkCard.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		UI_GenericCard card = currentTalkCard.GetComponent<UI_GenericCard>();
		card.text = words;
		card.lifetime = lifetime;
		card.target = transform;
		card.textSize = size;
	}

	IEnumerator CheckCameraDelay(float delay){
		while(true){
			yield return new WaitForSeconds(delay);
			CheckCameras();
		}
	}

	void CheckCameras(){
		onCameras.Clear();
		foreach(Arena_Camera c in FindObjectsOfType<Arena_Camera>()){
			if(c.visibleContestants.Contains(this)){
				onCameras.Add(c);
			}
		}
	}

	public void CameraInfluence(int faction, bool positive){
		foreach(Arena_Camera c in onCameras){
			c.displayRepChange(faction, positive);
		}
	}
}
