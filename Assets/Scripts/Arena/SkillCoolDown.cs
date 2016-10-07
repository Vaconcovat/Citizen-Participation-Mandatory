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

			if (s.currentCooldown == s.MaxCooldown) { //if any skill is ever completely cooled off
				s.isUseable = true; //Set the ability to be usable again
			}

		}

		//ABILITY ACTIVATION

		//ABILITY 0 - BIOSCAN
		if (Input.GetKeyDown (Ability0)) {
			if (StaticGameStats.instance.Abilites [0]) {
				//If the ability is not currently cooling down
				if ((skills [0].currentCooldown >= skills [0].MaxCooldown) && (skills[0].isUseable == true)) {
					BioScan ();
					skills [0].isUseable = false;
					StartCoroutine("BioScanWait");
				}
			} else {
				return;	
			}
		}

		//ABILITY 1 - BLACKOUT
		if (Input.GetKeyDown (Ability1)) {
			if (StaticGameStats.instance.Abilites [1]) {
				if ((skills [1].currentCooldown >= skills [1].MaxCooldown) && (skills[1].isUseable == true)) {
					Blackout ();
					skills [1].isUseable = false;
					StartCoroutine("BlackoutWait");
				}
			} else {
				return;
			}
		}

		//ABILITY 2 - OVERLOAD
		if (Input.GetKeyDown (Ability2)) {
			if (StaticGameStats.instance.Abilites [2]) {
				if (skills [2].currentCooldown >= skills [2].MaxCooldown) {
					Overload ();
					skills [2].isUseable = false;
					skills [2].currentCooldown = 0;
				}
			} else {
				return;
			}
		}

		//ABILITY 4 - SHOCK COLLAR
		if (Input.GetKeyDown (Ability3)) {
			if (StaticGameStats.instance.Abilites [3]) {
				if ((skills [3].currentCooldown >= skills [3].MaxCooldown) && (skills[3].isUseable == true)) {
					if (isPrimed) {
						Stun ();
						skills [3].isUseable = false;
						isPrimed = false;
						StartCoroutine("ShockCollarWait");
					} else {
						isPrimed = true;
						skills [3].currentCooldown = 0;
					}

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
		yield return new WaitForSeconds (bioscanActiveTime);
		skills [1].currentCooldown = 0;
	}

	IEnumerator ShockCollarWait(){
		yield return new WaitForSeconds (shockActiveTime);
		skills [0].currentCooldown = 0;
	}

	IEnumerator BlackoutWait(){
		yield return new WaitForSeconds (blindActiveTime);
		skills [3].currentCooldown = 0;
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


