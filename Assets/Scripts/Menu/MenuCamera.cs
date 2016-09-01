﻿using UnityEngine;
using System.Collections;

public class MenuCamera : MonoBehaviour {
	/// <summary>
	/// 0 = main menu, 1 = zoomed out, 2 = post
	/// </summary>
	public int state;
	public Transform[] waypoints;
	public bool teleport;
	public float speed;
	public AudioSource audioP;

	void Start(){
		if(StaticGameStats.toPost){
			state = 2;
			StaticGameStats.toPost = false;
			FindObjectOfType<BootInterfaceManager>().skipped = true;
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
		state = 0;
		audioP.Stop();
	}

	public void ZoomedOut(){
		state = 1;
		if (!audioP.isPlaying){
			audioP.Play();
		}
	}

	public void Post(){
		state = 2;
		if (!audioP.isPlaying){
			audioP.Play();
		}
	}

	public void Pre(){
		state = 3;
		if (!audioP.isPlaying){
			audioP.Play();
		}
	}

	public void Login(){
		state = 5;
		StaticGameStats.QuestionnaireDone = true;
	}

	public void Lose(){
		state = 6;
		FindObjectOfType<LoseInterfaceManager>().Lose();
	}

	public void Win(){
		state = 7;
	}

	public void UpgradeSponsor(){
		state = 8;
	}

	public void Upgrade(){
		state = 9;
	}

	public void Sponsor(){
		state = 10;
	}

	public void Arena_Start(){
		state = 11;
		FindObjectOfType<arena_start_Manager>().arena_start_text();
	}

	public void Questionaire(){
		if (StaticGameStats.QuestionnaireDone == false) {
			state = 12;
		} else {
			state = 1;	
		}

	}
}
