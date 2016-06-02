using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoomedInterfaceManager : MonoBehaviour {

	public Button preButton, postButton, arenaButton;
	public Text commit;
	//public Text activeSponsor;  
	public PreMenuInterfaceManager PreMenuScript;
	public Text StatusBarText, ArenaPlanningText, CommsText, ArenaText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (PreMenuScript.chosenSponsor == 0) {
			StatusBarText.text = "CHOSEN SPONSOR: MEGA CITY 1";
		} else if (PreMenuScript.chosenSponsor == 1) {
			StatusBarText.text = "CHOSEN SPONSOR: EXPLODENA DYNAMITE SOLUTIONS";
		}
		if(StaticGameStats.committed){
			preButton.interactable = false;
			postButton.interactable = false;
			arenaButton.interactable = true;
			commit.text = "COMMITTED";
			ArenaPlanningText.text = "LOCKED";
			ArenaText.text = "OPEN";
			CommsText.text = "LOCKED";
		}
		else{
			preButton.interactable = true;
			postButton.interactable = true;
			arenaButton.interactable = false;
			commit.text = "NOT COMMITTED";
		}
	}
}
