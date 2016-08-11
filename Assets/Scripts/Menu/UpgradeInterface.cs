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

		StaticGameStats.SponsorUpgrade [0] = false;
		StaticGameStats.SponsorUpgrade [1] = false;
		StaticGameStats.SponsorUpgrade [2] = false;

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

	//TIER ONE UPGRADES

	public void BuyTierOneUpgradeOne(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.TierOneUpgrades[0] = true;
			upgradebuttons[0].interactable = false;
		}
	}

	public void BuyTierOneUpgradeTwo(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.TierOneUpgrades[1] = true;
			upgradebuttons[1].interactable = false;
		}
	}

	public void BuyTierOneUpgradeThree(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.TierOneUpgrades[2] = true;
			upgradebuttons[2].interactable = false;
		}
	}

	public void BuyTierOneUpgradeFour(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.TierOneUpgrades[3] = true;
			upgradebuttons[3].interactable = false;
		}
	}




	//TIER TWO UPGRADES

	public void BuyTierTwoUpgradeOne(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.TierTwoUpgrades[0] = true;
			upgradebuttons[4].interactable = false;
		}
	}

	public void BuyTierTwoUpgradeTwo(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.TierTwoUpgrades[1] = true;
			upgradebuttons[5].interactable = false;
		}
	}

	public void BuyTierTwoUpgradeThree(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.TierTwoUpgrades[2] = true;
			upgradebuttons[6].interactable = false;
		}
	}

	public void BuyTierTwoUpgradeFour(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.TierTwoUpgrades[3] = true;
			upgradebuttons[7].interactable = false;
		}
	}




	//TIER THREE UPGRADES

	public void BuyTierThreeUpgradeOne(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.TierThreeUpgrades[0] = true;
			upgradebuttons[8].interactable = false;
		}
	}

	public void BuyTierThreeUpgradeTwo(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.TierThreeUpgrades[1] = true;
			upgradebuttons[9].interactable = false;
		}
	}

	public void BuyTierThreeUpgradeThree(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.TierThreeUpgrades[2] = true;
			upgradebuttons[10].interactable = false;
		}
	}

	public void BuyTierThreeUpgradeFour(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.TierThreeUpgrades[3] = true;
			upgradebuttons[11].interactable = false;
		}
	}




	//ABILITIES

	public void BuyAbilityOne(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.Abilites[0] = true;
			upgradebuttons[12].interactable = false;
		}
	}

	public void BuyAbilityTwo(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.Abilites[1] = true;
			upgradebuttons[13].interactable = false;
		}
	}

	public void BuyAbilityThree(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.Abilites[2] = true;
			upgradebuttons[14].interactable = false;
		}
	}

	public void BuyAbilityFour(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.Abilites[3] = true;
			upgradebuttons[15].interactable = false;
		}
	}

	//SPONSOR UPGRADES

	public void BuySponsorOne(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.SponsorUpgrade[0] = true;
			upgradebuttons[16].interactable = false;
		}
	}

	public void BuySponsorTwo(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.SponsorUpgrade[1] = true;
			upgradebuttons[17].interactable = false;
		}
	}

	public void BuySponsorThree(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			StaticGameStats.SponsorUpgrade[2] = true;
			upgradebuttons[18].interactable = false;
		}
	}
}
