using UnityEngine;
using System.Collections;

public class AI_MedicController : MonoBehaviour {
	bool retrieved;

	public Contestant target;
	public Vector3 spawn;

	NavMeshAgent agent;
	public GameObject tracker;
	UI_GenericCard tracker_card;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		GameObject spawned = (GameObject)Instantiate(tracker);
		spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		tracker_card = spawned.GetComponent<UI_GenericCard>();
		tracker_card.target = transform;
		agent.speed = GetComponent<Contestant>().movespeed;
	}
	
	// Update is called once per frame
	void Update () {
		if(!retrieved){
			Retrieving();
		}
		else{
			Evacuating();
		}
	}

	void Retrieving(){
		agent.destination = target.transform.position;
		if(Vector3.Distance(transform.position, target.transform.position) < 2){
			retrieved = true;
		}
		if(!target.isAlive){
			retrieved = true;
		}
	}

	void Evacuating(){
		agent.destination = spawn;
		if(agent.remainingDistance < 0.5f){
			if(target.isAlive){
				if (!StaticGameStats.FirstRun) {
					FindObjectOfType<StaticGameStats> ().Influence (StaticGameStats.InfluenceTrigger.SuccessfulExtraction, 0);
				}
				target.Die("MERCIED");
			}
			if(tracker_card != null){
				Destroy(tracker_card.gameObject);
			}
			Destroy(gameObject);
		}
	}
}
