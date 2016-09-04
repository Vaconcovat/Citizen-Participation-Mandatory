using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class TutorialManager : MonoBehaviour {

	[Header("UI GameObjects")]
	public Text ammo;
	public Image gunLogo;
	public Text announceText;
	public GameObject ExitDoor;
	public Contestant Victim;


	[Header("Player")]
	public Contestant player;

	[Header("Camera")]
	public NoiseAndScratches noise;
	public GameObject cameraGUI;

	[Header("Music")]
	public AudioSource music;

	//RoundManager rm;
	float announcetimer;
	bool activeannounce;

	// Use this for initialization
	void Start () {


		announceText.text = "";
		activeannounce = false;
	}

	// Update is called once per frame
	void Update () {
		if ((Victim.isAlive == false) && (BinTrigger.ThrownWeapon == true))
		{
			ExitDoor.gameObject.SetActive (false);
		}
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

		if(activeannounce){
			if(announcetimer > 0){
				announcetimer -= Time.deltaTime;
			}
			else{
				activeannounce = false;
				announceText.text = "";
			}
		}

		if(player.isAlive && player.onCameras.Count > 0){
			cameraGUI.SetActive(true);
		}
		else{
			cameraGUI.SetActive(false);
		}
	}

	public void Announce(string s, float time){
		announceText.text = s;
		activeannounce = true;
		announcetimer = time;
	}
}
