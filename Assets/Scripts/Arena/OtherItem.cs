using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class OtherItem : MonoBehaviour {
	public enum ItemEffect{Heal, Speed};

	public ItemEffect effect;
	public float effectAmount;
	public bool consume;
	public AudioSource audio;


	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Use(bool held){
		if (!held){
			AudioSource audio = GetComponent<AudioSource> ();
			audio.Play ();
			Debug.Log ("Audio Trigger");
			switch(effect){
				case ItemEffect.Heal:
					Heal(effectAmount);
					break;
				case ItemEffect.Speed:
					Speed(effectAmount);
					break;
			}
		}
	}

	void Heal(float amount){
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
		GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-amount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
		if (consume){
			audio.Play ();
			GetComponent<Item>().Unequip();
			Destroy(gameObject);
		}
	}

	void Speed(float amount){
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
		GetComponent<Item>().equipper.movespeed += amount;
		if (consume){
			audio.Play ();
			GetComponent<Item>().Unequip();
			Destroy(gameObject);
		}
	}
}
