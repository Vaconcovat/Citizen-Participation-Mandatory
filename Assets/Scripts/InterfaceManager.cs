using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Script for accessing and updating all interface components (in the arena)
/// </summary>
public class InterfaceManager : MonoBehaviour {

	public Text roundText;
	public Text aliveText;
	public Image healthBar;
	public PlayerAttributes player;
	public Text ammoText;
	public Text roundTime;
	public Image gunLogoImage;

	RoundManager rm;
	GameObject playerWeapon;

	// Use this for initialization
	void Start () {
		rm = FindObjectOfType<RoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
		//update the alive counter
		aliveText.text = "CONTESTANTS ALIVE: " + rm.aliveContestants.ToString() + "/" + rm.totalContestants.ToString();
		//fill the hp bar
		healthBar.fillAmount = (player.CurrentHealth * 1.0f / player.MaxHealth*1.0f);
		//count the ammo
		playerWeapon = player.GetComponent<PlayerAttributes>().Equipped;
		if (playerWeapon != null){
			ammoText.text = playerWeapon.GetComponent<Weapon>().ammo.ToString();
			gunLogoImage.enabled = true;
			gunLogoImage.sprite = playerWeapon.GetComponent<Weapon>().gunLogo;
		}
		else{
			gunLogoImage.enabled = false;
			ammoText.text = "--";
		}
		//show the time
		int min = Mathf.FloorToInt(Time.timeSinceLevelLoad / 60f);
		int sec = Mathf.FloorToInt(Time.timeSinceLevelLoad - min * 60);
		 string niceTime = string.Format("{0:D2}:{1:D2}", min, sec);
		 roundTime.text = niceTime;
	}

}
