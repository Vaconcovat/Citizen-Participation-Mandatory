using UnityEngine;
using System.Collections;

public class Snapshot : MonoBehaviour {

	public void FundingSnapshot()
	{
		StaticGameStats.embezzleHolder = StaticGameStats.embezzledMoney;
		StaticGameStats.moneyHolder = StaticGameStats.avaliableMoney;
	}
}
