using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadArena : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Contestant") {
			RoundManager.roundNumber = 1;
			SceneManager.LoadScene(1);
		}
	}
}
