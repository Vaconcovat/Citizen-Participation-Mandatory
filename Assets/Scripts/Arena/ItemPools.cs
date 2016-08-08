using UnityEngine;
using System.Collections;

public class ItemPools : MonoBehaviour {
	public GameObject[] Example_Pool;
	public GameObject[] Sponsor0_Pool;
	public GameObject[] Sponsor1_Pool;
	public GameObject[] Item_Pool;
	public GameObject[] NoHealthKitItem_Pool;
	public Pool example;
	public Pool sponsor0;
	public Pool sponsor1;
	public Pool item;

	// Use this for initialization
	void Awake () {
		example = new Pool(Example_Pool);
		sponsor0 = new Pool(Sponsor0_Pool);
		sponsor1 = new Pool(Sponsor1_Pool);
		if (StaticGameStats.TierTwoUpgrades [2]) {
			item = new Pool (NoHealthKitItem_Pool);
		} else {
			item = new Pool(Item_Pool);
		}

	}


	public class Pool{
		public GameObject[] items;

		//constructor
		public Pool(GameObject[] items){
			this.items = items;
		}
	}
}
