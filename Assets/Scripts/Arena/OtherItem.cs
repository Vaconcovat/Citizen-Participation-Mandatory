using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class OtherItem : MonoBehaviour {
	public enum ItemEffect{Heal, Speed, Damage, CameraSight};

	public ItemEffect effect;
	public float effectAmount;
	public bool consume;
	public AudioSource _audio;
	[Tooltip("How many Uses this Item has.")]
	/// <summary>
	/// The ammo.
	/// </summary>
	public int ammo;


	// Use this for initialization
	void Start () {
		_audio = GetComponent<AudioSource> ();
		if (StaticGameStats.TierOneUpgrades [0]) {
			ammo = ammo * StaticGameStats.Upgrade1ItemUsageBuff;
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
					StartCoroutine("Heal",effectAmount);
					break;
				case ItemEffect.Speed:
					Speed (effectAmount);
					break;
				case ItemEffect.Damage:
					Damage (effectAmount);
					break;
				case ItemEffect.CameraSight:
					CameraSight (effectAmount);
					break;
				}
			}
		}
	}

	IEnumerator Heal(float amount){
		if (ammo >= 1) {
			_audio.Play ();
			ammo -= 1;
			if (StaticGameStats.TierThreeUpgrades [1]) { //Heals 3% of max health 12 times over 12 seconds, total health restored 36
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.Upgrade10HealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.Upgrade10HealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.Upgrade10HealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.Upgrade10HealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.Upgrade10HealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.Upgrade10HealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.Upgrade10HealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.Upgrade10HealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.Upgrade10HealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.Upgrade10HealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.Upgrade10HealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.Upgrade10HealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
			} else { //heals a flat 25 health in a second
				GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-amount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
			}
			if ((consume) && (ammo <= 0)){
				GetComponent<Item>().Throw();
			}
		}
	}

	void Speed(float amount){
		if (ammo >= 1) {
			_audio.Play ();
			ammo -= 1;
			GetComponent<Item> ().equipper.movespeed += amount;
			if ((consume) && (ammo <= 0)) {
				GetComponent<Item> ().Throw ();
			}
		}
	}

	void Damage(float amount){
		
	}

	void CameraSight(float amount){
	}
}
