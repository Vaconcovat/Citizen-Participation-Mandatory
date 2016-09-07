using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArenaMap_Manager : MonoBehaviour {

	public AutoType at;
	string text;
	public Image logo;
	//bool displayed = false;
	public bool skipped = false;
	bool done = false;

	void Update(){
		if(done && Input.GetKeyDown(KeyCode.E)){
			FindObjectOfType<SceneChange>().Arena();
		}
	}


	public void ArenaMap_start_text(){
		Debug.Log ("I Am STARTING SOME TEXT NOW");
		//The line below is what is breaking it
		at.StartType();
	}

	public void Done(){
		done = true;
	}

	public void DisplayLogo(){
		if(!skipped){
			logo.enabled = true;
			//displayed = true;
		}
	}
}
