using UnityEngine;
using System.Collections;

public class PowerBox : MonoBehaviour {
	public GameObject Camera;
	public bool isAlive;
	public int Health;

	void Start () {
		Health = 25;
		isAlive = true;
	}

	void Update(){
		if (Health <= 0) {
			isAlive = false;
		}
		if (!isAlive) {
			Destroy (Camera);
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
}
