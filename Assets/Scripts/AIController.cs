﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is a very basic use of navmesh agents to pathfind towards the player constantly
/// </summary>
public class AIController : MonoBehaviour {
	public enum AIState{Searching, Hunting, Fighting, Fleeing, Beacon};

	NavMeshAgent agent;

	private Vector3 destination;
	float distance;
	float closestDistance;
	int closestEnemy;
	int closestWeapon;
	Contestant c;

	//just for testing
	public Vector3 project,rand,center,towardsCenter;

	private RangedWeapon[] weapons;
	[Range(0,1)]
	public float talkFrequency;

	[Range(-1,1)]
	public float confidence;
	public AIState state;
	public Contestant engagedTarget;

	//These are the FOV variables
	[Header("FOV Varibales")]
	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;
	public float detectRaduis;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	ContestantGenerator cGen;

	// Use this for initialization
	void Start () {
		c = GetComponent<Contestant>();
		weapons = FindObjectsOfType<RangedWeapon> ();
		agent = GetComponent<NavMeshAgent>();
		agent.speed = c.movespeed;
		cGen = FindObjectOfType<ContestantGenerator>();
		StartSearch();
		StartCoroutine ("FindTargetsWithDelay", .5f);
	}
	
	// Update is called once per frame
	void Update () {
		switch (state){
			case AIState.Searching:
				Searching();
				break;
			case AIState.Hunting:
				Hunting();
				break;
			case AIState.Fighting:
				Fighting();
				break;
			case AIState.Fleeing:
				Fleeing();
				break;
			case AIState.Beacon:
				break;
		}
		agent.destination = destination;
		confidence = Mathf.Clamp(confidence + (0.01f * Time.deltaTime),-1,1);
		if(c.health < 20 && confidence < 0 && state != AIState.Beacon){
			StartBeacon();
		}
	}

	void StartBeacon(){
		state = AIState.Beacon;
		agent.Stop();
		c.Say("Help me");
	}

	void Beacon(){
		if (c.equipped != null){
			c.equipped.Unequip();
			c.equipped = null;
		}
	}

	void StartHunt(){
		state = AIState.Hunting;
		agent.speed = c.movespeed * 0.66f;
		Wander();
	}

	void Wander(){
		project = transform.position + transform.forward.normalized * 3.0f;
		Vector2 randomCircle = Random.insideUnitCircle;
		rand = project + new Vector3(randomCircle.x,0,randomCircle.y)*3.0f;
		center = new Vector3(-5f,1.63f,-35.2f);
		towardsCenter = rand + ((center - rand).normalized) * (confidence*2);
		NavMeshPath path = new NavMeshPath();
		if(NavMesh.CalculatePath(transform.position, towardsCenter, NavMesh.AllAreas, path)){
			destination = towardsCenter;
		}
		else{
			Wander();
		}
	}

	void Hunting(){
		//if we're close enough to our destination, get a new one
		if(agent.remainingDistance < 0.5f){
			Wander();
		}
		//if we see somebody
		if(visibleTargets.Count > 0){
			engagedTarget = visibleTargets[0].gameObject.GetComponent<Contestant>();
			if(compareTarget(engagedTarget)){
				StartFight();
			}
			else{
				StartFlee();
			}
		}
		if(c.equipped == null){
			StartSearch();
		}

		//if our gun runs out of ammo, throw it away
		if(c.GetAmmo() == 0){
			c.ThrowEquipped();
		}
	}

	void StartFlee(){
		state = AIState.Fleeing;
		agent.speed = c.movespeed;
		Flee(engagedTarget.transform.position);
		if(Random.value < talkFrequency){
			c.Say(cGen.GetLine(ContestantGenerator.LineType.Retreat));
		}
	}

	void Flee(Vector3 scare){
		project = transform.position + (((-(scare-transform.position)).normalized)*10.0f);
		Vector2 randomCircle = Random.insideUnitCircle;
		rand = project + new Vector3(randomCircle.x,0,randomCircle.y)*5.0f;
		center = new Vector3(-5f,1.63f,-35.2f);
		towardsCenter = rand + ((center - rand).normalized) * (confidence*2);
		NavMeshPath path = new NavMeshPath();
		if(NavMesh.CalculatePath(transform.position, towardsCenter, NavMesh.AllAreas, path)){
			destination = towardsCenter;
		}
		else{
			Flee(scare);
		}
	}

	void Fleeing(){
		if(agent.remainingDistance < 0.5f){
			StartHunt();
		}

		//if our gun runs out of ammo, throw it away
		if(c.GetAmmo() == 0){
			c.ThrowEquipped();
		}
	}

	void StartSearch(){
		state = AIState.Searching;
		agent.speed = c.movespeed;
		findClosestWeapon();
	}

	void Searching(){
		if (c.equipped != null){
			StartHunt();
		}
		findClosestWeapon();
		if(visibleTargets.Count > 0){
			engagedTarget = visibleTargets[0].gameObject.GetComponent<Contestant>();
			if(!compareTarget(engagedTarget)){
				StartFlee();
			}
		}
	}

