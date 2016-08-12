using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContestantGenerator : MonoBehaviour {
	public TextAsset FirstNames, LastNames, TidBits, Pleading, Retreating, Fighting;
	private List<string> FnameList, LnameList, TidBitList, PleadingList, RetreatingList, FightingList;

	public enum LineType{Plead, Retreat, Fight};


	// Use this for initialization
	void Awake () {
		string FnameStr = FirstNames.text;
		string LnameStr = LastNames.text;
		string TidBitStr = TidBits.text;
		string PleadingStr = Pleading.text;
		string RetreatingStr = Retreating.text;
		string FightingStr = Fighting.text;

		//parse the first names
		FnameList = new List<string>();
		FnameList.AddRange(FnameStr.Split("\n"[0]));

		//parse the last names
		LnameList = new List<string>();
		LnameList.AddRange(LnameStr.Split("\n"[0]));

		//parse the tidbits
		TidBitList = new List<string>();
		TidBitList.AddRange(TidBitStr.Split("\n"[0]));

		//parse the Pleading lines
		PleadingList = new List<string>();
		PleadingList.AddRange(PleadingStr.Split("\n"[0]));

		//parse the Retreating lines
		RetreatingList = new List<string>();
		RetreatingList.AddRange(RetreatingStr.Split("\n"[0]));

		//parse the Fighting lines
		FightingList = new List<string>();
		FightingList.AddRange(FightingStr.Split("\n"[0]));
	}
	
	public string GetFirstName(){
		return FnameList[Random.Range(0,FnameList.Count)];
	}

	public string GetLastName(){
		return LnameList[Random.Range(0,LnameList.Count)];
	}

	public string GetTidBit(){
		return TidBitList[Random.Range(0,TidBitList.Count)];
	}

	public string GetLine(LineType type){
		string line = "";
		switch(type){
			case LineType.Plead:
				line = PleadingList[Random.Range(0,PleadingList.Count)];
				break;
			case LineType.Retreat:
				line = RetreatingList[Random.Range(0,RetreatingList.Count)];
				break;
			case LineType.Fight:
				line = FightingList[Random.Range(0,FightingList.Count)];
				break;
		}
		return line;
	}
}
