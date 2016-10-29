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
	Contestant player;
	PlayerController playercon;

	// Use this for initialization
	void Start () {
		rTrans = GetComponent<RectTransform>();
		c = FindObjectOfType<Canvas>();
		size = c.GetComponent<RectTransform>().sizeDelta;
		cardText = GetComponentInChildren<Text>();
		player = FindObjectOfType<PlayerController>().GetComponent<Contestant>();
		playercon = player.GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.WorldToScreenPoint(contest.head.position);
		float x = (pos.x / Screen.width) * size.x * c.transform.localScale.x;
		float y = (pos.y / Screen.height) * size.y * c.transform.localScale.y;
		rTrans.position = new Vector3(x,y);

		cardText.text = title + ":\n" + contest.contestantName;
		if((Vector3.Distance(contest.head.position, playercon.pos) < 1.5f || Vector3.Distance(contest.head.position, player.transform.position) < 2) && (contest.type == Contestant.ContestantType.AI || contest.type == Contestant.ContestantType.Target)){
			cardText.text = cardText.text + "\n" + contest.contestantTidBit;
		}
		if(!player.isAlive){
			Destroy(gameObject);
		}
	}
}
