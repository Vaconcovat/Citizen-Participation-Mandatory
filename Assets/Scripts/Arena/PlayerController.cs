﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Contestant contestant;
	public Vector3 pos;
	Vector3 moveDir;
	Rigidbody body;

	[Header("Settings")]
	public bool smoothed;
	public float cameraDistance;

	[Header("something else")]

	float gravity = 20.0f;

	void Awake(){
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+cameraDistance, transform.position.z);
	}

	// Use this for initialization
	void Start () {
		contestant = GetComponent<Contestant>();
		body = GetComponent<Rigidbody>();
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+cameraDistance, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		MouseControls();
		KeyboardControls();
		FaceMouse();
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+cameraDistance, transform.position.z);
		if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0){
			contestant.moving = false;
		}
		else{
			contestant.moving = true;
		}
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
			moveDir = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));

		}
		else{
			moveDir = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical")).normalized;
		}
		//moveDir = transform.TransformDirection(moveDir);
		moveDir *= contestant.movespeed;
		moveDir.y -= gravity * Time.deltaTime;
		body.velocity = moveDir;

		if (StaticGameStats.instance.TierOneUpgrades [0]) {
			if(Input.GetKeyUp(KeyCode.LeftControl)){
				contestant.swap();
			}
		}

	}

	void FaceMouse(){
		pos = Input.mousePosition;
		pos.z = Mathf.Abs(Camera.main.transform.position.y - transform.position.y);
		pos = Camera.main.ScreenToWorldPoint(pos);
		transform.LookAt(pos);
	}

	void OnDrawGizmos(){
		Gizmos.DrawCube(pos, Vector3.one * 0.5f);
	}
}
