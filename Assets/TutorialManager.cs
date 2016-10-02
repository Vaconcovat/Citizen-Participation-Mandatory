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
	public Image fader;
	public AutoType log_autoType;

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
	string plantName;
	bool fading = false;
	bool dead = false;
	bool VictimDead = false;

	// Use this for initialization
	void Start () {
		announceText.text = "";
		activeannounce = false;
		player.GetComponent<PlayerController>().enabled = false;
		music.volume = music.volume * PlayerPrefs.GetFloat("MusicVolume");
	}

	// Update is called once per frame
	void Update () {
		if (Victim.health <= 0) {
			VictimDead = true;
		}
		if ((VictimDead == true) && (BinTrigger.ThrownWeapon == true))
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

		if(!player.isAlive && !dead){
			dead = true;
			fading = false;
			PlantDied();
		}

		if(fading){
			fader.color = new Color(0,0,0,Mathf.Max(0, fader.color.a - Time.deltaTime));
		}
		else{
			fader.color = new Color(0,0,0,Mathf.Min(1, fader.color.a + Time.deltaTime));
		}
	}

	public void Announce(string s, float time){
		announceText.text = s;
		activeannounce = true;
		announcetimer = time;
	}

	public void start_log(){
		plantName = player.contestantName;
		log_autoType.displayedText[0] = "CONNECTING TO NEURAL INTERFACE...     \nSUCCESS!\nPLANT:   [   ";
		log_autoType.displayedText[1] = plantName;
		log_autoType.displayedText[2] = "   ]\n\nVITALS: 100%\nHEART RATE: " + Random.Range(90,121).ToString() +" BMP\nNEURAL INTERFERENCE: " + Random.Range(0.0001f, 0.001f).ToString() + "mY\nCONNECTION STABLE!\n\n                           ";
		log_autoType.finishedCallString = "Controls";
		log_autoType.StartType(); 
	}

	public void Controls(){
		log_autoType.GetComponent<Text>().alignment = TextAnchor.LowerLeft;
		player.GetComponent<PlayerController>().enabled = true;
		music.Play();
		fading = true;
		log_autoType.displayedText[0] = "FULL NEURAL CONTROL READY!\n                     \nUSE [ W A S D ] TO MOVE PLANT\nUSE [ MOUSE ] TO AIM\nUSE [ LMB ] TO FIRE WEAPONS/USE ITEMS\nUSE [ RMB ] TO THROW\n\n[   ";
		log_autoType.displayedText[1] = plantName;
		log_autoType.displayedText[2] = "   ]\nALL CONTESTANTS MUST COMPLETE\nORIENTATION IN ORDER TO COMPETE \nIN THE ARENA.\n\nCONTESTANTS MUST FIRE A WEAPON\nAT A LIVE TARGET, THEN DISPENSE\nOF THE WEAPON TO PASS.\n\n";
		log_autoType.finishedCallString = "ControlsDone";
		log_autoType.StartType(); 
	}

	public void ControlsDone(){
		log_autoType.GetComponent<Text>().color = new Color(1,1,1,0.5f);
	}

	public void PlantDied(){
		log_autoType.GetComponent<Text>().color = new Color(1,1,1,1f);
		plantName = player.contestantName;
		log_autoType.displayedText[0] = "DISCIPLINARY ACTION TAKEN ON PLANT:   [   ";
		log_autoType.displayedText[1] = plantName;
		log_autoType.displayedText[2] = "   ] \n DO NOT DISOBEY GUARDS INSTRUCTIONS \n\n [ " + plantName + " ] WILL BE REPLACED SHORTLY\nPLEASE WAIT...                                                                                             ";
		log_autoType.finishedCallString = "Restart";
		log_autoType.StartType(); 
	}

	public void Restart(){
		FindObjectOfType<SceneChange>().Tutorial();
	}
}
