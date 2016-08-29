using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
//using System;

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
	public static bool tutorialDone = false;

	//file to keep record
	//public TextAsset textFile;
	public static string path;

	//Sponsor Items
	public static float VelocitechItemDuration = 5.0f;
	public static float ExplodenaItemDuration = 5.0f;
	public static float PrismexItemDuration = 5.0f;

	//Arena variables
	/// <summary>
	/// [General Upgrade 0], [nothing]
	/// </summary>
	public static bool[] TierOneUpgrades = new bool[]{false, false, false, false};
	public static bool[] TierTwoUpgrades = new bool[]{false, false, false, false};
	public static bool[] TierThreeUpgrades = new bool[]{false, false, false, false};
	public static bool[] Abilites = new bool[]{false, false, false, false};
	public static bool[] MegaCity1SponsorUpgrade = new bool[]{false, false, false};
	public static bool[] ExplodenaSponsorUpgrade = new bool[]{false, false, false};
	public static bool[] VelocitechSponsorUpgrade = new bool[]{false, false, false};
	public static int sponsor;
	public static int arenasPlayed = 0;

	//Global Value Editing
	public static int Upgrade1ItemUsageBuff = 2; //Ability 1 Doubles the number of item uses
	public static int Upgrade2ThrownBuff = 2; //Ability 2 Doubles the damage of thrown weapons
	public static float Upgrade3ReputationGainBuff = 1.05f; //Ability 3 increases all rep gain by 5%
	public static float Upgrade4MaxAmmoBuff = 1.2f; //Ability 4 increases the ammo of all weapons by 20%

	public static float Upgrade5HealAmount = 10.0f;

	public static float Upgrade6FireRateNerf = 0.8f;
	public static int Upgrade6DamageBuff = 2; 

	public static bool Upgrade7AlreadyTriggered = false;
	public static int Upgrade7HealAmount = 20;

	public static int Upgrade8NormalSpeed = 10;
	public static int Upgrade8MovementSpeedBuff = 15;

	public static int Upgrade9NormalSpeed = 10;
	public static int Upgrade9MovementSpeedBuff = 15;
	public static int Upgrade9HealAmount = 20;

	public static float Upgrade10HealAmount = 3.0f;

	public static int Upgrade11ThrownBuff = 4;
	public static bool Upgrade11AlreadyTriggered = false;

	public static float Upgrade12DurationBuff = 1.5f;

	public static float Ability1MaxDistance = 10.0f;

	//Rep Gain Triggers
	//1.0f = 1% Rep Gain

	public enum InfluenceTrigger{Execution, OnCameraKill, EndOfRoundSurrender, KillGuard, ActivateMedicBeacon, SuccessfulExtraction, EndOfRoundTriumph, SponsorWeaponFire, SponsorWeaponKill, SponsorItemUse, SponsorWeaponDeath};
	public static List<InfluenceTrigger> influenceList;

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

	public static string PlayerName;



	// Use this for initialization
	//Probably shouldn't ever have anything here, if you do you're bad.
	void Awake () {
		path = Application.dataPath + "/Resources/InfluenceGains.txt";
		influenceList = new List<InfluenceTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
		if (StaticGameStats.TierOneUpgrades [2]) {
			StaticGameStats.Upgrade3ReputationGainBuff = 1.05f;
		} else {
			StaticGameStats.Upgrade3ReputationGainBuff = 1.0f;
		}
	
	}


	public void Influence(InfluenceTrigger type, float amount){
		influenceList.Add(type);
		switch(type){
			case InfluenceTrigger.ActivateMedicBeacon:
				UpdateInfluence(0, (amount!=0)?amount:GovActivateMedicBeaconDecrease);
				UpdateInfluence(2, (amount!=0)?amount:RebActivateMedicBeaconIncrease);
				break;
			case InfluenceTrigger.EndOfRoundSurrender:
				UpdateInfluence(0, (amount!=0)?amount:GovEndOfRoundSurrenderIncrease);
				UpdateInfluence(2, (amount!=0)?amount:RebEndOfRoundSurrenderDecrease);
				break;
			case InfluenceTrigger.EndOfRoundTriumph:
				UpdateInfluence(2, (amount!=0)?amount:RebEndOfRoundTriumphIncrease);
				break;
			case InfluenceTrigger.Execution:
				UpdateInfluence(0, (amount!=0)?amount:GovExecutionIncrease);
				break;
			case InfluenceTrigger.KillGuard:
				UpdateInfluence(2, (amount!=0)?amount:RebKillGuardsIncrease);
				break;
			case InfluenceTrigger.OnCameraKill:
				UpdateInfluence(0, (amount!=0)?amount:GovOnCameraKillIncrease);
				UpdateInfluence(2, (amount!=0)?amount:RebOnCameraKill);
				break;
			case InfluenceTrigger.SponsorItemUse:
				UpdateInfluence(1, (amount!=0)?amount:CorSponsorItemUseIncrease);
				break;
			case InfluenceTrigger.SponsorWeaponDeath:
				UpdateInfluence(1, (amount!=0)?amount:CorSponsorWeaponDeathDecrease);
				break;
			case InfluenceTrigger.SponsorWeaponFire:
				UpdateInfluence(1, (amount!=0)?amount:CorSponsorWeaponFireIncrease);
				break;
			case InfluenceTrigger.SponsorWeaponKill:
				UpdateInfluence(1, (amount!=0)?amount:CorSponsorWeaponKillIncrease);
				break;
			case InfluenceTrigger.SuccessfulExtraction:
				UpdateInfluence(2, (amount!=0)?amount:RebSuccessfulExtractionIncrease);
				break;
		}

	}

	public void UpdateInfluence(int faction, float amount){
		switch (faction){
		case 0:
			govRep += (amount * StaticGameStats.Upgrade3ReputationGainBuff);
			//Debug.Log ("GOV: MOD " + govRep.ToString ());
			break;
		case 1:
			corRep += (amount * StaticGameStats.Upgrade3ReputationGainBuff);
			//Debug.Log ("GOV: MOD " + govRep.ToString ());
			break;
		case 2:
			rebRep += (amount * StaticGameStats.Upgrade3ReputationGainBuff);
			//Debug.Log ("GOV: MOD " + govRep.ToString ());
			break;
		}
	}



