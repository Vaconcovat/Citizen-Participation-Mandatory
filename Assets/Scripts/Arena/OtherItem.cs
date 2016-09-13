using UnityEngine;
using System.Collections;

public class OtherItem : MonoBehaviour {
	public enum ItemEffect{Heal, Speed, Damage, CameraSight};

	public ItemEffect effect;
	public float effectAmount;
	public bool consume;
	public AudioClip _audio;
	Contestant effector;
	[Tooltip("How many Uses this Item has.")]
	/// <summary>
	/// The ammo.
	/// </summary>
	public int ammo;
	public GameObject flare;

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
				effector = GetComponent<Item>().equipper;
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
				ammo-=1;
			}
			else{
				if(GetComponent<Item>().equipper.type == Contestant.ContestantType.Player){
					FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().empty_gun, transform.position, 1.0f, false);
				}
			}
		}
	}

	IEnumerator Heal(float amount){
		if (ammo >= 1) {
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
		Explosion ();
	}

	IEnumerator Speed(float amount){
		if (ammo >= 1) {
			effector.movespeed += amount;
			if (StaticGameStats.TierThreeUpgrades [3]) {
				yield return new WaitForSeconds (StaticGameStats.VelocitechItemDuration * StaticGameStats.Upgrade12DurationBuff);
			} else {
				yield return new WaitForSeconds (StaticGameStats.VelocitechItemDuration);
			}
			effector.movespeed -= amount;
			if ((consume) && (ammo <= 0)) {
				GetComponent<Item> ().Throw ();
			}
		}
	}

	IEnumerator Damage(float amount){
		if (ammo >= 1) {;
			effector.ContestantDamageModifier += amount;
			if (StaticGameStats.TierThreeUpgrades [3]) {
				yield return new WaitForSeconds (StaticGameStats.ExplodenaItemDuration * StaticGameStats.Upgrade12DurationBuff);
			} else {
				yield return new WaitForSeconds (StaticGameStats.ExplodenaItemDuration);
			}
			effector.ContestantDamageModifier -= amount;
			if ((consume) && (ammo <= 0)) {
				GetComponent<Item> ().Throw ();
			}
		}
		
	}

	IEnumerator CameraSightRepGains(float amount){
		if (ammo >= 1) {
			effector.ContestantRepModifier += amount;
			if (StaticGameStats.TierThreeUpgrades [3]) {
				yield return new WaitForSeconds (StaticGameStats.PrismexItemDuration * StaticGameStats.Upgrade12DurationBuff);
			} else {
				yield return new WaitForSeconds (StaticGameStats.PrismexItemDuration);
			}
			effector.ContestantRepModifier -= amount;
			if ((consume) && (ammo <= 0)) {
				GetComponent<Item> ().Throw ();
			}
		}
	}

	void Explosion ()
	{
		//Debug.Log ("Health Kit Used");
		FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().explosion, transform.position, 0.3f, true);
		GameObject spawned = (GameObject)Instantiate (flare, transform.position, Quaternion.identity);
		spawned.transform.Rotate (90,0,0);
		Debug.Log ("Explosion");
		Destroy(spawned, 5.0f);
	}
}
