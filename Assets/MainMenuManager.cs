using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour {
	public Text loggingalert;
		
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("Analytics") != 1){
			loggingalert.enabled = false;
		}
		else{
			loggingalert.enabled = true;
		}
	}
}
