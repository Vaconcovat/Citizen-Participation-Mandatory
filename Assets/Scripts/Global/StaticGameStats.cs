using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class StaticGameStats : MonoBehaviour {
	//Reputation variables
	public static float govRep = 50.0f;
	public static float corRep = 50.0f;
	public static float rebRep = 50.0f;
	public static float oldgovRep = 50.0f;
	public static float oldcorRep = 50.0f;
	public static float oldrebRep = 50.0f;

	//money variables

	//sponsor variables

	//Commit

	//Sponsor Items

	//Arena variables
	/// <summary>
	/// [General Upgrade 0], [nothing]
	/// </summary>

	//Global Value Editing















	//Rep Gain Triggers
	//1.0f = 1% Rep Gain

	public enum InfluenceTrigger{Execution, OnCameraKill, EndOfRoundSurrender, KillGuard, ActivateMedicBeacon, SuccessfulExtraction, EndOfRoundTriumph, SponsorWeaponFire, SponsorWeaponKill, SponsorItemUse, SponsorWeaponDeath, EndOfTournamentDecay};

	//Government Rep Increase

	//Government Rep Decrease

	//Rebel Rep Decrease

	//Corporate Rep Increase

	//Corporate Rep Decrease




	// Use this for initialization
	//Probably shouldn't ever have anything here, if you do you're bad.
	void Awake () {

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
			if (govRep > 100.0f) {
				govRep = 100.0f;
			}
			if (govRep < 0.0f) {
				govRep = 0.0f;
			}
			break;
		case 1:
			if (corRep > 100.0f) {
				corRep = 100.0f;
			}
			if (corRep < 0.0f) {
				corRep = 0.0f;
			}
			break;
		case 2:
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
