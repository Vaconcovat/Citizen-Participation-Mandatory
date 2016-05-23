using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	public float useInterval;
	private GameObject player;
	float timer;
	private GameObject[] enemies;
	float distance;
	float closestDistance;
	int closestEnemy;

	// Use this for initialization
	void Start () {
		timer = useInterval;
		player = FindObjectOfType<PlayerController> ().gameObject;
		distance = 99.0f;
		closestDistance = 100.0f;
		enemies = new GameObject[7];

		enemies [0] = GameObject.Find ("Enemy"); //find all enemies
		//Debug.Log (enemies[0].name);
		enemies [1] = GameObject.Find ("Enemy (1)"); //find all enemies
		//Debug.Log (enemies[1].name);
		enemies [2] = GameObject.Find ("Enemy (2)"); //find all enemies
		//Debug.Log (enemies[2].name);
		enemies [3] = GameObject.Find ("Enemy (3)"); //find all enemies
		//Debug.Log (enemies[3].name);
		enemies [4] = GameObject.Find ("Enemy (4)"); //find all enemies
		//Debug.Log (enemies[4].name);
		enemies [5] = GameObject.Find ("Enemy (5)"); //find all enemies
		//Debug.Log (enemies[5].name);
		enemies [6] = GameObject.Find ("Enemy (6)"); //find all enemies
		//Debug.Log (enemies[6].name);




	}

	// Update is called once per frame
	void Update () {
		
		findClosestEnemy();
		//Visual	ize the raycast
		Debug.DrawRay(transform.position, transform.right * 30.0f);
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, 50.0f);
		//For all the colliders we hit
		for(int i = 0; i < hits.Length; i++){
			RaycastHit2D hit = hits[i];
			//If we hit ourselves, thor a debug message and skip
			if(hit.collider == this.gameObject.GetComponent<Collider2D>()){
				Debug.Log("Detected Myself");
				continue; //skips
			}
			//If we hit something else, stop looking through the hits.
			else{
				//If the something else we hit was a contestant, let's try to shoot.
				if (hit.transform.gameObject.tag  == "Contestant") {
					timer -= Time.deltaTime;
					if (timer <= 0){
						Debug.Log("Trying to use my item!");
						GetComponent<Contestant>().UseEquipped(true);
						if (GetComponent<Contestant>().GetAmmo() == 0){
							GetComponent<Contestant>().ThrowEquipped();
						}
						timer = useInterval;
					}
				}
				Debug.Log("Found " + hit.transform.tag.ToString() + " stopping looking");
				break; //ends the for loop
			}

		}
		FacePlayer();
	}

	void FacePlayer(){
		Vector3 dir = player.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

		player = enemies[closestEnemy];
	}
}
