﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages rounds within the arena gameplay.
/// </summary>
public class RoundManager : MonoBehaviour {

	static public int roundNumber = 1;
	public int totalContestants;
	public int aliveContestants;
	public PlayerAttributes player;

	PlayerAttributes[] players;
	float roundEndWait = 2.0f;
	bool roundOver = false;

	// Use this for initialization
	void Start () {
		players = FindObjectsOfType<PlayerAttributes>();
		totalContestants = players.Length;
		//assuming all the contestants are alive at the start of the round...
		aliveContestants = totalContestants;
		if (roundNumber < 5){
			FindObjectOfType<InterfaceManager>().roundText.text = "ROUND: " + roundNumber.ToString();
		}
		else{
			FindObjectOfType<InterfaceManager>().roundText.text = "FINAL ROUND";
		}
		Debug.Log(roundNumber.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		if(roundOver){
			roundEndWait -= Time.deltaTime;
		}
		if(roundEndWait <= 0){
			endRound();
		}
		if(!player.alive || aliveContestants == 1){
			roundOver = true;
		}
	}

	/// <summary>
	/// Call this when a contestant dies
	/// </summary>
	public void Death(){
		aliveContestants--;
	}

	void endRound(){
		if (roundNumber < 5){
			roundNumber++;
			SceneManager.LoadScene("Test");
		}
		else{
			SceneManager.LoadScene("postArena");
		}
	}
}
