using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeInterface : MonoBehaviour {

	public Text moneyText, embezText, directory;
	public Button commitButton;
	public Button[] upgradebuttons;
	public int moneyHolder;
	public int embezzledHolder;
	bool[] buttonActive;
	int numOfButtons; //Make sure you update this if the number of buttons change from 16
	Color greenColor;
	Color greyColor;
	Color whiteColor;





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

		moneyHolder = StaticGameStats.moneyHolder;
		embezzledHolder = StaticGameStats.embezzleHolder;

		/// <summary>
		/// This sets 16 values in buttonActive to false 
		/// </summary>
		numOfButtons = 16;
		buttonActive = new bool[16];
		for (int i = 0; i < numOfButtons; i++) {
			buttonActive [i] = false;
		}

		whiteColor = Color.white;
		greenColor = Color.green;
		greyColor = Color.grey;
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
		directory.text = @"G:\GovorNet\" + StaticGameStats.PlayerName + @"\PLANNING\UPGRADES.gov";
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
		if (!buttonActive [0]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierOneUpgrades [0] = true;
				ChangeColorToGreen(0);
				buttonActive [0] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierOneUpgrades [0] = false;
			buttonActive [0] = false;
			ChangeColorToGrey (0);
		}
	}

	public void BuyTierOneUpgradeTwo(){
		if (!buttonActive [1]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierOneUpgrades [1] = true;
				ChangeColorToGreen(1);
				buttonActive [1] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierOneUpgrades [1] = false;
			buttonActive [1] = false;
			ChangeColorToGrey (1);
		}
	}

	public void BuyTierOneUpgradeThree(){
		if (!buttonActive [2]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierOneUpgrades [2] = true;
				ChangeColorToGreen(2);
				buttonActive [2] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierOneUpgrades [2] = false;
			buttonActive [2] = false;
			ChangeColorToGrey (2);
		}
	}

	public void BuyTierOneUpgradeFour(){
		if (!buttonActive [3]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierOneUpgrades [3] = true;
				ChangeColorToGreen(3);
				buttonActive [3] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierOneUpgrades [3] = false;
			buttonActive [3] = false;
			ChangeColorToGrey (3);
		}
	}




	//TIER TWO UPGRADES

	public void BuyTierTwoUpgradeOne(){
		if (!buttonActive [4]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierTwoUpgrades [0] = true;
				ChangeColorToGreen(4);
				buttonActive [4] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierTwoUpgrades [0] = false;
			buttonActive [4] = false;
			ChangeColorToGrey (4);
		}
	}

	public void BuyTierTwoUpgradeTwo(){
		if (!buttonActive [5]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierTwoUpgrades [1] = true;
				ChangeColorToGreen(5);
				buttonActive [5] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierTwoUpgrades [1] = false;
			buttonActive [5] = false;
			ChangeColorToGrey (5);
		}
	}

	public void BuyTierTwoUpgradeThree(){
		if (!buttonActive [6]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierTwoUpgrades [2] = true;
				ChangeColorToGreen(6);
				buttonActive [6] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierTwoUpgrades [2] = false;
			buttonActive [6] = false;
			ChangeColorToGrey (6);
		}
	}

	public void BuyTierTwoUpgradeFour(){
		if (!buttonActive [8]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierTwoUpgrades [3] = true;
				ChangeColorToGreen(8);
				buttonActive [8] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierTwoUpgrades [3] = false;
			buttonActive [8] = false;
			ChangeColorToGrey (8);
		}
	}




	//TIER THREE UPGRADES

	public void BuyTierThreeUpgradeOne(){
		if (!buttonActive [8]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierThreeUpgrades [0] = true;
				ChangeColorToGreen(8);
				buttonActive [8] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierThreeUpgrades [0] = false;
			buttonActive [8] = false;
			ChangeColorToGrey (8);
		}
		
	}

	public void BuyTierThreeUpgradeTwo(){
		if (!buttonActive [9]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierThreeUpgrades [1] = true;
				ChangeColorToGreen(9);
				buttonActive [9] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierThreeUpgrades [1] = false;
			buttonActive [9] = false;
			ChangeColorToGrey (9);
		}
	}

	public void BuyTierThreeUpgradeThree(){
		if (!buttonActive [10]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierThreeUpgrades [2] = true;
				ChangeColorToGreen(10);
				buttonActive [10] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierThreeUpgrades [2] = false;
			buttonActive [10] = false;
			ChangeColorToGrey (10);
		}
	}

	public void BuyTierThreeUpgradeFour(){
		if (!buttonActive [11]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierThreeUpgrades [3] = true;
				ChangeColorToGreen(11);
				buttonActive [11] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierThreeUpgrades [3] = false;
			buttonActive [11] = false;
			ChangeColorToGrey (11);
		}
	}




	//ABILITIES

	public void BuyAbilityOne(){
		if (!buttonActive [12]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierThreeUpgrades [0] = true;
				ChangeColorToGreen(12);
				buttonActive [12] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierThreeUpgrades [0] = false;
			buttonActive [12] = false;
			ChangeColorToWhite (12);
		}
	}

	public void BuyAbilityTwo(){
		if (!buttonActive [13]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierThreeUpgrades [1] = true;
				ChangeColorToGreen(13);
				buttonActive [13] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierThreeUpgrades [1] = false;
			buttonActive [13] = false;
			ChangeColorToWhite (13);
		}
	}

	public void BuyAbilityThree(){
		if (!buttonActive [14]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierThreeUpgrades [2] = true;
				ChangeColorToGreen(14);
				buttonActive [14] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierThreeUpgrades [2] = false;
			buttonActive [14] = false;
			ChangeColorToWhite (14);
		}
	}

	public void BuyAbilityFour(){
		if (!buttonActive [15]) {
			if (StaticGameStats.avaliableMoney >= 4) {
				StaticGameStats.avaliableMoney -= 4;
				StaticGameStats.TierThreeUpgrades [3] = true;
				ChangeColorToGreen(15);
				buttonActive [15] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += 4;
			StaticGameStats.TierThreeUpgrades [3] = false;
			buttonActive [15] = false;
			ChangeColorToWhite (15);
		}
	}

	void ChangeColorToGreen(int num) {
		ColorBlock cb = upgradebuttons [num].colors;
		cb.normalColor = greenColor;
		cb.highlightedColor = greenColor;
		EventSystem.current.SetSelectedGameObject(null);
		upgradebuttons [num].colors = cb;
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