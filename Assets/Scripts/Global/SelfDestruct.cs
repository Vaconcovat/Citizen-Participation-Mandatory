using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public bool timed;
	public float time;
	public bool fade;
	public float fadeRate;
	public bool expand;
	public float expandRate;
	public bool dontDestroyOnLoad;

	void Start(){
		if(dontDestroyOnLoad){
			DontDestroyOnLoad(gameObject);
		}
		time = FindObjectOfType<SkillCoolDown>().bioscanActiveTime;
	}
	
	// Update is called once per frame
	void Update () {
		//reduce the time if enabled
		if(timed){
			time -= Time.deltaTime;
			if (time <= 0){
				Destroy(gameObject);
			}
		}

		//fade if enabled
		if (fade){
			Color previous = GetComponent<SpriteRenderer>().color;
			GetComponent<SpriteRenderer>().color = new Color(previous.r,previous.g,previous.b,previous.a - fadeRate);
			if (previous.a <= 0.01){
				Destroy(gameObject);
			}
		}

		//expand if enabled
		if (expand){
			transform.localScale = new Vector3(transform.localScale.x + expandRate, transform.localScale.y + expandRate, 1);
		}
	}
}
