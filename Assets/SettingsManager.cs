using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	float sfxVolume, musicVolume;
	public Slider sfxSlider, musicSlider, analyticsSlider;
	public Text sfxNum, musicNum, analyticsOn, analyticsOff;
	int analytics;
	
	// Use this for initialization
	void Awake () {
		if(PlayerPrefs.HasKey("SFXVolume")){
			sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
		}
		else{
			sfxVolume = 1;
			PlayerPrefs.SetFloat("SFXVolume", 1);
		}

		if(PlayerPrefs.HasKey("MusicVolume")){
			musicVolume = PlayerPrefs.GetFloat("MusicVolume");
		}
		else{
			musicVolume = 1;
			PlayerPrefs.SetFloat("MusicVolume", 1);
		}

		if(PlayerPrefs.HasKey("Analytics")){
			analytics = PlayerPrefs.GetInt("Analytics");
		}
		else{
			analytics = 1;
			PlayerPrefs.SetInt("Analytics", 1);
		}
	}

	void Start(){
		UpdatePrefs();
	}
	
	public void UpdatePrefs(){
		PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
		sfxVolume = sfxSlider.value;
		PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
		musicVolume = musicSlider.value;
		sfxNum.text = string.Format("{0:P0}", sfxVolume);
		musicNum.text = string.Format("{0:P0}", musicVolume);
		PlayerPrefs.SetInt("Analytics", Mathf.RoundToInt(analyticsSlider.value));
		analytics = Mathf.RoundToInt(analyticsSlider.value);
		analyticsOn.color = (analytics == 1)?Color.white:new Color(1,1,1,0.5f);
		analyticsOff.color = (analytics == 0)?Color.white:new Color(1,1,1,0.5f);
	}
}