//	public void checkText(string textToBeChecked){
//		
//		File.AppendAllText (path, "\n");
//
//		switch (textToBeChecked) {
//		case "GovExecutionIncrease":
//			//add predefined text to text file
//			// This text is added only once to the file.
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				string createText = "Increased Government Reputation: Execution";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "GovOnCameraKillIncrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				Debug.Log("Sent Message to text file");
//				string createText = "Increased Government Reputation: Kill On Camera";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "GovEndOfRoundSurrenderIncrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				Debug.Log("Sent Message to text file");
//				string createText = "Decreased Government Reputation: Refused to Surrender";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "GovKillGuardsDecrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				Debug.Log("Sent Message to text file");
//				string createText = "Decreased Government Reputation: Killed Guards";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "GovActivateMedicBeaconDecrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				Debug.Log("Sent Message to text file");
//				string createText = "Decreased Government Reputation: Activated Medic Beacon";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "RebSuccessfulExtractionIncrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("");
//				string createText = "Increased Rebel Reputation: Successful Extraction of Contestant";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "RebKillGuardsIncrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("Sent Message to text file");
//				string createText = "Increased Rebel Reputation: Killed a Guard";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "RebActivateMedicBeaconIncrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("Sent Message to text file");
//				string createText = "Increased Rebel Reputation: Activated a Medic Beacon";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "RebEndOfRoundTriumphIncrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("Sent Message to text file");
//				string createText = "Increased Rebel Reputation: Won the round";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "RebEndOfRoundSurrenderDecrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("Sent Message to text file");
//				string createText = "Decreased Rebel Reputation: Surrendered";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "RebOnCameraExecutionDecrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("Sent Message to text file");
//				string createText = "Decreased Rebel Reputation: Executed a Contestant on Camera";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "RebOnCameraKill":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("Sent Message to text file");
//				string createText = "Decreased Rebel Reputation: Killed a Contestant on Camera";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "CorSponsorWeaponFireIncrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("Sent Message to text file");
//				string createText = "Increased Corporate Reputation: Used Sponsor Weapon";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "CorSponsorWeaponKillIncrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("Sent Message to text file");
//				string createText = "Increased Corporate Reputation: Killed with the Sponsor Weapon";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "CorSponsorItemUseIncrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("Sent Message to text file");
//				string createText = "Increased Corporate Reputation: Used Sponsor's Item";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "CorEndOfTournamentDecayDecrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("Sent Message to text file");
//				string createText = "Decreased Corporate Reputation: Took a long time to finish round";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "CorSponsorWeaponDeathDecrease":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("Sent Message to text file");
//				string createText = "Decreased Corporate Reputation: Died with Sponsor Weapon";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		case "RebWeaponOnCamera":
//			//add predefined text to text file
//			if (File.Exists(path))
//			{
//				// Create a file to write to.
//				//Debug.Log("Sent Message to text file");
//				string createText = "Decreased Rebel Reputation: Used Weapon on Camera";
//				File.AppendAllText (path, createText);
//			}
//			//add new line to text file
//			break;
//		}
//	}
}
