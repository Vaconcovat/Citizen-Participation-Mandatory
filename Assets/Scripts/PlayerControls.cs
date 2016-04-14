using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	


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
		if (Input.GetMouseButton(0) && attributes.Equipped != null){
			attributes.Equipped.SendMessage("Fire");
		}
		//Throw on rightclick
		if (Input.GetMouseButtonDown(1)){
			ThrowEquipped();
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
		if (attributes.Equipped != null){
			Weapon wep = attributes.Equipped.GetComponent<Weapon>();
			Debug.Log(wep);
			if (wep != null){
				wep.Throw();
			}
			attributes.pickupCooldownCount = attributes.pickupCooldown;
		}
	}
}
