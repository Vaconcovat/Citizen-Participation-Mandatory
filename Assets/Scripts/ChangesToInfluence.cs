using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChangesToInfluence : MonoBehaviour {

	public TextAsset influenceFile;
	private List<string> influenceList;
	public Text textObj;

	void Awake  () {
		string influenceStr = influenceFile.text;

		influenceList = new List<string>();
		influenceList.AddRange(influenceStr.Split("\n"[0]));
	}

	// Use this for initialization
	void Start () {
		string[] infArray = influenceList.ToArray();
		for (int i = 0; i < influenceList.Count; i++) {
			foreach (char letter in infArray[i].ToCharArray()) {
				textObj.text += letter;
			}
			textObj.text += "\n";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
