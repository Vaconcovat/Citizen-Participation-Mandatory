using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SponsorsInterface : MonoBehaviour {

	public Text moneyText, embezText, sponsorText;
	public Button signMegaCity, signExplodena, signVelocitech, commitButton;
	public Sprite[] sponsorGunLogos;
	public int activeSponsor;
	public int chosenSponsor;
	public int moneyHolder;
	public int embezzledHolder;
	public Button[] upgradebuttons;



	// Use this for initialization
	void Start () {
		chosenSponsor = -1;
		activeSponsor = 0;
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

	public void SignMegaCity(){
		StaticGameStats.activeSponsor = 0;
		StaticGameStats.chosenSponsor = 0;
		signMegaCity.interactable = false;
		signVelocitech.interactable = true;
		signExplodena.interactable = true;
		ResetSponsorUpgrades ();
	}

	public void SignExplodena(){
		StaticGameStats.activeSponsor = 1;
		StaticGameStats.chosenSponsor = 1;
		signExplodena.interactable = false;
		signMegaCity.interactable = true;
		signVelocitech.interactable = true;
		ResetSponsorUpgrades ();
	}

	public void SignVelocitech(){
		StaticGameStats.activeSponsor = 2;
		StaticGameStats.chosenSponsor = 2;
		signExplodena.interactable = true;
		signMegaCity.interactable = true;
		signVelocitech.interactable = false;
		ResetSponsorUpgrades ();

	}

	void ResetSponsorUpgrades(){
		if (StaticGameStats.MegaCity1SponsorUpgrade [0] == true) {
			StaticGameStats.MegaCity1SponsorUpgrade [0] = false;
			StaticGameStats.avaliableMoney = StaticGameStats.avaliableMoney + 4;
		}

		if (StaticGameStats.MegaCity1SponsorUpgrade [1] == true) {
			StaticGameStats.MegaCity1SponsorUpgrade [1] = false;
			StaticGameStats.avaliableMoney = StaticGameStats.avaliableMoney + 4;
		}

		if (StaticGameStats.MegaCity1SponsorUpgrade [2] == true) {
			StaticGameStats.MegaCity1SponsorUpgrade [2] = false;
			StaticGameStats.avaliableMoney = StaticGameStats.avaliableMoney + 4;
		}

		//Explodena Sponsor Upgrades

		if (StaticGameStats.ExplodenaSponsorUpgrade [0] == true) {
			StaticGameStats.ExplodenaSponsorUpgrade [0] = false;
			StaticGameStats.avaliableMoney = StaticGameStats.avaliableMoney + 4;
		}

		if (StaticGameStats.ExplodenaSponsorUpgrade [1] == true) {
			StaticGameStats.ExplodenaSponsorUpgrade [1] = false;
			StaticGameStats.avaliableMoney = StaticGameStats.avaliableMoney + 4;
		}

		if (StaticGameStats.ExplodenaSponsorUpgrade [2] == true) {
			StaticGameStats.ExplodenaSponsorUpgrade [2] = false;
			StaticGameStats.avaliableMoney = StaticGameStats.avaliableMoney + 4;
		}

		// Velocitech Sponsor Upgrades

		if (StaticGameStats.VelocitechSponsorUpgrade [0] == true) {
			StaticGameStats.VelocitechSponsorUpgrade [0] = false;
			StaticGameStats.avaliableMoney = StaticGameStats.avaliableMoney + 4;
		}

		if (StaticGameStats.VelocitechSponsorUpgrade [1] == true) {
			StaticGameStats.VelocitechSponsorUpgrade [1] = false;
			StaticGameStats.avaliableMoney = StaticGameStats.avaliableMoney + 4;
		}

		if (StaticGameStats.VelocitechSponsorUpgrade [2] == true) {
			StaticGameStats.VelocitechSponsorUpgrade [2] = false;
			StaticGameStats.avaliableMoney = StaticGameStats.avaliableMoney + 4;
		}
	}
}
