using UnityEngine;
using System.Collections;

public class PowerBox : MonoBehaviour {
	public GameObject Camera;

	void OnCollisionEnter (Collision col)
	{
		if ((col.gameObject.tag == "Bullet") || (col.gameObject.tag == "Weapon"))
		{
			Destroy (Camera);
		}
	}

	public void TakeDamage(DamageParams damage){
		if (damage.damage > 0) {
			Destroy (Camera);
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
