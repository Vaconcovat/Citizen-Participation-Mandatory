using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class InterfaceManager : MonoBehaviour {

	[Header("UI GameObjects")]
	public Image healthbar;
	public Image HBSeg1;
	public Image HBSeg2;
	public Image HBSeg3;
	public Image HBSeg4;
	public Text ammo;
	public Text timer;
	public Image gunLogo;
	public Image backpackDisplay;
	public Text roundText;
	public Text aliveText;
	public Text deadText;
	public Text hpText;
	public Image[] corners;
	public Text recText;
	public Button abortButton;
	public Text abortText;
	public Image abortImage;
	public Text upgradesText;
	public Text announcementsText;
	float grainFloor;



	[Header("Player")]
	public Contestant player;

	[Header("Camera")]
	public NoiseAndScratches noise;

	[Header("Music")]
	public AudioSource music;

	RoundManager rm;
	float announcetimer;
	bool activeannounce;

	// Use this for initialization
	void Start () {
		rm = FindObjectOfType<RoundManager>();
		abortButton.enabled = false;
		abortText.enabled = false;
		abortImage.enabled = false;

		announcementsText.text = "";
		activeannounce = false;


		//Upgrades Text
		upgradesText.text = "";

		if(StaticGameStats.TierOneUpgrades[0]){
			upgradesText.text = upgradesText.text + "Items have +1 Available Charges!\n";
		}
		if(StaticGameStats.TierOneUpgrades[1]){
			upgradesText.text = upgradesText.text + "Thrown weapons deal double damage!\n";
		}
		if(StaticGameStats.TierOneUpgrades[2]){
			upgradesText.text = upgradesText.text + "All Reputation gain is increased by 5%!\n";
		}
		if(StaticGameStats.TierOneUpgrades[3]){
			upgradesText.text = upgradesText.text + "ALL weapons have +20% ammo!\n";
		}

		if(StaticGameStats.TierTwoUpgrades[0]){
			upgradesText.text = upgradesText.text + "Spacebar now uses items in the backpack slot!\n";
		}
		if(StaticGameStats.TierTwoUpgrades[1]){
			upgradesText.text = upgradesText.text + "Fire rate -20%, Bullet Damage +20%!\n";
		}
		if(StaticGameStats.TierTwoUpgrades[2]){
			upgradesText.text = upgradesText.text + "Gain +20% Max Health back upon emptying a weapon!\n";
		}
		if(StaticGameStats.TierTwoUpgrades[3]){
			upgradesText.text = upgradesText.text + "Gain +50% movement speed while holding an empty weapon!\n";
		}

		if(StaticGameStats.TierThreeUpgrades[0]){
			upgradesText.text = upgradesText.text + "Bloodlust Enabled!\n";
		}
		if(StaticGameStats.TierThreeUpgrades[1]){
			upgradesText.text = upgradesText.text + "Health kits are different!\n";
		}
		if(StaticGameStats.TierThreeUpgrades[2]){
			upgradesText.text = upgradesText.text + "Weapons are thrown automatically and deal 1.2x damage when thrown!\n";
		}
		if(StaticGameStats.TierThreeUpgrades[3]){
			upgradesText.text = upgradesText.text + "Guards always spawn with rifles!\n";
		}
	}
	
	// Update is called once per frame
	void Update () {
		healthbar.fillAmount = (player.health * 1.0f) / (player.maxHealth * 1.0f);
		if (player.equipped != null){
			if (player.equipped.type == Item.ItemType.Ranged){
				ammo.text = player.equipped.GetComponent<RangedWeapon>().ammo.ToString();
			}
			else{
				ammo.text = "--";
			}
			gunLogo.enabled = true;
			gunLogo.sprite = player.equipped.logo;
		}
		else{
			ammo.text = "--";
			gunLogo.enabled = false;
		}
		if(player.inventory != null){
			backpackDisplay.enabled = true;
			backpackDisplay.sprite = player.inventory.logo;
		}
		else{
			backpackDisplay.enabled = false;
		}
		timer.text = Time.timeSinceLevelLoad.ToString() + " (" + rm.govtime.ToString() + ")";
		grainFloor = Mathf.Lerp(0,1.0f,(1-healthbar.fillAmount));
		if(noise.grainIntensityMax > grainFloor){
			noise.grainIntensityMax -= Time.deltaTime * 1.0f;
		}
		noise.grainIntensityMin = noise.grainIntensityMax - 0.2f;
		roundText.text = "ROUND: " + RoundManager.roundNumber.ToString();
		aliveText.text = rm.aliveContestants.ToString() + " / " + rm.totalContestants.ToString() + " REMAIN.";

		if(!player.isAlive){
			hpText.text = "VITALS OFFLINE";
			deadText.text = "!! PLANT VITALS CRITICAL !!\n!!PLANT VITALS OFFLINE !!\nCONNECTION TO PLANT LOST.";
			Camera.main.orthographicSize += Time.deltaTime * 0.3f;
			foreach(Image g in corners){
				g.enabled = false;
			}
			roundText.enabled = false;
			aliveText.enabled = false;
			recText.text = "OFFLINE";
			ammo.enabled = false;
			music.Stop();
			abortButton.enabled = true;
			abortText.enabled = true;
			abortImage.enabled = true;
		}
		else if (healthbar.fillAmount < 0.15f){
			hpText.text = "!! VITALS CRITICAL !!";
			deadText.text = "!! PLANT VITALS CRITICAL !!";
		}
		else if(healthbar.fillAmount < 0.25f){
			HBSeg2.enabled = false;
			HBSeg3.enabled = false;
			HBSeg4.enabled = false;
			hpText.text = "VITALS < 25%";
			deadText.text = "";
		}
		else if(healthbar.fillAmount < 0.5f){
			HBSeg2.enabled = true;
			HBSeg3.enabled = false;
			HBSeg4.enabled = false;
			hpText.text = "VITALS < 50%";
			deadText.text = "";
		}
		else if(healthbar.fillAmount < 0.75f){
			HBSeg2.enabled = true;
			HBSeg3.enabled = true;
			HBSeg4.enabled = false;
			hpText.text = "VITALS < 75%";
			deadText.text = "";
		}
		else{
			HBSeg2.enabled = true;
			HBSeg3.enabled = true;
			HBSeg4.enabled = true;
			hpText.text = "";
			deadText.text = "";
		}

		if(activeannounce){
			if(announcetimer > 0){
				announcetimer -= Time.deltaTime;
			}
			else{
				activeannounce = false;
				announcementsText.text = "";
			}
		}
	}

	public void Announce(string s, float time){
		activeannounce = true;
		announcementsText.text = s;
		announcetimer = time;
	}
}
