using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeSponsorInterface : MonoBehaviour {

	public Text moneyText, sponsorText, directory;
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
		moneyText.text = "Total Available Funding:" + StaticGameStats.avaliableMoney.ToString();
		if(chosenSponsor != -1){
			commitButton.interactable = true;
		}
		else{
			commitButton.interactable = false;
		}
		directory.text = @"G:\GovorNet\" + StaticGameStats.PlayerName + @"\PLANNING.gov";
	}

	public void Revert(){
		StaticGameStats.committed = false;
		StaticGameStats.chosenSponsor = -1;
		StaticGameStats.activeSponsor = 0;
		StaticGameStats.TierOneUpgrades[0] = false;
		StaticGameStats.TierOneUpgrades[1] = false;
		StaticGameStats.TierOneUpgrades[2] = false;
		StaticGameStats.TierTwoUpgrades[0] = false;
		StaticGameStats.TierTwoUpgrades[1] = false;
		StaticGameStats.TierTwoUpgrades[2] = false;
		StaticGameStats.TierThreeUpgrades[0] = false;
		StaticGameStats.TierFourUpgrades[0] = false;
		StaticGameStats.Abilites[0] = false;
		StaticGameStats.Abilites[1] = false;
		StaticGameStats.Abilites[2] = false;
		StaticGameStats.Abilites[3] = false;

		for (int i = 0; i < 11; i++) {
			UpgradeInterface.buttonActive [i] = false;
			if (i <= 11) {
				ChangeColorToGrey (i);
			} else {
				ChangeColorToWhite (i);
			}
		}



		StaticGameStats.avaliableMoney = StaticGameStats.moneyHolder;
		moneyText.text = "Funding:" + StaticGameStats.avaliableMoney.ToString();
		signExplodena.interactable = true;
		signMegaCity1.interactable = true;
        commitButton.interactable = false;
		//need to reset the signed graphics here as well
	}

	public void Commit(){
		StaticGameStats.committed = true;
		StaticGameStats.sponsor = StaticGameStats.chosenSponsor;
		FindObjectOfType<MenuCamera>().ZoomedOut();
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
