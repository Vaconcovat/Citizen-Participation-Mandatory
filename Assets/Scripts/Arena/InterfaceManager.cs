using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class InterfaceManager : MonoBehaviour {

	[Header("UI GameObjects")]
	public Image healthbar;
	public Text ammo;
	public Text timer;
	public Image gunLogo;
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


	[Header("Player")]
	public Contestant player;

	[Header("Camera")]
	public NoiseAndScratches noise;

	[Header("Music")]
	public AudioSource music;

	RoundManager rm;
	// Use this for initialization
	void Start () {
		rm = FindObjectOfType<RoundManager>();
		abortButton.enabled = false;
		abortText.enabled = false;
		abortImage.enabled = false;
		//Upgrades Text
		upgradesText.text = "";
		if(StaticGameStats.generalUpgrades[0]){
			upgradesText.text = upgradesText.text + "General Upgrade Enabled!\n";
		}
		if(StaticGameStats.govUpgrades[0]){
			upgradesText.text = upgradesText.text + "Government Upgrade Enabled!\n";
		}
		if(StaticGameStats.corUpgrades[0]){
			upgradesText.text = upgradesText.text + "Corporate Upgrade Enabled!\n";
		}
		if(StaticGameStats.rebUpgrades[0]){
			upgradesText.text = upgradesText.text + "Rebel Upgrade Enabled!\n";
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
		timer.text = Time.timeSinceLevelLoad.ToString();
		noise.grainIntensityMax = Mathf.Lerp(0,2.5f,(1-healthbar.fillAmount));
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
			hpText.text = "VITALS < 25%";
			deadText.text = "";
		}
		else if(healthbar.fillAmount < 0.5f){
			hpText.text = "VITALS < 50%";
			deadText.text = "";
		}
		else if(healthbar.fillAmount < 0.75f){
			hpText.text = "VITALS < 75%";
			deadText.text = "";
		}
		else{
			hpText.text = "";
			deadText.text = "";
		}
	}
}
