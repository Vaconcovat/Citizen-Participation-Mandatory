using UnityEngine;
using System.Collections;

public class LoginInterfaceManager : MonoBehaviour {

	public AutoType consoleText, logInText, username, password;

	MenuCamera mcamera;
	bool started = false;
	// Use this for initialization
	void Start () {
		mcamera = FindObjectOfType<MenuCamera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (mcamera.state == 5 && !started){
			started = true;
			username.displayedText[0] = StaticGameStats.PlayerName;
			consoleText.StartType();
		}
	}
}
