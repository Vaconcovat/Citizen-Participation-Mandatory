using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	public float useInterval;
	public GameObject player;
	float timer;
	//-------------We don't need to store any of these, as we need to retrieve them every frame anyway.
	//Quaternion playerRotation;
	//GameObject thisPlayer;
	//Vector2 thisPlayerPos;
	//Vector2 playerDirection;
	//Transform myMuzzle;

	// Use this for initialization
	void Start () {
		timer = useInterval;
		player = FindObjectOfType<PlayerController>().gameObject;
		//----------This seems to be redundant?
		//thisPlayer = this.player;

		//---------if you were to store these, they'd need to be retrieved on update because the transform can change on any given frame.
		//playerRotation = thisPlayer.transform.rotation;
		//playerDirection = playerRotation*Vector2.right;
		//playerDirection.y = playerRotation [1];
		//playerDirection = playerDirection.normalized;
		//thisPlayerPos = thisPlayer.transform.position;

	}

	// Update is called once per frame
	void Update () {
		//Visualize the raycast
		Debug.DrawRay(transform.position, transform.right * 50.0f);
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
}
