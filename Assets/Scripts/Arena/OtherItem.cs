using UnityEngine;
using System.Collections;

public class OtherItem : MonoBehaviour {
	public enum ItemEffect{Heal, Speed, Damage, CameraSight};

	public ItemEffect effect;
	public float effectAmount;
	public bool consume;
	public AudioClip _audio;
	[Tooltip("How many Uses this Item has.")]
	/// <summary>
	/// The ammo.
	/// </summary>
	public int ammo;


	// Use this for initialization
	void Start () {
		if (StaticGameStats.TierOneUpgrades [0]) {
			ammo = ammo * StaticGameStats.Upgrade1ItemUsageBuff;
		}
	}

	// Update is called once per frame
	void Update () {
	}

	public void Use(bool held){
		if (!held){
			if (ammo != 0){
				FindObjectOfType<SoundManager>().PlayEffect(_audio, transform.position, 1.0f, true);
				switch (effect) {
				case ItemEffect.Heal:
					StartCoroutine("Heal",effectAmount);
					break;
				case ItemEffect.Speed:
					StartCoroutine("Speed", effectAmount);
					break;
				case ItemEffect.Damage:
					StartCoroutine("Damage", effectAmount);
					break;
				case ItemEffect.CameraSight:
					StartCoroutine("CameraSightRepGains", effectAmount);
					break;
				}
			}
			else{
				FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().empty_gun, transform.position, 1.0f, false);
			}
		}
	}

	IEnumerator Heal(float amount){
		if (ammo >= 1) {
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

	IEnumerator Speed(float amount){
		if (ammo >= 1) {
			ammo -= 1;
			GetComponent<Item> ().equipper.movespeed += amount;
			if (StaticGameStats.TierThreeUpgrades [3]) {
				yield return new WaitForSeconds (StaticGameStats.VelocitechItemDuration * StaticGameStats.Upgrade12DurationBuff);
			} else {
				yield return new WaitForSeconds (StaticGameStats.VelocitechItemDuration);
			}
			GetComponent<Item> ().equipper.movespeed -= amount;
			if ((consume) && (ammo <= 0)) {
				GetComponent<Item> ().Throw ();
			}
		}
	}

	IEnumerator Damage(float amount){
		if (ammo >= 1) {;
			ammo -= 1;
			GetComponent<Contestant> ().ContestantDamageModifier += amount;
			if (StaticGameStats.TierThreeUpgrades [3]) {
				yield return new WaitForSeconds (StaticGameStats.ExplodenaItemDuration * StaticGameStats.Upgrade12DurationBuff);
			} else {
				yield return new WaitForSeconds (StaticGameStats.ExplodenaItemDuration);
			}
			GetComponent<Contestant> ().ContestantDamageModifier -= amount;
			if ((consume) && (ammo <= 0)) {
				GetComponent<Item> ().Throw ();
			}
		}
		
	}

	IEnumerator CameraSightRepGains(float amount){
		if (ammo >= 1) {
			ammo -= 1;
			GetComponent<Contestant> ().ContestantRepModifier += amount;
			if (StaticGameStats.TierThreeUpgrades [3]) {
				yield return new WaitForSeconds (StaticGameStats.PrismexItemDuration * StaticGameStats.Upgrade12DurationBuff);
			} else {
				yield return new WaitForSeconds (StaticGameStats.PrismexItemDuration);
			}
			GetComponent<Contestant> ().ContestantRepModifier -= amount;
			if ((consume) && (ammo <= 0)) {
				GetComponent<Item> ().Throw ();
			}
		}
	}
}
