using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadArena : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		RoundManager.roundNumber = 1;
		SceneManager.LoadScene(1);
	}
}
