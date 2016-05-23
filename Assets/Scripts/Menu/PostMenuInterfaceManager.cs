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
	public AutoType infoText;
	public Image govBarOverlay, corBarOverlay, rebBarOverlay;
	public Image govBackground, corBackground, rebBackground;

	[Header("Settings")]
	public float lerpTime;
	public float[] thresholds;
	public int[] threshold_values;
	int govMoney, corMoney, rebMoney;
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

		StaticGameStats.avaliableMoney = govMoney + corMoney + rebMoney;
		totalMoney.text = "Total Funding Recieved: " + StaticGameStats.avaliableMoney.ToString();

		//set the overlay bars
		govBarOverlay.fillAmount = gov_P / 100.0f;
		corBarOverlay.fillAmount = cor_P / 100.0f;
		rebBarOverlay.fillAmount = reb_P / 100.0f;

		if (StaticGameStats.govRep >= 100 || StaticGameStats.corRep >= 100 || StaticGameStats.rebRep >= 100){
			infoText.displayedText[0] = "One or more RELOCATION OFFERS recieved!\n\n\nYou will be able to accept a relocation offer once you have committed this arena.";
			infoText.StartType();
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
		if (StaticGameStats.rebRep <= 100){
			rebBackground.color = new Color(Mathf.Abs(Mathf.Sin(t*10)),0,0,1);
		}
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
