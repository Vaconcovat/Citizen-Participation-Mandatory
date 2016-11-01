using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	void Awake(){
		StaticGameStats stats = FindObjectOfType<StaticGameStats>();
		if(stats != null){
			Destroy(stats.gameObject);
			Debug.Log("Reset singleton!");
		}
		TutorialTrigger.FromTutorial = false;
	}

	void Start(){
		SceneManager.LoadScene(0);
	}
}
