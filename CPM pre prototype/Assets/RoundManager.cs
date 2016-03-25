using UnityEngine;
using System.Collections;

/// <summary>
/// This script manages rounds within the arena gameplay.
/// </summary>
public class RoundManager : MonoBehaviour {

	public int roundNumber;
	public int totalContestants;
	public int aliveContestants;

	PlayerAttributes[] players;

	// Use this for initialization
	void Start () {
		players = FindObjectsOfType<PlayerAttributes>();
		totalContestants = players.Length;
		//assuming all the contestants are alive at the start of the round...
		aliveContestants = totalContestants;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Call this when a contestant dies
	/// </summary>
	public void Death(){
		aliveContestants--;
	}
}
