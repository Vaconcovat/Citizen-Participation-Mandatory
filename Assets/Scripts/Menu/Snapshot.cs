using UnityEngine;
using System.Collections;

public class Snapshot : MonoBehaviour {

	public void FundingSnapshot()
	{
		StaticGameStats.instance.moneyHolder = StaticGameStats.instance.avaliableMoney;
	}
}
