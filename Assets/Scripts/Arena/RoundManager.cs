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
    public GameObject outerBayDoors;
    public GameObject medicPrefab;

	Contestant[] contestants;
	bool roundOver = false;
	public float govtime;
	InterfaceManager im;

	// Use this for initialization

	void Awake () {
		im = FindObjectOfType<InterfaceManager>();
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
		if (aliveContestants == 1){
			if (!roundOver){
				roundOver = true;
				SpawnGuards();
			}
		}

		if(roundOver){
			im.Announce("ROUND OVER\nPRESS [E] TO SURRENDER");
		}
	}

	public void endRound(){
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
		outerBayDoors.SetActive(false);
		foreach(Transform t in outerSpawns){
			GameObject spawnedGuard = (GameObject)Instantiate(guardPrefab, t.position, Quaternion.identity);
			GameObject spawnedGun = (GameObject)Instantiate(guardWeapon, t.position,Quaternion.identity);
			spawnedGuard.GetComponent<Contestant>().equipped = spawnedGun.GetComponent<Item>();
			spawnedGun.GetComponent<Item>().equipper = spawnedGuard.GetComponent<Contestant>();
			spawnedGuard.GetComponent<AI_GuardController>().job = AI_GuardController.Job.EndRound;
			spawnedGuard.GetComponent<AI_GuardController>().target = FindObjectOfType<PlayerController>().GetComponent<Contestant>();
			spawnedGuard.GetComponent<AI_GuardController>().endStatus = AI_GuardController.endRoundStatus.Chase;
		}
		GetComponent<AudioSource>().Stop();
	}
}
