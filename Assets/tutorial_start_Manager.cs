using UnityEngine;
using System.Collections;

public class tutorial_start_Manager : MonoBehaviour {

	public AutoType at;
	string text;
	bool done = false;

	void Update(){
		if(done && (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))){
			FindObjectOfType<SceneChange>().Tutorial();
		}
		if (done && StaticGameStats.instance.tutorialDone && (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) ) {
			FindObjectOfType<SceneChange>().Arena();
		}
		if (done && StaticGameStats.instance.tutorialDone && (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) ) {
			FindObjectOfType<MenuCamera> ().ArenaMap ();
		}
	}

	void CheckTutorial(){
		text += "[ - - - - - - - - ]";
		text += "\nCONNECTION TO TRAINING CENTER READY";
		text += "\n               PRESS [1] TO LAUNCH TUTORIAL...";
		if (StaticGameStats.instance.tutorialDone) {
			text += "\n               PRESS [2] TO LAUNCH ARENA...";
			text += "\n               PRESS [3] TO LAUNCH ARENA MAP...";
		}

		at.displayedText[1] = text;
	}
		

	public void tutorial_start_text(){
		CheckTutorial ();
		at.StartType();
	}

	public void Done(){
		done = true;
	}
}