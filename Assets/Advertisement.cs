using UnityEngine;
using System.Collections;

public class Advertisement : MonoBehaviour {

	public Material[] mats;
	public bool autoSwap = false;
	public float timer = 0;
	public float readyTime;

	// Update is called once per frame
	void Update () {
		if(autoSwap){
			if(timer > 0){
				timer -= Time.deltaTime;
			}
			else{
				timer = readyTime;
				SwapAdvert ();
			}
		}
		else{
			if(Vector3.Distance(FindObjectOfType<PlayerController>().transform.position, transform.position) < 5 && !autoSwap){
				if(Input.GetKeyDown(KeyCode.E)){
					SwapAdvert();
				}
			}
		}
	}

	public void SwapAdvert(){
		int x = Random.Range (1, 3);
		switch (x) {
			case 1:
				Debug.Log ("1");
				GetComponent<Renderer> ().material = mats [0];
				break;
			case 2:
				Debug.Log ("2");
				GetComponent<Renderer> ().material = mats [1];
				break;
			case 3:
				Debug.Log ("3");
				GetComponent<Renderer> ().material = mats [2];
				break;
			}
		}
}
