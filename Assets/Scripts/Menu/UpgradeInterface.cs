using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeInterface : MonoBehaviour {

	public Text moneyText, directory;
	public Button commitButton;
	public Button[] upgradebuttons;
	public int moneyHolder;
	public static bool[] buttonActive = new bool[12];
	public AutoType at;
	Color greenColor;
	Color greyColor;
	Color whiteColor;
	Color blackColor;
	Color darkGreyColor;
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

	bool highlightColourChange;




	// Use this for initialization
	void Start () {
		moneyHolder = StaticGameStats.instance.moneyHolder;

		/// <summary>
		/// This sets 16 values in buttonActive to false 
		/// </summary>
		for (int i = 0; i < buttonActive.Length; i++) {
			buttonActive [i] = false;
		}

		whiteColor = Color.white;
		greenColor = Color.green;
		greyColor = Color.grey;
		blackColor = Color.black;
		darkGreyColor.r = 0.34f;
		darkGreyColor.b = 0.34f;
		darkGreyColor.g = 0.34f;
		darkGreyColor.a = 1;
		highlightColourChange = false;
	}

	// Update is called once per frame
	void Update () {
		
		moneyText.text = "Total Available Funding:" + StaticGameStats.instance.avaliableMoney.ToString();
		if(StaticGameStats.instance.avaliableMoney == 0 && StaticGameStats.instance.chosenSponsor != -1){
			commitButton.interactable = true;
		}
		else{
			commitButton.interactable = false;
		}
		directory.text = @"G:\GovorNet\" + StaticGameStats.instance.PlayerName + @"\PLANNING\UPGRADES.gov";

		if (!highlightColourChange) {
			highlightColourChangeAtStart ();
			highlightColourChange = true;
		}
	}



	//TIER ONE UPGRADES

	public void BuyTierOneUpgradeOne(){
		if (!buttonActive [0]) {
			if (StaticGameStats.instance.avaliableMoney >= upgrade1Cost) {
				StaticGameStats.instance.avaliableMoney -= upgrade1Cost;
				StaticGameStats.instance.TierOneUpgrades [0] = true;
				ChangeColorToGreen(0);
				buttonActive [0] = true;

			}
		} else {
			StaticGameStats.instance.avaliableMoney += upgrade1Cost;
			StaticGameStats.instance.TierOneUpgrades [0] = false;
			buttonActive [0] = false;
			ChangeColorToGrey (0);
			Deselect (0);
		}
	}

	public void BuyTierOneUpgradeTwo(){
		if (!buttonActive [1]) {
			if (StaticGameStats.instance.avaliableMoney >=upgrade2Cost) {
				StaticGameStats.instance.avaliableMoney -=upgrade2Cost;
				StaticGameStats.instance.TierOneUpgrades [1] = true;
				ChangeColorToGreen(1);
				buttonActive [1] = true;

			}
		} else {
			StaticGameStats.instance.avaliableMoney +=upgrade2Cost;
			StaticGameStats.instance.TierOneUpgrades [1] = false;
			buttonActive [1] = false;
			ChangeColorToGrey (1);
			Deselect (1);
		}
	}

	public void BuyTierOneUpgradeThree(){
		if (!buttonActive [2]) {
			if (StaticGameStats.instance.avaliableMoney >=upgrade3Cost) {
				StaticGameStats.instance.avaliableMoney -=upgrade3Cost;
				StaticGameStats.instance.TierOneUpgrades [2] = true;
				StaticGameStats.instance.Upgrade3ReputationGainBuff = 1.05f;
				ChangeColorToGreen(2);
				buttonActive [2] = true;

			}
		} else {
			StaticGameStats.instance.avaliableMoney +=upgrade3Cost;
			StaticGameStats.instance.TierOneUpgrades [2] = false;
			StaticGameStats.instance.Upgrade3ReputationGainBuff = 1.0f;
			buttonActive [2] = false;
			ChangeColorToGrey (2);
			Deselect (2);
		}
	}
		
	//TIER TWO UPGRADES

	public void BuyTierTwoUpgradeOne(){
		if (!buttonActive [3]) {
			if (StaticGameStats.instance.avaliableMoney >=upgrade4Cost) {
				StaticGameStats.instance.avaliableMoney -=upgrade4Cost;
				StaticGameStats.instance.TierTwoUpgrades [0] = true;
				ChangeColorToGreen(3);
				buttonActive [3] = true;

			}
		} else {
			StaticGameStats.instance.avaliableMoney +=upgrade4Cost;
			StaticGameStats.instance.TierTwoUpgrades [0] = false;
			buttonActive [3] = false;
			ChangeColorToGrey (3);
			Deselect (3);
		}
	}

	public void BuyTierTwoUpgradeTwo(){
		if (!buttonActive [4]) {
			if (StaticGameStats.instance.avaliableMoney >=upgrade5Cost) {
				StaticGameStats.instance.avaliableMoney -=upgrade5Cost;
				StaticGameStats.instance.TierTwoUpgrades [1] = true;
				ChangeColorToGreen(4);
				buttonActive [4] = true;
			}
		} else {
			StaticGameStats.instance.avaliableMoney +=upgrade5Cost;
			StaticGameStats.instance.TierTwoUpgrades [1] = false;
			buttonActive [4] = false;
			ChangeColorToGrey (4);
			Deselect (4);
		}
	}

	public void BuyTierTwoUpgradeThree(){
		if (!buttonActive [5]) {
			if (StaticGameStats.instance.avaliableMoney >=upgrade6Cost) {
				StaticGameStats.instance.avaliableMoney -=upgrade6Cost;
				StaticGameStats.instance.TierTwoUpgrades [2] = true;
				ChangeColorToGreen(5);
				buttonActive [5] = true;

			}
		} else {
			StaticGameStats.instance.avaliableMoney +=upgrade6Cost;
			StaticGameStats.instance.TierTwoUpgrades [2] = false;
			buttonActive [5] = false;
			ChangeColorToGrey (5);
			Deselect (5);
		}
	}

	//TIER THREE UPGRADES

	public void BuyTierThreeUpgradeOne(){
		if (!buttonActive [6]) {
			if (StaticGameStats.instance.avaliableMoney >=upgrade7Cost) {
				StaticGameStats.instance.avaliableMoney -=upgrade7Cost;
				StaticGameStats.instance.TierThreeUpgrades [0] = true;
				ChangeColorToGreen(6);
				buttonActive [6] = true;
			}
		} else {
			StaticGameStats.instance.avaliableMoney +=upgrade7Cost;
			StaticGameStats.instance.TierThreeUpgrades [0] = false;
			buttonActive [6] = false;
			ChangeColorToGrey (6);
			Deselect (6);
		}
		
	}

	//TIER FOUR UPGRADES

	public void BuyTierFourUpgradeOne(){
		if (!buttonActive [7]) {
			if (StaticGameStats.instance.avaliableMoney >=upgrade8Cost) {
				StaticGameStats.instance.avaliableMoney -=upgrade8Cost;
				StaticGameStats.instance.TierFourUpgrades [0] = true;
				ChangeColorToGreen(7);
				buttonActive [7] = true;

			}
		} else {
			StaticGameStats.instance.avaliableMoney +=upgrade8Cost;
			StaticGameStats.instance.TierFourUpgrades [0] = false;
			buttonActive [7] = false;
			ChangeColorToGrey (7);
			Deselect (7);
		}

	}

	//ABILITIES

	public void BuyAbilityOne(){
		if (!buttonActive [8]) {
			if (StaticGameStats.instance.avaliableMoney >=ability1Cost) {
				StaticGameStats.instance.avaliableMoney -=ability1Cost;
				StaticGameStats.instance.Abilites [0] = true;
				ColorBlock cb = upgradebuttons [8].colors;
				cb.normalColor = greenColor;
				cb.highlightedColor = greenColor;
				EventSystem.current.SetSelectedGameObject(null);
				upgradebuttons [8].colors = cb;
				buttonActive [8] = true;

			}
		} else {
			StaticGameStats.instance.avaliableMoney +=ability1Cost;
			StaticGameStats.instance.Abilites [0] = false;
			buttonActive [8] = false;
			ChangeColorToWhite (8);
			EventSystem.current.SetSelectedGameObject (null);
		}
	}

	public void BuyAbilityTwo(){
		if (!buttonActive [9]) {
			if (StaticGameStats.instance.avaliableMoney >=ability2Cost) {
				StaticGameStats.instance.avaliableMoney -=ability2Cost;
				StaticGameStats.instance.Abilites [1] = true;
				ColorBlock cb = upgradebuttons [9].colors;
				cb.normalColor = greenColor;
				cb.highlightedColor = greenColor;
				EventSystem.current.SetSelectedGameObject(null);
				upgradebuttons [9].colors = cb;
				buttonActive [9] = true;

			}
		} else {
			StaticGameStats.instance.avaliableMoney +=ability2Cost;
			StaticGameStats.instance.Abilites [1] = false;
			buttonActive [9] = false;
			ChangeColorToWhite (9);
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	public void BuyAbilityThree(){
		if (!buttonActive [10]) {
			if (StaticGameStats.instance.avaliableMoney >=ability3Cost) {
				StaticGameStats.instance.avaliableMoney -=ability3Cost;
				StaticGameStats.instance.Abilites [2] = true;
				ColorBlock cb = upgradebuttons [10].colors;
				cb.normalColor = greenColor;
				cb.highlightedColor = greenColor;
				EventSystem.current.SetSelectedGameObject(null);
				upgradebuttons [10].colors = cb;
				buttonActive [10] = true;

			}
		} else {
			StaticGameStats.instance.avaliableMoney +=ability3Cost;
			StaticGameStats.instance.Abilites [2] = false;
			buttonActive [10] = false;
			ChangeColorToWhite (10);
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	public void BuyAbilityFour(){
		if (!buttonActive [11]) {
			if (StaticGameStats.instance.avaliableMoney >=ability4Cost) {
				StaticGameStats.instance.avaliableMoney -=ability4Cost;
				StaticGameStats.instance.Abilites [3] = true;
				ColorBlock cb = upgradebuttons [11].colors;
				cb.normalColor = greenColor;
				cb.highlightedColor = greenColor;
				EventSystem.current.SetSelectedGameObject(null);
				upgradebuttons [11].colors = cb;
				buttonActive [11] = true;

			}
		} else {
			StaticGameStats.instance.avaliableMoney +=ability4Cost;
			StaticGameStats.instance.Abilites [3] = false;
			buttonActive [11] = false;
			ChangeColorToWhite (11);
			EventSystem.current.SetSelectedGameObject(null);
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
		cb.highlightedColor = darkGreyColor;
		upgradebuttons [num].colors = cb;
		upgradebuttons [num].transform.GetChild (0).GetComponent<Text> ().color = whiteColor;
	}

	void ChangeColorToWhite(int num) {
		ColorBlock cb = upgradebuttons [num].colors;
		cb.normalColor = whiteColor;
		cb.highlightedColor = darkGreyColor;
		upgradebuttons [num].colors = cb;
	}

	public void Upgrade(){
		at.StartType();
	}

	void highlightColourChangeAtStart(){
		int i = 0;
		foreach (Button upgrade in upgradebuttons) {
			ColorBlock cb = upgrade.colors;
			cb.highlightedColor = darkGreyColor;
			if (i > 7) {
				cb.normalColor = whiteColor;
				cb.highlightedColor = greyColor;
			}
			upgrade.colors = cb;
			i++;
		}
	}

	void Deselect(int num){
		upgradebuttons [num].interactable = false;
		upgradebuttons [num].interactable = true;
	}
}