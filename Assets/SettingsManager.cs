using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	float sfxVolume, musicVolume;
	public Slider sfxSlider, musicSlider, analyticsSlider;
	public Text sfxNum, musicNum, analyticsOn, analyticsOff, deleteText, saveText;
	public AudioSource sfx, music;
	int analytics;
	bool prompt = false;
	
	// Use this for initialization
	void OnEnable () {
	prompt = false;
		if(PlayerPrefs.HasKey("SFXVolume")){
			sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
			Debug.Log("Prefs has SFXVOLUME: " + sfxVolume);
			sfxSlider.value = sfxVolume;
		}
		else{
			sfxVolume = 1;
			PlayerPrefs.SetFloat("SFXVolume", 1);
		}

		if(PlayerPrefs.HasKey("MusicVolume")){
			musicVolume = PlayerPrefs.GetFloat("MusicVolume");
			Debug.Log("Prefs has MusicVolume: " + musicVolume);
			musicSlider.value = musicVolume;
		}
		else{
			musicVolume = 1;
			PlayerPrefs.SetFloat("MusicVolume", 1);
		}

		if(PlayerPrefs.HasKey("Analytics")){
			analytics = PlayerPrefs.GetInt("Analytics");
			Debug.Log("Prefs has Analytics: " + analytics);
			analyticsSlider.value = analytics;
		}
		else{
			analytics = 1;
			PlayerPrefs.SetInt("Analytics", 1);
		}

		deleteText.text = "DELETE SAVE";
		saveText.text = string.Format("{0}\nTournaments: {1}\nMoney: {2}\nRep: [ GOV {3:F0} | COR {4:F0} | REB {5:F0} ]", StaticGameStats.instance.PlayerName, StaticGameStats.instance.arenasPlayed, StaticGameStats.instance.avaliableMoney, StaticGameStats.instance.govRep, StaticGameStats.instance.corRep, StaticGameStats.instance.rebRep);
	}
		
	public void UpdatePrefs(int a){
		switch(a){
			case 0:
				sfxVolume = sfxSlider.value;
				PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
				sfxNum.text = string.Format("{0:P0}", sfxVolume);
				sfx.volume = sfxVolume;
				break;
			case 1:
				musicVolume = musicSlider.value;
				PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
				musicNum.text = string.Format("{0:P0}", musicVolume);
				music.volume = musicVolume;
				break;
			case 2:
				PlayerPrefs.SetInt("Analytics", Mathf.RoundToInt(analyticsSlider.value));
				analytics = Mathf.RoundToInt(analyticsSlider.value);
				analyticsOn.color = (analytics == 1)?Color.white:new Color(1,1,1,0.5f);
				analyticsOff.color = (analytics == 0)?Color.white:new Color(1,1,1,0.5f);
				break;
		}
	}

	public void DeleteSave(){
		if(prompt){
			if(!StaticGameStats.instance.DeleteSave()){
				saveText.text = "No file to delete!";
			}
		}
		else{
			prompt = true;
			deleteText.text = "<color=red>ARE YOU SURE?</color>";
		}
	}

	public void PlaySound(bool fx){
		if(fx){
			sfx.Play();
		}
		else{
			music.Play();
		}
	}
}
