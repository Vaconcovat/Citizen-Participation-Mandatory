using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	public float useInterval;
	public GameObject player;
	float timer;

	// Use this for initialization
	void Start () {
		timer = useInterval;
		player = FindObjectOfType<PlayerController>().gameObject;
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0){
			GetComponent<Contestant>().UseEquipped(true);
			if (GetComponent<Contestant>().GetAmmo() == 0){
				GetComponent<Contestant>().ThrowEquipped();
			}
			timer = useInterval;
		}
		FacePlayer();
	}
	void FacePlayer(){
		Vector3 dir = player.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
