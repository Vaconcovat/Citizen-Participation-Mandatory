using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour {

	public List<Skill> skills;
	public KeyCode Ability1;
	public KeyCode Ability2;
	public KeyCode Ability3;
	public KeyCode Ability4;
	public Contestant player;
	public GameObject weaponTrackerUI, contestantTrackerUI;
	public bool isPrimed = false;
	public Sprite LockedOut;


	void FixedUpdate()
	{
		if (Input.GetKeyDown (Ability1)) {
			if (!StaticGameStats.Abilites [0]) {
				return;
			} else {
				//If the ability is not currently cooling down
				if ((skills [0].currentCooldown >= skills [0].cooldown) && (skills [4].currentCooldown >= skills [4].cooldown)) {
					if (isPrimed) {
						Stun ();
						print ("Shock Collar Activated, ZZZZZZZAP");
						isPrimed = false;
						skills [0].currentCooldown = 0;

					} else {
						print ("Shock Collar Armed, Use with Caution");
						isPrimed = true;
						skills [4].currentCooldown = 0;
					}

				}
			}
		} else if (Input.GetKeyDown (Ability2)) {
			if (!StaticGameStats.Abilites [1]) {
				return;
			} else {
				//If the ability is not currently cooling down
				if (skills [1].currentCooldown >= skills [1].cooldown) {
					BioScan ();
					skills [1].currentCooldown = 0;
				}
			}
		} else if (Input.GetKeyDown (Ability3)) {
			if (!StaticGameStats.Abilites [2]) {
				return;
			} else {
				//If the ability is not currently cooling down
				if (skills [2].currentCooldown >= skills [2].cooldown) {
					Overload ();
					skills [2].currentCooldown = 0;
				}
			}
		} else if (Input.GetKeyDown (Ability4)) {
			if (!StaticGameStats.Abilites [3]) {
				return;
			} else {
				//If the ability is not currently cooling down
				if (skills [3].currentCooldown >= skills [3].cooldown) {
					Blackout ();
					skills [3].currentCooldown = 0;
				}
			}
		}
			
	}

	void Start()
	{
		skills [0].currentCooldown = skills [0].cooldown;
		skills [1].currentCooldown = skills [1].cooldown;
		skills [2].currentCooldown = skills [2].cooldown;
		skills [3].currentCooldown = skills [3].cooldown;
		skills [4].currentCooldown = skills [4].cooldown;

		if (!StaticGameStats.Abilites [0]) {
			skills [0].skillIcon.sprite = LockedOut;
		}
		if (!StaticGameStats.Abilites [1]) {
			skills [1].skillIcon.sprite = LockedOut;
		}
		if (!StaticGameStats.Abilites [2]) {
			skills [2].skillIcon.sprite = LockedOut;
		}
		if (!StaticGameStats.Abilites [3]) {
			skills [3].skillIcon.sprite = LockedOut;
		}
	}

	void Update()
	{
		foreach (Skill s in skills) 
		{
			if (s.currentCooldown < s.cooldown) 
			{
				s.currentCooldown += Time.deltaTime;
				s.skillIcon.fillAmount = s.currentCooldown / s.cooldown;
			}

		}
	}

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

		//Display info about all weapons
//		Item[] items = FindObjectsOfType<Item>();
//		foreach (Item i in items){
//			if(i.type != Item.ItemType.Ranged){
//				continue;
//			}
//			if(i.equipper != null){
//				continue;
//			}
//			GameObject spawned = (GameObject)Instantiate(weaponTrackerUI);
//			spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
//			UI_WeaponTracker tracker = spawned.GetComponent<UI_WeaponTracker>();
//			tracker.item = i;
//		}
	}

	public void Overload(){
		float closestDist = Mathf.Infinity;
		ItemSpawner closestSpawner = null;
		Transform player = FindObjectOfType<PlayerController>().transform;
		foreach(ItemSpawner i in FindObjectsOfType<ItemSpawner>()){
			float dist = Vector3.Distance(player.position, i.transform.position);
			if(dist < closestDist){
				closestDist = dist;
				closestSpawner = i;
			}
		}
		closestSpawner.timer = 0f;
	}

	public void Stun()
	{
		Contestant[] contestants = FindObjectsOfType<Contestant>();

		foreach (Contestant c in contestants) {
			if (c.type == Contestant.ContestantType.AI) {
				
				float distance = Vector3.Distance (PlayerController.pos, c.transform.position);
				if (distance < StaticGameStats.Ability1MaxDistance) {
					c.ThrowEquipped ();
				}
			}
		}

		AIController[] aicontroller = FindObjectsOfType<AIController>();
		foreach (AIController a in aicontroller)
		{
			float distance = Vector3.Distance (PlayerController.pos, a.transform.position);
			if (distance < StaticGameStats.Ability1MaxDistance) {
				a.StartShocked ();
			}
		}
	}

	public void Blackout()
	{
		AIController[] aicontroller = FindObjectsOfType<AIController>();
		foreach (AIController a in aicontroller)
		{
			a.StartBlinded ();
		}
	}
}

[System.Serializable]
public class Skill
{
	public float cooldown;
	public Image skillIcon;
	public string AbilityName;
	[HideInInspector]
	public float currentCooldown;
}


