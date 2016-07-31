using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeInterface : MonoBehaviour {

	public Text moneyText, embezText;
	public Button commitButton;
	public Button[] upgradebuttons;
	public int moneyHolder;
	public int embezzledHolder;



	// Use this for initialization
	void Start () {
		StaticGameStats.generalUpgrades[0] = false;
		StaticGameStats.govUpgrades[0] = false;
		StaticGameStats.corUpgrades[0] = false;
		StaticGameStats.rebUpgrades[0] = false;
		moneyHolder = StaticGameStats.moneyHolder;
		embezzledHolder = StaticGameStats.embezzleHolder;
	}

	// Update is called once per frame
	void Update () {
		moneyText.text = "Funding:" + StaticGameStats.avaliableMoney.ToString();
		embezText.text = StaticGameStats.embezzledMoney.ToString();
		if(StaticGameStats.avaliableMoney == 0 && StaticGameStats.chosenSponsor != -1){
			commitButton.interactable = true;
		}
		else{
			commitButton.interactable = false;
		}
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

	public void BuyGenericUpgrade1(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.generalUpgrades[0] = true;
			upgradebuttons[0].interactable = false;
		}
	}

	public void BuyGovUpgrade1(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.govUpgrades[0] = true;
			upgradebuttons[1].interactable = false;
		}
	}

	public void BuySponsorUpgrade1(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.corUpgrades[0] = true;
			upgradebuttons[2].interactable = false;
		}
	}

	public void BuyRebelUpgrade1(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.rebUpgrades[0] = true;
			upgradebuttons[3].interactable = false;
		}
	}
}
