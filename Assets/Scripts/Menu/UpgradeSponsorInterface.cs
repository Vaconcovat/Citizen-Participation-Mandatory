using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeSponsorInterface : MonoBehaviour {

	public Text moneyText, embezText, sponsorText, directory;
	public Image gunIcon, gunIcon2;
	public Button signMegaCity1, signExplodena, commitButton;
	public Button[] upgradebuttons;
	public Sprite[] sponsorGunLogos;
	public int chosenSponsor;
	public int availableMoney;

	Color greyColor;
	Color whiteColor;

	// Use this for initialization
	void Start () {
		chosenSponsor = StaticGameStats.chosenSponsor;
		availableMoney = StaticGameStats.avaliableMoney;
		greyColor = Color.grey;
		whiteColor = Color.white;
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

		for (int i = 0; i < 16; i++) {
			UpgradeInterface.buttonActive [i] = false;
			if (i <= 11) {
				ChangeColorToGrey (i);
			} else {
				ChangeColorToWhite (i);
			}
		}



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

	void ChangeColorToGrey(int num) {
		ColorBlock cb = upgradebuttons [num].colors;
		cb.normalColor = greyColor;
		cb.highlightedColor = greyColor;
		upgradebuttons [num].colors = cb;
	}

	void ChangeColorToWhite(int num) {
		ColorBlock cb = upgradebuttons [num].colors;
		cb.normalColor = whiteColor;
		cb.highlightedColor = whiteColor;
		upgradebuttons [num].colors = cb;
	}
}
