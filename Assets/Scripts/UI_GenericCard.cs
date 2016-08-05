using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_GenericCard : MonoBehaviour {

	public Transform target;
	public string text;
	/// <summary>
	/// Set to non-zero to make the card have a limited lifetime
	/// </summary>
	public float lifetime;
	public bool trackOffscreen;

	bool limited;
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
		if (lifetime <= 0){
			limited = false;
		}
		else{
			limited = true;
		}

	}

	// Update is called once per frame
	void Update () {
		if(limited){
			if(lifetime <= 0){
				Destroy(gameObject);
			}
			else{
				lifetime -= Time.deltaTime;
			}
		}
		cardText.text = text;
		Vector3 pos = Camera.main.WorldToScreenPoint(target.position);
		float x = (pos.x / Screen.width) * size.x * c.transform.localScale.x;
		float y = (pos.y / Screen.height) * size.y * c.transform.localScale.y;
		if(trackOffscreen){
			x = Mathf.Clamp(x,0,c.GetComponent<RectTransform>().rect.width * c.scaleFactor);
			y = Mathf.Clamp(y,0,c.GetComponent<RectTransform>().rect.height * c.scaleFactor);
		}
		rTrans.position = new Vector3(x,y);
	}
}
