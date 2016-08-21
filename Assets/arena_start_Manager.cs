using UnityEngine;
using System.Collections;

public class arena_start_Manager : MonoBehaviour {

	public AutoType at;
	string text;
	bool done = false;

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

		//Sponsor Upgrades Mega City 1
		if(StaticGameStats.MegaCity1SponsorUpgrade[0]){
			text += "\nMega City 1 Sponsor Upgrade 1 TEXT";
		}
		if(StaticGameStats.MegaCity1SponsorUpgrade[1]){
			text += "\nMega City 1 Sponsor Upgrade 2 TEXT";
		}
		if(StaticGameStats.MegaCity1SponsorUpgrade[2]){
			text += "\nMega City 1 Sponsor Upgrade 3 TEXT";
		}

		//Sponsor Upgrades Explodena
		if(StaticGameStats.ExplodenaSponsorUpgrade[0]){
			text += "\nExplodena Sponsor Upgrade 1 TEXT";
		}
		if(StaticGameStats.ExplodenaSponsorUpgrade[1]){
			text += "\nExplodena Sponsor Upgrade 2 TEXT";
		}
		if(StaticGameStats.ExplodenaSponsorUpgrade[2]){
			text += "\nExplodena Sponsor Upgrade 3 TEXT";
		}

		//Sponsor Upgrades Velocitech
		if(StaticGameStats.VelocitechSponsorUpgrade[0]){
			text += "\nVelocitech Sponsor Upgrade 1 TEXT";
		}
		if(StaticGameStats.VelocitechSponsorUpgrade[1]){
			text += "\nVelocitech Sponsor Upgrade 2 TEXT";
		}
		if(StaticGameStats.VelocitechSponsorUpgrade[2]){
			text += "\nVelocitech Sponsor Upgrade 3 TEXT";
		}

		text += "\n[ - - - - ABILITIES - - - - ]";

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

		text += "\n[ - - - - SPONSORSHIP - - - - ]";

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

		at.displayedText[1] = text;
	}
	
	public void arena_start_text(){
		CheckUpgrades();
		at.StartType();
	}

	public void Done(){
		done = true;
	}
}
