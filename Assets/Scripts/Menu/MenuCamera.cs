using UnityEngine;
using System.Collections;

public class MenuCamera : MonoBehaviour {
	/// <summary>
	/// 0 = main menu, 1 = zoomed out, 2 = post
	/// </summary>
	public int state;
	public Transform[] waypoints;
	public GameObject _MainMenu, _ZoomedOut, _Post, _Boot, _Login, _Lose, _Win, _UpgradeSponsor, _Upgrades, _Sponsors, _Arena_Start, _Questionaire, _Tutorial_start, _ArenaMap, _Orientation, _Settings;
	public bool teleport;
	public float speed;
	public AudioSource audioP;
	public bool isAlreadyStarted = false;

	void Start(){
		Debug.Log("Save location: " + Application.persistentDataPath);
		audioP.volume = PlayerPrefs.GetFloat("MusicVolume");
		if(StaticGameStats.instance.toPost){
			StaticGameStats.instance.TierOneUpgrades[0] = false;
			StaticGameStats.instance.TierOneUpgrades[1] = false;
			StaticGameStats.instance.TierOneUpgrades[2] = false;

			StaticGameStats.instance.TierTwoUpgrades[0] = false;
			StaticGameStats.instance.TierTwoUpgrades[1] = false;
			StaticGameStats.instance.TierTwoUpgrades[2] = false;

			StaticGameStats.instance.TierThreeUpgrades[0] = false;

			StaticGameStats.instance.TierFourUpgrades [0] = false;

			StaticGameStats.instance.Abilites[0] = false;
			StaticGameStats.instance.Abilites[1] = false;
			StaticGameStats.instance.Abilites[2] = false;
			StaticGameStats.instance.Abilites[3] = false;
			Post();
			StaticGameStats.instance.toPost = false;
		}
		else if (TutorialTrigger.FromTutorial == true) {
			Arena_Start ();
		}
		else{
			Boot();
		}
	}

	// Update is called once per frame
	void Update () {
		if(teleport){
			transform.position = waypoints[state].position;
		}
		else{
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(waypoints[state].position.x,waypoints[state].position.y,-10),speed);
		}

		if (state == 1 && Input.GetKeyDown(KeyCode.Delete)){
			StaticGameStats.instance.committed = !StaticGameStats.instance.committed;
		}

		if(state == 9 && Input.GetKeyDown(KeyCode.Delete)){
			StaticGameStats.instance.avaliableMoney++;
		}
	}

	public void MainMenu(){
		StaticGameStats.instance.Load();
		DisableAllBut(_MainMenu);
		state = 0;
		audioP.Stop();
	}

	public void ZoomedOut(){
		DisableAllBut(_ZoomedOut);
		state = 1;
		if (!audioP.isPlaying){
			audioP.Play();
		}
	}

	public void Post(){
		DisableAllBut(_Post);
		state = 2;
		if (!audioP.isPlaying){
			audioP.Play();
		}
		if ((StaticGameStats.instance.govRep <= 0) || (StaticGameStats.instance.corRep <= 0) || (StaticGameStats.instance.rebRep <= 0)) {
			FindObjectOfType<PostMenuInterfaceManager> ().Lose ();
		} else if ((StaticGameStats.instance.govRep >= 100) || (StaticGameStats.instance.corRep >= 100) || (StaticGameStats.instance.rebRep >= 100)) {
			FindObjectOfType<PostMenuInterfaceManager> ().Victory ();
		} else {
			FindObjectOfType<PostMenuInterfaceManager> ().Normal ();
		}
	}

	public void Boot(){
		DisableAllBut(_Boot);
		state = 4;
	}

	public void Login(){
		DisableAllBut(_Login);
		state = 5;
		StaticGameStats.instance.QuestionnaireDone = true;
	}

	public void Lose(){
		DisableAllBut(_Lose);
		state = 6;
		FindObjectOfType<LoseInterfaceManager>().Lose();
	}

	public void Win(){
		DisableAllBut(_Win);
		state = 7;
		FindObjectOfType<WinInterfaceManager>().Win();
	}

	public void UpgradeSponsor(){
		DisableAllBut(_UpgradeSponsor);
		state = 8;
	}

	public void Upgrade(){
		DisableAllBut(_Upgrades);
		state = 9;
		FindObjectOfType<UpgradeInterface>().Upgrade();
	}

	public void Sponsor(){
		DisableAllBut(_Sponsors);
		state = 10;
	}

	public void Arena_Start(){
		if (isAlreadyStarted == false) {
			DisableAllBut(_Arena_Start);
			state = 11;
			FindObjectOfType<arena_start_Manager>().arena_start_text();
			isAlreadyStarted = true;
		}
	}

	public void Questionaire(){
		DisableAllBut(_Questionaire);
		if (StaticGameStats.instance.QuestionnaireDone == false) {
			state = 12;
		} else {
			ZoomedOut();	
		}
	}

	public void Tutorial_Start(){
		DisableAllBut(_Tutorial_start);
		state = 13;
		StaticGameStats.instance.Save();
		FindObjectOfType<tutorial_start_Manager>().tutorial_start_text();
	}

	public void ArenaMap(){
		DisableAllBut (_ArenaMap);
		state = 14;
		FindObjectOfType<ArenaMap_Manager>().ArenaMap_start_text();
	}

	public void Orientation(){
		DisableAllBut(_Orientation);
		if(StaticGameStats.instance.QuestionnaireDone == false){
			state = 3;
			FindObjectOfType<orientation_manager>().Orientation();
		}
		else{
			ZoomedOut();
		}
	}

	void DisableAllBut(GameObject screen){
		GameObject[] screens = new GameObject[]{_MainMenu, _ZoomedOut, _Post, _Boot, _Login, _Lose, _Win, _UpgradeSponsor, _Upgrades, _Sponsors, _Arena_Start, _Questionaire, _Tutorial_start, _ArenaMap, _Orientation, _Settings};
		foreach(GameObject g in screens){
			if(screen != g){
				g.SetActive(false);
			}
			else{
				g.SetActive(true);
			}
		}
	}

	public void Shutdown(){
		Application.Quit();
		//UnityEditor.EditorApplication.isPlaying = false;
	}

	public void Settings(){
		DisableAllBut (_Settings);
		state = 15;
	}
}
