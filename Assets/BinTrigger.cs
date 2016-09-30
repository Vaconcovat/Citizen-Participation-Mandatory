using UnityEngine;
using System.Collections;

public class BinTrigger : MonoBehaviour {

	public static bool ThrownWeapon = false;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Weapon") {
			Destroy(other.gameObject);
			other.gameObject.GetComponent<Item> ().tracker.gameObject.SetActive(false);
			ThrownWeapon = true;
		}
	}
}
