using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
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
	public Text announcementsText, influenceFeed;
	float grainFloor;



	[Header("Player")]
	public Contestant player;

	[Header("Camera")]
	public NoiseAndScratches noise;
	public ColorCorrectionCurves colourCurves;
	[Range(0,5)]
	public float offCameraSaturation;
	[Range(0,5)]
	public float onCameraSaturation;
	public GameObject cameraGUI;

	[Header("Music")]
	public AudioSource music;

	public GameObject Backpack;

	RoundManager rm;
	float announcetimer;
	bool activeannounce;
	[HideInInspector]
	public static string influenceText;
	int[] influenceCounts;

	List<StaticGameStats.InfluenceTrigger> influences;

	// Use this for initialization
	void Start () {
		influences = new List<StaticGameStats.InfluenceTrigger>();
		rm = FindObjectOfType<RoundManager>();
		abortButton.enabled = false;
		abortText.enabled = false;
		abortImage.enabled = false;
		announcementsText.text = "";
		activeannounce = false;
		influenceCounts = new int[4];

	}
	
	// Update is called once per frame
	void Update () {
		if (StaticGameStats.TierOneUpgrades [1]) {
			Backpack.gameObject.SetActive (true);
		} else {
			Backpack.gameObject.SetActive (false);
		}
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

		if(player.isAlive && player.onCameras.Count > 0){
			cameraGUI.SetActive(true);
			colourCurves.saturation = onCameraSaturation;
		}
		else{
			cameraGUI.SetActive(false);
			colourCurves.saturation = offCameraSaturation;
		}

		//influences
		influenceFeed.text = influenceText;
	}

	public void Announce(string s, float time){
		activeannounce = true;
		announcementsText.text = s;
		announcetimer = time;
	}

	public void Influence(StaticGameStats.InfluenceTrigger type){
		if(!influences.Contains(type)){
			influences.Add(type);
			influenceCounts[influences.Count - 1] = 1;
		}
		else{
			int index = influences.IndexOf(type);
			influenceCounts[index] += 1;
		}

		if(influences.Count > 3){
			influences.RemoveAt(0);
			influenceCounts[0] = influenceCounts[1];
			influenceCounts[1] = influenceCounts[2];
			influenceCounts[2] = influenceCounts[3];
		}

		influenceText = "";
		string s = "";
		int x = 0;
		foreach(StaticGameStats.InfluenceTrigger i in influences){
			switch(i){
				case StaticGameStats.InfluenceTrigger.ActivateMedicBeacon:
					s = "(REB+ | GOV-) Televised 'Act of Mercy'";
					break;
				case StaticGameStats.InfluenceTrigger.EndOfRoundSurrender:
					s = "(GOV+ | REB-) Surrendered at end of round";
					break;
				case StaticGameStats.InfluenceTrigger.EndOfRoundTriumph:
					s = "(REB+) Resisted all guards!";
					break;
				case StaticGameStats.InfluenceTrigger.Execution:
					s = "(GOV+) Televised Execution";
					break;
				case StaticGameStats.InfluenceTrigger.KillGuard:
					s = "(REB+) Guard killed!";
					break;
				case StaticGameStats.InfluenceTrigger.OnCameraKill:
					s = "(GOV+ | REB -) Televised Kill";
					break;
				case StaticGameStats.InfluenceTrigger.SponsorItemUse:
					s = "(COR+) Televised sponsor item use";
					break;
				case StaticGameStats.InfluenceTrigger.SponsorWeaponDeath:
					s = "(COR-) Televised death with a sponsored weapon";
					break;
				case StaticGameStats.InfluenceTrigger.SponsorWeaponFire:
					s = "(COR+) Televised sponsor weapon use";
					break;
				case StaticGameStats.InfluenceTrigger.SponsorWeaponKill:
					s = "(COR+) Televised kill with a sponsor weapon";
					break;
				case StaticGameStats.InfluenceTrigger.SuccessfulExtraction:
					s = "(REB+) Injured contestant successfully extracted!";
					break;
			}
			s += (influenceCounts[x] > 1)?(" x" + influenceCounts[x]):"";
			influenceText += s + "\n";
			x++;

		}
	}
}
