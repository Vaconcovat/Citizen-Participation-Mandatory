using UnityEngine;
using System.Collections;

public class AnimationSettings : MonoBehaviour {

	public KeyCode Left;
	public KeyCode Right;
	public KeyCode Up;
	public KeyCode Down;
	public string MyTrigger;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (Left)) {
			GetComponent<Animator> ().SetTrigger (MyTrigger);
		}

		if (Input.GetKey (Right)) {
			GetComponent<Animator> ().SetTrigger (MyTrigger);
		}

		if (Input.GetKey (Up)) {
			GetComponent<Animator> ().SetTrigger (MyTrigger);
		}

		if (Input.GetKey (Down)) {
			GetComponent<Animator> ().SetTrigger (MyTrigger);
		}
	
	}
}
