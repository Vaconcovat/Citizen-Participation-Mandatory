using UnityEngine;
using System.Collections;

public class arena_start_Manager : MonoBehaviour {

	public AutoType at;
	string text;
	bool done = false;

	void Update(){
		if(done && Input.GetKeyDown(KeyCode.E)){
			FindObjectOfType<SceneChange> ().Arena ();
		}
		if (done && Input.GetKeyDown (KeyCode.Q)) {
			FindObjectOfType<MenuCamera> ().ArenaMap ();
		}
	}

	void CheckUpgrades() {
		text += "\n[ - - - - UPGRADES - - - - ]";
		
		//T1
		if(StaticGameStats.TierOneUpgrades[0]){
			text += "\nBackpack Slot Unlocked, Press L-CTRL to Swap Weapons!";
		}
		if(StaticGameStats.TierOneUpgrades[1]){
			text += "\nAll Non-Sponsored Weapons have +50% Ammo";
		}
		if(StaticGameStats.TierOneUpgrades[2]){
			text += "\nAll Reputation gain is increased by 5%!";
		}

		//T2
		if(StaticGameStats.TierTwoUpgrades[0]){
			text += "\nAmount of Cameras in Arena Doubled";
		}
		if(StaticGameStats.TierTwoUpgrades[1]){
			text += "\nThrown Weapons now Cause Knockback";
		}
		if(StaticGameStats.TierTwoUpgrades[2]){
			text += "\nAll Contestants are now Merciful";
		}

		//T3
		if(StaticGameStats.TierThreeUpgrades[0]){
			text += "\nHealth Kits Now Restore More Health over time";
		}

		//T4
		if(StaticGameStats.TierFourUpgrades[0]){
			text += "\nExecuting an enemy grants bonus Weapon Damage";
			text += "\nShowing Mercy restores player health";
		}

		text += "\n[ - - - - ABILITIES - - - - ]";

		//Abilities
		if(StaticGameStats.Abilites[0]){
			text += "\nBIO-SCAN";
		}
		if(StaticGameStats.Abilites[1]){
			text += "\nBLACKOUT";
		}
		if(StaticGameStats.Abilites[2]){
			text += "\nVENDOR OVERLOAD";
		}
		if(StaticGameStats.Abilites[3]){
			text += "\nSHOCK COLLAR";
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
