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
	public KeyCode Ability5;

	void FixedUpdate()
	{
		if (Input.GetKey (Ability1)) 
		{
			if (skills [0].currentCooldown >= skills [0].cooldown) 
			{
				//Whatever Skill 1 Does
				skills [0].currentCooldown = 0;
			}
		}

		else if (Input.GetKey (Ability2)) 
		{
			if (skills [1].currentCooldown >= skills [1].cooldown) 
			{
				//Whatever Skill 1 Does
				skills [1].currentCooldown = 0;
			}
		}

		else if (Input.GetKey (Ability3)) 
		{
			if (skills [2].currentCooldown >= skills [2].cooldown) 
			{
				//Whatever Skill 1 Does
				skills [2].currentCooldown = 0;
			}
		}

		else if (Input.GetKey (Ability4)) 
		{
			if (skills [3].currentCooldown >= skills [3].cooldown) 
			{
				//Whatever Skill 1 Does
				skills [3].currentCooldown = 0;
			}
		}

		else if (Input.GetKey (Ability5)) 
		{
			if (skills [4].currentCooldown >= skills [4].cooldown) 
			{
				//Whatever Skill 1 Does
				skills [4].currentCooldown = 0;
			}
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
}

[System.Serializable]
public class Skill
{
	public float cooldown;
	public Image skillIcon;
	[HideInInspector]
	public float currentCooldown;
}

