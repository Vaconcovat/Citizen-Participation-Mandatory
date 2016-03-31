using UnityEngine;
using System.Collections;

public class BloodSplatter : MonoBehaviour {

	public float fadespeed;
	SpriteRenderer render;

	// Use this for initialization
	void Start () {
		render = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		if(render.color.a > 0.1f){
			render.color = new Color(1,1,1,render.color.a - Time.deltaTime * fadespeed);
		}
		else{
			Destroy(gameObject);
		}
	}
}
