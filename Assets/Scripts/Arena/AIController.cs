using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	public float useInterval;
	public GameObject player;
	float timer;
	Quaternion playerRotation;
	GameObject thisPlayer;
	Vector2 thisPlayerPos;
	Vector2 playerDirection;
	Transform myMuzzle;

	// Use this for initialization
	void Start () {
		timer = useInterval;
		player = FindObjectOfType<PlayerController>().gameObject;
		thisPlayer = this.player;

		playerRotation = thisPlayer.transform.rotation;
		playerDirection = playerRotation*Vector2.right;
		//playerDirection.y = playerRotation [1];
		//playerDirection = playerDirection.normalized;
		thisPlayerPos = thisPlayer.transform.position;

	}

	// Update is called once per frame
	void Update () {
		//TO DO: CHANGE transform.position to be just beyond the end of the gun so the raycast doesn't count the player.
		RaycastHit2D hit = Physics2D.Raycast(thisPlayerPos, playerDirection, 50.0f);
		Debug.Log (playerDirection); // For testing only
		if (hit.collider != null) {
			//Debug.Log (hit.transform.gameObject.tag); // For testing only
			if (hit.transform.gameObject.tag  == "Contestant") {
				timer -= Time.deltaTime;
				if (timer <= 0){
					GetComponent<Contestant>().UseEquipped(true);
					if (GetComponent<Contestant>().GetAmmo() == 0){
						GetComponent<Contestant>().ThrowEquipped();
					}
					timer = useInterval;
				}
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
