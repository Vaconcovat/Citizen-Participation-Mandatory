using UnityEngine;
using System.Collections;

public class CameramanSpawner : MonoBehaviour {

	public GameObject cameraman;
	float spawnchance;

	// Use this for initialization
	void Start () {
		if(StaticGameStats.rebUpgrades[0]){
			spawnchance = 0.75f;
		}
		else{
			spawnchance = 0.5f;
		}
		GetComponent<SpriteRenderer>().enabled = false;
		if(Random.value < spawnchance){
			Instantiate(cameraman, transform.position, transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
