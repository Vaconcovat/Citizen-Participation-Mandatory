using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeInterface : MonoBehaviour {

	public Text moneyText, directory;
	public Button commitButton;
	public Button[] upgradebuttons;
	public int moneyHolder;
	public static bool[] buttonActive = new bool[11];
	public AutoType at;
	Color greenColor;
	Color greyColor;
	Color whiteColor;
	Color blackColor;
	[Header("Upgrade Costs")]
	public int upgrade1Cost = 2; //Description
	public int upgrade2Cost = 2; //Description
	public int upgrade3Cost = 2; //Description
	public int upgrade4Cost = 3; //Description
	public int upgrade5Cost = 3; //Description
	public int upgrade6Cost = 3; //Description
	public int upgrade7Cost = 4; //Description
	public int upgrade8Cost = 5; //Description
	[Header("Ability Costs")]
	public int ability1Cost = 4; //Description
	public int ability2Cost = 4; //Description
	public int ability3Cost = 4; //Description
	public int ability4Cost = 4; //Description




	// Use this for initialization
	void Start () {
		moneyHolder = StaticGameStats.moneyHolder;

		/// <summary>
		/// This sets 16 values in buttonActive to false 
		/// </summary>
		for (int i = 0; i <= buttonActive.Length; i++) {
			buttonActive [i] = false;
		}

		whiteColor = Color.white;
		greenColor = Color.green;
		greyColor = Color.grey;
		blackColor = Color.black;
	}

	// Update is called once per frame
	void Update () {
		moneyText.text = "Total Available Funding:" + StaticGameStats.avaliableMoney.ToString();
		if(StaticGameStats.avaliableMoney == 0 && StaticGameStats.chosenSponsor != -1){
			commitButton.interactable = true;
		}
		else{
			commitButton.interactable = false;
		}
		directory.text = @"G:\GovorNet\" + StaticGameStats.PlayerName + @"\PLANNING\UPGRADES.gov";
	}



	//TIER ONE UPGRADES

	public void BuyTierOneUpgradeOne(){
		if (!buttonActive [0]) {
			if (StaticGameStats.avaliableMoney >= upgrade1Cost) {
				StaticGameStats.avaliableMoney -= upgrade1Cost;
				StaticGameStats.TierOneUpgrades [0] = true;
				ChangeColorToGreen(0);
				buttonActive [0] = true;

			}
		} else {
			StaticGameStats.avaliableMoney += upgrade1Cost;
			StaticGameStats.TierOneUpgrades [0] = false;
			buttonActive [0] = false;
			ChangeColorToGrey (0);
		}
	}

	public void BuyTierOneUpgradeTwo(){
		if (!buttonActive [1]) {
			if (StaticGameStats.avaliableMoney >=upgrade2Cost) {
				StaticGameStats.avaliableMoney -=upgrade2Cost;
				StaticGameStats.TierOneUpgrades [1] = true;
				ChangeColorToGreen(1);
				buttonActive [1] = true;

			}
		} else {
			StaticGameStats.avaliableMoney +=upgrade2Cost;
			StaticGameStats.TierOneUpgrades [1] = false;
			buttonActive [1] = false;
			ChangeColorToGrey (1);
		}
	}

	public void BuyTierOneUpgradeThree(){
		if (!buttonActive [2]) {
			if (StaticGameStats.avaliableMoney >=upgrade3Cost) {
				StaticGameStats.avaliableMoney -=upgrade3Cost;
				StaticGameStats.TierOneUpgrades [2] = true;
				StaticGameStats.Upgrade3ReputationGainBuff = 1.05f;
				ChangeColorToGreen(2);
				buttonActive [2] = true;

			}
		} else {
			StaticGameStats.avaliableMoney +=upgrade3Cost;
			StaticGameStats.TierOneUpgrades [2] = false;
			StaticGameStats.Upgrade3ReputationGainBuff = 1.0f;
			buttonActive [2] = false;
			ChangeColorToGrey (2);
		}
	}
		
	//TIER TWO UPGRADES

	public void BuyTierTwoUpgradeOne(){
		if (!buttonActive [3]) {
			if (StaticGameStats.avaliableMoney >=upgrade4Cost) {
				StaticGameStats.avaliableMoney -=upgrade4Cost;
				StaticGameStats.TierTwoUpgrades [0] = true;
				ChangeColorToGreen(3);
				buttonActive [3] = true;

			}
		} else {
			StaticGameStats.avaliableMoney +=upgrade4Cost;
			StaticGameStats.TierTwoUpgrades [0] = false;
			buttonActive [3] = false;
			ChangeColorToGrey (3);
		}
	}

	public void BuyTierTwoUpgradeTwo(){
		if (!buttonActive [4]) {
			if (StaticGameStats.avaliableMoney >=upgrade5Cost) {
				StaticGameStats.avaliableMoney -=upgrade5Cost;
				StaticGameStats.TierTwoUpgrades [1] = true;
				ChangeColorToGreen(4);
				buttonActive [4] = true;

			}
		} else {
			StaticGameStats.avaliableMoney +=upgrade5Cost;
			StaticGameStats.TierTwoUpgrades [1] = false;
			buttonActive [4] = false;
			ChangeColorToGrey (4);
		}
	}

	public void BuyTierTwoUpgradeThree(){
		if (!buttonActive [5]) {
			if (StaticGameStats.avaliableMoney >=upgrade6Cost) {
				StaticGameStats.avaliableMoney -=upgrade6Cost;
				StaticGameStats.TierTwoUpgrades [2] = true;
				ChangeColorToGreen(5);
				buttonActive [5] = true;

			}
		} else {
			StaticGameStats.avaliableMoney +=upgrade6Cost;
			StaticGameStats.TierTwoUpgrades [2] = false;
			buttonActive [5] = false;
			ChangeColorToGrey (5);
		}
	}

	//TIER THREE UPGRADES

	public void BuyTierThreeUpgradeOne(){
		if (!buttonActive [6]) {
			if (StaticGameStats.avaliableMoney >=upgrade7Cost) {
				StaticGameStats.avaliableMoney -=upgrade7Cost;
				StaticGameStats.TierThreeUpgrades [0] = true;
				ChangeColorToGreen(6);
				buttonActive [6] = true;

			}
		} else {
			StaticGameStats.avaliableMoney +=upgrade7Cost;
			StaticGameStats.TierThreeUpgrades [0] = false;
			buttonActive [6] = false;
			ChangeColorToGrey (6);
		}
		
	}

	//TIER FOUR UPGRADES

	public void BuyTierFourUpgradeOne(){
		if (!buttonActive [7]) {
			if (StaticGameStats.avaliableMoney >=upgrade8Cost) {
				StaticGameStats.avaliableMoney -=upgrade8Cost;
				StaticGameStats.TierThreeUpgrades [0] = true;
				ChangeColorToGreen(7);
				buttonActive [7] = true;

			}
		} else {
			StaticGameStats.avaliableMoney +=upgrade8Cost;
			StaticGameStats.TierThreeUpgrades [0] = false;
			buttonActive [7] = false;
			ChangeColorToGrey (7);
		}

	}

	//ABILITIES

	public void BuyAbilityOne(){
		if (!buttonActive [8]) {
			if (StaticGameStats.avaliableMoney >=ability1Cost) {
				StaticGameStats.avaliableMoney -=ability1Cost;
				StaticGameStats.Abilites [0] = true;
				ColorBlock cb = upgradebuttons [8].colors;
				cb.normalColor = greenColor;
				cb.highlightedColor = greenColor;
				EventSystem.current.SetSelectedGameObject(null);
				upgradebuttons [8].colors = cb;
				buttonActive [8] = true;

			}
		} else {
			StaticGameStats.avaliableMoney +=ability1Cost;
			StaticGameStats.Abilites [0] = false;
			buttonActive [8] = false;
			ColorBlock cb = upgradebuttons [8].colors;
			cb.normalColor = whiteColor;
			cb.highlightedColor = whiteColor;
			EventSystem.current.SetSelectedGameObject(null);
			upgradebuttons [8].colors = cb;
		}
	}

	public void BuyAbilityTwo(){
		if (!buttonActive [9]) {
			if (StaticGameStats.avaliableMoney >=ability2Cost) {
				StaticGameStats.avaliableMoney -=ability2Cost;
				StaticGameStats.Abilites [1] = true;
				ColorBlock cb = upgradebuttons [9].colors;
				cb.normalColor = greenColor;
				cb.highlightedColor = greenColor;
				EventSystem.current.SetSelectedGameObject(null);
				upgradebuttons [9].colors = cb;
				buttonActive [9] = true;

			}
		} else {
			StaticGameStats.avaliableMoney +=ability2Cost;
			StaticGameStats.Abilites [1] = false;
			buttonActive [9] = false;
			ColorBlock cb = upgradebuttons [9].colors;
			cb.normalColor = whiteColor;
			cb.highlightedColor = whiteColor;
			EventSystem.current.SetSelectedGameObject(null);
			upgradebuttons [9].colors = cb;
		}
	}

	public void BuyAbilityThree(){
		if (!buttonActive [10]) {
			if (StaticGameStats.avaliableMoney >=ability3Cost) {
				StaticGameStats.avaliableMoney -=ability3Cost;
				StaticGameStats.Abilites [2] = true;
				ColorBlock cb = upgradebuttons [10].colors;
				cb.normalColor = greenColor;
				cb.highlightedColor = greenColor;
				EventSystem.current.SetSelectedGameObject(null);
				upgradebuttons [10].colors = cb;
				buttonActive [10] = true;

			}
		} else {
			StaticGameStats.avaliableMoney +=ability3Cost;
			StaticGameStats.Abilites [2] = false;
			buttonActive [10] = false;
			ColorBlock cb = upgradebuttons [10].colors;
			cb.normalColor = whiteColor;
			cb.highlightedColor = whiteColor;
			EventSystem.current.SetSelectedGameObject(null);
			upgradebuttons [10].colors = cb;
		}
	}

	public void BuyAbilityFour(){
		if (!buttonActive [11]) {
			if (StaticGameStats.avaliableMoney >=ability4Cost) {
				StaticGameStats.avaliableMoney -=ability4Cost;
				StaticGameStats.Abilites [3] = true;
				ColorBlock cb = upgradebuttons [11].colors;
				cb.normalColor = greenColor;
				cb.highlightedColor = greenColor;
				EventSystem.current.SetSelectedGameObject(null);
				upgradebuttons [11].colors = cb;
				buttonActive [11] = true;

			}
		} else {
			StaticGameStats.avaliableMoney +=ability4Cost;
			StaticGameStats.Abilites [3] = false;
			buttonActive [11] = false;
			ColorBlock cb = upgradebuttons [11].colors;
			cb.normalColor = whiteColor;
			cb.highlightedColor = whiteColor;
			EventSystem.current.SetSelectedGameObject(null);
			upgradebuttons [11].colors = cb;
		}
	}

	void ChangeColorToGreen(int num) {
		ColorBlock cb = upgradebuttons [num].colors;
		cb.normalColor = greenColor;
		cb.highlightedColor = greenColor;
		EventSystem.current.SetSelectedGameObject(null);
		upgradebuttons [num].colors = cb;
		upgradebuttons [num].transform.GetChild (0).GetComponent<Text> ().color = blackColor;
	}

	void ChangeColorToGrey(int num) {
		ColorBlock cb = upgradebuttons [num].colors;
		cb.normalColor = greyColor;
		cb.highlightedColor = greyColor;
		upgradebuttons [num].colors = cb;
		upgradebuttons [num].transform.GetChild (0).GetComponent<Text> ().color = whiteColor;
	}

	void ChangeColorToWhite(int num) {
		ColorBlock cb = upgradebuttons [num].colors;
		cb.normalColor = whiteColor;
		cb.highlightedColor = whiteColor;
		upgradebuttons [num].colors = cb;
	}

	public void Upgrade(){
		at.StartType();
	}

}