using UnityEngine;
using System.Collections;

/// <summary>
/// This is a very basic use of navmesh agents to pathfind towards the player constantly
/// </summary>
public class AIController : MonoBehaviour {

	NavMeshAgent agent;
	Transform destination;
	
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		destination = FindObjectOfType<PlayerController>().transform;
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = destination.position;
	}
}
