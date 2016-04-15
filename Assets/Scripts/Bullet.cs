using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public enum MovementType{Standard};

	[Header("Bullet Settings")]
	[Tooltip("How much damage this bullet does.")]
	/// <summary>
	/// How much damage this bullet does.
	/// </summary>
	public int damage;
	[Tooltip("What special movement type does this bullet have.")]
	/// <summary>
	/// A special movement modifier.
	/// </summary>
	public MovementType moveType;
	[Tooltip("A multiplier applied to the muzzle velocity of the gun this bullet is fired out of.")]
	/// <summary>
	/// Scalar applied to incoming velocities.
	/// </summary>
	public float velocityModifier;
	[Tooltip("The area that this bullet splashes its damage into. 0 for no splash.")]
	/// <summary>
	/// The size of the splash damage area
	/// </summary>
	public float areaOfEffect;
	[Tooltip("How long this bullet lives in the air.")]
	/// <summary>
	/// How long this bullet lives for.
	/// </summary>
	public float lifetime;

	[Header("Runtime Only")]
	/// <summary>
	/// Who shot this bullet.
	/// </summary>
	public Contestant owner;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
