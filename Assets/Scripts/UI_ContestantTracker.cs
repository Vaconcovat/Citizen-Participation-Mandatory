using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_ContestantTracker : MonoBehaviour {
	
	public Contestant contest;

	RectTransform rTrans;
	Canvas c;
	Vector2 size;
	public Text rightText, LeftText;
	public GameObject rightObj, leftObj;
	AIController ai;

	public bool onRight;

	// Use this for initialization
	void Start () {
		rTrans = GetComponent<RectTransform>();
		c = FindObjectOfType<Canvas>();
		size = c.GetComponent<RectTransform>().sizeDelta;
		ai = contest.GetComponent<AIController>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.WorldToScreenPoint(contest.transform.position);
		float x = (pos.x / Screen.width) * size.x * c.transform.localScale.x;
		float y = (pos.y / Screen.height) * size.y * c.transform.localScale.y;
		x = Mathf.Clamp(x,0,c.GetComponent<RectTransform>().rect.width * c.scaleFactor);
		y = Mathf.Clamp(y,0,c.GetComponent<RectTransform>().rect.height * c.scaleFactor);
		rTrans.position = new Vector3(x,y);

		//if we're on the right
		if(rTrans.position.x > (c.GetComponent<RectTransform>().rect.width * c.scaleFactor)/2.0f){
			onRight = true;
		}
		else{
			onRight = false;
		}

		if(onRight){
			rightObj.SetActive(false);
			leftObj.SetActive(true);

		}
		else{
			rightObj.SetActive(true);
			leftObj.SetActive(false);
		}

		string text = "Health: " + contest.health.ToString() + "%\nStatus: " + ai.state.ToString() + "\nConfidence: " + ai.confidence.ToString("F2");
		rightText.text = text;
		LeftText.text = text;

		if(!contest.isAlive){
			Destroy(gameObject);
		}

	}


}
