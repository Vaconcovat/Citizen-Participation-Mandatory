using UnityEngine;
using System.Collections;

public class OtherItem : MonoBehaviour {
	public enum ItemEffect{Heal};

	public ItemEffect effect;
	public float effectAmount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Use(bool held){
		if (!held){
			switch(effect){
				case ItemEffect.Heal:
					Heal(effectAmount);
					break;
			}
		}

	}

	void Heal(float amount){
		GetComponent<Item>().equipper.TakeDamage(new Contestant.DamageParams(Mathf.FloorToInt(-amount),GetComponent<Item>().equipper,Vector2.zero,Vector2.zero));
	}
}
