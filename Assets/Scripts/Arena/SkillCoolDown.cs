using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour {

	public List<Skill> skills;
	public KeyCode Ability0;
	public KeyCode Ability1;
	public KeyCode Ability2;
	public KeyCode Ability3;
	public Contestant player;
	public GameObject weaponTrackerUI, contestantTrackerUI;
	public bool isPrimed = false;
	public Sprite LockedOut;
	public float shockActiveTime;
	public float blindActiveTime;
	public float bioscanActiveTime;
	public float ShockCollarPrimerCooldownTime;

	void Start()
	{
		//If the player does not own an ability
		//set that abilitis logo to a locked out symbol
		for (int i = 0; i < 4; i++) {
			if (!StaticGameStats.instance.Abilites [i]) {
				skills [i].skillIcon.sprite = LockedOut;
			}
		}

		//reset each skills cooldown
		//set each skill to be useable
		foreach (Skill s in skills) {
			s.currentCooldown = s.MaxCooldown;
			s.isUseable = true;
		}
	}

	void Update()
	{

		//COOLDOWN MANAGEMENT

		foreach (Skill s in skills) //For each skill
		{
			if (s.currentCooldown < s.MaxCooldown) 	//if the skill's current cooldown is less than its max cooldown
				//When activated skill's currentcooldown values get set to 0
				//a skill with a current cooldown equal to its maxCooldown is a fully cooled down ability that is ready for use
			{
				s.currentCooldown += Time.deltaTime; //increase current cooldown by a factor of time
				s.skillIcon.fillAmount = s.currentCooldown / s.MaxCooldown; //update the skillicon fillamount to reflect this change
			}

			if (s.currentCooldown > s.MaxCooldown) { //if the current cooldown somehow gets above max cooldown through addition of deltaTime weirdness
				s.currentCooldown = s.MaxCooldown;
			}
		}

		//ABILITY ACTIVATION

		//ABILITY 0 - BIOSCAN
		if (Input.GetKeyDown (Ability0)) { //Press the BioScan Button
			if (StaticGameStats.instance.Abilites [0]) { //Does the Player Own the BioScan Ability
				if (skills [0].isUseable == true) { //Is The Ability Fully Cooled Down
					if (skills [0].currentCooldown == skills [0].MaxCooldown) { //Is The Ability Useable
						StartCoroutine ("BioScanWait"); //Waits the active time of the Ability
					} else {
						return;
					}
				} else {
					return;
				}
			} else {
				return;	
			}
		}

		//ABILITY 1 - BLACKOUT
		if (Input.GetKeyDown (Ability1)) { //Press the BioScan Button
			if (StaticGameStats.instance.Abilites [1]) { //Does the Player Own the BioScan Ability
				if (skills [1].isUseable == true) { //Is The Ability Fully Cooled Down
					if (skills [1].currentCooldown == skills [1].MaxCooldown) { //Is The Ability Useable
						StartCoroutine ("BlackoutWait"); 
					} else {
						return;
					}
				} else {
					return;
				}
			} else {
				return;	
			}
		}
			
		//ABILITY 2 - OVERLOAD
		if (Input.GetKeyDown (Ability2)) { //Press the BioScan Button
			if (StaticGameStats.instance.Abilites [2]) { //Does the Player Own the BioScan Ability
				if (skills [2].isUseable == true) { //Is The Ability Fully Cooled Down
					if (skills [2].currentCooldown == skills [2].MaxCooldown) { //Is The Ability Useable
						StartCoroutine ("OverloadWait");
					} else {
						return;
					}
				} else {
					return;
				}
			} else {
				return;	
			}
		}




		//ABILITY 4 - SHOCK COLLAR
		if (Input.GetKeyDown (Ability3)) { //Press the BioScan Button
			if (StaticGameStats.instance.Abilites [3]) { //Does the Player Own the BioScan Ability
				if (skills [3].isUseable == true) { //Is The Ability Fully Cooled Down
					if (skills [3].currentCooldown == skills [3].MaxCooldown) { //Is The Ability Useable
						if (isPrimed) {
							StartCoroutine ("ShockCollarWait");
						} else {
							StartCoroutine ("ShockCollarPrimerWait");
						}
					} else {
						return;
					}
				} else {
					return;
				}
			} else {
				return;	
			}
		}


	}
		
	//ABILITIES

	//ABILITY 1

	public void BioScan()
	{
		Contestant[] contestants = FindObjectsOfType<Contestant>();
		//Display Information About Contestants
		foreach (Contestant c in contestants){
			if(c.type == Contestant.ContestantType.AI){
				GameObject spawned = (GameObject)Instantiate(contestantTrackerUI);
				spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
				UI_ContestantTracker tracker = spawned.GetComponent<UI_ContestantTracker>();
				tracker.contest = c;
			}
		}
	}

	//ABILITY 2

	public void Blackout()
	{
		AIController[] aicontroller = FindObjectsOfType<AIController>();
		foreach (AIController a in aicontroller)
		{
			a.StartBlinded ();
		}
		Arena_Camera[] cameras = FindObjectsOfType<Arena_Camera>();
		foreach(Arena_Camera a in cameras){
			a.active = false;
		}
	}

	//ABILITY 3

	public void Overload(){
		float closestDist = Mathf.Infinity;
		ItemSpawner closestSpawner = null;
		Transform player = FindObjectOfType<PlayerController>().transform;
		foreach(ItemSpawner i in FindObjectsOfType<ItemSpawner>()){
			if (i.selection == ItemSpawner.poolselection.Example) { //checks if the spawner is an itemSpawner vs a Weapon Spawner
				float dist = Vector3.Distance(player.position, i.transform.position);
				if(dist < closestDist){
					closestDist = dist;
					closestSpawner = i;
				}
			}
		}
		closestSpawner.timer = 0f;
		closestSpawner.SpawnerCooldown = closestSpawner.SpawnerCooldown + 2;
	}

	//ABILITY 4

	public void Stun()
	{
		Contestant[] contestants = FindObjectsOfType<Contestant>();
		foreach (Contestant c in contestants) {
			if (c.type == Contestant.ContestantType.AI) {
				
				float distance = Vector3.Distance (FindObjectOfType<PlayerController>().pos, c.transform.position);
				if (distance < StaticGameStats.instance.Ability1MaxDistance) {
					c.ThrowEquipped ();
				}
			}
		}

		AIController[] aicontroller = FindObjectsOfType<AIController>();
		foreach (AIController a in aicontroller)
		{
			float distance = Vector3.Distance (FindObjectOfType<PlayerController>().pos, a.transform.position);
			if (distance < StaticGameStats.instance.Ability1MaxDistance) {
				a.StartShocked ();
			}
		}
	}



	IEnumerator BioScanWait(){
		skills [0].isUseable = false;
		BioScan (); 
		yield return new WaitForSeconds (bioscanActiveTime);
		skills [0].currentCooldown = 0;
		yield return new WaitForSeconds (skills [0].MaxCooldown);
		skills [0].isUseable = true;
	}
		
	IEnumerator BlackoutWait(){
		skills [1].isUseable = false;
		Blackout (); 
		yield return new WaitForSeconds (blindActiveTime);
		Arena_Camera[] cameras = FindObjectsOfType<Arena_Camera>();
		foreach(Arena_Camera a in cameras){
			a.active = true;
		}
		skills [1].currentCooldown = 0;
		yield return new WaitForSeconds (skills [1].MaxCooldown);
		skills [1].isUseable = true;
	}

	IEnumerator OverloadWait(){
		skills [2].isUseable = false;
		Overload (); //Activates the Ability
		skills [2].currentCooldown = 0;
		yield return new WaitForSeconds (skills [2].MaxCooldown);
		skills [2].isUseable = true;	
	}

	IEnumerator ShockCollarWait(){
		isPrimed = false;
		skills [3].isUseable = false;
		Stun ();
		yield return new WaitForSeconds (shockActiveTime);
		skills [3].currentCooldown = 0;
		yield return new WaitForSeconds (skills [3].MaxCooldown);
		skills [3].isUseable = true;
	}

	IEnumerator ShockCollarPrimerWait(){
		isPrimed = true;
		skills [3].isUseable = false;
		skills [3].currentCooldown = skills [3].MaxCooldown - ShockCollarPrimerCooldownTime;
		yield return new WaitForSeconds (ShockCollarPrimerCooldownTime);
		skills [3].isUseable = true;
	}


}

[System.Serializable]
public class Skill
{
	public float currentCooldown;
	public float MaxCooldown;
	public Image skillIcon;
	public string AbilityName;
	public bool isUseable;
}


