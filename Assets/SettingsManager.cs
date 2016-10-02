using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	float sfxVolume, musicVolume;
	public Slider sfxSlider, musicSlider;
	public Text sfxNum, musicNum;
	
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
	}
	
	public void UpdatePrefs(){
		PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
		sfxVolume = sfxSlider.value;
		PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
		musicVolume = musicSlider.value;
		sfxNum.text = string.Format("{0:P0}", sfxVolume);
		musicNum.text = string.Format("{0:P0}", musicVolume);
	}
}
