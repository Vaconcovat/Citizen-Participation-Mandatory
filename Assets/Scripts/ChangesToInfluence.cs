using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChangesToInfluence : MonoBehaviour {

	string inflText;
	private List<string> influenceList;
	bool roundOver;

	void Awake  () {
		influenceList = new List<string>();
		roundOver = false;
	}

	// Use this for initialization
	void Start () {
		inflText = InterfaceManager.influenceText.ToString();
		influenceList.AddRange(inflText.Split("\n"[0]));
		//string[] infArray = influenceList.ToArray();
	}
	
	// Update is called once per frame
	void Update () {
		if(roundOver){
			//do something printInfluence();

		}
	}

	void printInfluence() {
		for (int i = 0; i < influenceList.Count; i++) {
			foreach (char letter in influenceList[i].ToCharArray()) {
				this.GetComponent<Text>().text += letter;
			}
			this.GetComponent<Text>().text += "\n";
		}
	}
}
