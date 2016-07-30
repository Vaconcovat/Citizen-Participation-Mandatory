using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeSponsorInterface : MonoBehaviour {

	public Text moneyText, embezText;

	// Update is called once per frame
	void Update () {
		moneyText.text = "Funding:" + StaticGameStats.avaliableMoney.ToString();
		embezText.text = StaticGameStats.embezzledMoney.ToString();
	}

	public void AddMoney(){
		if(StaticGameStats.avaliableMoney >= 1){
			StaticGameStats.avaliableMoney--;
			StaticGameStats.embezzledMoney++;
		}
	}

	public void AddMoney5(){
		if(StaticGameStats.avaliableMoney >= 5){
			for (int i = 0; i <= 4; i++) {
				StaticGameStats.avaliableMoney--;
				StaticGameStats.embezzledMoney++;
			}
		}
	}

	public void AddMoney10(){
		if(StaticGameStats.avaliableMoney >= 10){
			for (int i = 0; i <= 9; i++) {
				StaticGameStats.avaliableMoney--;
				StaticGameStats.embezzledMoney++;
			}	
		}
	}
}
