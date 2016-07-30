using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour {

	static public int roundNumber = 1;
	public int totalContestants;
	public int aliveContestants;
	public float roundEndWait;
	public List<Transform> contestantSpawns, outerSpawns;
	public GameObject guardPrefab;
	public GameObject guardWeapon;
    public bool autoSpawn;

	Contestant[] contestants;
	bool roundOver = false;
	public float govtime;
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
        if (StaticGameStats.govUpgrades[0]){
        	govtime = 45.0f;
        }
        else{
        	govtime = 30.0f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (aliveContestants == 1){
			if (!roundOver){
				roundOver = true;
				SpawnGuards();
				Debug.Log(aliveContestants);
			}
		}
	}

	public void endRound(){
		if (Time.timeSinceLevelLoad < govtime){
			FindObjectOfType<StaticGameStats>().Influence(0, 5.0f);
			FindObjectOfType<StaticGameStats>().Influence(2, -2.0f);
		}else{
			FindObjectOfType<StaticGameStats>().Influence(0, -5.0f);
		}
		if (roundNumber < 5){
			roundNumber++;
			GetComponent<SceneChange>().RoundRestart();
		}
		else{
			GetComponent<SceneChange>().ToPostArena();
		}
	}

	public void Death(){
		aliveContestants--;
	}

	void SpawnGuards(){
		foreach(Transform t in outerSpawns){
			GameObject spawnedGuard = (GameObject)Instantiate(guardPrefab, t.position, Quaternion.identity);
			GameObject spawnedGun = (GameObject)Instantiate(guardWeapon, t.position,Quaternion.identity);
			spawnedGuard.GetComponent<Contestant>().equipped = spawnedGun.GetComponent<Item>();
			spawnedGun.GetComponent<Item>().equipper = spawnedGuard.GetComponent<Contestant>();
			spawnedGuard.GetComponent<AI_GuardController>().job = AI_GuardController.Job.EndRound;
			spawnedGuard.GetComponent<AI_GuardController>().target = FindObjectOfType<PlayerController>().GetComponent<Contestant>();
			spawnedGuard.GetComponent<AI_GuardController>().endStatus = AI_GuardController.endRoundStatus.Chase;
		}
	}
}
