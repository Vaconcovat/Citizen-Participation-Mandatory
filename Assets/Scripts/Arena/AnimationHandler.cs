using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationHandler : MonoBehaviour {

	public KeyCode Button1;
	public KeyCode Button2;
	public KeyCode Button3;
	public KeyCode Button4;
	public KeyCode Button5;

	public AnimationClip Animation1;
	//in addition to attaching this script make sure to attach an animation component with multiple elements

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (Button1)) {
			GetComponent<Animation> ().Play ("run"); //Enter into these brackets the name of the first animation.. eg: "run"
		} else if (Input.GetKey (Button2)) {
			GetComponent<Animation> ().Play ("jump"); //Enter into these brackets the name of the second animation.. eg: "jump"
		} else if (Input.GetKey (Button3)) {
			GetComponent<Animation> ().Play ("sprint"); //Enter into these brackets the name of the third animation.. eg: "sprint"
		} else if (Input.GetKey (Button4)) {
			GetComponent<Animation> ().Play ("walk"); //Enter into these brackets the name of the fourth animation.. eg: "walk"
		} else if (Input.GetKey (Button5)) {
			GetComponent<Animation> ().Play ("crouch"); //Enter into these brackets the name of the fifth animation.. eg: "crouch"
		} else {
			GetComponent<Animation> ().Play ("idle");
		}

	}
}