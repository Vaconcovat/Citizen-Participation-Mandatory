using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class StaticGameStats : MonoBehaviour {

	public static StaticGameStats instance;


	public float govRep = 50.0f;
	public float corRep = 50.0f;
	public float rebRep = 50.0f;
	public float oldgovRep = 50.0f;
	public float oldcorRep = 50.0f;
	public float oldrebRep = 50.0f;

	//money variables
	public int avaliableMoney;
	public int moneyHolder;

	//sponsor variables
	public int chosenSponsor = -1;
	public int activeSponsor;

	//Commit
	public bool committed = true;
	public bool toPost = false;
	public bool tutorialDone = false;
	public bool QuestionnaireDone = false;
	public bool FirstRun = true;
	public  int NumTimesClicked = 0;

	//Sponsor Items
	public float VelocitechItemDuration = 5.0f;
	public float ExplodenaItemDuration = 5.0f;
	public float PrismexItemDuration = 5.0f;

	//Arena variables
	/// <summary>
	/// [General Upgrade 0], [nothing]
	/// </summary>
	public bool[] TierOneUpgrades = new bool[]{false, true, false};
	public bool[] TierTwoUpgrades = new bool[]{false, false, false};
	public bool[] TierThreeUpgrades = new bool[]{false};
	public bool[] TierFourUpgrades = new bool[]{false};
	public bool[] Abilites = new bool[]{true, false, false, false};
	public int sponsor;
	public int arenasPlayed = 0;

	//Global Value Editing

	public int FirstAidHereHealDuration = 11;
	public float FirstAidHereHealAmount = 4.0f;

	public int KarmaGetSomeHealDuration = 5;
	public float KarmaGetSomeHealAmount = 5.0f;

	public int KarmaGetSomeDamageBuffDuration = 10;
	public float NormalDamageBuff = 1.0f;
	public float KaramGetSomeDamageBuff = 1.10f;

	public int Upgrade1ItemUsageBuff = 2; //Ability 1 Doubles the number of item uses
	public float Upgrade3ReputationGainBuff = 1.05f; //Ability 3 increases all rep gain by 5%
	public float Upgrade4MaxAmmoBuff = 1.2f; //Ability 4 increases the ammo of all weapons by 20%

	public float Upgrade5HealAmount = 10.0f;

	public float Upgrade6FireRateNerf = 0.8f;
	public int Upgrade6DamageBuff = 2; 

	public bool Upgrade7AlreadyTriggered = false;
	public int Upgrade7HealAmount = 20;

	public int Upgrade8NormalSpeed = 10;
	public int Upgrade8MovementSpeedBuff = 15;

	public int Upgrade9NormalSpeed = 10;
	public int Upgrade9MovementSpeedBuff = 15;
	public int Upgrade9HealAmount = 20;



	public int Upgrade11ThrownBuff = 4;
	public bool Upgrade11AlreadyTriggered = false;

	public float Upgrade12DurationBuff = 1.5f;

	public float Ability1MaxDistance = 10.0f;

	//Rep Gain Triggers
	//1.0f = 1% Rep Gain

	public enum InfluenceTrigger{Execution, OnCameraKill, EndOfRoundSurrender, KillGuard, ActivateMedicBeacon, SuccessfulExtraction, EndOfRoundTriumph, SponsorWeaponFire, SponsorWeaponKill, SponsorItemUse, SponsorWeaponDeath, EndOfTournamentDecay};
	public List<InfluenceTrigger> influenceList;

	//Government Rep Increase
	public float GovExecutionIncrease = 2.0f;
	public float GovOnCameraKillIncrease = 1.5f;
	public float GovEndOfRoundSurrenderIncrease = 0.50f;

	//Government Rep Decrease
	public float GovKillGuardsDecrease = -1.0f;
	public float GovActivateMedicBeaconDecrease = -0.75f;
		//Rebel Rep Increase
	public float RebSuccessfulExtractionIncrease = 2.5f;
	public float RebKillGuardsIncrease = 1.0f;
	public float RebActivateMedicBeaconIncrease = 1.5f;
	public float RebEndOfRoundTriumphIncrease = 5.5f;

	//Rebel Rep Decrease
	public float RebEndOfRoundSurrenderDecrease = -0.50f;
	public float RebOnCameraExecutionDecrease = -1.5f;
	public float RebOnCameraKill = -1.25f;

	//Corporate Rep Increase
	public float CorSponsorWeaponFireIncrease = 0.05f;
	public float CorSponsorWeaponKillIncrease = 2.5f;
	public float CorSponsorItemUseIncrease = 1.0f;

	//Corporate Rep Decrease
	public float CorEndOfTournamentDecayDecrease = -1.0f;
	public float CorSponsorWeaponDeathDecrease = -1.0f;

	public string PlayerName;

	public string RebelName;



	void Awake () {

		//SINGLETON DEFINITION
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(instance != this){
			Destroy(gameObject);
		}

		influenceList = new List<InfluenceTrigger>();
	}


	public void Influence(InfluenceTrigger type, float amount){
			influenceList.Add (type);
			FindObjectOfType<InterfaceManager> ().Influence (type);
			switch (type) {
			case InfluenceTrigger.ActivateMedicBeacon:
				UpdateInfluence (0, (amount != 0) ? amount : GovActivateMedicBeaconDecrease);
				UpdateInfluence (2, (amount != 0) ? amount : RebActivateMedicBeaconIncrease);
				break;
			case InfluenceTrigger.EndOfRoundSurrender:
				UpdateInfluence (0, (amount != 0) ? amount : GovEndOfRoundSurrenderIncrease);
				UpdateInfluence (2, (amount != 0) ? amount : RebEndOfRoundSurrenderDecrease);
				break;
			case InfluenceTrigger.EndOfRoundTriumph:
				UpdateInfluence (2, (amount != 0) ? amount : RebEndOfRoundTriumphIncrease);
				break;
			case InfluenceTrigger.Execution:
				UpdateInfluence (0, (amount != 0) ? amount : GovExecutionIncrease);
				break;
			case InfluenceTrigger.KillGuard:
				UpdateInfluence (2, (amount != 0) ? amount : RebKillGuardsIncrease);
				break;
			case InfluenceTrigger.OnCameraKill:
				UpdateInfluence (0, (amount != 0) ? amount : GovOnCameraKillIncrease);
				UpdateInfluence (2, (amount != 0) ? amount : RebOnCameraKill);
				break;
			case InfluenceTrigger.SponsorItemUse:
				UpdateInfluence (1, (amount != 0) ? amount : CorSponsorItemUseIncrease);
				break;
			case InfluenceTrigger.SponsorWeaponDeath:
				UpdateInfluence (1, (amount != 0) ? amount : CorSponsorWeaponDeathDecrease);
				break;
			case InfluenceTrigger.SponsorWeaponFire:
				UpdateInfluence (1, (amount != 0) ? amount : CorSponsorWeaponFireIncrease);
				break;
			case InfluenceTrigger.SponsorWeaponKill:
				UpdateInfluence (1, (amount != 0) ? amount : CorSponsorWeaponKillIncrease);
				break;
			case InfluenceTrigger.SuccessfulExtraction:
				UpdateInfluence (2, (amount != 0) ? amount : RebSuccessfulExtractionIncrease);
				break;
			case InfluenceTrigger.EndOfTournamentDecay:
				UpdateInfluence (1, (amount != 0) ? amount : CorEndOfTournamentDecayDecrease);
				break;
			}
	}

	public void UpdateInfluence(int faction, float amount){
		switch (faction){
		case 0:
			govRep += (amount * StaticGameStats.instance.Upgrade3ReputationGainBuff);
			if (govRep > 100.0f) {
				govRep = 100.0f;
			}
			if (govRep < 0.0f) {
				govRep = 0.0f;
			}
			break;
		case 1:
			corRep += (amount * StaticGameStats.instance.Upgrade3ReputationGainBuff);
			if (corRep > 100.0f) {
				corRep = 100.0f;
			}
			if (corRep < 0.0f) {
				corRep = 0.0f;
			}
			break;
		case 2:
			rebRep += (amount * StaticGameStats.instance.Upgrade3ReputationGainBuff);
			if (rebRep > 100.0f) {
				rebRep = 100.0f;
			}
			if (rebRep < 0.0f) {
				rebRep = 0.0f;
			}
			break;
		}
	}

	public void Save(){
		Debug.LogAssertion("SAVED!");
		//Set up the binary formatter and filestream
		BinaryFormatter binary = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/SaveData.gov");

		//set up the savedata
		SaveData data = new SaveData();
		data.govRep = govRep;
		data.corRep = corRep;
		data.rebRep = rebRep;

		data.avaliableMoney = avaliableMoney;
		data.moneyHolder = moneyHolder;

		data.chosenSponsor = chosenSponsor;
		data.activeSponsor = activeSponsor;

		data.committed = committed;
		data.toPost = toPost ;
		data.tutorialDone = tutorialDone;
		data.QuestionnaireDone = QuestionnaireDone;
		data.FirstRun = FirstRun;
		data.NumTimesClicked = NumTimesClicked;

		data.TierOneUpgrades0 = TierOneUpgrades[0];
		data.TierOneUpgrades1 = TierOneUpgrades[1];
		data.TierOneUpgrades2 = TierOneUpgrades[2];
		data.TierTwoUpgrades0 = TierTwoUpgrades[0];
		data.TierTwoUpgrades1 = TierTwoUpgrades[1];
		data.TierTwoUpgrades2 = TierTwoUpgrades[2];
		data.TierThreeUpgrade = TierThreeUpgrades[0];
		data.TierFourUpgrade = TierFourUpgrades[0];
		data.Abilites0 = Abilites[0];
		data.Abilites1 = Abilites[1];
		data.Abilites2 = Abilites[2];
		data.Abilites3 = Abilites[3];
		data.sponsor = sponsor;
		data.arenasPlayed = arenasPlayed;

		data.PlayerName = PlayerName;

		//Serialize the savedata to the file
		binary.Serialize(file, data);
		file.Close();
	}

	public void Load(){
		if(File.Exists(Application.persistentDataPath + "/SaveData.gov")){
			Debug.LogAssertion("LOADED!");
			//Set up the binary formatter and open the file
			BinaryFormatter binary = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/SaveData.gov", FileMode.Open);

			//deserialize the save file
			SaveData data = (SaveData)binary.Deserialize(file);
			file.Close();

			//load the variables
			govRep = data.govRep;
			corRep = data.corRep;
			rebRep = data.rebRep;
				
			avaliableMoney = data.avaliableMoney;
			moneyHolder = data.moneyHolder;
	
			chosenSponsor = data.chosenSponsor;
			activeSponsor = data.activeSponsor;
	
			committed = data.committed;
			toPost = data.toPost ;
			tutorialDone = data.tutorialDone;
			QuestionnaireDone = data.QuestionnaireDone;
			FirstRun = data.FirstRun;
			NumTimesClicked = data.NumTimesClicked;
	
			TierOneUpgrades[0] = data.TierOneUpgrades0;
			TierOneUpgrades[1] = data.TierOneUpgrades1;
			TierOneUpgrades[2] = data.TierOneUpgrades2;
			TierTwoUpgrades[0] = data.TierTwoUpgrades0;
			TierTwoUpgrades[1] = data.TierTwoUpgrades1;
			TierTwoUpgrades[2] = data.TierTwoUpgrades2;
			TierThreeUpgrades[0] = data.TierThreeUpgrade;
			TierFourUpgrades[0] = data.TierFourUpgrade;
			Abilites[0] = data.Abilites0;
			Abilites[1] = data.Abilites1;
			Abilites[2] = data.Abilites2;
			Abilites[3] = data.Abilites3;
			sponsor = data.sponsor;
			arenasPlayed = data.arenasPlayed;
	
			PlayerName = data.PlayerName;
		}
		else{
			Debug.LogAssertion("LOAD FAILED: NO FILE");
		}

	}

	public bool DeleteSave(){
		if(File.Exists(Application.persistentDataPath + "/SaveData.gov")){
			File.Delete(Application.persistentDataPath + "/SaveData.gov");
			SceneManager.LoadScene(3);
			return true;
		}
		else{
			Debug.Log("No file to delete");
			return false;
		}
	}
}

[Serializable]
class SaveData{
	public float govRep;
	public float corRep;
	public float rebRep;

	public int avaliableMoney;
	public int moneyHolder;

	public int chosenSponsor;
	public int activeSponsor;

	public bool committed;
	public bool toPost ;
	public bool tutorialDone;
	public bool QuestionnaireDone;
	public bool FirstRun;
	public int NumTimesClicked;

	public bool TierOneUpgrades0, TierOneUpgrades1, TierOneUpgrades2;
	public bool TierTwoUpgrades0, TierTwoUpgrades1, TierTwoUpgrades2;
	public bool TierThreeUpgrade;
	public bool TierFourUpgrade;
	public bool Abilites0, Abilites1, Abilites2, Abilites3;
	public int sponsor;
	public int arenasPlayed;

	public string PlayerName;
}
