using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BootInterfaceManager : MonoBehaviour {
	public Image logo;
	public float logoWaitTime;
	bool displayed = false;
	public bool skipped = false;

	
	// Update is called once per frame
	void Update () {
		if(displayed){
			logoWaitTime -= Time.deltaTime;
		}
		if (logoWaitTime <= 0){
			FindObjectOfType<MenuCamera>().MainMenu();
			displayed = false;
			skipped = true;
			logoWaitTime = 3;
		}
//		if (Input.GetKeyDown(KeyCode.Space) && !skipped){
//			FindObjectOfType<MenuCamera>().MainMenu();
//			displayed = false;
//			skipped = true;
//			logoWaitTime = 3;
//		}
		if (Input.GetKeyDown(KeyCode.Delete) && !skipped){
			FindObjectOfType<MenuCamera>().MainMenu();
			StaticGameStats.QuestionnaireDone = true;
			displayed = false;
			skipped = true;
			logoWaitTime = 3;
		}
	}

	public void DisplayLogo(){
		if(!skipped){
			logo.enabled = true;
			displayed = true;
		}
	}
}
