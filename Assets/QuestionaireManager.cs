using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestionaireManager : MonoBehaviour {
	public Text RebelIncorrect1;
	public Text RebelIncorrect2;
	public Text GovSecretIncorrect1;
	public Text GovSecretIncorrect2;
	public Text UsernameIncorrect;
	public Text MindControlIncorrect;
	public Text DeathsIncorrect;
	public Text JudgedIncorrect;
	public Text ConstructionIncorrect;
	public InputField RebelFName;
	public InputField RebelLName;
	public InputField GovPlans;
	public InputField Username;
	public Toggle MindControl;
	public Toggle Deaths;
	public Toggle Performance;
	public Toggle Funding;
	public Toggle Rebels;
	public Toggle GovernmentPlans;
	public Image Redacted1;
	public Image Redacted2;
	public Image Redacted3;
	public Image Redacted4;
	public Image Redacted5;
	public Image Redacted6;
	public Image Redacted7;
	public Button Submit;

	public Text RedactedText1;
	public Text RedactedText2;
	public Text RedactedText3;
	public Text RedactedText4;
	public Text RedactedText5;
	public Text RedactedText6;
	public Text RedactedText7;
	
	// Update is called once per frame
	void Update () {
		if ((Username.text != "") && (MindControl.isOn == true) && (Deaths.isOn == true) && (Performance.isOn == true) && (Funding.isOn == true) && (RebelFName.text != "") && (RebelLName.text != "") && (Rebels.isOn == false) && (GovPlans.text == "") && (GovernmentPlans.isOn == true)) {
			Submit.interactable = true;
		} else {
			Submit.interactable = false;
		}
	}

	public void RebelIncorrect(){
		if (Rebels.isOn) {
			RebelIncorrect2.enabled = true;
		} else {
			RebelIncorrect2.enabled = false;
		}

		if (RebelFName.text == "") {
			RebelIncorrect1.enabled = true;
		}

		if (RebelLName.text == "") {
			RebelIncorrect1.enabled = true;
		}

		if ((RebelFName.text != "") && (RebelLName.text != "")) {
			RebelIncorrect1.enabled = false;
		}
	}

	public void GovernmentIncorrect(){
		if (GovPlans.text == "") {
			GovSecretIncorrect1.enabled = false;
		} else {
			GovSecretIncorrect1.enabled = true;
		}

		if (GovernmentPlans.isOn) {
			GovSecretIncorrect2.enabled = false;
		} else {
			GovSecretIncorrect2.enabled = true;
		}
	}

	public void SetUsername(){
		if (Username.text != "") {
			StaticGameStats.PlayerName = Username.text;
		}

		if (Username.text == "") {
			UsernameIncorrect.enabled = true;
		} else {
			UsernameIncorrect.enabled = false;
		}
		
	}

	public void SetREDACTED(){
		if (MindControl.isOn) {
			Redacted1.enabled = false;
			RedactedText1.enabled = false;
			Redacted2.enabled = false;
			RedactedText2.enabled = false;
			MindControlIncorrect.enabled = false;
		} else {
			Redacted1.enabled = true;
			RedactedText1.enabled = true;
			Redacted2.enabled = true;
			RedactedText2.enabled = true;
			MindControlIncorrect.enabled = true;
		}

		if (Deaths.isOn) {
			Redacted3.enabled = false;
			RedactedText3.enabled = false;
			DeathsIncorrect.enabled = false;
		} else {
			Redacted3.enabled = true;
			RedactedText3.enabled = true;
			DeathsIncorrect.enabled = true;
		}

		if (Performance.isOn) {
			Redacted4.enabled = false;
			RedactedText4.enabled = false;
			Redacted5.enabled = false;
			RedactedText5.enabled = false;
			JudgedIncorrect.enabled = false;
		} else {
			Redacted4.enabled = true;
			RedactedText4.enabled = true;
			Redacted5.enabled = true;
			RedactedText5.enabled = true;
			JudgedIncorrect.enabled = true;
		}

		if (Funding.isOn) {
			Redacted6.enabled = false;
			RedactedText6.enabled = false;
			Redacted7.enabled = false;
			RedactedText7.enabled = false;
			ConstructionIncorrect.enabled = false;
		} else {
			Redacted6.enabled = true;
			RedactedText6.enabled = true;
			Redacted7.enabled = true;
			RedactedText7.enabled = true;
			ConstructionIncorrect.enabled = true;
		}
	}
}
