using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using UnityEngine.Analytics;

public class LoseInterfaceManager : MonoBehaviour {
	public AutoType at;
	NoiseAndScratches ns;
	bool fin = false;
	public int NumSecondsTillStaticStart;
	bool done;

	// Use this for initialization
	void Start () {
		ns = FindObjectOfType<NoiseAndScratches>();
	}
	
	// Update is called once per frame
	void Update () {
		if(fin){
			ns.grainIntensityMin += Time.deltaTime;
			ns.grainIntensityMax += Time.deltaTime;
		}
		if (done) {
			if (Input.GetKeyDown (KeyCode.Alpha1) || (Input.GetKeyDown (KeyCode.Keypad1))) {
				FindObjectOfType<MenuCamera> ().MainMenu ();
				ns.grainIntensityMin = 0.0f;
				ns.grainIntensityMax = 0.0f;
			}
			if (Input.GetKeyDown (KeyCode.Alpha2) || (Input.GetKeyDown (KeyCode.Keypad2))) {
				FindObjectOfType<MenuCamera> ().Shutdown ();
			}
		}
	}

	public void Lose(){
		at.StartType();
		if(PlayerPrefs.GetInt("Analytics") == 1){
			Analytics.CustomEvent("Lose");
		}
	}

	public void Finished(){
		done = true;
		StartCoroutine ("StaticWait");
	}

	IEnumerator StaticWait(){
		yield return new WaitForSeconds (NumSecondsTillStaticStart);
		fin = true;
	}
}
