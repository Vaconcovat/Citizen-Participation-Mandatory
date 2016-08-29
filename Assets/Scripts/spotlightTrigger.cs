using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spotlightTrigger : MonoBehaviour {

	public GameObject toBeRemoved;
	public Light lightToBeChanged;
	public GameObject target;
	public bool activated = false;

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
			if (TargetsDead () == true || StaticGameStats.tutorialDone) {
				Destroy (toBeRemoved, 0.5f);
				lightToBeChanged.color = Color.green;
				StaticGameStats.tutorialDone = true;
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
		if (target.GetComponent<Contestant>().isAlive == false) {
			return true;
		} else {
			return false;
		}
	}
}
