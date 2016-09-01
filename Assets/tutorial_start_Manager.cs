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
		at.StartType();
	}

	public void Done(){
		done = true;
	}
}