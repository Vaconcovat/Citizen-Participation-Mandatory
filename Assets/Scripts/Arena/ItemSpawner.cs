﻿using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {
	public enum poolselection{Example, Sponsor, Item};
	public poolselection selection;
	public ItemPools.Pool pool;
	public bool respawning;
	public float timer;

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

		if(selection == poolselection.Sponsor){
			GameObject spawned = (GameObject)Instantiate(pool.items[Random.Range(0,pool.items.Length)],this.transform.position,Quaternion.identity);
			spawned.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere*10);
		}
		if(respawning){
			StartCoroutine("respawn");
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator respawn(){
		while(true){
			GameObject spawned = (GameObject)Instantiate(pool.items[Random.Range(0,pool.items.Length)],this.transform.position,Quaternion.identity);
			spawned.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere*10);
			yield return new WaitForSeconds(timer);
		}
	}
}
