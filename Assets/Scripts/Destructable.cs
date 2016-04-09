using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour {

	public int health;
	public GameObject drop;
	public float dropchance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0 ){
			if (Random.Range(0,1.0f) < dropchance){
				GameObject droppeditem = (GameObject)Instantiate(drop, transform.position, Quaternion.identity);
				droppeditem.gameObject.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-20.0f,20.0f));
			}
			Destroy(gameObject);
		}
	}

	public void TakeDamage(int damage){
		health -= damage;
	}
}

