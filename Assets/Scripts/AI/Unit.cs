using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    //public Transform firstTarget;
	//public Transform secondTarget;
    public float speed = .08f; //defines speed of the enemy chasing
	int followingTarget; //holds information about which target to follow.
    Vector3[] path;
    int targetIndex;
	public float timer = 1.0f;
	//public bool pathReached = false;
	private GameObject targetPos;
	float distance;
	float closestDistance;
	int closestEnemy;
	int closestWeapon;
	//int closestHealth;
	//int closestSpeed;
	private GameObject[] enemies;
	private GameObject[] weapons;

    void Start()
    {
		targetPos = FindObjectOfType<PlayerController> ().gameObject;
		closestDistance = 100.0f;
		followingTarget = 0; //sets target to be following
		/*
		/ Following target
		/ 	0. Target moves towards a weapon
		/	1. Target has been reached
		/	2. Target moves towards a still enemy
		/	3. Target moves towards a moving enemy
		/	4. Target moves towards a health pickup
		/	5. Target moves towards a speed pickup
		*/

		//healthPickup = GameObject.FindGameObjectsWithTag("HealthPickup");
		//speedPickup = GameObject.FindGameObjectsWithTag("SpeedPickup");
		//health = GameObject.FindGameObjectsWithTag("Health");
		

		enemies = new GameObject[7];
		//weapons = new GameObject[9];
		enemies[0] = GameObject.Find("Enemy"); //find all enemies
		Debug.Log (enemies[0].name);
		enemies[1] = GameObject.Find("Enemy (1)"); //find all enemies
		Debug.Log (enemies[1].name);
		enemies[2] = GameObject.Find("Enemy (2)"); //find all enemies
		Debug.Log (enemies[2].name);
		enemies[3] = GameObject.Find("Enemy (3)"); //find all enemies
		Debug.Log (enemies[3].name);
		enemies[4] = GameObject.Find("Enemy (4)"); //find all enemies
		Debug.Log (enemies[4].name);
		enemies[5] = GameObject.Find("Enemy (5)"); //find all enemies
		Debug.Log (enemies[5].name);
		enemies[6] = GameObject.Find("Enemy (6)"); //find all enemies
		Debug.Log (enemies[6].name);
		weapons = GameObject.FindGameObjectsWithTag("Weapon");

		/*if (this.gameObject.GetComponent<Contestant>().equipped == null){
			//Debug.Log ("No Weapon Equipped");
			followingTarget = 0;
			for(int i = 0; i < weapons.Length; i++)
			{
				if(weapons[i].GetComponent<RangedWeapon>().ammo == 0 || weapons[i].GetComponent<Item>().equipper != null){
					//Debug.Log ("Weapon" + i);
					break;
				} else {
					distance = Vector3.Distance(weapons[i].transform.position, transform.position);//find distance between enemy and self
					//Debug.Log ("Weapon" + i);
					//Debug.Log ("Distance equals: " + distance);
					if (distance < closestDistance) {
						closestDistance = distance;
						//Debug.Log ("ClosestDistance equals: " + distance);
						closestWeapon = i; //for referencing the chosen enemy
					}
				}
			}
			Debug.Log (weapons[closestWeapon].name);
			targetPos = weapons[closestWeapon].transform.position;
			
		} else if (this.gameObject.GetComponent<Contestant>().equipped != null){
			*///Debug.Log ("Weapon Equipped");

		//}
	}

	void Update()
	{
		findClosestEnemy ();
		Debug.Log (targetPos.transform.position + "; and I'm " + this.name);
		StartCoroutine ("updateEnemyPath");
		/*
		if(followingTarget == 0){
			StartCoroutine("updateWeaponPath");
		} else if (followingTarget == 2) {
			StartCoroutine ("updateEnemyPath");
		} else if (followingTarget == 3) {
			StopCoroutine ("updateEnemyPath");
			StartCoroutine ("updateEnemyPath");
		}*/
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


	IEnumerator updateEnemyPath()
	{
		/*if (targetPos != enemies[closestEnemy].transform.position) {
			targetPos = enemies[closestEnemy].transform.position;
			followingTarget = 3;
		}*/
		PathRequester.RequestPath (this.transform.position, targetPos.transform.position, OnPathFound);
		yield return new WaitForSeconds (timer);
	}
	
	/*IEnumerator updateWeaponPath()
	{
		if (targetPos != weapons[closestWeapon].transform.position) {
			targetPos = weapons[closestWeapon].transform.position;
			followingTarget = 2;
		}
		PathRequester.RequestPath (transform.position, targetPos, OnPathFound);
		yield return new WaitForSeconds (timer);
	}*/


    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
			//Debug.Log("Path Successfully Found!");
            path = newPath;
			//Debug.Log ("Coroutine 'FollowPath' stopped!");
			StopCoroutine("FollowPath");

			//Debug.Log ("Coroutine 'FollowPath' started");
            StartCoroutine("FollowPath");
		}
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        
		while (true)
        {
			if (Vector3.Distance(transform.position,currentWaypoint) < 0.1f) // if distance from next waypoint from this objects position is less than 0.1f
            {
                targetIndex++; //increment targetIndex
                if (targetIndex >= path.Length)//if the targetIndex exceeds the length of the path array
                {
					targetIndex = 0;
					path = new Vector3[0];
                    yield break;
                }
					
                currentWaypoint = path[targetIndex];

            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
            yield return null;
        } 
		//targetIndex = 0;
    }

	public void findClosestEnemy() {
		closestDistance = 100.0f;

		for (int i = 0; i < enemies.Length; i++) {
			if (enemies [i].name == this.gameObject.name) {
				//Debug.Log(enemies [i].name + " == " + this.gameObject.name);
				continue;
			}
			distance = Vector3.Distance (enemies [i].transform.position, transform.position);//find distance between enemy and self
			//Debug.Log ("The closest enemy is " + distance + " away; and I'm " + this.name);
			if (distance <= closestDistance) {
				//Debug.Log (closestDistance + " is less than " + distance);
				closestDistance = distance;
				closestEnemy = i; //for referencing the chosen enemy
				//Debug.Log(closestEnemy);
			}
		}
		//distance = Vector3.Distance (enemies [closestEnemy].transform.position, transform.position);
		//Debug.Log ("My closest enemy is: " + enemies [closestEnemy].name + "; They're "+ distance + " away from me; and I'm " + this.name);

		targetPos = enemies[closestEnemy];
	}

	public void findClosestWeapon() {
		closestDistance = 100.0f;

		for (int i = 0; i < weapons.Length; i++) {
			if (weapons [i].GetComponent <RangedWeapon>().ammo == 0) {
				//Debug.Log(enemies [i].name + " == " + this.gameObject.name);
				continue;
			}
			distance = Vector3.Distance (enemies [i].transform.position, transform.position);//find distance between enemy and self
			//Debug.Log ("The closest enemy is " + distance + " away; and I'm " + this.name);
			if (distance <= closestDistance) {
				//Debug.Log (closestDistance + " is less than " + distance);
				closestDistance = distance;
				closestWeapon = i; //for referencing the chosen enemy
				//Debug.Log(closestEnemy);
			}
		}
		//distance = Vector3.Distance (enemies [closestEnemy].transform.position, transform.position);
		//Debug.Log ("My closest enemy is: " + enemies [closestEnemy].name + "; They're "+ distance + " away from me; and I'm " + this.name);

		targetPos = weapons[closestWeapon];
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
    }

}
