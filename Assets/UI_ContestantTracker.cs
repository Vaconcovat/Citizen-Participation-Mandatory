using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_ContestantTracker : MonoBehaviour {
	
	public Contestant contest;

	RectTransform rTrans;
	Canvas c;
	Vector2 size;
	Text healthText;

	// Use this for initialization
	void Start () {
		rTrans = GetComponent<RectTransform>();
		c = FindObjectOfType<Canvas>();
		size = c.GetComponent<RectTransform>().sizeDelta;
		healthText = GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.WorldToScreenPoint(contest.transform.position);
		float x = (pos.x / Screen.width) * size.x * c.transform.localScale.x;
		float y = (pos.y / Screen.height) * size.y * c.transform.localScale.y;
		rTrans.position = new Vector3(x,y);

		healthText.text = "Health: " + contest.health.ToString() + "%";

		if(!contest.isAlive){
			Destroy(gameObject);
		}

	}


}
