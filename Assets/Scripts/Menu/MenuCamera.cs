using UnityEngine;
using System.Collections;

public class MenuCamera : MonoBehaviour {
	/// <summary>
	/// 0 = main menu, 1 = zoomed out, 2 = post
	/// </summary>
	public int state;
	public Transform[] waypoints;
	public GameObject _MainMenu, _ZoomedOut, _Post, _Boot, _Login, _Lose, _Win, _UpgradeSponsor, _Upgrades, _Sponsors, _Arena_Start, _Questionaire, _Tutorial_start;
	public bool teleport;
	public float speed;
	public AudioSource audioP;

	void Awake(){
		if(StaticGameStats.toPost){
			Post();
			StaticGameStats.toPost = false;
		}
		else{
			state = 4;
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
	}

	public void MainMenu(){
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
	}

	public void Login(){
		DisableAllBut(_Login);
		state = 5;
		StaticGameStats.QuestionnaireDone = true;
	}

	public void Lose(){
		DisableAllBut(_Lose);
		state = 6;
		FindObjectOfType<LoseInterfaceManager>().Lose();
	}

	public void Win(){
		DisableAllBut(_Win);
		state = 7;
	}

	public void UpgradeSponsor(){
		DisableAllBut(_UpgradeSponsor);
		state = 8;
	}

	public void Upgrade(){
		DisableAllBut(_Upgrades);
		state = 9;
	}

	public void Sponsor(){
		DisableAllBut(_Sponsors);
		state = 10;
	}

	public void Arena_Start(){
		DisableAllBut(_Arena_Start);
		state = 11;
		FindObjectOfType<arena_start_Manager>().arena_start_text();
	}

	public void Questionaire(){
		DisableAllBut(_Questionaire);
		if (StaticGameStats.QuestionnaireDone == false) {
			state = 12;
		} else {
			ZoomedOut();	
		}
	}

	public void Tutorial_Start(){
		DisableAllBut(_Tutorial_start);
		state = 13;
		FindObjectOfType<tutorial_start_Manager>().tutorial_start_text();
	}

	void DisableAllBut(GameObject screen){
		GameObject[] screens = new GameObject[]{_MainMenu, _ZoomedOut, _Post, _Boot, _Login, _Lose, _Win, _UpgradeSponsor, _Upgrades, _Sponsors, _Arena_Start, _Questionaire, _Tutorial_start};
		foreach(GameObject g in screens){
			if(screen != g){
				g.SetActive(false);
			}
			else{
				g.SetActive(true);
			}
		}
	}
}
