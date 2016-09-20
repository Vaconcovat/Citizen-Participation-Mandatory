using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadArena : MonoBehaviour {

	public static bool FromTutorial = false;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Contestant") {
			StaticGameStats.tutorialDone = true;
			RoundManager.roundNumber = 1;
			FromTutorial = true;
			SceneManager.LoadScene(0);
		}
	}
}
