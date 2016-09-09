using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {
	public enum poolselection{Example, Sponsor, Item};
	public poolselection selection;
	public ItemPools.Pool pool;
	public bool autoSpawn;
	public bool ready;
	public float readyTime;
	public GameObject UI_Card;
	public bool announce = true;

	public float timer;
	UI_GenericCard tracker;

	// Use this for initialization
	void Start () {
		switch(selection){
			case poolselection.Example:
			pool = FindObjectOfType<ItemPools>().BasicWeapons;
				break;

			case poolselection.Sponsor:
				switch(StaticGameStats.sponsor){
					case 0:
						pool = FindObjectOfType<ItemPools>().sponsor0;
						break;
					case 1:
						pool = FindObjectOfType<ItemPools>().sponsor1;
						break;
					case 2:
						pool = FindObjectOfType<ItemPools> ().sponsor2;
						break;
				}
				break;

			case poolselection.Item:
				pool = FindObjectOfType<ItemPools>().item;
				break;
		}
		GameObject spawned = (GameObject)Instantiate(UI_Card);
		spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		tracker = spawned.GetComponent<UI_GenericCard>();
		tracker.target = transform.parent;
		timer = 0;
		//Spawn();

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
				if(autoSpawn){
					Spawn();
				}
			}
			tracker.text = "";
		}
		else{
			if(Vector3.Distance(FindObjectOfType<PlayerController>().transform.position, transform.position) < 3 && !autoSpawn){
				if(announce){
					FindObjectOfType<InterfaceManager>().Announce("Press [E] to dispense weapon", 0.2f);
				}
				if(Input.GetKeyDown(KeyCode.E)){
					Spawn();
				}
			}
			if(!autoSpawn){
				tracker.text = "READY";
			}
		}
	
	}

	public GameObject Spawn(){
		FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().vendor, transform.position, 1.0f, true);
		ready = false;
		GameObject spawned = (GameObject)Instantiate(pool.items[Random.Range(0,pool.items.Length)],this.transform.position,Quaternion.identity);
		spawned.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere*10);
		return spawned;
	}
}
