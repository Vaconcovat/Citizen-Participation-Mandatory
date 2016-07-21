using UnityEngine;
using System.Collections;

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

	//StartCoroutine ("updatePath");

	
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
	}
	
	// Update is called once per frame
	void Update () {
		if(c.equipped == null){
			findClosestWeapon();
			FaceTarget();
		}
		else{
			findClosestEnemy ();
			FaceTarget();
			if(RaycastForward(50)){
				c.UseEquipped(true);
				c.UseEquipped(false);
			}
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

	void FaceTarget(){
		transform.LookAt (targetPos);
	}

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
