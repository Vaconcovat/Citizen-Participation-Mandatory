using UnityEngine;
using System.Collections;

public class BinTrigger : MonoBehaviour {

	public static bool ThrownWeapon = false;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Weapon") {
			other.gameObject.GetComponent<RangedWeapon>().ammo = 0;
			if(FindObjectOfType<TutorialManager>().state == TutorialManager.TutorialState.Bin){
				ThrownWeapon = true;
			}
		}
	}
}
