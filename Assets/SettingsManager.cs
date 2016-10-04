using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	float sfxVolume, musicVolume;
	public Slider sfxSlider, musicSlider, analyticsSlider;
	public Text sfxNum, musicNum, analyticsOn, analyticsOff;
	int analytics;
	
	// Use this for initialization
	void OnEnable () {
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
	}
		
	public void UpdatePrefs(int a){
		switch(a){
			case 0:
				sfxVolume = sfxSlider.value;
				PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
				sfxNum.text = string.Format("{0:P0}", sfxVolume);
				break;
			case 1:
				musicVolume = musicSlider.value;
				PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
				musicNum.text = string.Format("{0:P0}", musicVolume);
				break;
			case 2:
				PlayerPrefs.SetInt("Analytics", Mathf.RoundToInt(analyticsSlider.value));
				analytics = Mathf.RoundToInt(analyticsSlider.value);
				analyticsOn.color = (analytics == 1)?Color.white:new Color(1,1,1,0.5f);
				analyticsOff.color = (analytics == 0)?Color.white:new Color(1,1,1,0.5f);
				break;
		}

	}
}
