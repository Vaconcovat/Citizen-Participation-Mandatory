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
	Ray ray;
	RaycastHit hit;
	public bool isReady = false;

	void FixedUpdate()
	{
		if (Input.GetKeyDown (Ability1)) 
		{
			//If the ability is not currently cooling down
			if (skills [0].currentCooldown >= skills [0].cooldown)
			{
				Stun ();
			}
		}

		else if (Input.GetKeyDown (Ability2)) 
		{
			//If the ability is not currently cooling down
			if (skills [1].currentCooldown >= skills [1].cooldown) 
			{
				BioScan();
				skills [1].currentCooldown = 0;
			}
		}

		else if (Input.GetKeyDown (Ability3)) 
		{
			//If the ability is not currently cooling down
			if (skills [2].currentCooldown >= skills [2].cooldown) 
			{
				
				skills [2].currentCooldown = 0;
			}
		}

		else if (Input.GetKeyDown (Ability4)) 
		{
			//If the ability is not currently cooling down
			if (skills [3].currentCooldown >= skills [3].cooldown) 
			{
				//Whatever Skill 1 Does
				skills [3].currentCooldown = 0;
			}
		}
			
	}

	void Start()
	{
		skills [0].currentCooldown = skills [0].cooldown;
		skills [1].currentCooldown = skills [1].cooldown;
		skills [2].currentCooldown = skills [2].cooldown;
		skills [3].currentCooldown = skills [3].cooldown;
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
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	}

	public void BioScan()
	{
		//Display Information About Contestants
		Contestant[] contestants = FindObjectsOfType<Contestant>();
		foreach (Contestant c in contestants){
			if(c.type == Contestant.ContestantType.AI){
				GameObject spawned = (GameObject)Instantiate(contestantTrackerUI);
				spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
				UI_ContestantTracker tracker = spawned.GetComponent<UI_ContestantTracker>();
				tracker.contest = c;
			}
		}

		//Display info about all weapons
		Item[] items = FindObjectsOfType<Item>();
		foreach (Item i in items){
			if(i.type != Item.ItemType.Ranged){
				continue;
			}
			GameObject spawned = (GameObject)Instantiate(weaponTrackerUI);
			spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
			UI_WeaponTracker tracker = spawned.GetComponent<UI_WeaponTracker>();
			tracker.item = i;
		}
	}

	public void Stun()
	{
		if (isReady == true) {
			if (Physics.Raycast (ray, out hit)) 
			{
				if (hit.collider.tag == "Contestant") 
				{
					//Only Performs this code if the ray hits a contestant
					print (hit.collider.name);
				}
			}
			print ("Shock Collar Activated, ZZZZZZZAP");
			skills [0].currentCooldown = 0;
			isReady = false;
		} 
		else {
			print ("Shock Collar Armed, Use with Caution");
			skills [0].currentCooldown = 0;
			isReady = true;
		}

		
	}
}

[System.Serializable]
public class Skill
{
	public float cooldown;
	public Image skillIcon;
	[HideInInspector]
	public float currentCooldown;
}


