using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour {

	static public int roundNumber = 1;
	public int totalContestants;
	public int aliveContestants;
	public Contestant player;
	public float roundEndWait;

	Contestant[] contestants;
	bool roundOver = false;
	// Use this for initialization
	void Awake () {
		contestants = FindObjectsOfType<Contestant>();
		totalContestants = contestants.Length;
		aliveContestants = totalContestants;
	}
	
	// Update is called once per frame
	void Update () {
		if(roundOver){
			roundEndWait -= Time.deltaTime;
		}
		if (roundEndWait <= 0){
			endRound();
		}
		if (aliveContestants == 1){
			if (!roundOver){
				roundOver = true;
				Debug.Log(aliveContestants);
			}
		}
	}

	void endRound(){
		if (roundNumber < 5){
			roundNumber++;
			GetComponent<SceneChange>().Arena();
		}
		else{
			GetComponent<SceneChange>().Menu();
		}
	}

	public void Death(){
		aliveContestants--;
	}
}
