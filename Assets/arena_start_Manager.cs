using UnityEngine;
using System.Collections;

public class arena_start_Manager : MonoBehaviour {

	public AutoType at;
	[TextArea(3,5)]
	public string text;
	bool done = false;

	// Use this for initialization
	void Start(){
		CheckUpgrades();
	}

	void Update(){
		if(done && Input.GetKeyDown(KeyCode.E)){
			FindObjectOfType<SceneChange>().Arena();
		}
	}

	void CheckUpgrades() {
		//T1
		if(StaticGameStats.TierOneUpgrades[0]){
			text += "\nT1U1 TEXT";
		}
		if(StaticGameStats.TierOneUpgrades[1]){
			text += "\nT1U2 TEXT";
		}
		if(StaticGameStats.TierOneUpgrades[2]){
			text += "\nT1U3 TEXT";
		}
		if(StaticGameStats.TierOneUpgrades[3]){
			text += "\nT1U4 TEXT";
		}

		//T2
		if(StaticGameStats.TierTwoUpgrades[0]){
			text += "\nT2U1 TEXT";
		}
		if(StaticGameStats.TierTwoUpgrades[1]){
			text += "\nT2U2 TEXT";
		}
		if(StaticGameStats.TierTwoUpgrades[2]){
			text += "\nT2U3 TEXT";
		}
		if(StaticGameStats.TierTwoUpgrades[3]){
			text += "\nT2U4 TEXT";
		}

		//T3
		if(StaticGameStats.TierThreeUpgrades[0]){
			text += "\nT3U1 TEXT";
		}
		if(StaticGameStats.TierThreeUpgrades[1]){
			text += "\nT3U2 TEXT";
		}
		if(StaticGameStats.TierThreeUpgrades[2]){
			text += "\nT3U3 TEXT";
		}
		if(StaticGameStats.TierThreeUpgrades[3]){
			text += "\nT3U4 TEXT";
		}

		//Abilities
		if(StaticGameStats.Abilites[0]){
			text += "\nAbility1 TEXT";
		}
		if(StaticGameStats.Abilites[1]){
			text += "\nAbility2 TEXT";
		}
		if(StaticGameStats.Abilites[2]){
			text += "\nAbility3 TEXT";
		}
		if(StaticGameStats.Abilites[3]){
			text += "\nAbility4 TEXT";
		}

		//Sponsor Upgrades
		if(StaticGameStats.SponsorUpgrade[0]){
			text += "\nSponsorUpgrade1 TEXT";
		}
		if(StaticGameStats.SponsorUpgrade[1]){
			text += "\nSponsorUpgrade2 TEXT";
		}
		if(StaticGameStats.SponsorUpgrade[2]){
			text += "\nSponsorUpgrade3 TEXT";
		}

		//chosen sponsor
		switch(StaticGameStats.sponsor){
			case(0):
				text += "\nSponsor 1 chosen";
				break;
			case(1):
				text += "\nSponsor 2 chosen";
				break;
			case(2):
				text += "\nSponsor 3 chosen";
				break;
		}

		at.displayedText[0] = text;
	}
	
	public void arena_start_text(){
		CheckUpgrades();
		at.StartType();
	}

	public void Done(){
		done = true;
	}
}
