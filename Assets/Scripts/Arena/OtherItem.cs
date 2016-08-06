using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class OtherItem : MonoBehaviour {
	public enum ItemEffect{Heal, Speed};

	public ItemEffect effect;
	public float effectAmount;
	public bool consume;
	public AudioSource _audio;
	[Tooltip("How many Uses this Item has.")]
	/// <summary>
	/// The ammo.
	/// </summary>
	public int ammo;
	public SpriteRenderer sr;
	public Sprite ThrownSprite;


	// Use this for initialization
	void Start () {
		_audio = GetComponent<AudioSource> ();
		sr = GetComponent<SpriteRenderer> ();
		if (StaticGameStats.TierOneUpgrades [0]) {
			ammo = ammo * StaticGameStats.Upgrade1Modification;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Use(bool held){
		if (!held){
			if (ammo != 0)
			{
				switch (effect) {
				case ItemEffect.Heal:
					Heal (effectAmount);
					ammo -= 1;
					break;
				case ItemEffect.Speed:
					Speed (effectAmount);
					ammo -= 1;
					break;
				}
			}
		}
	}

	void Heal(float amount){
		_audio.Play ();
		GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-amount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
		if ((consume) && (ammo <= 0)){
			sr.sprite = ThrownSprite;
			GetComponent<Item>().Unequip();
		}
	}

	void Speed(float amount){
		GetComponent<Item>().equipper.movespeed += amount;
		if ((consume) && (ammo > 0)){
			_audio.Play ();
			sr.sprite = ThrownSprite;
			GetComponent<Item>().Unequip();
		}
	}
}
