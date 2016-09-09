using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PostMenuInterfaceManager : MonoBehaviour {
	[Header("UI objects")]
	//public bool wow;
	public Text govRepText; 
	public Text corRepText, rebRepText;
	public Text govMoneyText, corMoneyText, rebMoneyText;
	public Image govBar, corBar, rebBar;
	public Text totalMoney;
	public AutoType LoseText, WinText;
	public Image govBarOverlay, corBarOverlay, rebBarOverlay;
	public Image govBackground, corBackground, rebBackground;
	public GameObject WinButton, LoseButton, back, spend;
	public GameObject moneyobject;
	public Text NormalText, directory;
	public Text GovStatus, CorStatus, RebStatus;

	[Header("Settings")]
	public float lerpTime;
	public float[] thresholds;
	public int[] threshold_values;
	int govMoney, corMoney, rebMoney;
	int MoneyRecievedThisRound;
	float t;

	//pointers
	float gov_P, cor_P, reb_P;

	//changes
	float gov_change, cor_change, reb_change;

	// Use this for initialization
	void Start () {
		t = 0.0f;
		gov_P = StaticGameStats.oldgovRep;
		cor_P = StaticGameStats.oldcorRep;
		reb_P = StaticGameStats.oldrebRep;

		gov_change = StaticGameStats.govRep - StaticGameStats.oldgovRep;
		cor_change = StaticGameStats.corRep - StaticGameStats.oldcorRep;
		reb_change = StaticGameStats.rebRep - StaticGameStats.oldrebRep;

		//dish out the money
		govMoney = CheckThresholds(gov_change);
		corMoney = CheckThresholds(cor_change);
		rebMoney = CheckThresholds(reb_change);

		//Disable the Win / Lose Text and Buttons
		WinButton.SetActive(false);
		WinText.enabled = false;
		LoseButton.SetActive(false);
		LoseText.enabled = false;

		if (StaticGameStats.govRep > 80.0f){
			GovStatus.text = "Current Status:\t\tEVACUATION ORDER IMMINENT";
			GovStatus.color = Color.blue;
		} else if (StaticGameStats.govRep >= 75.0f) {
			GovStatus.text = "Current Status:\t\tPleased";
			GovStatus.color = Color.green;
		} else if (StaticGameStats.govRep >= 50.0f) {
			GovStatus.text = "Current Status:\t\tNeutral";
			GovStatus.color = Color.white;
		} else if (StaticGameStats.govRep > 25.0f){
			GovStatus.text = "Current Status:\t\tAngry";
			GovStatus.color = Color.red;
		} else if (StaticGameStats.govRep < 25.0f){
			GovStatus.text = "Current Status:\t\tEXECUTION ORDER IMMINENT";
			GovStatus.color = Color.yellow;
		}

		if (StaticGameStats.corRep > 80.0f){
			CorStatus.text = "Current Status:\t\tEVACUATION ORDER IMMINENT";
			CorStatus.color = Color.blue;
		} else if (StaticGameStats.corRep >= 75.0f) {
			CorStatus.text = "Current Status:\t\tPleased";
			CorStatus.color = Color.green;
		} else if (StaticGameStats.corRep >= 50.0f) {
			CorStatus.text = "Current Status:\t\tNeutral";
			CorStatus.color = Color.white;
		} else if (StaticGameStats.corRep > 25.0f){
			CorStatus.text = "Current Status:\t\tAngry";
			CorStatus.color = Color.red;
		} else if (StaticGameStats.corRep < 25.0f){
			CorStatus.text = "Current Status:\t\tEXECUTION ORDER IMMINENT";
			CorStatus.color = Color.yellow;
		}

		if (StaticGameStats.rebRep > 80.0f){
			RebStatus.text = "Current Status:\t\tEVACUATION ORDER IMMINENT";
			RebStatus.color = Color.blue;
		} else if (StaticGameStats.rebRep >= 75.0f) {
			RebStatus.text = "Current Status:\t\tPleased";
			RebStatus.color = Color.green;
		} else if (StaticGameStats.rebRep >= 50.0f) {
			RebStatus.text = "Current Status:\t\tNeutral";
			RebStatus.color = Color.white;
		} else if (StaticGameStats.rebRep > 25.0f){
			RebStatus.text = "Current Status:\t\tAngry";
			RebStatus.color = Color.red;
		} else if (StaticGameStats.rebRep < 25.0f){
			RebStatus.text = "Current Status:\t\tEXECUTION ORDER IMMINENT";
			RebStatus.color = Color.yellow;
		}

		MoneyRecievedThisRound = govMoney + corMoney + rebMoney;
		if (StaticGameStats.toPost) {
			StaticGameStats.avaliableMoney += MoneyRecievedThisRound;
		}
		totalMoney.text = "Funding Recived from Last Tournament: " + MoneyRecievedThisRound.ToString();

		//set the overlay bars
		govBarOverlay.fillAmount = gov_P / 100.0f;
		corBarOverlay.fillAmount = cor_P / 100.0f;
		rebBarOverlay.fillAmount = reb_P / 100.0f;


		if ((StaticGameStats.govRep <= 0) || (StaticGameStats.corRep <= 0) || (StaticGameStats.rebRep <= 0)){
			NormalText.enabled = false;
			LoseText.enabled = true;
			//LoseText.displayedText[0] = "You have recieved a 0% satisfaction report.\nDisciplinary actions will be administered.\n\nGovorNet Systems takes this matter very\nseriously. We do not issue warnings. You cannot\nmake an appeal to these consequences.\n\nPlease connect to the GovorNet Department of Re-Education\nfor decommision. Do not disconnect from GovorNet systems. \nDo not power off this machine.\n\nFailure to follow these instructions will be considered treason.";
			LoseText.StartType();
			LoseButton.SetActive(true);
			back.SetActive(false);
			spend.SetActive(false);
			moneyobject.SetActive(false);
		}

		else{
			if ((StaticGameStats.govRep >= 100) || (StaticGameStats.corRep >= 100) || (StaticGameStats.rebRep >= 100)){
				NormalText.enabled = false;
				WinText.enabled = true;
                //WinText.displayedText[0] = "You have recieved a 100% arena satisfaction report.\nOne or more RELOCATION OFFERS received for your efforts.\n\nGovorNet Systems takes this matter very\nseriously. We do not issue warnings. You cannot\nmake an appeal to these awards.\n\nPlease connect to the GovorNet Department of Ceremonies\nfor awarding. Do not disconnect from GovorNet systems. \nDo not power off this machine.\n\nFailure to follow these instructions will result in no awards.";
                WinText.StartType();
				WinButton.SetActive(true);
				back.SetActive(false);
				spend.SetActive(false);
				moneyobject.SetActive(false);
            }
            else{
            	WinText.enabled = false;
            	LoseText.enabled = false;
            	NormalText.enabled = true;
            }
        }


	}
	
	// Update is called once per frame
	void Update () {

		//timer for lerping (hahaha what a funny SHUT UP)
		t += Time.deltaTime / lerpTime;

		//lerp it up
		if(StaticGameStats.oldgovRep != StaticGameStats.govRep){
			StaticGameStats.oldgovRep = Mathf.Lerp(gov_P, StaticGameStats.govRep, t);
		}
		if(StaticGameStats.oldcorRep != StaticGameStats.corRep){
			StaticGameStats.oldcorRep = Mathf.Lerp(cor_P, StaticGameStats.corRep, t);
		}
		if(StaticGameStats.oldrebRep != StaticGameStats.rebRep){
			StaticGameStats.oldrebRep = Mathf.Lerp(reb_P, StaticGameStats.rebRep, t);
		}

		//Update the UI objects
		govRepText.text = "GOVERNMENT: \n" + Mathf.Floor(StaticGameStats.oldgovRep).ToString() + " %\n" + gov_change.ToString();
		govBar.fillAmount = StaticGameStats.oldgovRep / 100.0f;
		govMoneyText.text = govMoney.ToString();

		corRepText.text = "CORPORATE: \n" + Mathf.Floor(StaticGameStats.oldcorRep).ToString() + " %\n" + cor_change.ToString();
		corBar.fillAmount = StaticGameStats.oldcorRep / 100.0f;
		corMoneyText.text = corMoney.ToString();

		rebRepText.text = "REBEL: \n" + Mathf.Floor(StaticGameStats.oldrebRep).ToString() + " %\n" + reb_change.ToString();
		rebBar.fillAmount = StaticGameStats.oldrebRep / 100.0f;
		rebMoneyText.text = rebMoney.ToString();

		if (StaticGameStats.govRep >= 100){
			govBackground.color = new Color(0,0,Mathf.Abs(Mathf.Sin(t*10)),1);
		}
		if (StaticGameStats.corRep >= 100){
			corBackground.color = new Color(0,0,Mathf.Abs(Mathf.Sin(t*10)),1);
		}
		if (StaticGameStats.rebRep >= 100){
			rebBackground.color = new Color(0,0,Mathf.Abs(Mathf.Sin(t*10)),1);
		}
		if (StaticGameStats.govRep <= 0){
			govBackground.color = new Color(Mathf.Abs(Mathf.Sin(t*10)),0,0,1);
		}
		if (StaticGameStats.corRep <= 0){
			corBackground.color = new Color(Mathf.Abs(Mathf.Sin(t*10)),0,0,1);
		}
		if (StaticGameStats.rebRep <= 0){
			rebBackground.color = new Color(Mathf.Abs(Mathf.Sin(t*10)),0,0,1);
		}
		directory.text = @"G:\GovorNet\" + StaticGameStats.PlayerName + @"\COMMS.gov";
	}

	int CheckThresholds(float change){
		//0 = 20, 1=10, 2=0, 3=-10
		if(change >= thresholds[3]){
			if(change >= thresholds[2]){
				if(change >= thresholds[1]){
					if(change >= thresholds[0]){
						//above threshold 0
						return threshold_values[0];
					}
					else{
						//below threshold 0
						return threshold_values[1];
					}
				}
				else{
					//below threshold 1
					return threshold_values[2];
				}
			}
			else{
				//below threshold 2
				return threshold_values[3];
			}
		}
		else{
			//below threshold 3
			return threshold_values[4];
		}
	}
		
}
