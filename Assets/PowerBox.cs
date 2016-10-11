using UnityEngine;
using System.Collections;

public class PowerBox : MonoBehaviour {
	public GameObject Camera;
	public bool isAlive;
	public int Health;
	public GameObject flare;
	bool hasDied = false;

	void Start () {
		Health = 25;
		isAlive = true;
	}

	void Update(){
		if (Health <= 0) {
			isAlive = false;
		}
		if (!isAlive) {
			Kill(Camera);
			if (!hasDied) {
				CameraExplosion (Camera);
				hasDied = true;
			}
		}

	}

	public void TakeDamage(DamageParams damage){
		if (isAlive) {
			if (damage.damage > 0) {
				Health -= damage.damage;
			}
		}
	}
		

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

	void Kill(GameObject g){
		g.GetComponent<Arena_Camera>().enabled = false;
		g.GetComponent<Rigidbody>().isKinematic = false;
	}

	void CameraExplosion (GameObject g)
	{
		//Debug.Log ("Health Kit Used");
		FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().explosion, transform.position, 0.3f, true);
		GameObject spawned = (GameObject)Instantiate (flare, g.transform.position, Quaternion.identity);
		spawned.transform.Rotate (90,0,0);
		spawned.transform.parent = this.transform;
		Destroy(spawned, 4.0f);
	}
}
