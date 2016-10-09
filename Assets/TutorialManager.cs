using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class TutorialManager : MonoBehaviour {
	public enum TutorialState{Pre, Enter, Vendor, Target, Bin, Camera, Exit, Disobey};
	public TutorialState state;

	[Header("UI GameObjects")]
	public Text ammo;
	public Image gunLogo;
	public Text announceText;
	public GameObject ExitDoor;
	public Contestant Victim;
	public Image fader;
	public AutoType log_autoType;
	public TutorialMarker Camera1, Camera2, Vendor, Bin, Target, Exit;
	public Contestant escort;
	AI_GuardController escort_AI;
	public LineRenderer tutorialLine;

	[Header("Player")]
	public Contestant player;

	[Header("Camera")]
	public NoiseAndScratches noise;
	public GameObject cameraGUI;

	[Header("Music")]
	public AudioSource music;

	[TextArea(1,3)]
	public string GuardStartText, GuardVendorText, GuardTargetText, GuardBinText, GuardExitText;

	//RoundManager rm;
	float announcetimer;
	bool activeannounce;
	string plantName;
	bool fading = false;
	bool dead = false;
	bool VictimDead = false;
	public Contestant[] guards;

	public MeshRenderer floor;

	// Use this for initialization
	void Start () {
		escort_AI = escort.GetComponent<AI_GuardController>();
		state = TutorialState.Pre;
		tutorialLine.SetPositions(new Vector3[]{player.transform.position, player.transform.position});
		tutorialLine.enabled = false;
		floor.material.color = Color.gray;
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


		switch(state){
			case TutorialState.Enter:
				if(escort.currentTalkCard == null){
					escort.Say(GuardStartText, 40);
				}
				break;
			case TutorialState.Vendor:
				tutorialLine.SetPositions(new Vector3[]{player.transform.position, Vendor.transform.position});
				if(player.equipped != null){
					_Target();
				}
				if(escort.currentTalkCard == null){
					escort.Say(GuardVendorText, 40);
				}
				break;
			case TutorialState.Target:
				tutorialLine.SetPositions(new Vector3[]{player.transform.position, Target.transform.position});
				if(!Victim.isAlive){
					_Bin();
				}
				if(escort.currentTalkCard == null){
					escort.Say(GuardTargetText, 40);
				}
				break;
			case TutorialState.Bin:
				tutorialLine.SetPositions(new Vector3[]{player.transform.position, Bin.transform.position});
				if(BinTrigger.ThrownWeapon){
					_Exit();
				}
				if(escort.currentTalkCard == null){
					escort.Say(GuardBinText, 40);
				}
				break;
			case TutorialState.Exit:
				tutorialLine.SetPositions(new Vector3[]{player.transform.position, Exit.transform.position});
				if(escort.currentTalkCard == null){
					escort.Say(GuardExitText, 40);
				}
				break;
			case TutorialState.Disobey:
				bool noGuards = true;
				foreach(Contestant a in guards){
					if(a.isAlive){
						noGuards = false;
					}
				}
				if(noGuards){
					ExitDoor.gameObject.SetActive (false);
				}
				break;

		}
		if(escort_AI.endStatus == AI_GuardController.endRoundStatus.Fight && state != TutorialState.Disobey){
			state = TutorialState.Disobey;
			tutorialLine.enabled = false;
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
		music.Play();
		fading = true;
		log_autoType.displayedText[0] = "FULL NEURAL CONTROL READY!\n                     \n[   ";
		log_autoType.displayedText[1] = plantName;
		log_autoType.displayedText[2] = "   ]\nALL CONTESTANTS MUST COMPLETE\nORIENTATION IN ORDER TO COMPETE \nIN THE ARENA.\n\nCONTESTANTS MUST FIRE A WEAPON\nAT A LIVE TARGET, THEN DISPENSE\nOF THE WEAPON TO PASS.\n\nUSE [ W A S D ] TO MOVE PLANT\nUSE [ MOUSE ] TO AIM\nUSE [ LMB ] TO FIRE WEAPONS/USE ITEMS\nUSE [ RMB ] TO THROW\n\n";
		log_autoType.finishedCallString = "ControlsDone";
		log_autoType.StartType(); 
	}

	public void ControlsDone(){
		player.GetComponent<PlayerController>().enabled = true;
		log_autoType.GetComponent<Text>().color = new Color(1,1,1,0.5f);
		_Enter();
	}

	public void PlantDied(){
		log_autoType.gameObject.SetActive(true);
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

	public void _Enter(){
		state = TutorialState.Enter;
	}

	public void _Vendor(){
		log_autoType.gameObject.SetActive(false);
		tutorialLine.enabled = true;
		state = TutorialState.Vendor;
		escort.Say(GuardVendorText, 40);
		Vendor.active = true;
		Target.active = false;
		Bin.active = false;
		Camera1.active = true;
		Camera2.active = true;
	}

	public void _Target(){
		state = TutorialState.Target;
		escort.Say(GuardTargetText, 40);
		Vendor.active = false;
		Target.active = true;
		Bin.active = false;
	}

	public void _Bin(){
		state = TutorialState.Bin;
		escort.Say(GuardBinText, 40);
		Vendor.active = false;
		Target.active = false;
		Bin.active = true;
	}

	public void _Exit(){
		state = TutorialState.Exit;
		escort.Say(GuardExitText, 40);
		escort_AI.speed = 1;
		Vendor.active = false;
		Target.active = false;
		Bin.active = false;
	}

}
