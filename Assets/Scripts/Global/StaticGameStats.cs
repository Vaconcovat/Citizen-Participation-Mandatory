using UnityEngine;
using System.Collections;

public class StaticGameStats : MonoBehaviour {


	public static float govRep = 50.0f;
	public static float corRep = 50.0f;
	public static float rebRep = 50.0f;
	public static float oldgovRep = 50.0f;
	public static float oldcorRep = 50.0f;
	public static float oldrebRep = 50.0f;

	public static int money;
	public static int arenasPlayed = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Influence the specified faction and amount.
	/// </summary>
	/// <param name="faction">0 = gov, 1= cor, 2 = reb</param>
	/// <param name="amount">Amount.</param>
	public void Influence(int faction, float amount){
		switch (faction){
			case 0:
				govRep += amount;
				Debug.Log("GOV: " + govRep.ToString());
				break;
			case 1:
				corRep += amount;
				Debug.Log("COR: " + corRep.ToString());
				break;
			case 2:
				rebRep += amount;
				Debug.Log("REB: " + rebRep.ToString());
				break;
		}
	}
}
