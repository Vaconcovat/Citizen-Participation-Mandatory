using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SponsorsInterface : MonoBehaviour {

	public Text moneyText, embezText, sponsorText;
	public Image gunIcon, gunIcon2;
	public Button sponsor1Button, sponsor2Button, signedButton, commitButton;
	public Sprite[] sponsorGunLogos;
	public int activeSponsor;
	public int chosenSponsor;
	public int moneyHolder;
	public int embezzledHolder;



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
		gunIcon.sprite = sponsorGunLogos[StaticGameStats.activeSponsor];
		gunIcon2.sprite = sponsorGunLogos[StaticGameStats.activeSponsor+2];
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

	public void SetSponsor1(){
			StaticGameStats.activeSponsor = 0;
			StaticGameStats.chosenSponsor = 0;
			sponsorText.text = "MEGA CITY 1";
	}

	public void SetSponsor2(){
			StaticGameStats.activeSponsor = 1;
			StaticGameStats.chosenSponsor = 1;
			sponsorText.text = "EXPLODENA";
	}

	public void Sign(){
		StaticGameStats.chosenSponsor = StaticGameStats.activeSponsor;
		signedButton.interactable = false;
		sponsor1Button.interactable = false;
		sponsor2Button.interactable = false;
	}
}
