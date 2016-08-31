using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class followGuard : MonoBehaviour {
	public GameObject guardText;

	void Update () {
		guardText.transform.position = new Vector3(transform.position.x-6, 0, transform.position.z-2);
	}

}
