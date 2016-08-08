using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

	public Light light1;
	public Light light2;
	public Light light3;
	public Light light4;


	// Use this for initialization
	void Start () {
		light1 = GetComponent<Light>();
		light2 = GetComponent<Light>();
		light3 = GetComponent<Light>();
		light4 = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		light1.color -= Color.white / 2.0F * Time.deltaTime;
		light2.color -= Color.white / 2.0F * Time.deltaTime;
		light3.color -= Color.white / 2.0F * Time.deltaTime;
		light4.color -= Color.white / 2.0F * Time.deltaTime;
	}
}
