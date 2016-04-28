using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChange : MonoBehaviour {

	public void Exit(){
		Application.Quit();
	}

	public void Arena(){
		SceneManager.LoadScene(1);
	}

	public void Menu(){
		SceneManager.LoadScene(0);
	}
}
