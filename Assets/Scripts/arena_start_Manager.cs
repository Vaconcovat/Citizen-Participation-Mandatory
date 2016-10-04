using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class arena_start_Manager : MonoBehaviour {

	public AutoType at;
	string textTier1;
	string textTier2;
	string textTier3;
	string textTier4;
	string textAbilities;
	string textSponsorship;
	bool done = false;

	void Update(){
		if(done && (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))){
			//----------
			//Analytics
			if(PlayerPrefs.GetInt("Analytics") == 1){
				Analytics.CustomEvent("ArenaStart", new Dictionary<string, object>{
					{"Sponsor", StaticGameStats.sponsor}
				});
			}
			//---------
			FindObjectOfType<SceneChange> ().Arena ();
		}
		if (done && (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))) {
			FindObjectOfType<MenuCamera> ().ArenaMap ();
		}
	}

	void CheckUpgrades() {
		textTier1 += "\n[- - - TIER 1 UPGRADES - - -]";
		
		//T1
		if(StaticGameStats.TierOneUpgrades[0]){
			textTier1 += "\nBackpack Slot Unlocked, Press L-CTRL to Swap Weapons!";
		}
		if(StaticGameStats.TierOneUpgrades[1]){
			textTier1 += "\nAll Non-Sponsored Weapons have +50% Ammo";
		}
		if(StaticGameStats.TierOneUpgrades[2]){
			textTier1 += "\nAll Reputation gain is increased by 5%!";
		}

		textTier2 += "\n[- - - TIER 2 UPGRADES - - -]";
		//T2
		if(StaticGameStats.TierTwoUpgrades[0]){
			textTier2 += "\nAmount of Cameras in Arena Doubled";
		}
		if(StaticGameStats.TierTwoUpgrades[1]){
			textTier2 += "\nThrown Weapons now Cause Knockback";
		}
		if(StaticGameStats.TierTwoUpgrades[2]){
			textTier2 += "\nAll Contestants are now Merciful";
		}

		textTier3 += "\n[- - - TIER 3 UPGRADES - - -]";
		//T3
		if(StaticGameStats.TierThreeUpgrades[0]){
			textTier3 += "\nHealth Kits Now Restore More Health over time";
		}

		textTier4 += "\n[- - - TIER 4 UPGRADES - - -]";
		//T4
		if(StaticGameStats.TierFourUpgrades[0]){
			textTier4 += "\nExecuting an enemy grants bonus Weapon Damage";
			textTier4 += "\nShowing Mercy restores player health";
		}

		textAbilities += "\n[- - - ABILITIES - - -]";

		//Abilities
		if(StaticGameStats.Abilites[0]){
			textAbilities += "\nBIO-SCAN";
		}
		if(StaticGameStats.Abilites[1]){
			textAbilities += "\nBLACKOUT";
		}
		if(StaticGameStats.Abilites[2]){
			textAbilities += "\nVENDOR OVERLOAD";
		}
		if(StaticGameStats.Abilites[3]){
			textAbilities += "\nSHOCK COLLAR";
		}

		textSponsorship += "\n[- - - SPONSORSHIP - - -]";

		//chosen sponsor
		switch(StaticGameStats.sponsor){
			case(0):
				textSponsorship += "\nPrismex Technologies Contract Signed!";
				break;
			case(1):
				textSponsorship += "\nExplodena Industries Contract Signed!";
				break;
			case(2):
				textSponsorship += "\nVelocitech Incorporated Contract Signed!";
				break;
		}

		at.displayedText[1] = textTier1;
		at.displayedText[2] = textTier2;
		at.displayedText[3] = textTier3;
		at.displayedText[4] = textTier4;
		at.displayedText[5] = textAbilities;
		at.displayedText[6] = textSponsorship;
	}
	
	public void arena_start_text(){
		CheckUpgrades();
		at.StartType();
	}

	public void Done(){
		done = true;
	}
}
