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

	// Use this for initialization
	void Start () {
		rTrans = GetComponent<RectTransform>();
		c = FindObjectOfType<Canvas>();
		size = c.GetComponent<RectTransform>().sizeDelta;
		ammoText = GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.WorldToScreenPoint(item.transform.position);
		float x = (pos.x / Screen.width) * size.x * c.transform.localScale.x;
		float y = (pos.y / Screen.height) * size.y * c.transform.localScale.y;
		rTrans.position = new Vector3(x,y);

		RangedWeapon wep = item.GetComponent<RangedWeapon>();
		if(wep.ammo <= 0){
			Destroy(gameObject);
		}

		if(displayAmmo){
			if(wep != null){
				ammoText.text = wep.ammo.ToString() + " / " + wep.Maxammo.ToString();
			}else{
				ammoText.text = "--";
			}
		}
	}


}
