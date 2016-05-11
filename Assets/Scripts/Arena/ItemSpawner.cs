using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {
	public enum poolselection{Example, Sponsor, Item};
	public poolselection selection;
	public ItemPools.Pool pool;

	// Use this for initialization
	void Start () {
		switch(selection){
			case poolselection.Example:
				pool = FindObjectOfType<ItemPools>().example;
				break;
			case poolselection.Sponsor:
				pool = FindObjectOfType<ItemPools>().sponsor;
				break;
			case poolselection.Item:
				pool = FindObjectOfType<ItemPools>().item;
				break;
		}
		GameObject spawned = (GameObject)Instantiate(pool.items[Random.Range(0,pool.items.Length)],this.transform.position,Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
