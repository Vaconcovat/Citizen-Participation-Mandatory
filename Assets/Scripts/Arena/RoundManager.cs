using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour {

	static public int roundNumber = 1;
	public int totalContestants;
	public int aliveContestants;
	public Contestant player;
	public float roundEndWait;
	public List<Transform> contestantSpawns;

    public bool autoSpawn;

	Contestant[] contestants;
	bool roundOver = false;
	// Use this for initialization
	void Awake () {
		contestants = FindObjectsOfType<Contestant>();
		totalContestants = contestants.Length;
		aliveContestants = totalContestants;
        //shuffle the spawnpoints
        if (autoSpawn) {
            for (int i = 0; i < contestantSpawns.Count; i++)
            {
                Transform temp = contestantSpawns[i];
                int index = Random.Range(i, contestantSpawns.Count);
                contestantSpawns[i] = contestantSpawns[index];
                contestantSpawns[index] = temp;
            }
            int j = 0;
            foreach (Contestant c in contestants)
            {
                c.transform.position = contestantSpawns[j].position;
                j++;
            }
        }
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
		if (Time.timeSinceLevelLoad < 30){
			FindObjectOfType<StaticGameStats>().Influence(0, 10.0f);
		}else{
			FindObjectOfType<StaticGameStats>().Influence(0, -10.0f);
		}
		if (roundNumber < 5){
			roundNumber++;
			GetComponent<SceneChange>().RoundRestart();
		}
		else{
			GetComponent<SceneChange>().Menu();
		}
	}

	public void Death(){
		aliveContestants--;
	}
}
