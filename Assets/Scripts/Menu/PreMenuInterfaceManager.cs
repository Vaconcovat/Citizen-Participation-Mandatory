using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PreMenuInterfaceManager : MonoBehaviour {

	public Text moneyText, embezText, sponsorText;
	public Image gunIcon, gunIcon2;
	public Button sponsor1Button, sponsor2Button, signedButton, commitButton;
	public Button[] upgradebuttons;
	public Sprite[] sponsorGunLogos;
	public int activeSponsor;
	public int chosenSponsor;


	// Use this for initialization
	void Start () {
		chosenSponsor = -1;
		activeSponsor = 0;
		StaticGameStats.generalUpgrades[0] = false;
		StaticGameStats.govUpgrades[0] = false;
		StaticGameStats.corUpgrades[0] = false;
		StaticGameStats.rebUpgrades[0] = false;
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = "Funding:\n" + StaticGameStats.avaliableMoney.ToString();
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

	public void Commit(){
		StaticGameStats.committed = true;
		StaticGameStats.sponsor = chosenSponsor;
		//do upgrades here
		FindObjectOfType<MenuCamera>().ZoomedOut();
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
