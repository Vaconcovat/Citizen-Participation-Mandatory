using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeSponsorInterface : MonoBehaviour {

	public Text moneyText, embezText, sponsorText, directory;
	public Image gunIcon, gunIcon2;
	public Button signMegaCity1, signExplodena, commitButton;
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
		StaticGameStats.TierOneUpgrades[0] = false;
		StaticGameStats.TierOneUpgrades[1] = false;
		StaticGameStats.TierOneUpgrades[2] = false;
		StaticGameStats.TierOneUpgrades[3] = false;

		StaticGameStats.TierTwoUpgrades[0] = false;
		StaticGameStats.TierTwoUpgrades[1] = false;
		StaticGameStats.TierTwoUpgrades[2] = false;
		StaticGameStats.TierTwoUpgrades[3] = false;

		StaticGameStats.TierThreeUpgrades[0] = false;
		StaticGameStats.TierThreeUpgrades[1] = false;
		StaticGameStats.TierThreeUpgrades[2] = false;
		StaticGameStats.TierThreeUpgrades[3] = false;

		StaticGameStats.Abilites[0] = false;
		StaticGameStats.Abilites[1] = false;
		StaticGameStats.Abilites[2] = false;
		StaticGameStats.Abilites[3] = false;
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
		directory.text = @"G:\GovorNet\" + StaticGameStats.PlayerName + @"\PLANNING.gov";
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
		StaticGameStats.TierOneUpgrades[0] = false;
		StaticGameStats.TierOneUpgrades[1] = false;
		StaticGameStats.TierOneUpgrades[2] = false;
		StaticGameStats.TierOneUpgrades[3] = false;
		StaticGameStats.TierTwoUpgrades[0] = false;
		StaticGameStats.TierTwoUpgrades[1] = false;
		StaticGameStats.TierTwoUpgrades[2] = false;
		StaticGameStats.TierTwoUpgrades[3] = false;
		StaticGameStats.TierThreeUpgrades[0] = false;
		StaticGameStats.TierThreeUpgrades[1] = false;
		StaticGameStats.TierThreeUpgrades[2] = false;
		StaticGameStats.TierThreeUpgrades[3] = false;
		StaticGameStats.Abilites[0] = false;
		StaticGameStats.Abilites[1] = false;
		StaticGameStats.Abilites[2] = false;
		StaticGameStats.Abilites[3] = false;
		upgradebuttons[0].interactable = true;
		upgradebuttons[1].interactable = true;
		upgradebuttons[2].interactable = true;
		upgradebuttons[3].interactable = true;
		upgradebuttons[4].interactable = true;
		upgradebuttons[5].interactable = true;
		upgradebuttons[6].interactable = true;
		upgradebuttons[7].interactable = true;
		upgradebuttons[8].interactable = true;
		upgradebuttons[9].interactable = true;
		upgradebuttons[10].interactable = true;
		upgradebuttons[11].interactable = true;
		upgradebuttons[12].interactable = true;
		upgradebuttons[13].interactable = true;
		upgradebuttons[14].interactable = true;
		upgradebuttons[15].interactable = true;
		StaticGameStats.avaliableMoney = StaticGameStats.moneyHolder;
		StaticGameStats.embezzledMoney = StaticGameStats.embezzleHolder;
		moneyText.text = "Funding:" + StaticGameStats.avaliableMoney.ToString();
		embezText.text = StaticGameStats.embezzledMoney.ToString();
		signExplodena.interactable = true;
		signMegaCity1.interactable = true;
        commitButton.interactable = false;
		//need to reset the signed graphics here as well
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
