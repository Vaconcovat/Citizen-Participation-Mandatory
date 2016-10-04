using UnityEngine;
using System.Collections;

public class CameraEnabler : MonoBehaviour {

	public GameObject Camera1;
	public GameObject Camera2;
	public GameObject Camera3;
	public GameObject Camera4;
	public GameObject Camera5;
	public GameObject Camera6;

	public GameObject Powerbox1;
	public GameObject Powerbox2;
	public GameObject Powerbox3;
	public GameObject Powerbox4;
	public GameObject Powerbox5;
	public GameObject Powerbox6;

	void Awake () {
		if (StaticGameStats.instance.TierThreeUpgrades [0]) {
			Camera1.SetActive (true);
			Camera2.SetActive (true);
			Camera3.SetActive (true);
			Camera4.SetActive (true);
			Camera5.SetActive (true);
			Camera6.SetActive (true);

			Powerbox1.SetActive (true);
			Powerbox2.SetActive (true);
			Powerbox3.SetActive (true);
			Powerbox4.SetActive (true);
			Powerbox5.SetActive (true);
			Powerbox6.SetActive (true);
		} else {
			Camera1.SetActive (false);
			Camera2.SetActive (false);
			Camera3.SetActive (false);
			Camera4.SetActive (false);
			Camera5.SetActive (false);
			Camera6.SetActive (false);

			Powerbox1.SetActive (false);
			Powerbox2.SetActive (false);
			Powerbox3.SetActive (false);
			Powerbox4.SetActive (false);
			Powerbox5.SetActive (false);
			Powerbox6.SetActive (false);
		}
	}
}
