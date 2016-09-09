using UnityEngine;
using System.Collections;

public class Snapshot : MonoBehaviour {

	public void FundingSnapshot()
	{
		StaticGameStats.moneyHolder = StaticGameStats.avaliableMoney;
	}
}
