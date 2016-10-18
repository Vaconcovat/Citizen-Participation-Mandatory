using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public GameObject prefab;
	public AudioClip bounce, bullet_hit_wall, death, empty_gun, equip, execute, explosion, gatline_lazer, hit, hurt, medkit, mercy, pistol, rifle, rpg, shotgun, sin_lazer, sniper, throw_item, vendor, UI_Beep;

	public void PlayEffect(AudioClip audio, Vector3 position, float volume, bool spacial){
		GameObject spawned = (GameObject)Instantiate(prefab, position, Quaternion.identity);
		AudioSource source = spawned.GetComponent<AudioSource>();
		source.clip = audio;
		source.volume = volume * PlayerPrefs.GetFloat("SFXVolume");
		source.spatialBlend = (spacial)?1.0f:0.0f;
		source.Play();
	}

	public void UIBeep(){
		PlayEffect(UI_Beep, Vector3.zero, 1, false);
	}
}
