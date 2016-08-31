using UnityEngine;
using System.Collections;

public class BinTrigger : MonoBehaviour {


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Weapon") {
			Debug.Log ("Gun entered the trigger");
			Destroy(other.gameObject);
		}
	}

	void OnTriggerStay (Collider other)
	{
		Debug.Log ("Gun stayed in the trigger");
	}

	void OnTriggerExit (Collider other)
	{
		Debug.Log ("Gun exited the trigger");
	}
}
