using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is a very basic use of navmesh agents to pathfind towards the player constantly
/// </summary>
public class AIController : MonoBehaviour {
	public enum AIState{Searching, Hunting, Fighting, Fleeing};

	NavMeshAgent agent;

	private Vector3 destination;
	float distance;
	float closestDistance;
	int closestEnemy;
	int closestWeapon;
	Contestant c;

	//just for testing
	public Vector3 project,rand,center,towardsCenter;

	private Contestant[] contestants;
	private RangedWeapon[] weapons;

	[Range(-1,1)]
	public float confidence;
	public AIState state;

	//These are the FOV variables
	[Header("FOV Varibales")]
	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	void StartHunt(){
		project = transform.position + transform.forward.normalized * 3.0f;
		Vector2 randomCircle = Random.insideUnitCircle;
		rand = project + new Vector3(randomCircle.x,0,randomCircle.y)*3.0f;
		center = new Vector3(-5f,1.63f,-35.2f);
		towardsCenter = rand + (center - rand).normalized;
		//towardsCenter = towardsCenter * confidence;
		NavMeshPath path = new NavMeshPath();
		if(NavMesh.CalculatePath(transform.position, towardsCenter, NavMesh.AllAreas, path)){
			destination = towardsCenter;
		}
		else{
			StartHunt();
		}
	}

	// Use this for initialization
	void Start () {
		StartHunt();
		c = GetComponent<Contestant>();
		closestDistance = 1000.0f;
		//followingTarget = 0; //sets target to be following
		contestants = FindObjectsOfType<Contestant>();
		weapons = FindObjectsOfType<RangedWeapon> ();
		//findClosestWeapon ();
		agent = GetComponent<NavMeshAgent>();
		//Vector3 fwd = transform.TransformDirection (Vector3.forward);
		//Runs FOV processes on a delay
		//StartCoroutine ("FindTargetsWithDelay", .5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(c.equipped == null){
			//findClosestWeapon();
			//FaceTarget();
		}
		else{
			//findClosestEnemy ();
			//FaceTarget();
			if(c.GetAmmo() == 0){
				c.ThrowEquipped();
			}
		}

		switch (state){
			case AIState.Searching:
				break;
			case AIState.Hunting:
				Hunting();
				break;
			case AIState.Fighting:
				break;
			case AIState.Fleeing:
				break;
		}
		agent.destination = destination;
	}

	public void findClosestEnemy() {
		closestDistance = 100.0f;
		for (int i = 0; i < contestants.Length; i++) {
			if (contestants[i] == GetComponent<Contestant>() || !contestants[i].isAlive) {
				continue;
			}
			distance = Vector3.Distance (contestants[i].transform.position, transform.position);//find distance between enemy and self
			if (distance <= closestDistance) {
				closestDistance = distance;
				closestEnemy = i; //for referencing the chosen enemy
			}
		}
		destination = contestants[closestEnemy].transform.position;
	}

	public void findClosestWeapon() {
		weapons = FindObjectsOfType<RangedWeapon>();
		closestDistance = 100.0f;
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons[i].GetComponent<RangedWeapon>().ammo == 0) {
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
		//Go through that list
		for(int i = 0; i < targetsInViewRadius.Length; i++){
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			//Check if the target is within our view angle
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);
				//Check if there is an obstacle between ourselves and our target
				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					//Do something when enemy is in view
					visibleTargets.Add(target);
					transform.LookAt (target);
					if(RaycastForward(50)) {
						c.UseEquipped (true);
						c.UseEquipped (false);
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

//	void FaceTarget(){
//		transform.LookAt (targetPos);
//	}

	

	/// <summary>
	/// Retruns true if the raycast hits a contestant
	/// </summary>
	/// <returns><c>true</c>, if forward was raycasted, <c>false</c> otherwise.</returns>
	/// <param name="distance">Distance.</param>
	bool RaycastForward(float distance){
		Debug.DrawRay(transform.position, transform.forward * distance);
		RaycastHit hit;
		Ray r = new Ray(transform.position, transform.forward);
		Physics.Raycast(r, out hit, distance);
		Debug.Log("Hit " + hit.collider.name);
		if(hit.collider.tag == "Contestant"){
			return true;
		}
		else{
			return false;
		}
	}

	/// <summary>
	/// Check if we are facing our target directly.
	/// </summary>
	/// <returns><c>true</c>, if we are looking directly at our target, <c>false</c> otherwise.</returns>
	/// <param name="losTarget">Los target.</param>
	bool LineOfSight(Collider losTarget){
		Debug.DrawRay(transform.position, transform.forward * distance);
		RaycastHit hit;
		Ray r = new Ray(transform.position, transform.forward);
		Physics.Raycast(r, out hit, distance);
		Debug.Log("Hit " + hit.collider.name);
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

	void Hunting(){
		if(agent.remainingDistance < 1.0f){
			StartHunt();
		}

	}
}
