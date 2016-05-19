using UnityEngine;
using System.Collections;

public class CameraMan : MonoBehaviour {

	public GameObject bloodSplatter;
	public GameObject player;
	Rigidbody2D body;
	Collider2D coll;
	public LayerMask layers;


	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
		player = FindObjectOfType<PlayerController>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (coll.enabled){
			FacePlayer();
		}
	}

	public void TakeDamage(Contestant.DamageParams damage){
		body.AddForce(damage.knockback, ForceMode2D.Impulse);
		Die();
		GameObject blood = (GameObject)Instantiate(bloodSplatter,damage.location,Quaternion.AngleAxis(Random.Range(0f,360f),Vector3.forward));
		float scale = Random.Range(0.08f,0.2f);
		blood.transform.localScale = new Vector3(scale,scale,1);
	}

	public void Die(){
		//body.isKinematic = true;
		coll.enabled = false;
		GetComponent<SpriteRenderer>().color = Color.white;
		FindObjectOfType<StaticGameStats>().Influence(2,5.0f);
		//TODO: other corpse related things here
	}

	void FacePlayer(){
		Vector3 dir = player.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
