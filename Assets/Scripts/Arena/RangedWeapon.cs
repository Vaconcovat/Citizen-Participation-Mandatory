using UnityEngine;
using System.Collections;

public class RangedWeapon : MonoBehaviour {

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
	[Tooltip("How many shots this gun can hold")]
	/// <summary>
	/// The ammo.
	/// </summary>
	public int Maxammo;
	[Tooltip("How many shots this gun currently has.")]
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
	public LayerMask obstacleMask;
	public GameObject muzzleFlash;
	public AudioClip shootSound, emptySound;

	public float RangeHintMin = 1;
	public float RangeHintMax = 5;

	/// <summary>
	/// Internal counter for the gun's fire rate.
	/// </summary>
	float cooldownCounter;

	// Use this for initialization
	void Start () {
		if(StaticGameStats.TierOneUpgrades[3]){
			ammo = Mathf.RoundToInt(ammo * StaticGameStats.Upgrade4MaxAmmoBuff);
			Maxammo = ammo;
		}
		if (StaticGameStats.TierTwoUpgrades [1]) {
			fireRate = fireRate * StaticGameStats.Upgrade6FireRateNerf;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate != 0){
			if (cooldownCounter > 0){
				cooldownCounter -= Time.deltaTime;
			}
		}
		if(GetComponent<Item>().equipper != null){
			Debug.DrawRay(muzzle.position, GetComponent<Item>().equipper.transform.position  + new Vector3(0,1,0) - muzzle.position);
		}
	}

	public void Fire(bool held){
		//first check that our gun's location is legal (not through a wall)
		Ray r = new Ray(muzzle.position, GetComponent<Item>().equipper.transform.position + new Vector3(0,1,0) - muzzle.position);

		if(Physics.Raycast(r,Vector3.Distance(muzzle.position, GetComponent<Item>().equipper.transform.position + new Vector3(0,1,0)),obstacleMask)){
			Debug.Log("can't fire");
			return;
		}
		//if we're holding the trigger and we're not a fan the hammer gun
		if (held && cooldownCounter <= 0){
			if (ammo > 0){
				Shoot();
				SubtractAmmo (1);
				cooldownCounter = fireRate;
			}
			else{
				FindObjectOfType<SoundManager>().PlayEffect(emptySound, transform.position, 0.7f, true);
				cooldownCounter = fireRate;
			}
		}
	}

	void Shoot(){
		FindObjectOfType<SoundManager>().PlayEffect(shootSound, transform.position, 1.0f, true);
		for (int i = 0; i < bulletsPerShot; i++){
			GameObject firedBullet = (GameObject)Instantiate(bullet, muzzle.position, muzzle.rotation);
			firedBullet.tag = "Bullet";
			Vector3 angle = new Vector3(transform.forward.x + (Random.Range(-spread, spread)),0, transform.forward.z + (Random.Range(-spread, spread))).normalized;
			firedBullet.GetComponent<Bullet>().Fire(angle * muzzleVelocity);
			firedBullet.GetComponent<Bullet>().owner = GetComponent<Item>().equipper;
			Instantiate(muzzleFlash, muzzle.position, muzzle.rotation);
		}
		if(GetComponent<Item>().isSponsored && GetComponent<Item>().equipper.onCameras.Count > 0){
			FindObjectOfType<StaticGameStats>().Influence(StaticGameStats.InfluenceTrigger.SponsorWeaponFire, 0); //updated to RebWeaponOnCamera
			GetComponent<Item>().equipper.CameraInfluence(1,true);
		}
	}

	public void AddAmmo(int addedAmmo){
		ammo = ammo + addedAmmo;
	}

	public void SubtractAmmo(int lostAmmo){
		ammo = ammo - lostAmmo;
	}
}