	public void findClosestWeapon() {
		weapons = FindObjectsOfType<RangedWeapon>();
		if(weapons.Length == 0){
			destination = center;
			return;
		}
		closestDistance = 100.0f;
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons[i].ammo == 0 || weapons[i].GetComponent<Item>().equipper != null) {
				continue;
			}
			distance = Vector3.Distance (weapons[i].transform.position, transform.position);
			if (distance <= closestDistance) {
				closestDistance = distance;
				closestWeapon = i;
			}
		}
		destination = weapons[closestWeapon].transform.position;
	}

	void StartFight(){
		state = AIState.Fighting;
		agent.speed = c.movespeed * 0.8f;
		if(Random.value < talkFrequency){
			c.Say(cGen.GetLine(ContestantGenerator.LineType.Fight));
		}
	}

	void Fighting(){
		destination = engagedTarget.transform.position;

		//constantly re-evaluate
		if(!compareTarget(engagedTarget)){
			StartFlee();
		}

		//move towards our target, slow down when we're really close
		//need to define engagement range here!
		if(agent.remainingDistance < 5){
			agent.speed = 0.5f;
		}
		else{
			agent.speed = c.movespeed * 0.8f;
		}

		//rotate towards our target
		transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,(engagedTarget.transform.position - transform.position), Time.deltaTime, 0));

		//if we're looking directly at them, shoot
		if(LineOfSight(engagedTarget.GetComponent<Collider>(),viewRadius)){
			c.UseEquipped(true);
			c.UseEquipped(false);
		}

		//if our gun runs out of ammo, throw it away
		if(c.GetAmmo() == 0){
			c.ThrowEquipped();
		}

		//if our target dies, gain confidence and move on
		if(!engagedTarget.isAlive){
			confidence += 0.2f;
			StartHunt ();
		}

		//if we don't have a gun, we can't fight
		if(c.equipped == null){
			StartSearch();
		}
	}

	/// <summary>
	/// Compares the target.
	/// </summary>
	/// <returns><c>true</c>, if you should engage the target, <c>false</c> otherwise.</returns>
	/// <param name="target">Target.</param>
	bool compareTarget(Contestant target){
	float myThreat, theirThreat;
		if(c.equipped != null){
			myThreat = c.equipped.threat;
		}
		else{
			myThreat = 0;
		}

		if(target.equipped != null){
			theirThreat = target.equipped.threat;
		}
		else{
			theirThreat = 0;
		}


		if(confidence > -(myThreat - theirThreat)){
			Debug.Log("I'm not scared of my target because my confidence says i'm more threatening");
			return true;
		}
		else{
			Debug.Log("I'm scared because i'm not confident enough to take on my target");
			return false;
		}

	}
		
	//FOV METHODS - THOMAS F
	IEnumerator FindTargetsWithDelay(float delay){
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}

	void FindVisibleTargets() {
		visibleTargets.Clear ();
		//Defines a list of targets within a certain radius of the individual
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for(int i = 0; i < targetsInViewRadius.Length; i++){
			//make sure we are not detecing ourselves
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			float dstToTarget = Vector3.Distance (transform.position, target.position);
			//are they close enough?
			if (dstToTarget < detectRaduis){
				//Check if there is an obstacle between ourselves and our target
				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					//if it is an alive contestant
					if(target.gameObject.GetComponent<Contestant>().isAlive){
						//make sure we don't detect ourselves
						if(target != transform){
							visibleTargets.Add(target);
						}
					}
				}
			}
			//Check if the target is within our view angle
			else if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				//Check if there is an obstacle between ourselves and our target
				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					//if it is an alive contestant
					if(target.gameObject.GetComponent<Contestant>().isAlive){
						//make sure we don't detect ourselves
						if(target != transform){
							visibleTargets.Add(target);
						}
					}
				}
			}
		}
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin (angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos (angleInDegrees * Mathf.Deg2Rad));
	}

	/// <summary>
	/// Check if we are facing our target directly.
	/// </summary>
	/// <returns><c>true</c>, if we are looking directly at our target, <c>false</c> otherwise.</returns>
	/// <param name="losTarget">Los target.</param>
	bool LineOfSight(Collider losTarget, float dist){
		Debug.DrawRay(transform.position, transform.forward * dist);
		RaycastHit hit;
		Ray r = new Ray(transform.position, transform.forward);
		Physics.Raycast(r, out hit, dist);
		if(hit.collider == losTarget){
			return true;
		}
		else{
			return false;
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawCube(project, Vector3.one * 0.1f);
		Gizmos.DrawCube(rand, Vector3.one * 0.1f);
		Gizmos.DrawLine(project,rand);
		Gizmos.DrawLine(transform.position, project);
		Gizmos.DrawCube(towardsCenter, Vector3.one * 0.1f);
		Gizmos.DrawLine(rand,towardsCenter);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, towardsCenter);
	}




}
