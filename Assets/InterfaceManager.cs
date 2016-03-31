using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Script for accessing and updating all interface components (in the arena)
/// </summary>
public class InterfaceManager : MonoBehaviour {

	public Text roundText;
	public Text aliveText;
	public Image healthBar;
	public PlayerAttributes player;

	RoundManager rm;

	// Use this for initialization
	void Start () {
		rm = FindObjectOfType<RoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
		//update the alive counter
		aliveText.text = "CONTESTANTS ALIVE: " + rm.aliveContestants.ToString() + "/" + rm.totalContestants.ToString();
		healthBar.fillAmount = (player.CurrentHealth * 1.0f / player.MaxHealth*1.0f);
	}

}
