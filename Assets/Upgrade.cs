using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour {

	public int UpgradeCost;
	public static int Cost;

	void Start(){
		Cost = UpgradeCost;
	}

}
