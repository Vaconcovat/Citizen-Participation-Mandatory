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
			if (StaticGameStats.TierThreeUpgrades [0]) { //Heals 4% of max health 12 times over 12 seconds, total health restored 36
				for (int i = 0; i <= StaticGameStats.FirstAidHereHealDuration; i++) {
					GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.FirstAidHereHealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
					yield return new WaitForSeconds (1.0f);
				}
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
			Explosion ();
			effector.movespeed = effector.movespeed + amount;
			if ((consume) && (ammo <= 0)) {
				GetComponent<Item> ().Throw ();
			}
			yield return new WaitForSeconds (StaticGameStats.VelocitechItemDuration);
			effector.movespeed = effector.movespeed - amount;
		}
	}

	IEnumerator Damage(float amount){
		effector = GetComponent<Item>().equipper;
		if (ammo >= 1) {;
			Explosion ();
			effector.ContestantDamageModifier = effector.ContestantDamageModifier + amount;
			if ((consume) && (ammo <= 0)) {
				GetComponent<Item> ().Throw ();
			}
			yield return new WaitForSeconds (StaticGameStats.ExplodenaItemDuration);
			effector.ContestantDamageModifier = effector.ContestantDamageModifier - amount;


		}
	}

	IEnumerator CameraSightRepGains(float amount){
		effector = GetComponent<Item>().equipper;
		if (ammo >= 1) {
			Explosion ();
			effector.ContestantRepModifier = effector.ContestantRepModifier + amount;
			if ((consume) && (ammo <= 0)) {
				GetComponent<Item> ().Throw ();
			}
			yield return new WaitForSeconds (StaticGameStats.PrismexItemDuration);
			effector.ContestantRepModifier = effector.ContestantRepModifier - amount;

		}
	}

	void Explosion ()
	{
		//Debug.Log ("Health Kit Used");
		FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().explosion, transform.position, 0.3f, true);
		GameObject spawned = (GameObject)Instantiate (flare, this.GetComponent<Item>().equipper.transform.position, Quaternion.identity);
		spawned.transform.Rotate (90,0,0);
		spawned.transform.parent = this.GetComponent<Item>().equipper.transform;
		Debug.Log (this.GetComponent<Item> ().equipper.transform);
		Debug.Log ("Explosion");
		Destroy(spawned, 5.0f);

	}
}
