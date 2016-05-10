using UnityEngine;
using System.Collections;

public class ItemPools : MonoBehaviour {
	public GameObject[] Example_Pool;
	public GameObject[] Sponsor_Pool;
	public GameObject[] Item_Pool;
	public Pool example;
	public Pool sponsor;
	public Pool item;

	// Use this for initialization
	void Awake () {
		example = new Pool(Example_Pool);
		sponsor = new Pool(Sponsor_Pool);
		item = new Pool(Item_Pool);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public class Pool{
		public GameObject[] items;

		//constructor
		public Pool(GameObject[] items){
			this.items = items;
		}
	}
}
