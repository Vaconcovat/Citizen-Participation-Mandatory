using UnityEngine;
using System.Collections;

public class spotlightTrigger : MonoBehaviour {

	public GameObject toBeRemoved;
	public Light lightToBeChanged;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Contestant") {
			Debug.Log ("Player entered the trigger");
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Contestant") {
			Debug.Log ("Player is within trigger");
			Destroy (toBeRemoved, 0.5f);
			lightToBeChanged.color = Color.green;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Contestant") {
			Debug.Log ("Player exited the trigger");
			lightToBeChanged.color = Color.white;
		}
	}
}
