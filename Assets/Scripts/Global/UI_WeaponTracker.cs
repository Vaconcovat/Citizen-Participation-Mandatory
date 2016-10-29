using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_WeaponTracker : MonoBehaviour {
	
	public Item item;
	public bool displayAmmo;

	RectTransform rTrans;
	Canvas c;
	Vector2 size;
	Text ammoText;
	RangedWeapon wep;
	Contestant player;

	// Use this for initialization
	void Start () {
		rTrans = GetComponent<RectTransform>();
		c = FindObjectOfType<Canvas>();
		size = c.GetComponent<RectTransform>().sizeDelta;
		ammoText = GetComponentInChildren<Text>();
		if(item.type == Item.ItemType.Ranged){
			wep = item.GetComponent<RangedWeapon>();
		}
		player = FindObjectOfType<PlayerController>().GetComponent<Contestant>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.WorldToScreenPoint(item.transform.position);
		float x = (pos.x / Screen.width) * size.x * c.transform.localScale.x;
		float y = (pos.y / Screen.height) * size.y * c.transform.localScale.y;
		rTrans.position = new Vector3(x,y);


		if(item.GetAmmo() <= 0){
			Destroy(gameObject);
		}


		if(displayAmmo){
			if(item.type == Item.ItemType.Ranged){
				ammoText.text = item.itemName + "\n" + wep.ammo.ToString() + " / " + wep.Maxammo.ToString();
			}else{
				ammoText.text = item.itemName + "\n--";
			}
		}

		if(!player.isAlive){
			Destroy(gameObject);
		}
	}


}
