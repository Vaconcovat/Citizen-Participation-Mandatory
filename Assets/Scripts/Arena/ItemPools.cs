using UnityEngine;
using System.Collections;

public class ItemPools : MonoBehaviour {
	public GameObject[] Example_Pool;
	public GameObject[] Sponsor0_Pool;
	public GameObject[] Sponsor1_Pool;
	public GameObject[] Item_Pool;
	public Pool example;
	public Pool sponsor0;
	public Pool sponsor1;
	public Pool item;

	// Use this for initialization
	void Awake () {
		example = new Pool(Example_Pool);
		item = new Pool(Item_Pool);
		sponsor0 = new Pool(Sponsor0_Pool);
		sponsor1 = new Pool(Sponsor1_Pool);
	}


	public class Pool{
		public GameObject[] items;

		//constructor
		public Pool(GameObject[] items){
			this.items = items;
		}
	}
}
