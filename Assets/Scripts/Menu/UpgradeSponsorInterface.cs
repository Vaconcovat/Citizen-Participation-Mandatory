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
	public Text Critical, Warning, UnspentMoney, UnchosenSponsor;

	Color greyColor;
	Color whiteColor;

	// Use this for initialization
	void Start () {
		chosenSponsor = StaticGameStats.instance.chosenSponsor;
		availableMoney = StaticGameStats.instance.avaliableMoney;
		greyColor = Color.grey;
		whiteColor = Color.white;
	}

	// Update is called once per frame
	void Update () {
		availableMoney = StaticGameStats.instance.avaliableMoney;
		chosenSponsor = StaticGameStats.instance.chosenSponsor;
		moneyText.text = "Total Available Funding:" + StaticGameStats.instance.avaliableMoney.ToString();
		if(chosenSponsor != -1){
			commitButton.interactable = true;
		}
		else{
			commitButton.interactable = false;
		}
		directory.text = @"G:\GovorNet\" + StaticGameStats.instance.PlayerName + @"\PLANNING.gov";

		if (chosenSponsor == -1) {
			Critical.enabled = true;
			UnchosenSponsor.enabled = true;
		} else {
			Critical.enabled = false;
			UnchosenSponsor.enabled = false;
		}

		if (availableMoney > 0) {
			Warning.enabled = true;
			UnspentMoney.enabled = true;
		} else {
			Warning.enabled = false;
			UnspentMoney.enabled = false;
		}
	}

	public void Revert(){
		StaticGameStats.instance.committed = false;
		StaticGameStats.instance.chosenSponsor = -1;
		StaticGameStats.instance.activeSponsor = 0;
		StaticGameStats.instance.TierOneUpgrades[0] = false;
		StaticGameStats.instance.TierOneUpgrades[1] = false;
		StaticGameStats.instance.TierOneUpgrades[2] = false;
		StaticGameStats.instance.TierTwoUpgrades[0] = false;
		StaticGameStats.instance.TierTwoUpgrades[1] = false;
		StaticGameStats.instance.TierTwoUpgrades[2] = false;
		StaticGameStats.instance.TierThreeUpgrades[0] = false;
		StaticGameStats.instance.TierFourUpgrades[0] = false;
		StaticGameStats.instance.Abilites[0] = false;
		StaticGameStats.instance.Abilites[1] = false;
		StaticGameStats.instance.Abilites[2] = false;
		StaticGameStats.instance.Abilites[3] = false;

		for (int i = 0; i <= 11; i++) {
			UpgradeInterface.buttonActive [i] = false;
			if (i <= 11) {
				ChangeColorToGrey (i);
			} else {
				ChangeColorToWhite (i);
			}
		}



		StaticGameStats.instance.avaliableMoney = StaticGameStats.instance.moneyHolder;
		moneyText.text = "Funding:" + StaticGameStats.instance.avaliableMoney.ToString();
		signExplodena.interactable = true;
		signMegaCity1.interactable = true;
        commitButton.interactable = false;
		//need to reset the signed graphics here as well
	}

	public void Commit(){
		StaticGameStats.instance.committed = true;
		StaticGameStats.instance.sponsor = StaticGameStats.instance.chosenSponsor;
		StaticGameStats.instance.Save();
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
