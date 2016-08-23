using UnityEngine;
using System.Collections;

public class ItemPools : MonoBehaviour {
	public GameObject[] Basic_Weapons_Pool; //Pool of all the basic weapons, ALWAYS ACTIVE
	public GameObject[] Prismex_Pool; //Sponsor 1 Weapon Pool, ACITVE IF SELECTED
	public GameObject[] Explodena_Pool; //Sponsor 2 Weapon Pool, ACTIVE IF SELECTED
    public GameObject[] Velocitech_Pool; //Sponsor 3 Weapon Pool, ACTIVE IF SELECTED
    public GameObject[] Item_Pool; //Item Pool Containing the health kit, ACTIVE BY DEFAULT
	public GameObject[] HealthkitPrismex; //Item pool containing the health kit and the prismex item, ACTIVE IF SELECTED
	public GameObject[] HealthkitExplodena; //Item pool containing the health kit and the explodena item, ACTIVE IF SELECTED
	public GameObject[] HealthkitVelocitech; //Item pool containing the health kit and the velocitech item, ACTIVE IF SELECTED
	public GameObject[] NoHealthkitPrismex; //Item pool containing only the prismex item. ACTIVE IF UPGRADE IS SELECTED AND SPONSOR IS SELECTED
	public GameObject[] NoHealthkitExplodena; //Item pool containing only the explodena item, ACTIVE IF UPGRADE IS SELECTED AND SPONSOR IS SELECTED
	public GameObject[] NoHealthkitVelocitech; //Item pool containing only the velocitech item, ACTIVE IF UPGRADE IS SELECTED AND SPONSOR IS SELECTED
	public Pool BasicWeapons;
	public Pool sponsor0;
	public Pool sponsor1;
    public Pool sponsor2;
	public Pool sponsor0HealthKit;
	public Pool sponsor0NoHealthKit;
	public Pool sponsor1HealthKit;
	public Pool sponsor1NoHealthKit;
	public Pool sponsor2HealthKit;
	public Pool sponsor2NoHealthKit;
    public Pool item;

	// Use this for initialization
	void Awake () {
		BasicWeapons = new Pool(Basic_Weapons_Pool);
		sponsor0 = new Pool(Prismex_Pool);
		sponsor1 = new Pool(Explodena_Pool);
        sponsor2 = new Pool(Velocitech_Pool);
		if ((StaticGameStats.TierTwoUpgrades [2]) && StaticGameStats.chosenSponsor == 0) {
			item = new Pool (NoHealthkitPrismex);
		} else if ((StaticGameStats.TierTwoUpgrades [2]) && StaticGameStats.chosenSponsor == 1) {
			item = new Pool (NoHealthkitExplodena);
		} else if ((StaticGameStats.TierTwoUpgrades [2]) && StaticGameStats.chosenSponsor == 2) {
			item = new Pool (NoHealthkitVelocitech);
		} else if (StaticGameStats.chosenSponsor == 0) {
			item = new Pool (HealthkitPrismex);
		} else if (StaticGameStats.chosenSponsor == 1) {
			item = new Pool (HealthkitExplodena);
		} else if (StaticGameStats.chosenSponsor == 2) {
			item = new Pool (HealthkitVelocitech);
		} else {
			item = new Pool (Item_Pool);
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
