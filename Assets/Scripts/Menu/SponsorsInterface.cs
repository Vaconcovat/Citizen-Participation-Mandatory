using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SponsorsInterface : MonoBehaviour {

	public Text moneyText, sponsorText, directory;
	public Button signMegaCity, signExplodena, signVelocitech, commitButton;
	public Sprite[] sponsorGunLogos;
	public int activeSponsor;
	public int chosenSponsor;
	public int moneyHolder;
	public Button[] upgradebuttons;



	// Use this for initialization
	void Start () {
		chosenSponsor = -1;
		activeSponsor = 0;
		moneyHolder = StaticGameStats.instance.moneyHolder;
	}

	// Update is called once per frame
	void Update () {
		moneyText.text = "Funding:" + StaticGameStats.instance.avaliableMoney.ToString();
		if(StaticGameStats.instance.avaliableMoney == 0 && StaticGameStats.instance.chosenSponsor != -1){
			commitButton.interactable = true;
		}
		else{
			commitButton.interactable = false;
		}
		directory.text = @"G:\GovorNet\" + StaticGameStats.instance.PlayerName + @"\PLANNING\SPONSORS.gov";
	}


	public void SignMegaCity(){
		StaticGameStats.instance.activeSponsor = 0;
		StaticGameStats.instance.chosenSponsor = 0;
		signMegaCity.interactable = false;
		signVelocitech.interactable = true;
		signExplodena.interactable = true;
	}

	public void SignExplodena(){
		StaticGameStats.instance.activeSponsor = 1;
		StaticGameStats.instance.chosenSponsor = 1;
		signExplodena.interactable = false;
		signMegaCity.interactable = true;
		signVelocitech.interactable = true;
	}

	public void SignVelocitech(){
		StaticGameStats.instance.activeSponsor = 2;
		StaticGameStats.instance.chosenSponsor = 2;
		signExplodena.interactable = true;
		signMegaCity.interactable = true;
		signVelocitech.interactable = false;

	}
}
