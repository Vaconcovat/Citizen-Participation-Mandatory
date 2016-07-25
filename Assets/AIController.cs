using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is a very basic use of navmesh agents to pathfind towards the player constantly
/// </summary>
public class AIController : MonoBehaviour {

	NavMeshAgent agent;
	Transform destination;

	private Transform targetPos;
	float distance;
	float closestDistance;
	int closestEnemy;
	int closestWeapon;
	Contestant c;

	private Contestant[] contestants;
	private RangedWeapon[] weapons;

	//These are the FOV variables
	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();
	
	// Use this for initialization
	void Start () {

		c = GetComponent<Contestant>();
		closestDistance = 1000.0f;
		//followingTarget = 0; //sets target to be following
		contestants = FindObjectsOfType<Contestant>();
		weapons = FindObjectsOfType<RangedWeapon> ();
		//findClosestWeapon ();

		agent = GetComponent<NavMeshAgent>();
		destination = targetPos;
		//Vector3 fwd = transform.TransformDirection (Vector3.forward);
		//Runs FOV processes on a delay
		StartCoroutine ("FindTargetsWithDelay", .5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(c.equipped == null){
			findClosestWeapon();
			//FaceTarget();
		}
		else{
			findClosestEnemy ();
			//FaceTarget();
			if(c.GetAmmo() == 0){
				c.ThrowEquipped();
			}
		}
		destination = targetPos;
		agent.destination = destination.position;
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
		targetPos = contestants[closestEnemy].transform;
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
		targetPos = weapons[closestWeapon].transform;
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
}
