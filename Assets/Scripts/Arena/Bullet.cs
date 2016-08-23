using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public enum MovementType{Standard, Zany};

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
	public GameObject trail;
	public float explosiveForce;
	public int bounces;
	public bool isSponsored;
	[Header("Runtime Only")]
	/// <summary>
	/// Who shot this bullet.
	/// </summary>
	public Contestant owner;

	Rigidbody body;
	Vector3 startPos;
	float startTime;

	Vector3 v;

	// Use this for initialization
	void Awake () {
		body = GetComponent<Rigidbody>();
		startPos = transform.position;
		startTime = Time.time;
		if(StaticGameStats.TierTwoUpgrades[1]){
			damage = Mathf.RoundToInt(damage * StaticGameStats.Upgrade6DamageBuff);
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch (moveType){
			case MovementType.Standard:
				break;
			case MovementType.Zany:
				Zany();
				break;
		}
		lifetime -= Time.deltaTime;
		if (lifetime <= 0){
			Destroy(gameObject);
		}
	}

	public void Fire(Vector3 vector){
		v = vector * velocityModifier;
		body.AddForce(v, ForceMode.Impulse);
		if (isSponsored) {
			FindObjectOfType<StaticGameStats>().Influence(1, StaticGameStats.CorSponsorWeaponFireIncrease, "CorSponsorWeaponFireIncrease");
		}
	}

	void OnCollisionEnter(Collision coll){
		if (areaOfEffect > 0){
			Collider[] colliders = Physics.OverlapSphere(transform.position, areaOfEffect);
			foreach (Collider a in colliders){
				if(!Physics.Raycast(transform.position, (a.transform.position-transform.position).normalized, Vector3.Distance(transform.position, a.transform.position), LayerMask.NameToLayer("Unwalkable"))){
					a.gameObject.SendMessage("TakeDamage", new Contestant.DamageParams(damage, owner, (a.transform.position - this.transform.position).normalized * explosiveForce / Vector3.Distance(a.transform.position, this.transform.position), a.transform.position), SendMessageOptions.DontRequireReceiver);
				}

			}
			Destroy(gameObject);
		}
		else{
			if (coll.gameObject.tag == "Contestant"){
				if (owner.type == Contestant.ContestantType.AI) {
						owner.GetComponent<AIController> ().confidence += damage / 200f;
				}
				coll.gameObject.SendMessage("TakeDamage", new Contestant.DamageParams(damage, owner, body.velocity.normalized, coll.contacts[0].point), SendMessageOptions.DontRequireReceiver);
				Destroy(gameObject);
			}
			else if(bounces > 0){
				body.AddForce(Vector3.Reflect(transform.forward, coll.contacts[0].normal).normalized * v.magnitude, ForceMode.Impulse);
				bounces--;
			}
			else{
				Destroy(gameObject);
			}

		}
	}

	void Zany(){
		body.velocity = Vector3.zero;
		startPos += transform.forward * Time.deltaTime * 10;
		transform.position = startPos + transform.right * Mathf.Sin(15 * (Time.time-startTime)) * 0.5f;


		//body.AddForce(Quaternion.AngleAxis(90,Vector3.forward) * transform.right * Mathf.Cos(Time.frameCount/10) * 0.5f, ForceMode.Impulse);
		//transform.localScale = new Vector3(Mathf.Cos(Time.frameCount/10), Mathf.Cos(Time.frameCount/10), 1) * 0.5f;
	}
}
