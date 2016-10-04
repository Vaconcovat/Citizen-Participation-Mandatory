using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	void Awake(){
		StaticGameStats stats = FindObjectOfType<StaticGameStats>();
		if(stats != null){
			Destroy(FindObjectOfType<StaticGameStats>().gameObject);
		}
	}

	void Start(){
		SceneManager.LoadScene(0);
	}
}
