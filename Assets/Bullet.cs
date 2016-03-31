using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Rigidbody2D body;
	public int damage;

	public void Fire(Vector2 velocity){
		body.AddForce(velocity, ForceMode2D.Impulse);
	}

	void Update(){
		//if(body.velocity.magnitude < 5){
			//Destroy(gameObject);
		//}
	}

	void OnCollisionEnter2D(Collision2D coll){
		Debug.Log(coll.gameObject);
		if(coll.gameObject.tag != "Item"){
			coll.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}
}
