using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class InterfaceManager : MonoBehaviour {

	[Header("UI GameObjects")]
	public Image healthbar;
	public Text ammo;
	public Image equippedIcon;
	public Text timer;
	public Image gunLogo;

	[Header("Player")]
	public Contestant player;

	[Header("Camera")]
	public NoiseAndScratches noise;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		healthbar.fillAmount = (player.health * 1.0f) / (player.maxHealth * 1.0f);
		if (player.equipped != null){
			ammo.text = player.equipped.GetComponent<RangedWeapon>().ammo.ToString();
			gunLogo.enabled = true;
			gunLogo.sprite = player.equipped.logo;	
		}
		else{
			ammo.text = "--";
			gunLogo.enabled = false;
		}
		timer.text = Time.frameCount.ToString();
		noise.grainIntensityMax = (1 - healthbar.fillAmount) + 0.1f;
	}
}
