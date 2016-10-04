using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spotlightTrigger : MonoBehaviour {

	public GameObject toBeRemoved;
	public Light lightToBeChanged;
	public GameObject[] targets;
	public bool activated = false;
	int targetsDead;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Contestant") {
			//Debug.Log ("Player entered the trigger");
			activated=true;
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Contestant") {
			//Debug.Log ("Player is within trigger");
			if (TargetsDead () == true || StaticGameStats.instance.tutorialDone) {
				Destroy (toBeRemoved, 0.5f);
				lightToBeChanged.color = Color.green;
				StaticGameStats.instance.tutorialDone = true;
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Contestant") {
			//Debug.Log ("Player exited the trigger");
		}
	}

	bool TargetsDead () {
		targetsDead = 0;
		for (int i = 0; i < targets.Length; i++) {
			if (targets [i].GetComponent<Contestant> ().isAlive == false) {
				targetsDead++;
			}
		}
		if(targetsDead == targets.Length) {
			return true;
		} else {
			return false;
		}
	}
}
