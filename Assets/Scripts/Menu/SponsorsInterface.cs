using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SponsorsInterface : MonoBehaviour {

	public Text moneyText, embezText, sponsorText;
	public Image gunIcon, gunIcon2;
	public Button sponsor1Button, sponsor2Button, signedButton, commitButton;
	public Button[] upgradebuttons;
	public Sprite[] sponsorGunLogos;
	public int activeSponsor;
	public int chosenSponsor;
	public int moneyHolder;
	public int embezzledHolder;



	// Use this for initialization
	void Start () {
		//TandC.interactable = false;
		chosenSponsor = -1;
		activeSponsor = 0;
		moneyHolder = StaticGameStats.avaliableMoney;
		embezzledHolder = StaticGameStats.embezzledMoney;
	}

	// Update is called once per frame
	void Update () {
		moneyText.text = "Funding:" + StaticGameStats.avaliableMoney.ToString();
		embezText.text = StaticGameStats.embezzledMoney.ToString();
		gunIcon.sprite = sponsorGunLogos[activeSponsor];
		gunIcon2.sprite = sponsorGunLogos[activeSponsor+2];
		if(StaticGameStats.avaliableMoney == 0 && chosenSponsor != -1){
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

	public void TakeMoney(){
		if(StaticGameStats.embezzledMoney > 0){
			StaticGameStats.embezzledMoney--;
			StaticGameStats.avaliableMoney++;
		}
	}

	public void SetSponsor1(){
		if (chosenSponsor == -1){
			activeSponsor = 0;
			sponsorText.text = "MEGA CITY 1";
		}
	}

	public void SetSponsor2(){
		if (chosenSponsor == -1){
			activeSponsor = 1;
			sponsorText.text = "EXPLODENA";
		}
	}

	public void Sign(){
		chosenSponsor = activeSponsor;
		signedButton.interactable = false;
		sponsor1Button.interactable = false;
		sponsor2Button.interactable = false;
	}


	public void Revert(){
		StaticGameStats.committed = false;
		chosenSponsor = -1;
		activeSponsor = 0;
		StaticGameStats.generalUpgrades[0] = false;
		StaticGameStats.govUpgrades[0] = false;
		StaticGameStats.corUpgrades[0] = false;
		StaticGameStats.rebUpgrades[0] = false;
		upgradebuttons[0].interactable = true;
		upgradebuttons[1].interactable = true;
		upgradebuttons[2].interactable = true;
		upgradebuttons[3].interactable = true;
		StaticGameStats.avaliableMoney = moneyHolder;
		StaticGameStats.embezzledMoney = embezzledHolder;
		signedButton.interactable = true;
		sponsor1Button.interactable = true;
		sponsor2Button.interactable = true;
	}

	public void Commit(){
		StaticGameStats.committed = true;
		StaticGameStats.sponsor = chosenSponsor;
		//do upgrades here
		if((StaticGameStats.govRep >= 100 || StaticGameStats.corRep >= 100 || StaticGameStats.rebRep >= 100)&&StaticGameStats.embezzledMoney >= 100){
			FindObjectOfType<MenuCamera>().Win();
		}
		else{
			FindObjectOfType<MenuCamera>().ZoomedOut();
		}

	}
}
