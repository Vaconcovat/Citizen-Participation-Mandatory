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

		StaticGameStats.MegaCity1SponsorUpgrade [0] = false;
		StaticGameStats.MegaCity1SponsorUpgrade [1] = false;
		StaticGameStats.MegaCity1SponsorUpgrade [2] = false;

		StaticGameStats.ExplodenaSponsorUpgrade [0] = false;
		StaticGameStats.ExplodenaSponsorUpgrade [1] = false;
		StaticGameStats.ExplodenaSponsorUpgrade [2] = false;

		StaticGameStats.ExplodenaSponsorUpgrade [0] = false;
		StaticGameStats.ExplodenaSponsorUpgrade [1] = false;
		StaticGameStats.ExplodenaSponsorUpgrade [2] = false;

		upgradebuttons [16].interactable = false;
		upgradebuttons [17].interactable = false;
		upgradebuttons [18].interactable = false;

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

	public void UnlockSponsorUpgrades(){
		//Sponsor Upgrade 1
		if (StaticGameStats.chosenSponsor == -1) {
			Debug.Log ("Sponsor was invalid");
			upgradebuttons [16].interactable = false;
		} else if (StaticGameStats.MegaCity1SponsorUpgrade [0] == true){
			Debug.Log ("MC1Selected");
			upgradebuttons [16].interactable = false;
		} else if (StaticGameStats.ExplodenaSponsorUpgrade [0] == true){
			Debug.Log ("E1Selected");
			upgradebuttons [16].interactable = false;
		} else if (StaticGameStats.VelocitechSponsorUpgrade [0] == true){
			Debug.Log ("V1Selected");
			upgradebuttons [16].interactable = false;
		} else {
			Debug.Log ("All Good");
			upgradebuttons [16].interactable = true;
		}

		//Sponsor Upgrade 2
		if (StaticGameStats.chosenSponsor == -1) {
			Debug.Log ("Sponsor was invalid");
			upgradebuttons [17].interactable = false;
		} else if (StaticGameStats.MegaCity1SponsorUpgrade [1] == true){
			Debug.Log ("MC2Selected");
			upgradebuttons [17].interactable = false;
		} else if (StaticGameStats.ExplodenaSponsorUpgrade [1] == true){
			Debug.Log ("E2Selected");
			upgradebuttons [17].interactable = false;
		} else if (StaticGameStats.VelocitechSponsorUpgrade [1] == true){
			Debug.Log ("V2Selected");
			upgradebuttons [17].interactable = false;
		} else {
			Debug.Log ("All Good");
			upgradebuttons [17].interactable = true;
		}

		//Sponsor Upgrade 3
		if (StaticGameStats.chosenSponsor == -1) {
			Debug.Log ("Sponsor was invalid");
			upgradebuttons [18].interactable = false;
		} else if (StaticGameStats.MegaCity1SponsorUpgrade [2] == true){
			Debug.Log ("MC3Selected");
			upgradebuttons [18].interactable = false;
		} else if (StaticGameStats.ExplodenaSponsorUpgrade [2] == true){
			Debug.Log ("E3Selected");
			upgradebuttons [18].interactable = false;
		} else if (StaticGameStats.VelocitechSponsorUpgrade [2] == true){
			Debug.Log ("V3Selected");
			upgradebuttons [18].interactable = false;
		} else {
			Debug.Log ("All Good");
			upgradebuttons [18].interactable = true;
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
			upgradebuttons[16].interactable = false;
			switch (StaticGameStats.chosenSponsor) {
			case 0:
				Debug.Log ("I am setting True");
				StaticGameStats.MegaCity1SponsorUpgrade [0] = true;
				break;
			case 1:
				StaticGameStats.ExplodenaSponsorUpgrade [0] = true;
				break;
			case 2:
				StaticGameStats.VelocitechSponsorUpgrade [0] = true;
				break;
			}
		}
	}

	public void BuySponsorTwo(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			upgradebuttons[17].interactable = false;
			switch (StaticGameStats.chosenSponsor) {
			case 0:
				StaticGameStats.MegaCity1SponsorUpgrade [1] = true;
				break;
			case 1:
				StaticGameStats.ExplodenaSponsorUpgrade [1] = true;
				break;
			case 2:
				StaticGameStats.VelocitechSponsorUpgrade [1] = true;
				break;
			}
		}
	}

	public void BuySponsorThree(){
		if (StaticGameStats.avaliableMoney >= 4){
			StaticGameStats.avaliableMoney -= 4;
			upgradebuttons[18].interactable = false;
			switch (StaticGameStats.chosenSponsor) {
			case 0:
				StaticGameStats.MegaCity1SponsorUpgrade [2] = true;
				break;
			case 1:
				StaticGameStats.ExplodenaSponsorUpgrade [2] = true;
				break;
			case 2:
				StaticGameStats.VelocitechSponsorUpgrade [2] = true;
				break;
			}
		}
	}
}