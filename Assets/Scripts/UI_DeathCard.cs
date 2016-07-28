using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_DeathCard : MonoBehaviour {

	public Contestant contest;

	RectTransform rTrans;
	Canvas c;
	Vector2 size;
	Text cardText;

	// Use this for initialization
	void Start () {
		rTrans = GetComponent<RectTransform>();
		c = FindObjectOfType<Canvas>();
		size = c.GetComponent<RectTransform>().sizeDelta;
		cardText = GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.WorldToScreenPoint(contest.transform.position);
		float x = (pos.x / Screen.width) * size.x * c.transform.localScale.x;
		float y = (pos.y / Screen.height) * size.y * c.transform.localScale.y;
		rTrans.position = new Vector3(x,y);

		cardText.text = "DECEASED:\n" + contest.contestantName + "\n" + contest.contestantTidBit;
	}
}
