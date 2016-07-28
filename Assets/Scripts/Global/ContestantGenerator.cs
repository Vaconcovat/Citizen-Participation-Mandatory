using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContestantGenerator : MonoBehaviour {
	public TextAsset FirstNames, LastNames, TidBits;
	private List<string> FnameList, LnameList, TidBitList;



	// Use this for initialization
	void Awake () {
		string FnameStr = FirstNames.text;
		string LnameStr = LastNames.text;
		string TidBitStr = TidBits.text;

		//parse the first names
		FnameList = new List<string>();
		FnameList.AddRange(FnameStr.Split("\n"[0]));

		//parse the last names
		LnameList = new List<string>();
		LnameList.AddRange(LnameStr.Split("\n"[0]));

		//parse the tidbits
		TidBitList = new List<string>();
		TidBitList.AddRange(TidBitStr.Split("\n"[0]));

		Debug.Log("Random Info: " + FnameList[Random.Range(0,FnameList.Count)] + " " + LnameList[Random.Range(0,LnameList.Count)] + ": " +TidBitList[Random.Range(0,TidBitList.Count)]);
	}
	
	public string GetFirstName(){
		return FnameList[Random.Range(0,FnameList.Count)];
	}
}
