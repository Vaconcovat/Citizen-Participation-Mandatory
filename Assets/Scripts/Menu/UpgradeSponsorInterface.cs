using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeSponsorInterface : MonoBehaviour {

	public Text moneyText, embezText, sponsorText;
	public Image gunIcon, gunIcon2;
	public Button sponsor1Button, sponsor2Button, signedButton, commitButton;
	public Button[] upgradebuttons;
	public Sprite[] sponsorGunLogos;
	public int activeSponsor;
	public int chosenSponsor;
	public int availableMoney;

	// Use this for initialization
	void Start () {
		//TandC.interactable = false;
		chosenSponsor = StaticGameStats.chosenSponsor;
		activeSponsor = 0;
		availableMoney = StaticGameStats.avaliableMoney;
		StaticGameStats.generalUpgrades[0] = false;
		StaticGameStats.govUpgrades[0] = false;
		StaticGameStats.corUpgrades[0] = false;
		StaticGameStats.rebUpgrades[0] = false;
	}

	// Update is called once per frame
	void Update () {
		availableMoney = StaticGameStats.avaliableMoney;
		chosenSponsor = StaticGameStats.chosenSponsor;
		moneyText.text = "Funding:" + StaticGameStats.avaliableMoney.ToString();
		embezText.text = StaticGameStats.embezzledMoney.ToString();
		if(availableMoney == 0 && chosenSponsor != -1){
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

	public void Revert(){
		StaticGameStats.committed = false;
		StaticGameStats.chosenSponsor = -1;
		StaticGameStats.activeSponsor = 0;
		StaticGameStats.generalUpgrades[0] = false;
		StaticGameStats.govUpgrades[0] = false;
		StaticGameStats.corUpgrades[0] = false;
		StaticGameStats.rebUpgrades[0] = false;
		upgradebuttons[0].interactable = true;
		upgradebuttons[1].interactable = true;
		upgradebuttons[2].interactable = true;
		upgradebuttons[3].interactable = true;
		StaticGameStats.avaliableMoney = StaticGameStats.moneyHolder;
		StaticGameStats.embezzledMoney = StaticGameStats.embezzleHolder;
		moneyText.text = "Funding:" + StaticGameStats.avaliableMoney.ToString();
		embezText.text = StaticGameStats.embezzledMoney.ToString();
		signedButton.interactable = true;
		sponsor1Button.interactable = true;
		sponsor2Button.interactable = true;
	}

	public void Commit(){
		StaticGameStats.committed = true;
		StaticGameStats.sponsor = StaticGameStats.chosenSponsor;
		//do upgrades here
		if((StaticGameStats.govRep >= 100 || StaticGameStats.corRep >= 100 || StaticGameStats.rebRep >= 100)&&StaticGameStats.embezzledMoney >= 100){
			FindObjectOfType<MenuCamera>().Win();
		}
		else{
			FindObjectOfType<MenuCamera>().ZoomedOut();
		}

	}
}
