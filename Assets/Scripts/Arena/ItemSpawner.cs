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

		if(StaticGameStats.corUpgrades[0] && selection == poolselection.Sponsor){
			Instantiate(pool.items[Random.Range(0,pool.items.Length)],new Vector2(transform.position.x, transform.position.y + 0.5f),Quaternion.identity);
			Instantiate(pool.items[Random.Range(0,pool.items.Length)],new Vector2(transform.position.x, transform.position.y - 0.5f),Quaternion.identity);
		}
		else{
			Instantiate(pool.items[Random.Range(0,pool.items.Length)],this.transform.position,Quaternion.identity);
		}
		GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
