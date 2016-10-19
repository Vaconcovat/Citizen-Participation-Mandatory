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
	public Text ammoText;
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
	public MeshRenderer floor;



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

	public GameObject AbilityBar;
	public Text disconnect_text;
	public float disconnectHoldTime;
	float disconnecttimer;
	bool disconnecting = false;

	RoundManager rm;
	float announcetimer;
	bool activeannounce;
	[HideInInspector]
	public static string influenceText;
	int[] influenceCounts;

	List<StaticGameStats.InfluenceTrigger> influences;

	// Use this for initialization
	void Start () {
		rm = FindObjectOfType<RoundManager>();
		floor.material.color = Color.gray;
		influences = new List<StaticGameStats.InfluenceTrigger>();
		if(rm.GetRound() == 1){
			influences.Clear();
			influenceText = "";
		}
		abortButton.enabled = false;
		abortText.enabled = false;
		abortImage.enabled = false;
		announcementsText.text = "";
		activeannounce = false;
		influenceCounts = new int[4];
		if ((StaticGameStats.instance.Abilites [0] == false) && (StaticGameStats.instance.Abilites [1] == false) && (StaticGameStats.instance.Abilites [2] == false) && (StaticGameStats.instance.Abilites [3] == false)) {
			AbilityBar.SetActive (false);
		} else {
			AbilityBar.SetActive (true);
		}
		disconnect_text.color = new Color(1,1,1,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (StaticGameStats.instance.TierOneUpgrades [0]) {
			Backpack.gameObject.SetActive (true);
		} else {
			Backpack.gameObject.SetActive (false);
		}
		healthbar.fillAmount = (player.health * 1.0f) / (player.maxHealth * 1.0f);
		if (player.equipped != null){
			if (player.equipped.type == Item.ItemType.Ranged){
				ammo.text = player.equipped.GetComponent<RangedWeapon> ().ammo.ToString () + " / " + player.equipped.GetComponent<RangedWeapon> ().Maxammo.ToString ();
			}
			else{
				ammo.text = "--";
			}
			gunLogo.enabled = true;
			gunLogo.sprite = player.equipped.logo;
			ammoText.enabled = true;
		}
		else{
			ammo.text = "--";
			gunLogo.enabled = false;
			ammoText.enabled = false;
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
		hpText.text = player.health.ToString() + " / " + player.maxHealth.ToString();
		if (!player.isAlive) {
			deadText.text = "!! PLANT VITALS CRITICAL !!\n!!PLANT VITALS OFFLINE !!\nCONNECTION TO PLANT LOST.";
		} else {
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

		if(StaticGameStats.instance.FirstRun){
			influenceFeed.text = "NO REPUTATION ASSIGNED - NEW MANAGER";
		}
		else{
			influenceFeed.text = influenceText;
		}


		//disconnect
		if(player.isAlive){
			if(Input.GetKeyDown(KeyCode.Escape)){
				disconnecting = true;
			}
			if(Input.GetKeyUp(KeyCode.Escape)){
				disconnecting = false;
			}
			if(disconnecting){
				disconnecttimer -= Time.deltaTime;
				disconnect_text.color = new Color(1,1,1,Mathf.Max(0,(1.0f-(disconnecttimer / disconnectHoldTime))));
			}
			else{
				disconnecttimer = disconnectHoldTime;
				disconnect_text.color = new Color(1,1,1,0);
			}
			if(disconnecttimer < 0){
				player.Die("");
			}
		}
		else{
			disconnect_text.color = new Color(1,1,1,0);
		}
	}

	/// <summary>
	/// Makes an announcement
	/// </summary>
	/// <param name="s">Text to display</param>
	/// <param name="time">How long does this announcement last</param>
	public void Announce(string s, float time){
		activeannounce = true;
		announcementsText.text = s;
		announcementsText.fontSize = 50;
		announcetimer = time;
	}

	/// <summary>
	/// Makes an announcement, at a certain text size
	/// </summary>
	/// <param name="s">Text to display</param>
	/// <param name="time">How long does this announement last</param>
	/// <param name="size">Text size. 50 is default.</param>
	public void Announce(string s, float time, int size){
		activeannounce = true;
		announcementsText.text = s;
		announcementsText.fontSize = size;
		announcetimer = time;
	}

	public void Influence(StaticGameStats.InfluenceTrigger type){
		Debug.Log("Influence called  " + type); 
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
