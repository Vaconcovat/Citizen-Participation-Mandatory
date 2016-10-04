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
	public AutoType LoseText, WinText, NormalText;
	public Image govBarOverlay, corBarOverlay, rebBarOverlay;
	public Image govBackground, corBackground, rebBackground;
	public GameObject WinButton, LoseButton, back, spend;
	public GameObject moneyobject;
	public Text directory;
	public Text GovStatus, CorStatus, RebStatus, NormalTextObject, LoseTextObject, WinTextObject;
	public GameObject NMGov, NMCor, NMReb;
	public Image GMoney, RMoney, CMoney;

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

		gov_P = StaticGameStats.instance.oldgovRep;
		cor_P = StaticGameStats.instance.oldcorRep;
		reb_P = StaticGameStats.instance.oldrebRep;

		gov_change = StaticGameStats.instance.govRep - StaticGameStats.instance.oldgovRep;
		cor_change = StaticGameStats.instance.corRep - StaticGameStats.instance.oldcorRep;
		reb_change = StaticGameStats.instance.rebRep - StaticGameStats.instance.oldrebRep;

		//dish out the money
		if (StaticGameStats.instance.FirstRun) {
			govMoney = 0;
			corMoney = 0;
			rebMoney = 0;
			NMGov.SetActive (true);
			NMCor.SetActive (true);
			NMReb.SetActive (true);
			govMoneyText.enabled = false;
			GMoney.enabled = false;

			corMoneyText.enabled = false;
			CMoney.enabled = false;

			rebMoneyText.enabled = false;
			RMoney.enabled = false;
		} else {
			govMoney = CheckThresholds (gov_change);
			corMoney = CheckThresholds (cor_change);
			rebMoney = CheckThresholds (reb_change);
			NMGov.SetActive (false);
			NMCor.SetActive (false);
			NMReb.SetActive (false);
			govMoneyText.enabled = true;
			GMoney.enabled = true;

			corMoneyText.enabled = true;
			CMoney.enabled = true;

			rebMoneyText.enabled = true;
			RMoney.enabled = true;
		}

		if (StaticGameStats.instance.FirstRun) {
			MoneyRecievedThisRound = 9;
		} else {
			MoneyRecievedThisRound = govMoney + corMoney + rebMoney;
		}
		if (StaticGameStats.instance.toPost) {
			StaticGameStats.instance.avaliableMoney += MoneyRecievedThisRound;
			StaticGameStats.instance.arenasPlayed++;
			StaticGameStats.instance.Save();
		}
		totalMoney.text = "Total Funding Recived: " + MoneyRecievedThisRound.ToString();

		//set the overlay bars
		govBarOverlay.fillAmount = gov_P / 100.0f;
		corBarOverlay.fillAmount = cor_P / 100.0f;
		rebBarOverlay.fillAmount = reb_P / 100.0f;

		if (StaticGameStats.instance.govRep > 80.0f){
			GovStatus.text = "Current Status:\t\tEVACUATION ORDER IMMINENT";
			GovStatus.color = Color.blue;
		} else if (StaticGameStats.instance.govRep >= 75.0f) {
			GovStatus.text = "Current Status:\t\tPleased";
			GovStatus.color = Color.green;
		} else if (StaticGameStats.instance.govRep >= 50.0f) {
			GovStatus.text = "Current Status:\t\tNeutral";
			GovStatus.color = Color.white;
		} else if (StaticGameStats.instance.govRep > 25.0f){
			GovStatus.text = "Current Status:\t\tAngry";
			GovStatus.color = Color.red;
		} else if (StaticGameStats.instance.govRep < 25.0f){
			GovStatus.text = "Current Status:\t\tEXECUTION ORDER IMMINENT";
			GovStatus.color = Color.yellow;
		}

		if (StaticGameStats.instance.corRep > 80.0f){
			CorStatus.text = "Current Status:\t\tEVACUATION ORDER IMMINENT";
			CorStatus.color = Color.blue;
		} else if (StaticGameStats.instance.corRep >= 75.0f) {
			CorStatus.text = "Current Status:\t\tPleased";
			CorStatus.color = Color.green;
		} else if (StaticGameStats.instance.corRep >= 50.0f) {
			CorStatus.text = "Current Status:\t\tNeutral";
			CorStatus.color = Color.white;
		} else if (StaticGameStats.instance.corRep > 25.0f){
			CorStatus.text = "Current Status:\t\tAngry";
			CorStatus.color = Color.red;
		} else if (StaticGameStats.instance.corRep < 25.0f){
			CorStatus.text = "Current Status:\t\tEXECUTION ORDER IMMINENT";
			CorStatus.color = Color.yellow;
		}

		if (StaticGameStats.instance.rebRep > 80.0f){
			RebStatus.text = "Current Status:\t\tEVACUATION ORDER IMMINENT";
			RebStatus.color = Color.blue;
		} else if (StaticGameStats.instance.rebRep >= 75.0f) {
			RebStatus.text = "Current Status:\t\tPleased";
			RebStatus.color = Color.green;
		} else if (StaticGameStats.instance.rebRep >= 50.0f) {
			RebStatus.text = "Current Status:\t\tNeutral";
			RebStatus.color = Color.white;
		} else if (StaticGameStats.instance.rebRep > 25.0f){
			RebStatus.text = "Current Status:\t\tAngry";
			RebStatus.color = Color.red;
		} else if (StaticGameStats.instance.rebRep < 25.0f){
			RebStatus.text = "Current Status:\t\tEXECUTION ORDER IMMINENT";
			RebStatus.color = Color.yellow;
		}

	}

	public void Victory(){
		NormalText.enabled = false;
		LoseText.enabled = false;
		WinText.enabled = true;

		NormalTextObject.enabled = false;
		LoseTextObject.enabled = false;
		WinTextObject.enabled = true;

		WinButton.SetActive(true);
		back.SetActive(false);
		spend.SetActive(false);
		moneyobject.SetActive(false);

		WinText.StartType ();
		Debug.Log ("You Win");
	}

	public void Lose(){
		
		NormalText.enabled = false;
		LoseText.enabled = true;
		WinText.enabled = false;

		NormalTextObject.enabled = false;
		LoseTextObject.enabled = true;
		WinTextObject.enabled = false;

		LoseButton.SetActive(true);
		back.SetActive(false);
		spend.SetActive(false);
		moneyobject.SetActive(false);

		LoseText.StartType ();
		Debug.Log ("You Lose");
	}

	public void Normal(){
		
		NormalText.enabled = true;
		LoseText.enabled = false;
		WinText.enabled = false;

		NormalTextObject.enabled = true;
		LoseTextObject.enabled = false;
		WinTextObject.enabled = false;

		NormalText.StartType ();
	}
	// Update is called once per frame
	void Update () {

		//timer for lerping (hahaha what a funny SHUT UP)
		t += Time.deltaTime / lerpTime;

		//lerp it up
		if(StaticGameStats.instance.oldgovRep != StaticGameStats.instance.govRep){
			StaticGameStats.instance.oldgovRep = Mathf.Lerp(gov_P, StaticGameStats.instance.govRep, t);
		}
		if(StaticGameStats.instance.oldcorRep != StaticGameStats.instance.corRep){
			StaticGameStats.instance.oldcorRep = Mathf.Lerp(cor_P, StaticGameStats.instance.corRep, t);
		}
		if(StaticGameStats.instance.oldrebRep != StaticGameStats.instance.rebRep){
			StaticGameStats.instance.oldrebRep = Mathf.Lerp(reb_P, StaticGameStats.instance.rebRep, t);
		}

		if (StaticGameStats.instance.FirstRun) {
			govMoneyText.text = "N/A - New Manager";
		} else {
			govMoneyText.text = govMoney.ToString ();
			corMoneyText.text = corMoney.ToString ();
			rebMoneyText.text = rebMoney.ToString ();
		}


		//Update the UI objects
		govRepText.text = "GOVERNMENT: \n" + Mathf.Floor(StaticGameStats.instance.oldgovRep).ToString() + " %\n" + gov_change.ToString();
		govBar.fillAmount = StaticGameStats.instance.oldgovRep / 100.0f;


		corRepText.text = "CORPORATE: \n" + Mathf.Floor(StaticGameStats.instance.oldcorRep).ToString() + " %\n" + cor_change.ToString();
		corBar.fillAmount = StaticGameStats.instance.oldcorRep / 100.0f;


		rebRepText.text = "REBEL: \n" + Mathf.Floor(StaticGameStats.instance.oldrebRep).ToString() + " %\n" + reb_change.ToString();
		rebBar.fillAmount = StaticGameStats.instance.oldrebRep / 100.0f;


		if (StaticGameStats.instance.govRep >= 100){
			govBackground.color = new Color(0,0,Mathf.Abs(Mathf.Sin(t*10)),1);
		}
		if (StaticGameStats.instance.corRep >= 100){
			corBackground.color = new Color(0,0,Mathf.Abs(Mathf.Sin(t*10)),1);
		}
		if (StaticGameStats.instance.rebRep >= 100){
			rebBackground.color = new Color(0,0,Mathf.Abs(Mathf.Sin(t*10)),1);
		}
		if (StaticGameStats.instance.govRep <= 0){
			govBackground.color = new Color(Mathf.Abs(Mathf.Sin(t*10)),0,0,1);
		}
		if (StaticGameStats.instance.corRep <= 0){
			corBackground.color = new Color(Mathf.Abs(Mathf.Sin(t*10)),0,0,1);
		}
		if (StaticGameStats.instance.rebRep <= 0){
			rebBackground.color = new Color(Mathf.Abs(Mathf.Sin(t*10)),0,0,1);
		}
		directory.text = @"G:\GovorNet\" + StaticGameStats.instance.PlayerName + @"\COMMS.gov";
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
