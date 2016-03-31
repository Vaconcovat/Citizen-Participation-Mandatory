using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Arena(){
		SceneManager.LoadScene("Test");
	}

	public void PreArena(){
		SceneManager.LoadScene("preArena");
	}

	public void PostArena(){
		SceneManager.LoadScene("postArena");
	}
}
