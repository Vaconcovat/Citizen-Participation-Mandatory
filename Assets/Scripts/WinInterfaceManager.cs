using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using UnityEngine.Analytics;

public class WinInterfaceManager : MonoBehaviour {

    public AutoType at;
    NoiseAndScratches ns;
    bool fin = false;
	bool done;

    // Use this for initialization
    void Start()
    {
        ns = FindObjectOfType<NoiseAndScratches>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fin)
        {
            ns.grainIntensityMin += Time.deltaTime;
            ns.grainIntensityMax += Time.deltaTime;
        }
		if (done) {
			if (Input.GetKeyDown (KeyCode.Alpha1) || (Input.GetKeyDown (KeyCode.Keypad1))) {
				FindObjectOfType<MenuCamera> ().MainMenu ();
			}
			if (Input.GetKeyDown (KeyCode.Alpha2) || (Input.GetKeyDown (KeyCode.Keypad2))) {
				FindObjectOfType<MenuCamera> ().Shutdown ();
			}
		}
    }

    public void Win()
    {
        at.StartType();
		if(PlayerPrefs.GetInt("Analytics") == 1){
			Analytics.CustomEvent("Win");
		}
    }

	public void Done(){
		done = true;
	}

    public void Finished()
    {
        fin = true;
    }
}
