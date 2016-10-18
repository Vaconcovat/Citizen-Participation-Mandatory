﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is a very basic use of navmesh agents to pathfind towards the player constantly
/// </summary>
public class AIController : MonoBehaviour {
	public enum AIState{Searching, Hunting, Fighting, Fleeing, Beacon, Evacuating, Dead, Shocked, Blinded};

	NavMeshAgent agent;

	private Vector3 destination;
	float distance;
	float closestDistance;
	int closestEnemy;
	int closestWeapon;
	Contestant c;
	public Contestant player;
	public Light DirectionalLight;
	public Light ContestantLight;
	public Light PlayerLight;
	float shockActiveTime, blindActiveTime;

	//just for testing
	[HideInInspector]
	public Vector3 project,rand,center,towardsCenter,towardsplayer;

	public GameObject beaconCard;

	private RangedWeapon[] weapons;
	[Range(0,1)]
	public float talkFrequency;

	[Range(-1,1)]
	public float confidence;
	public AIState state;
	public Contestant engagedTarget;
	public float Player_Gravitation = 2;

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
	[HideInInspector]
	public UI_GenericCard beacon;
	public AI_MedicController medic;

	bool medicVisited = false;
	float confidenceGain;
	public LayerMask raycastMask;

	// Use this for initialization
	void Start () {
		c = GetComponent<Contestant>();
		weapons = FindObjectsOfType<RangedWeapon> ();
		agent = GetComponent<NavMeshAgent>();
		agent.speed = c.movespeed;
		cGen = FindObjectOfType<ContestantGenerator>();
		StartSearch();
		StartCoroutine ("FindTargetsWithDelay", .5f);
		if(c.traits.Contains(Contestant.Trait.Fearless)){
			confidenceGain = 0.02f;
			confidence = 0.5f;
		}
		else if(c.traits.Contains(Contestant.Trait.Scared)){
			confidenceGain = 0.005f;
			confidence = -0.5f;
		}
		else{
			confidenceGain = 0.01f;
			confidence = 0;
		}
		SkillCoolDown sk = FindObjectOfType<SkillCoolDown>();
		if(sk != null){
			shockActiveTime = sk.shockActiveTime;
			blindActiveTime = sk.blindActiveTime;
		}

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
				Beacon();
				break;
			case AIState.Evacuating:
				Evacuating();
				break;
			case AIState.Blinded:
				StartCoroutine ("Blinded");
				break;
			case AIState.Shocked:
				StartCoroutine ("Shocked");
				break;
		}
		if(agent.isOnNavMesh){
			agent.destination = destination;
		}
	}

	public void StartShocked(){
		state = AIState.Shocked;
	}


	IEnumerator Shocked() {
		GetComponent<NavMeshAgent>().enabled = false;
		yield return new WaitForSeconds (shockActiveTime);
		GetComponent<NavMeshAgent>().enabled = true;
		StartHunt ();
	}

	public void StartBlinded(){
		state = AIState.Blinded;
	}

	IEnumerator Blinded(){
		viewAngle = 60.0f;
		viewRadius = 10.0f;
		DirectionalLight.intensity = 0.0f;
		ContestantLight.intensity = 8.0f;
		PlayerLight.intensity = 8.0f;
		yield return new WaitForSeconds (blindActiveTime);
		viewAngle = 120.0f;
		viewRadius = 12.0f;
		DirectionalLight.intensity = 0.5f;
		ContestantLight.intensity = 0.0f;
		PlayerLight.intensity = 0.0f;
		StartHunt ();
	}

	public void StartEvac(){
		FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().mercy, transform.position, 0.7f, true);
		state = AIState.Evacuating;
		beacon.text = "[ MEDICAL EVAC INBOUND ]";
		RoundManager rm = FindObjectOfType<RoundManager>();
		Vector3 medicSpawn = rm.outerSpawns[Random.Range(0,rm.outerSpawns.Count)].position;
		GameObject spawned = (GameObject)Instantiate(rm.medicPrefab, medicSpawn, Quaternion.identity);
		medic = spawned.GetComponent<AI_MedicController>();
		medic.target = this.c;
		medic.spawn = medicSpawn;
		FindObjectOfType<InterfaceManager>().Announce("[ " + c.contestantName + " MERCIED ]", 3);
		if(c.onCameras.Count > 0){
			if (!StaticGameStats.instance.FirstRun) {
				StaticGameStats.instance.Influence (StaticGameStats.InfluenceTrigger.ActivateMedicBeacon, 0);	
			}
			c.CameraInfluence(2, true);
		}
		gameObject.layer = 14;
	}

	void Evacuating(){
		agent.speed = 7;
		if (c.equipped != null){
			c.equipped.Unequip();
			c.equipped = null;
		}
		if(Vector3.Distance(medic.transform.position, transform.position) < 4 && !medicVisited){
			medicVisited = true;
			agent.destination = medic.transform.position;
			agent.Resume();
			beacon.text = "[ EVAC ]";
		}

		if(medicVisited){
			destination = medic.transform.position;
		}
	}

	void StartBeacon(){
		state = AIState.Beacon;
		agent.Stop();
		GameObject spawned = (GameObject)Instantiate(beaconCard);
		spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		beacon = spawned.GetComponent<UI_GenericCard>();
		beacon.target = transform;
	}

	void Beacon(){
		if (c.equipped != null){
			c.equipped.Unequip();
			c.equipped = null;
		}
		if(Vector3.Distance(FindObjectOfType<PlayerController>().transform.position, transform.position) < 2){
			beacon.text = "[ E ] Mercy | [ Q ] Execute";
			beacon.textSize = 50;
			if(Input.GetKeyDown(KeyCode.E)){
				StartEvac();
				if (StaticGameStats.instance.TierFourUpgrades [0]) {
					if (c.killer.isPlayer == true) {
						StartCoroutine("KarmaHeal");
					}

				}
			}
			if(Input.GetKeyDown(KeyCode.Q)){
				Execute();
				if (StaticGameStats.instance.TierFourUpgrades [0]) {
					if (c.killer.isPlayer == true) {
						StartCoroutine("KarmaDamage");
					}
				}
			}
		}
		else{
			beacon.text = "[ REQUESTING MEDIC ]";
			beacon.textSize = 30;
		}
	}

	IEnumerator KarmaHeal(){
		for (int i = 1; i <= StaticGameStats.instance.KarmaGetSomeHealDuration; i++) {
			player.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.instance.KarmaGetSomeHealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
			}
	}

	IEnumerator KarmaDamage(){
		player.ContestantDamageModifier = StaticGameStats.instance.KaramGetSomeDamageBuff;
		for (int i = 1; i <= StaticGameStats.instance.KarmaGetSomeDamageBuffDuration; i++) {
			yield return new WaitForSeconds (1.0f);
		}
		player.ContestantDamageModifier = StaticGameStats.instance.NormalDamageBuff;
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
		towardsCenter = rand + (((center - rand).normalized) * (confidence*2));
		towardsplayer = towardsCenter + (((FindObjectOfType<PlayerController>().transform.position - towardsCenter).normalized)*(Player_Gravitation));
		destination = towardsplayer;
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

		confidence = Mathf.Clamp(confidence + (confidenceGain * Time.deltaTime),-1,1);
		if(c.health < 20 && confidence < 0 && state != AIState.Beacon){
			StartBeacon();
		}
		c.moving = true;
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
			destination = center;
		}
	}

	void Fleeing(){
		agent.speed = c.movespeed;
		if(agent.remainingDistance < 0.5f){
			StartHunt();
		}

		//if our gun runs out of ammo, throw it away
		if(c.GetAmmo() == 0){
			c.ThrowEquipped();
		}
		confidence = Mathf.Clamp(confidence + (confidenceGain * Time.deltaTime),-1,1);
		if(c.health < 20 && confidence < 0 && state != AIState.Beacon){
			StartBeacon();
		}
		c.moving = true;
	}

	void StartSearch(){
		state = AIState.Searching;
		agent.speed = c.movespeed;
		Vector3 closestWep = findClosestWeapon();
		if(closestWep == Vector3.zero || (Vector3.Distance(transform.position, closestWep) > 30 && Random.value > 0.2f)){
		 	destination = FindObjectsOfType<ItemSpawner>()[Random.Range(0,8)].transform.position;
		}
		else{
			destination = closestWep;
		}
	}

	void Searching(){
		if (c.equipped != null){
			if(c.equipped.type == Item.ItemType.Ranged){
				StartHunt();
			}
			else{
				if(c.GetAmmo() == 0){
					c.equipped.Throw();
				}
				else{
					c.UseEquipped(true);
					c.UseEquipped(false);
				}

			}

		}

		if(agent.remainingDistance < 0.1f){
			StartSearch();
		}

		foreach(ItemSpawner i in FindObjectsOfType<ItemSpawner>()){
			if(i.ready){
				if(Vector3.Distance(i.transform.position, transform.position) < 2){
					i.Spawn();
				}
			}
		}

		if(visibleTargets.Count > 0){
			engagedTarget = visibleTargets[0].gameObject.GetComponent<Contestant>();
			if(!compareTarget(engagedTarget)){
				StartFlee();
			}
		}
		confidence = Mathf.Clamp(confidence + (confidenceGain * Time.deltaTime),-1,1);
		if(c.health < 20 && confidence < 0 && state != AIState.Beacon){
			StartBeacon();
		}
		c.moving = true;
	}

	public Vector3 findClosestWeapon() {
		weapons = FindObjectsOfType<RangedWeapon>();
		if(weapons.Length == 0){
			return Vector3.zero;
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
		if(closestDistance == 100.0f){
			return Vector3.zero;
		}
		else{
			return weapons[closestWeapon].transform.position;
		}
	}

	void StartFight(){
		state = AIState.Fighting;
		agent.speed = c.movespeed * 0.8f;
		if(Random.value < talkFrequency){
			c.Say(cGen.GetLine(ContestantGenerator.LineType.Fight));
		}
	}

	void Fighting(){
		//if we don't have a gun, we can't fight
		if(c.equipped == null){
			StartSearch();
			return;
		}
		destination = engagedTarget.transform.position;

		//constantly re-evaluate
		if(!compareTarget(engagedTarget)){
			StartFlee();
		}

		//move towards our target, slow down when we're really close
		if(!engagedTarget.isPlayer){
			if(engagedTarget.GetComponent<AIController>().state != AIState.Beacon){
				if(agent.remainingDistance < c.equipped.GetRangeHint(true)){
					agent.speed = 0.1f;
					c.moving = false;
				}
				else{
					agent.speed = c.movespeed * 0.8f;
					c.moving = true;
				}
			}
			else{
				agent.speed = c.movespeed * 0.8f;
				if(agent.remainingDistance < 2.0f && engagedTarget.isAlive){
					if(c.traits.Contains(Contestant.Trait.Merciful)){
						engagedTarget.GetComponent<AIController>().StartEvac();
						StartHunt();
					}
					else if(c.traits.Contains(Contestant.Trait.Relentless)){
						engagedTarget.GetComponent<AIController>().Execute();
						StartHunt();
					}
					else{
						if(confidence > 0){
							engagedTarget.GetComponent<AIController>().StartEvac();
							StartHunt();
						}
						else{
							engagedTarget.GetComponent<AIController>().Execute();
							StartHunt();
						}
					}
				}
			}
		}
		else if(c.equipped != null){
			if(agent.remainingDistance < c.equipped.GetRangeHint(true)){
				agent.speed = 0.1f;
				c.moving = false;
			}
			else{
				agent.speed = c.movespeed * 0.8f;
				c.moving = true;
			}
		}



		//rotate towards our target
		transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,(engagedTarget.transform.position - transform.position), Time.deltaTime*3, 0));

		//if we're looking directly at them and we're in range, shoot
		if(c.equipped != null){
			if(LineOfSight(engagedTarget.GetComponent<Collider>(),viewRadius) && agent.remainingDistance < c.equipped.GetRangeHint(false)){
				if(!engagedTarget.isPlayer){
					if(engagedTarget.GetComponent<AIController>().state != AIState.Beacon){
						c.UseEquipped(true);
						c.UseEquipped(false);
					}
				}
				else{
					c.UseEquipped(true);
					c.UseEquipped(false);
				}
			}
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


		confidence = Mathf.Clamp(confidence + (confidenceGain * Time.deltaTime),-1,1);
		if(c.health < 20 && confidence < 0 && state != AIState.Beacon){
			StartBeacon();
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
			return true;
		}
		else{
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
			if (dstToTarget < detectRaduis || Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2){
				//Check if there is an obstacle between ourselves and our target
				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					//if it is an alive contestant
					if(target.gameObject.GetComponent<Contestant>().isAlive){
						if(target.gameObject.GetComponent<Contestant>().type == Contestant.ContestantType.Medic){
							continue;
						}
						if(target.gameObject.GetComponent<Contestant>().type == Contestant.ContestantType.AI){
							if(target.gameObject.GetComponent<AIController>().state == AIState.Beacon || target.gameObject.GetComponent<AIController>().state == AIState.Evacuating){
								continue;
							}
						}
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
		Debug.DrawRay(transform.position + new Vector3(0,1,0), transform.forward * dist);
		RaycastHit hit;
		Ray r = new Ray(transform.position + new Vector3(0,1,0), transform.forward);
		Physics.Raycast(r, out hit, dist, raycastMask.value);

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
		Gizmos.DrawCube(towardsplayer, Vector3.one * 0.1f);
		Gizmos.DrawLine(towardsCenter,towardsplayer);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, towardsplayer);
	}

	public void Execute(){
		FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().execute, transform.position, 0.7f, true);
		FindObjectOfType<InterfaceManager>().Announce("[ " + c.contestantName + " EXECUTED ]", 3);
		if(c.onCameras.Count > 0){
			if (!StaticGameStats.instance.FirstRun) {
				StaticGameStats.instance.Influence (StaticGameStats.InfluenceTrigger.Execution, 0);
			}
			c.CameraInfluence(0, true);
			c.Die("EXECUTED ON CAMERA");
		}
		else{
			c.Die("EXECUTED");
		}
	}


}