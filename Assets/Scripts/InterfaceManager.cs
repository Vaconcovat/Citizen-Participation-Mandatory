using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InterfaceManager : MonoBehaviour {

	[Header("UI GameObjects")]
	public Image healthbar;
	public Text ammo;
	public Image equippedIcon;
	public Text timer;

	[Header("Player")]
	public Contestant player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		healthbar.fillAmount = player.health / player.maxHealth;
		if (player.equipped != null){
			ammo.text = player.equipped.GetComponent<RangedWeapon>().ammo.ToString();	
		}
		else{
			ammo.text = "--";
		}
		timer.text = Time.frameCount.ToString();
	}
}
