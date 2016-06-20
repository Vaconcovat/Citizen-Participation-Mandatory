using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Contestant contestant;
	Rigidbody body;

	public bool smoothed;

	// Use this for initialization
	void Start () {
		contestant = GetComponent<Contestant>();
		body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		MouseControls();
		KeyboardControls();
		//FaceMouse();
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+10, transform.position.z);
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
		if(smoothed){
			//float mag = Mathf.Min(Mathf.Abs(Input.GetAxis("Horizontal") + Mathf.Abs(Input.GetAxis("Vertical"))), 1.0f);
			//Debug.Log(mag);
			body.velocity = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical")) * contestant.movespeed;
		}
		else{
			body.velocity = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical")).normalized * contestant.movespeed;
		}
		if(Input.GetKeyUp(KeyCode.LeftControl)){
			contestant.swap();
		}
	}

	void FaceMouse(){
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
