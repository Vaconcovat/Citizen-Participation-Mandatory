using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {
	public enum poolselection{Example, Sponsor, Item};
	public poolselection selection;
	public ItemPools.Pool pool;
	public bool autoSpawn;
	public int SpawnerCooldown;
	public bool ready;
	public GameObject UI_Card;
	public bool announce = true;
	public int SpawnsRemaining;
	int ContestantsRemaining;
	InterfaceManager im;

	public float timer;
	UI_GenericCard tracker;

	void Awake(){
		im = FindObjectOfType<InterfaceManager>();
	}
	// Use this for initialization
	void Start () {
		ContestantsRemaining = FindObjectOfType<RoundManager> ().aliveContestants;
		switch(selection){
			case poolselection.Example:
				pool = FindObjectOfType<ItemPools>().BasicWeapons;
				break;

			case poolselection.Sponsor:
				switch(StaticGameStats.instance.sponsor){
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
		if (StaticGameStats.instance.TierThreeUpgrades [0]) {
			if (selection == poolselection.Item) {
				SpawnerCooldown = SpawnerCooldown + 15;
			} 
		}
		GameObject spawned = (GameObject)Instantiate(UI_Card);
		spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		tracker = spawned.GetComponent<UI_GenericCard>();
		tracker.target = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		ContestantsRemaining = FindObjectOfType<RoundManager> ().aliveContestants;
		if(!ready){ //if the vendor is not ready to spawn an item
			if(timer > 0){
				timer -= Time.deltaTime;
			}
			else{ //if the timer is 0
				/* Old Spawner System
				 * timer = SpawnerCooldown;
				 * ready = true;
				 * if(autoSpawn){
				 * 		Spawn();
				 * }
				 * */
				switch (selection) {
				case(poolselection.Item): //if the vendor is an item vendor
					timer = SpawnerCooldown;
					ready = true;
					if (autoSpawn) {
						Spawn();
					}
					break;
				case(poolselection.Example): //if the vendor is a weapon vendor
					timer = SpawnerCooldown;
					ready = true;
					if (autoSpawn) {
						Spawn();
					}
					break;
				case poolselection.Sponsor: //if the vendor is a sponsor vendor
					switch (SpawnsRemaining) {
					case (4): //if there are 4 spawns remaining, just spawn the weapon
						timer = SpawnerCooldown;
						ready = true;
						Spawn ();
						SpawnsRemaining--;
						break;
					case (3): //if there are 3 spawns remaining, check that there are 6 or less contestants
						if (ContestantsRemaining <= 6) {
							timer = SpawnerCooldown;
							ready = true;
							Spawn ();
							im.Announce ("6 Contestants Remaining - Sponsor Weapon Spawned", 5);
							SpawnsRemaining--;
						}
						break;
					case (2): //if there are 2 spawns remaining, check that there are 4 or less contestants
						if (ContestantsRemaining <= 4) {
							timer = SpawnerCooldown;
							ready = true;
							Spawn ();
							im.Announce ("4 Contestants Remaining - Sponsor Weapon Spawned", 5);
							SpawnsRemaining--;
						}
						break;
					case (1): //if there is 1 spawn remaining, check that there are 2 or less contestants
						if (ContestantsRemaining <= 2) {
							timer = SpawnerCooldown;
							ready = true;
							Spawn ();
							im.Announce ("2 Contestants Remaining - Sponsor Weapon Spawned", 5);
							SpawnsRemaining--;
						}
						break;
					default: //if there are 0 spawns remaining, do nothing
						break;
					}
					break;
				}
			}
			tracker.text = "";
		}
		else{
			if(Vector3.Distance(FindObjectOfType<PlayerController>().transform.position, transform.position) < 3 && !autoSpawn){
				if(announce){
					switch (selection) {
					case(poolselection.Item):
						FindObjectOfType<InterfaceManager> ().Announce ("Press [E] to dispense Item", 0.2f);
						break;
					case(poolselection.Example):
						FindObjectOfType<InterfaceManager> ().Announce ("Press [E] to dispense Weapon", 0.2f);
						break;
					case poolselection.Sponsor:
						break;
					}
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
