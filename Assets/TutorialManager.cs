using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class TutorialManager : MonoBehaviour {

	[Header("UI GameObjects")]
	public Text ammo;
	public Image gunLogo;
	public Image[] corners;
	public Text recText;
	public Text announcementsText;


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
		//rm = FindObjectOfType<RoundManager>();
		//abortButton.enabled = false;
		//abortText.enabled = false;
		//abortImage.enabled = false;

		announcementsText.text = "";
		activeannounce = false;
	}

	// Update is called once per frame
	void Update () {
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
				announcementsText.text = "";
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
		activeannounce = true;
		announcementsText.text = s;
		announcetimer = time;
	}
}
