using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Contestant contestant;
	Rigidbody2D body;

	// Use this for initialization
	void Start () {
		contestant = GetComponent<Contestant>();
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		MouseControls();
		KeyboardControls();
		FaceMouse();
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}

	void MouseControls(){
		//When holding left mouse
		if (Input.GetMouseButton(0)){
			contestant.UseEquipped(true);
		}
		//When left mouse is pressed
		if (Input.GetMouseButtonDown(0)){
			contestant.UseEquipped(false);
		}
		//When right mouse is pressed
		if (Input.GetMouseButtonDown(1)){
			contestant.ThrowEquipped();
		}
	}

	void KeyboardControls(){
		//Assuming the axis are set up properly
		body.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * contestant.movespeed;
	}

	void FaceMouse(){
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
