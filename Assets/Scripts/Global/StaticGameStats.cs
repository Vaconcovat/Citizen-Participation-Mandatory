using UnityEngine;
using System.Collections;
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

	//file to keep record
	//public TextAsset textFile;
	public static string path;



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
	public static float RebWeaponOnCamera = 0.1f;

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
		path = Application.dataPath + "/Resources/InfluenceGains.txt";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Influence the specified faction and amount.
	/// </summary>
	/// <param name="faction">0 = gov, 1= cor, 2 = reb</param>
	/// <param name="amount">Amount.</param>
	public void Influence(int faction, float amount, string text){
		checkText (text);
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

	public void checkText(string textToBeChecked){
		
		File.AppendAllText (path, "\n");

		switch (textToBeChecked) {
		case "GovExecutionIncrease":
			//add predefined text to text file
			// This text is added only once to the file.
			if (File.Exists(path))
			{
				// Create a file to write to.
				string createText = "Increased Government Reputation: Execution";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "GovOnCameraKillIncrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				Debug.Log("Sent Message to text file");
				string createText = "Increased Government Reputation: Kill On Camera";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "GovEndOfRoundSurrenderIncrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				Debug.Log("Sent Message to text file");
				string createText = "Decreased Government Reputation: Refused to Surrender";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "GovKillGuardsDecrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				Debug.Log("Sent Message to text file");
				string createText = "Decreased Government Reputation: Killed Guards";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "GovActivateMedicBeaconDecrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				Debug.Log("Sent Message to text file");
				string createText = "Decreased Government Reputation: Activated Medic Beacon";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "RebSuccessfulExtractionIncrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("");
				string createText = "Increased Rebel Reputation: Successful Extraction of Contestant";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "RebKillGuardsIncrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("Sent Message to text file");
				string createText = "Increased Rebel Reputation: Killed a Guard";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "RebActivateMedicBeaconIncrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("Sent Message to text file");
				string createText = "Increased Rebel Reputation: Activated a Medic Beacon";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "RebEndOfRoundTriumphIncrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("Sent Message to text file");
				string createText = "Increased Rebel Reputation: Won the round";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "RebEndOfRoundSurrenderDecrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("Sent Message to text file");
				string createText = "Decreased Rebel Reputation: Surrendered";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "RebOnCameraExecutionDecrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("Sent Message to text file");
				string createText = "Decreased Rebel Reputation: Executed a Contestant on Camera";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "RebOnCameraKill":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("Sent Message to text file");
				string createText = "Decreased Rebel Reputation: Killed a Contestant on Camera";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "CorSponsorWeaponFireIncrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("Sent Message to text file");
				string createText = "Increased Corporate Reputation: Used Sponsor Weapon";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "CorSponsorWeaponKillIncrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("Sent Message to text file");
				string createText = "Increased Corporate Reputation: Killed with the Sponsor Weapon";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "CorSponsorItemUseIncrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("Sent Message to text file");
				string createText = "Increased Corporate Reputation: Used Sponsor's Item";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "CorEndOfTournamentDecayDecrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("Sent Message to text file");
				string createText = "Decreased Corporate Reputation: Took a long time to finish round";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "CorSponsorWeaponDeathDecrease":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("Sent Message to text file");
				string createText = "Decreased Corporate Reputation: Died with Sponsor Weapon";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		case "RebWeaponOnCamera":
			//add predefined text to text file
			if (File.Exists(path))
			{
				// Create a file to write to.
				//Debug.Log("Sent Message to text file");
				string createText = "Decreased Rebel Reputation: Used Weapon on Camera";
				File.AppendAllText (path, createText);
			}
			//add new line to text file
			break;
		}
	}
}
