using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public float speed = .08f; //defines speed of the enemy chasing
	//int followingTarget; //holds information about which target to follow.
    Vector3[] path;
    int targetIndex;
	public float timer = 1.0f;
	//public bool pathReached = false;
	private Transform targetPos;
	float distance;
	float closestDistance;
	int closestEnemy;
	int closestWeapon;
	Contestant c;
	//int closestHealth;
	//int closestSpeed;
	private Contestant[] contestants;
	private RangedWeapon[] weapons;
	Grid grid;
	public LayerMask unwalkableMask;

    void Start()
    {
    	c = GetComponent<Contestant>();
    	grid = FindObjectOfType<Grid>();
		closestDistance = 100.0f;
		//followingTarget = 0; //sets target to be following
		contestants = FindObjectsOfType<Contestant>();
		targetPos = transform;

		StartCoroutine ("updatePath");
		/*
		/ Following target
		/ 	0. Target moves towards a weapon
		/	1. Target has been reached
		/	2. Target moves towards a still enemy
		/	3. Target moves towards a moving enemy
		/	4. Target moves towards a health pickup
		/	5. Target moves towards a speed pickup
		*/
	}


	//DEON'S SIMPLE AI
	//if you don't have a weapon, go grab the closest one that has ammo in it.
	//if you do have a weapon, track the closest contestant.
	//always face your target.
	//if you're tracking an enemy, shoot when you get in line of sight.
	//when your gun runs out of ammo, throw it away.
	void Update()
	{
		if(c.equipped == null){
			findClosestWeapon();
			FaceTarget();
		}
		else{
			findClosestEnemy ();
			FaceTarget();
			if(!RaycastForward(50)){
				c.UseEquipped(true);
				c.UseEquipped(false);
			}
			if(c.GetAmmo() == 0){
				c.ThrowEquipped();
			}
		}
	}
/*
 * 	PROCESS FOR DETERMINING MOVEMENT:
 *	From all gameObjects, check if Tag == items
 *	for objects that return true
 *		check if gun
 *			if true add to array "Guns"
 *
 *	From all gameObjects, check if Tag == Contestant
 *	for objects that return true
 *		add to array "Enemies"
 *
 *	from array "Guns"
 *		check position of gun
 *		find closest
 *		moveToward
 *		repeat per frame till weapon is reached
 *	
 *	Once the weapon is reached
 *	from array "Enemies"
 *		check position of enemies
 *		find closest
 *		movetoward
 * 		(alter distance from player to walk too based on weapon type (ranged, melee, other)
 * 		repeat per frame till enemy is dead or weapon ammo runs out
 * 
 * 	if enemy is dead, find new closest enemy and traverse to them
 * 	if weapon ammo runs out, find new weapon and traverse to it
 *
*/


	IEnumerator updatePath()
	{
		while(true){
			if (!grid.NodeFromWorldPoint(transform.position).walkable){
				PathRequester.RequestPath (this.transform.position - transform.right, targetPos.position, OnPathFound);
			}
			else{
				PathRequester.RequestPath (this.transform.position, targetPos.position, OnPathFound);
			}
			Debug.Log("Waiting to update path");
			yield return new WaitForSeconds (timer);
		}
	}



    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful){
            path = newPath;

			//Debug.Log ("Coroutine 'FollowPath' stopped!");
			StopCoroutine("FollowPath");

			//Debug.Log ("Coroutine 'FollowPath' started");
            StartCoroutine("FollowPath");
		}
    }

    IEnumerator FollowPath(){
	   	Vector3 currentWaypoint = path[0];
    	while(true){
    		if(Vector3.Distance(transform.position, currentWaypoint) < 0.01f){
    			targetIndex++;
    			if(targetIndex >= path.Length){
    				targetIndex = 0;
    				path = new Vector3[0];
    				yield break;
    			}
    			currentWaypoint = path[targetIndex];
    		}
			if (Physics2D.OverlapCircle(transform.position, 0.5f, unwalkableMask)){
				Debug.Log("Path is unwalkable");
				Vector3 _backwards = transform.position - (transform.forward*2);
				transform.position = Vector3.MoveTowards(transform.position, _backwards, speed);

			}

    		transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
			yield return null;
    	}
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
		Vector3 dir = targetPos.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	/// <summary>
	/// Retruns true if the raycast hits a wall
	/// </summary>
	/// <returns><c>true</c>, if forward was raycasted, <c>false</c> otherwise.</returns>
	/// <param name="distance">Distance.</param>
	bool RaycastForward(float distance){
		Debug.DrawRay(transform.position, transform.right * distance);
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, distance);
		//For all the colliders we hit
		for(int i = 0; i < hits.Length; i++){
			RaycastHit2D hit = hits[i];
			//If we hit ourselves, thor a debug message and skip
			if(hit.collider == this.gameObject.GetComponent<Collider2D>()){
				continue; //skips
			}
			//If we hit something else, stop looking through the hits.
			else{
				//If the something else we hit was a contestant, let's try to shoot.
				if (hit.collider.tag == "Wall") {
					return true;
				}
			}
			return false; //ends the for loop
		}
		return false;
	}

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one * 0.5f);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
		Gizmos.DrawCube(this.transform.position - transform.right,Vector3.one);
    }

}
