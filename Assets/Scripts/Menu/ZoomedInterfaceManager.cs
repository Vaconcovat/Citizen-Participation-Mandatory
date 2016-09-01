using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoomedInterfaceManager : MonoBehaviour {

	public Button preButton, postButton, arenaButton;
	public Text commit;
	//public Text activeSponsor;  
	public PreMenuInterfaceManager PreMenuScript;
	public Text StatusBarText, ArenaPlanningText, CommsText, ArenaText, WelcomeText, directory;

	[TextArea(1,5)]
	public string preCommitText, postCommitText, arenaCommitText, preNotCommitText, postNotCommitText, arenaNotCommitText;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(StaticGameStats.committed){
			preButton.interactable = false;
			postButton.interactable = false;
			arenaButton.interactable = true;
			commit.text = "COMMITTED";
			ArenaPlanningText.text = "LOCKED";
			ArenaText.text = "OPEN";
			CommsText.text = "LOCKED";

			preButton.GetComponent<HoverText> ().tooltip = preCommitText;
			arenaButton.GetComponent<HoverText> ().tooltip = arenaCommitText;
			postButton.GetComponent<HoverText> ().tooltip = postCommitText;

		}
		else{
			preButton.interactable = true;
			postButton.interactable = true;
			arenaButton.interactable = false;
			commit.text = "NOT COMMITTED";

			preButton.GetComponent<HoverText> ().tooltip = preNotCommitText;
			arenaButton.GetComponent<HoverText> ().tooltip = arenaNotCommitText;
			postButton.GetComponent<HoverText> ().tooltip = postNotCommitText;
		}
		WelcomeText.text = "Welcome to GovorNet Systems, " + StaticGameStats.PlayerName;
		directory.text = @"G:\GovorNet\" + StaticGameStats.PlayerName + @"\MAIN.gov";
	}
}
