using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_DeathCard : MonoBehaviour {

	public Contestant contest;
	public string title = "DECEASED";

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

		cardText.text = title + ":\n" + contest.contestantName;
		if(Vector3.Distance(contest.transform.position, FindObjectOfType<PlayerController>().pos) < 1.5f || Vector3.Distance(contest.transform.position, FindObjectOfType<PlayerController>().transform.position) < 2){
			cardText.text = cardText.text + "\n" + contest.contestantTidBit;
		}
		if(!FindObjectOfType<PlayerController>().GetComponent<Contestant>().isAlive){
			Destroy(gameObject);
		}
	}
}
