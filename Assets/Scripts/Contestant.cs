﻿using UnityEngine;
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
	public Transform anchor;
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

	[Header("Runtime Only")]
	/// <summary>
	/// The active health of the contestant.
	/// </summary>
	public int health;
	/// <summary>
	/// True if the contestant is alive, false if they are a corpse.
	/// </summary>
	public bool isAlive;
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

	Rigidbody2D body;
	Collider2D coll;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: do we need something here?
	}

	/// <summary>
	/// Call this to take damage, from a source. You must use a DAMAGEPARAMS class in order to parse the informaiton properly!!
	/// </summary>
	/// <param name="damage"></param>
	public void TakeDamage(DamageParams damage){
		health -= damage.damage;
		body.AddForce(damage.knockback, ForceMode2D.Impulse);
		if (health <= 0){
			killer = damage.owner;
			Die();
		}
		GameObject blood = (GameObject)Instantiate(bloodSplatter,damage.location,Quaternion.AngleAxis(Random.Range(0f,360f),Vector3.forward));
		float scale = Random.Range(0.08f,0.2f);
		blood.transform.localScale = new Vector3(scale,scale,1);
	}

	/// <summary>
	/// Turns this contestant into a corpse.
	/// </summary>
	public void Die(){
		if (!isPlayer){
			GetComponent<AIController>().enabled = false;
		}
		body.isKinematic = true;
		coll.enabled = false;
		isAlive = false;
		if (equipped != null){
			equipped.Unequip();
		}
		equipped = null;
		GetComponent<SpriteRenderer>().color = Color.white;
		//TODO: other corpse related things here
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

	/// <summary>
	/// This class stores information about incoming damage, including who is dealing the damage.
	/// </summary>
	public class DamageParams{
		public int damage;
		public Contestant owner;
		public Vector2 knockback;
		public Vector2 location;

		//Constructor
		public DamageParams(int damage, Contestant owner, Vector2 knockback, Vector2 location){
			this.damage = damage;
			this.owner = owner;
			this.location = location;
			this.knockback = knockback;
		}
	}

}
