using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
	public bool noGuardDamage;

	Contestant[] contestants;
	bool roundOver = false;
	public float govtime;
	InterfaceManager im;

	// Use this for initialization

	void Awake () {
		noGuardDamage = true;
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

	void Start(){
		File.WriteAllText (StaticGameStats.path, "");
	}
	
	// Update is called once per frame
	void Update () {
		if (aliveContestants == 1){
			if (!roundOver){
				SpawnGuards();
				roundOver = true;
			}
		}

		if(roundOver){
			
			outerBayDoors.SetActive(false);
			bool noGuards = true;
			foreach(AI_GuardController a in FindObjectsOfType<AI_GuardController>()){
				if(a.GetComponent<Contestant>().isAlive){
					noGuards = false;
				}
			}
			if (noGuards) { //if there are no guards alive
				im.Announce ("GUARDS EXHAUSTED\nPLEASE WAIT FOR FURTHER INSTRUCTIONS", 1);
				Invoke ("Triumph", 5);
			} else if ((!noGuards) && (noGuardDamage)) { //if there are guards alive and none are damaged
				im.Announce ("ROUND OVER\nPRESS [Q] TO SURRENDER", 1);
			} else { //if there are guards alive and they have been damaged
				im.Announce ("CONTESTANT RESISTING DETAINMENT\nALL UNITS OPEN FIRE!", 1);
			}
		}
		else{
			if(FindObjectsOfType<AI_MedicController>().Length > 0){
				outerBayDoors.SetActive(false);
			}
			else{
				outerBayDoors.SetActive(true);
			}
		}
	}

	void Triumph(){
		FindObjectOfType<StaticGameStats>().Influence(StaticGameStats.InfluenceTrigger.EndOfRoundTriumph, 0);
		endRound();
	}

	public void endRound(){
		if (roundNumber < 3){
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
