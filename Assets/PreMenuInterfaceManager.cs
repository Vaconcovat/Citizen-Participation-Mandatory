using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PreMenuInterfaceManager : MonoBehaviour {

	public Text moneyText, embezText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = StaticGameStats.avaliableMoney.ToString();
		embezText.text = StaticGameStats.embezzledMoney.ToString();
	}

	public void AddMoney(){
		if(StaticGameStats.avaliableMoney > 0){
			StaticGameStats.avaliableMoney--;
			StaticGameStats.embezzledMoney++;
		}
	}

	public void TakeMoney(){
		if(StaticGameStats.embezzledMoney > 0){
			StaticGameStats.embezzledMoney--;
			StaticGameStats.avaliableMoney++;
		}
	}
}
