using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public GameObject gunAnchor;
	public GameObject Equipped;
	public float pickupCooldown;

	float pickupCooldownCount = 0;
	Rigidbody2D body;
	PlayerAttributes attributes;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		attributes = GetComponent<PlayerAttributes>();
	}
	
	// Update is called once per frame
	void Update () {
		//rotate sprite to face the mouse
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		//keep the camera on the player
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
		//Shoot on leftclick
		if (Input.GetMouseButton(0) && Equipped != null){
			Equipped.SendMessage("Fire");
		}
		//Throw on rightclick
		if (Input.GetMouseButtonDown(1)){
			ThrowEquipped();
		}
		//run down our pickup timer
		if (pickupCooldownCount > 0){
			pickupCooldownCount -= Time.deltaTime;
		}
		if (pickupCooldownCount < 0){
			Equipped = null;
			pickupCooldownCount = 0;
		}
	}

	// Use FixedUpdate for all physics calculations, like the player's movement
	void FixedUpdate(){
		//Holy shit this is not the way you correctly get the input shut up, it works
		//-------------SINGLE KEYPRESS------------
		//Only W, no other keys
		if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
			body.velocity = Vector2.up * attributes.speed;
		}	
		//Only S, no other keys
		else if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
			body.velocity = Vector2.down * attributes.speed;
		}
		//Only A, no other keys
		else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
			body.velocity = Vector2.left * attributes.speed;
		}
		//Only D, no other keys
		else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)){
			body.velocity = Vector2.right * attributes.speed;
		}
		//-------------DOUBLE KEYPRESS------------
		//Only W & D, no other keys
		else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)){
			body.velocity = new Vector2(0.5f,0.5f).normalized * attributes.speed;
		}	
		//Only D & S, no other keys
		else if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)){
			body.velocity = new Vector2(0.5f,-0.5f).normalized * attributes.speed;
		}
		//Only S & A, no other keys
		else if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
			body.velocity = new Vector2(-0.5f,-0.5f).normalized * attributes.speed;
		}
		//Only A & W, no other keys
		else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
			body.velocity = new Vector2(-0.5f,0.5f).normalized * attributes.speed;
		}
		//--------------NO KEYPRESS---------------
		else{
			body.velocity = Vector2.zero;
		}
	}
	public void ThrowEquipped(){
		if (Equipped != null){
			Weapon wep = Equipped.GetComponent<Weapon>();
			if (wep != null){
				wep.Throw();
			}
			pickupCooldownCount = pickupCooldown;
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Item" && Equipped == null){
			coll.gameObject.SendMessage("Equip", this);
			Equipped = coll.gameObject;
		}
	}
}
