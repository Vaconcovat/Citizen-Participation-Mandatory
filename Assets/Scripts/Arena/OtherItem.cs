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
	public int Maxammo;
	public GameObject flare;

	public void Use(bool held){
		if (!held){
			if (ammo > 0){
				ammo-=1;
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
			}
			else{
				if(GetComponent<Item>().equipper.type == Contestant.ContestantType.Player){
					FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().empty_gun, transform.position, 1.0f, false);
				}
			}
		}
	}

	IEnumerator Heal(float amount){
		effector = GetComponent<Item> ().equipper;
		if (StaticGameStats.instance.TierThreeUpgrades [0]) { //Heals 4% of max health 12 times over 12 seconds, total health restored 36
			for (int i = 0; i <= StaticGameStats.instance.FirstAidHereHealDuration; i++) {
				effector.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-StaticGameStats.instance.FirstAidHereHealAmount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
				yield return new WaitForSeconds (1.0f);
			}
		} else { //heals a flat 25 health in a second
			effector.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-amount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
		}
		Explosion ();
	}

	IEnumerator Speed(float amount){
		effector = GetComponent<Item>().equipper;
		Explosion ();
		effector.movespeed = effector.movespeed + amount;
		yield return new WaitForSeconds (StaticGameStats.instance.VelocitechItemDuration);
		effector.movespeed = effector.movespeed - amount;
		if (effector.movespeed < 10) {
			effector.movespeed = 10;
		}	
	}

	IEnumerator Damage(float amount){
		effector = GetComponent<Item>().equipper;
		Explosion ();
		effector.ContestantDamageModifier = effector.ContestantDamageModifier + amount;
		yield return new WaitForSeconds (StaticGameStats.instance.ExplodenaItemDuration);
		effector.ContestantDamageModifier = effector.ContestantDamageModifier - amount;
		if (effector.ContestantDamageModifier < 1) {
			effector.ContestantDamageModifier = 1;
		}
	}

	IEnumerator CameraSightRepGains(float amount){
		effector = GetComponent<Item>().equipper;
		Explosion ();
		effector.ContestantRepModifier = effector.ContestantRepModifier + amount;
		yield return new WaitForSeconds (StaticGameStats.instance.PrismexItemDuration);
		effector.ContestantRepModifier = effector.ContestantRepModifier - amount;
		if (effector.ContestantRepModifier < 1) {
			effector.ContestantRepModifier = 1;
		}
	}

	void Explosion ()
	{
		//Debug.Log ("Health Kit Used");
		FindObjectOfType<SoundManager>().PlayEffect(FindObjectOfType<SoundManager>().explosion, transform.position, 0.3f, true);
		Vector3 pos = this.GetComponent<Item> ().equipper.transform.position;
		pos.y = pos.y + 1;
		GameObject spawned = (GameObject)Instantiate (flare, pos, Quaternion.identity);
		spawned.transform.Rotate (90,0,0);
		spawned.transform.parent = this.GetComponent<Item>().equipper.transform;
		Debug.Log (this.GetComponent<Item> ().equipper.transform);
		Debug.Log ("Explosion");
		Destroy(spawned, 5.0f);

	}
}
