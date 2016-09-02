using UnityEngine;
using System.Collections;

public class tutorial_start_Manager : MonoBehaviour {

	public AutoType at;
	string text;
	bool done = false;

	void Update(){
		if(done && Input.GetKeyDown(KeyCode.E)){
			FindObjectOfType<SceneChange>().Tutorial();
		}
	}
		

	public void tutorial_start_text(){
		Debug.Log ("I Am STARTING SOME TEXT NOW");
		//The line below is what is breaking it
		at.StartType();
	}

	public void Done(){
		done = true;
	}
}