using UnityEngine;
using System.Collections;

public class arena_start_Manager : MonoBehaviour {

	public AutoType at;
	string text;
	bool done = false;

	void Update(){
		if(done && Input.GetKeyDown(KeyCode.E)){
			FindObjectOfType<SceneChange>().Tutorial();
		}
	}

	void CheckUpgrades() {
		//T1
		if(StaticGameStats.TierOneUpgrades[0]){
			text += "\nItems have +1 Available Charges!";
		}
		if(StaticGameStats.TierOneUpgrades[1]){
			text += "\nThrown weapons deal double damage!";
		}
		if(StaticGameStats.TierOneUpgrades[2]){
			text += "\nAll Reputation gain is increased by 5%!";
		}
		if(StaticGameStats.TierOneUpgrades[3]){
			text += "\nALL weapons have +20% ammo!";
		}

		//T2
		if(StaticGameStats.TierTwoUpgrades[0]){
			text += "\nKills on Camera restore 10% Helath";
		}
		if(StaticGameStats.TierTwoUpgrades[1]){
			text += "\nFire rate -20%, Bullet Damage +20%!";
		}
		if(StaticGameStats.TierTwoUpgrades[2]){
			text += "\nGain +20% Max Health back upon emptying a weapon!";
		}
		if(StaticGameStats.TierTwoUpgrades[3]){
			text += "\nGain +50% movement speed while holding an empty weapon!";
		}

		//T3
		if(StaticGameStats.TierThreeUpgrades[0]){
			text += "\nBloodlust Enabled!";
		}
		if(StaticGameStats.TierThreeUpgrades[1]){
			text += "\nHealth kits are different!";
		}
		if(StaticGameStats.TierThreeUpgrades[2]){
			text += "\nWeapons are thrown automatically and deal 1.2x damage when thrown!";
		}
		if(StaticGameStats.TierThreeUpgrades[3]){
			text += "\nSponsor Item Duration Increased by 50%";
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
			text += "\nSHOCK COLLAR";
		}
		if(StaticGameStats.Abilites[1]){
			text += "\nBIO SCAN";
		}
		if(StaticGameStats.Abilites[2]){
			text += "\nVENDOR OVERLOAD";
		}
		if(StaticGameStats.Abilites[3]){
			text += "\nBLACKOUT";
		}

		text += "\n[ - - - - SPONSORSHIP - - - - ]";

		//chosen sponsor
		switch(StaticGameStats.sponsor){
			case(0):
				text += "\nPrismex Technologies Contract Signed!";
				break;
			case(1):
				text += "\nExplodena Industries Contract Signed!";
				break;
			case(2):
				text += "\nVelocitech Incorporated Contract Signed!";
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
