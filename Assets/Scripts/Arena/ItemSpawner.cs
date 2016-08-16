using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {
	public enum poolselection{Example, Sponsor, Item};
	public poolselection selection;
	public ItemPools.Pool pool;

	public bool ready;
	public float readyTime;

	float timer;

	// Use this for initialization
	void Start () {
		switch(selection){
			case poolselection.Example:
				pool = FindObjectOfType<ItemPools>().example;
				break;

			case poolselection.Sponsor:
				switch(StaticGameStats.sponsor){
					case 0:
						pool = FindObjectOfType<ItemPools>().sponsor0;
						break;
					case 1:
						pool = FindObjectOfType<ItemPools>().sponsor1;
						break;
				}
				break;

			case poolselection.Item:
				pool = FindObjectOfType<ItemPools>().item;
				break;
		}

		Spawn();

	}
	
	// Update is called once per frame
	void Update () {
		if(!ready){
			if(timer > 0){
				timer -= Time.deltaTime;
			}
			else{
				timer = readyTime;
				ready = true;
			}
		}
		else{
			if(Vector3.Distance(FindObjectOfType<PlayerController>().transform.position, transform.position) < 3){
				FindObjectOfType<InterfaceManager>().Announce("Press [E] to dispense weapon");
				if(Input.GetKeyDown(KeyCode.E)){
					Spawn();
					ready = false;
				}
			}
		}
	
	}

	public GameObject Spawn(){
		GameObject spawned = (GameObject)Instantiate(pool.items[Random.Range(0,pool.items.Length)],this.transform.position,Quaternion.identity);
		spawned.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere*10);
		return spawned;
	}
}
