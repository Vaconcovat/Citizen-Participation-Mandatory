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
	public static bool[] SponsorUpgrade = new bool[]{false, false, false};
	public static int sponsor;
	public static int arenasPlayed = 0;

	//Global Value Editing
	public static int Upgrade1ItemUsageBuff = 2; //Ability 1 Doubles the number of item uses
	public static int Upgrade2ThrownBuff = 2; //Ability 2 Doubles the damage of thrown weapons
	public static float Upgrade3ReputationGainBuff = 1.05f; //Ability 3 increases all rep gain by 5%
	public static float Upgrade4MaxAmmoBuff = 1.2f; //Ability 4 increases the ammo of all weapons by 20%

	public static int Upgrade5PlayerMoveSpeed = 10;
	public static int Upgrade5PlayerNewSpeed = 15;

	public static float Upgrade6FireRateNerf = 0.8f;
	public static int Upgrade6DamageBuff = 2; //wanted to be 1.2 but this can only accept whole numbers

	public static bool Upgrade7AlreadyTriggered = false;
	public static int Upgrade7HealAmount = 20;

	public static int Upgrade8NormalSpeed = 10;
	public static int Upgrade8MovementSpeedBuff = 15;

	public static int Upgrade9NormalSpeed = 10;
	public static int Upgrade9MovementSpeedBuff = 15;
	public static int Upgrade9HealAmount = 20;

	public static float Upgrade10HealAmount = 3.0f;

	public static int Upgrade11ThrownBuff = 2;
	public static bool Upgrade11AlreadyTriggered = false;

	public static float Ability1MaxDistance = 10.0f;

	//Rep Gain Triggers
	//1.0f = 1% Rep Gain

	//Government Rep Increase
	public static float GovExecutionIncrease = 2.0f;
	public static float GovOnCameraKillIncrease = 0.5f;
	public static float GovEndOfRoundSurrenderIncrease = 0.25f;

	//Government Rep Decrease
	public static float GovKillGuardsDecrease = -1.5f;
	public static float GovActivateMedicBeaconDecrease = -0.75f;

	//Rebel Rep Increase
	public static float RebSuccessfulExtractionIncrease = 2.5f;
	public static float RebKillGuardsIncrease = 1.0f;
	public static float RebActivateMedicBeaconIncrease = 0.5f;
	public static float RebEndOfRoundTriumphIncrease = 5.0f;

	//Rebel Rep Decrease
	public static float RebEndOfRoundSurrenderDecrease = -1.0f;
	public static float RebOnCameraExecutionDecrease = -1.5f;
	public static float RebOnCameraKill = -0.25f;

	//Corporate Rep Increase
	public static float CorSponsorWeaponFireIncrease = 0.05f;
	public static float CorSponsorWeaponKillIncrease = 1.5f;
	public static float CorSponsorItemUseIncrease = 1.0f;

	//Corporate Rep Decrease
	public static float CorEndOfTournamentDecayDecrease = -2.0f;
	public static float CorSponsorWeaponDeathDecrease = -1.0f;





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
				//Debug.Log ("GOV: MOD " + govRep.ToString ());
				break;
			} else {
				govRep += amount;
				//Debug.Log("GOV: " + govRep.ToString());
				break;
			}
				
		case 1:
			if (TierOneUpgrades [2]) {
				corRep += (amount * StaticGameStats.Upgrade3ReputationGainBuff);
				//Debug.Log ("COR: MOD " + corRep.ToString ());
				break;
			} else {
				corRep += amount;
				//Debug.Log("COR: " + corRep.ToString());
				break;
			}
		case 2:
			if (TierOneUpgrades [2]) {
				rebRep += (amount * StaticGameStats.Upgrade3ReputationGainBuff);
				//Debug.Log ("REB: MOD" + rebRep.ToString ());
				break;
			} else {
				rebRep += amount;
				//Debug.Log("REB: " + rebRep.ToString());
				break;
			}
		}
	}
}
