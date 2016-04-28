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

	[Header("Runtime Only")]
	/// <summary>
	/// Who shot this bullet.
	/// </summary>
	public Contestant owner;

	Rigidbody2D body;

	// Use this for initialization
	void Awake () {
		body = GetComponent<Rigidbody2D>();
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

	public void Fire(Vector2 vector){
		Vector2 v = vector * velocityModifier;
		body.AddForce(v, ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (areaOfEffect > 0){
			Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x, this.transform.position.y), areaOfEffect);
			foreach (Collider2D a in colliders){
				a.gameObject.SendMessage("TakeDamage", new Contestant.DamageParams(damage, owner, (a.transform.position - this.transform.position).normalized * explosiveForce / Vector3.Distance(a.transform.position, this.transform.position), a.transform.position), SendMessageOptions.DontRequireReceiver);
			}
			Destroy(gameObject);
		}
		else{
			coll.gameObject.SendMessage("TakeDamage", new Contestant.DamageParams(damage, owner, body.velocity.normalized, coll.contacts[0].point), SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}

	void Zany(){
		body.AddForce(Quaternion.AngleAxis(90,Vector3.forward) * transform.right * Mathf.Cos(Time.frameCount/10) * 0.2f, ForceMode2D.Impulse);
		transform.localScale = new Vector3(Mathf.Cos(Time.frameCount/10), Mathf.Cos(Time.frameCount/10), 1) * 0.5f;
	}
}
