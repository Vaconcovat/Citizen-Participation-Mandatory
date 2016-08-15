using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

	public Light light1;
	public Light light2;
	public Light light3;
	public Light light4;
	Contestant[] targets;



	// Use this for initialization
	void Start () {
		targets = FindObjectsOfType<Contestant> ();
		light1 = GetComponent<Light>();
		light2 = GetComponent<Light>();
		light3 = GetComponent<Light>();
		light4 = GetComponent<Light>();
		for (int i = 0; i < targets.Length; i++) {
			targets [i].health = 30;
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*light1.color -= Color.white / 2.0F * Time.deltaTime;
		light2.color -= Color.white / 2.0F * Time.deltaTime;
		light3.color -= Color.white / 2.0F * Time.deltaTime;
		light4.color -= Color.white / 2.0F * Time.deltaTime;*/

		for (int i = 0; i < targets.Length; i++) {
			if (targets [i].health <= 0) {
				targets [i].isAlive = false;
				targets [i].Say ("I'm dead");
				Destroy (targets [i]);
			}
		}
	}
}
