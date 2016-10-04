using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

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



	// Use this for initialization
	//Probably shouldn't ever have anything here, if you do you're bad.
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

}
