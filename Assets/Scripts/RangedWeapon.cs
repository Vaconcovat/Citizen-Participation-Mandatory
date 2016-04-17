using UnityEngine;
using System.Collections;

public class RangedWeapon : MonoBehaviour {
	public enum RangeHint{Long,Med,Short};

	[Header("Ranged Weapon Settings")]
	[Tooltip("The time in seconds that this weapon must wait between shots. Set to 0 for fan the hammer.")]
	/// <summary>
	/// Time in seconds that the gun waits between shots. 0 for fan the hammer.
	/// </summary>
	public float fireRate;
	[Tooltip("How many bullets are created per shot.")]
	/// <summary>
	/// How many bullets are created per shot
	/// </summary>
	public int bulletsPerShot;
	[Range(0.0f,1.0f)]
	[Tooltip("The mutation on the vector that this gun feeds to its bullets.")]
	/// <summary>
	/// The mutation on the vector that this gun feeds to its bullets.
	/// </summary>
	public float spread;
	[Tooltip("How many shots this gun has.")]
	/// <summary>
	/// The ammo.
	/// </summary>
	public int ammo;
	[Tooltip("How fast the bullets come out of the gun. A scalar to a unit vector. (That's why it seems small)")]
	/// <summary>
	/// The muzzle velocity. Scalar to the mutated unit vector for direction.
	/// </summary>
	public float muzzleVelocity;
	[Tooltip("Where do the bullets come from??")]
	/// <summary>
	/// Where the bullets are instatiated.
	/// </summary>
	public Transform muzzle;
	[Tooltip("The bullet prefab that this gun fires.")]
	/// <summary>
	/// The bullet prefab used by this gun.
	/// </summary>
	public GameObject bullet;

	/// <summary>
	/// Internal counter for the gun's fire rate.
	/// </summary>
	float cooldownCounter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate != 0){
			if (cooldownCounter > 0){
				cooldownCounter -= Time.deltaTime;
			}
		}
	}

	public void Fire(bool held){
		//if we're holding the trigger and we're not a fan the hammer gun
		if (held && fireRate != 0){
			if (ammo > 0 && cooldownCounter <= 0){
				Shoot();
				ammo -= 1;
				cooldownCounter = fireRate;
			}
		}
		//if we tapped the trigger and we are a fan the hammer gun
		else if (!held && fireRate == 0){
			if (ammo > 0){
				Shoot();
				ammo-=1;
			}
		}
	}

	void Shoot(){
		for (int i = 0; i < bulletsPerShot; i++){
			GameObject firedBullet = (GameObject)Instantiate(bullet, muzzle.position, muzzle.rotation);
			//TODO: Make sure these rotations are correct!
			Vector2 angle = new Vector2(transform.right.x + (Random.Range(-spread, spread)), transform.right.y + (Random.Range(-spread, spread))).normalized;
			firedBullet.GetComponent<Bullet>().Fire(angle * muzzleVelocity);
			firedBullet.GetComponent<Bullet>().owner = GetComponent<Item>().equipper;
		}
	}
}
