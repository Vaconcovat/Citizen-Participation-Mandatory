using UnityEngine;
using System.Collections;

public class DamageNumber : MonoBehaviour {

	public float fadespeed;
	TextMesh text;
	float speed;

	// Use this for initialization
	void Start () {
		text = GetComponent<TextMesh>();
		GetComponent<MeshRenderer>().sortingLayerName = "UI";
		speed = Random.Range(0.001f,0.01f);
	}

	// Update is called once per frame
	void Update () {
		if(text.color.a > 0.1f){
			text.color = new Color(1,1,1,text.color.a - Time.deltaTime * fadespeed);
		}
		else{
			Destroy(gameObject);
		}
		transform.Translate(0,speed,0);
	}
}
