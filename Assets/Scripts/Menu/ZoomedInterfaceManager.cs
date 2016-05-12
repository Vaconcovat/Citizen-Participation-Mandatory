using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoomedInterfaceManager : MonoBehaviour {

	public Button preButton, postButton, arenaButton;
	public Text commit;
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
		}
		else{
			preButton.interactable = true;
			postButton.interactable = true;
			arenaButton.interactable = false;
			commit.text = "NOT COMMITTED";
		}
	}
}
