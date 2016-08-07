using UnityEngine;
using System.Collections;

public class StaticGameStats : MonoBehaviour {
	//Reputation variables
	public static float govRep = 50.0f;
	public static float corRep = 50.0f;
	public static float rebRep = 50.0f;
	public static float oldgovRep = 50.0f;
	public static float oldcorRep = 50.0f;
	public static float oldrebRep = 50.0f;

	//money variables
	public static int avaliableMoney;
	public static int embezzledMoney = 0;
	public static int moneyHolder;
	public static int embezzleHolder;

	//sponsor variables
	public static int chosenSponsor = -1;
	public static int activeSponsor;

	//Commit
	public static bool committed = false;
	public static bool toPost = false;

	//Arena variables
	/// <summary>
	/// [General Upgrade 0], [nothing]
	/// </summary>
	public static bool[] TierOneUpgrades = new bool[]{false, false, false, false};
	public static bool[] TierTwoUpgrades = new bool[]{false, false, false, false};
	public static bool[] TierThreeUpgrades = new bool[]{false, false, false, false};
	public static bool[] Abilites = new bool[]{false, false, false, false};
	public static int sponsor;
	public static int arenasPlayed = 0;

	//Global Value Editing
	public static int Upgrade1ItemUsageBuff = 2; //Ability 1 Doubles the number of item uses
	public static int Upgrade2ThrownBuff = 2; //Ability 2 Doubles the damage of thrown weapons
	public static float Upgrade3ReputationGainBuff = 1.05f; //Ability 3 increases all rep gain by 5%
	public static float Upgrade4MaxAmmoBuff = 1.2f; //Ability 4 increases the ammo of all weapons by 20%

	public static float Upgrade6FireRateNerf = 0.8f;
	public static int Upgrade6DamageBuff = 2; //wanted to be 1.2 but this can only accept whole numbers

	public static bool Upgrade7AlreadyTriggered = false;
	public static int Upgrade7HealAmount = 20;

	public static int Upgrade8NormalSpeed = 10;
	public static int Upgrade8MovementSpeedBuff = 15;



	// Use this for initialization
	//Probably shouldn't ever have anything here, if you do you're bad.
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
			if (TierOneUpgrades [2]) {
				govRep += (amount * StaticGameStats.Upgrade3ReputationGainBuff);
				Debug.Log ("GOV: MOD " + govRep.ToString ());
				break;
			} else {
				govRep += amount;
				Debug.Log("GOV: " + govRep.ToString());
				break;
			}
				
		case 1:
			if (TierOneUpgrades [2]) {
				corRep += (amount * StaticGameStats.Upgrade3ReputationGainBuff);
				Debug.Log ("COR: MOD " + corRep.ToString ());
				break;
			} else {
				corRep += amount;
				Debug.Log("COR: " + corRep.ToString());
				break;
			}
		case 2:
			if (TierOneUpgrades [2]) {
				rebRep += (amount * StaticGameStats.Upgrade3ReputationGainBuff);
				Debug.Log ("REB: MOD" + rebRep.ToString ());
				break;
			} else {
				rebRep += amount;
				Debug.Log("REB: " + rebRep.ToString());
				break;
			}
		}
	}
}
