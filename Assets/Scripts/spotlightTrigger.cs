using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spotlightTrigger : MonoBehaviour {

	public GameObject toBeRemoved;
	public Light lightToBeChanged;
	public GameObject[] targetsInScene;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Contestant") {
			//Debug.Log ("Player entered the trigger");
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Contestant") {
			//Debug.Log ("Player is within trigger");
			if (toBeRemoved.name == "Wall (85)" || toBeRemoved.name == "Wall (89)") {
				if (allTargetsDead () == true) {
					Destroy (toBeRemoved, 0.5f);
					lightToBeChanged.color = Color.green;
				}
			} else {
				Destroy (toBeRemoved, 0.5f);
				lightToBeChanged.color = Color.green;
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Contestant") {
			//Debug.Log ("Player exited the trigger");
		}
	}

	bool allTargetsDead () {
		int targets = 0;
		int totalTargets = targetsInScene.Length;
		for(int i = 0; i<targetsInScene.Length; i++) {
			if (targetsInScene[i].GetComponent<Contestant>().type.ToString() == "Target") { 
				if (targetsInScene[i].GetComponent<Contestant>().isAlive == false) {
					targets++;
				}
			}
		}
		//Currently in place of text system (Still having trouble understanding
		print (targets + " out of " + totalTargets + " targets killed!");
		if (targets == targetsInScene.Length) {
			return true;
		} else {
			return false;
		}
	}
}
