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
	public Transform anchor;
	[Tooltip("How much time in seconds after unequipping a weapon must the contestant wait")]
	/// <summary>
	/// How much time in seconds after unequipping a weapon must the contestant wait
	/// </summary>
	public float pickupCooldown;

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Call this to take damage, from a source. The parameters are wrapped in an object array, so make sure you get it right!!!!!
	/// </summary>
	/// <param name="damageDetails">IMPORTANT: This object array MUST contain int: damage and Contestant: owner!!!</param>
	public void TakeDamage(Object[] damageDetails){
		int damage = damageDetails[0];
		Contestant owner = damageDetails[1];
		//THIS DOESNT WORK
		//todo
	}

}
