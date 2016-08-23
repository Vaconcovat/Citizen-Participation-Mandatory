using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arena_Camera : MonoBehaviour {

	public List<Contestant> visibleContestants = new List<Contestant>();

	[Header("FOV Settings")]
	[Range(0,180)]
	public float angle;
	public float radius;

	public LayerMask detectMask, cullMask;
	public GameObject ui_card, line;
	UI_GenericCard card;

	// Use this for initialization
	void Start () {
		//StartCoroutine("LookDelay", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(transform.position, transform.position + ((DirFromAngle(-angle / 2, false))*radius));
		Debug.DrawLine(transform.position, transform.position + ((DirFromAngle(angle / 2, false))*radius));
		Look();
	}

	IEnumerator LookDelay(float delay){
		while(true){
			yield return new WaitForSeconds(delay);
			Look();
		}
	}

	void Look(){
		visibleContestants.Clear();
		Collider[] inRadius = Physics.OverlapSphere(transform.position, radius, detectMask);
		if(inRadius.Length == 0){
			return;
		}
		foreach(Collider c in inRadius){
			Vector3 dir = (c.transform.position - transform.position).normalized;
			float dist = Vector3.Distance(c.transform.position,transform.position);
			if(Vector3.Angle(transform.forward, dir) < angle / 2){
				//double check that they're a contestant
				if(c.GetComponent<Contestant>() != null){
					//check there's nothing in the way
					if(!Physics.Raycast(transform.position,dir,dist,cullMask)){
						Debug.DrawLine(transform.position, c.transform.position, Color.green);
						visibleContestants.Add(c.GetComponent<Contestant>());
					}
					else{
						Debug.DrawLine(transform.position, c.transform.position, Color.red);
					}

				}
			}
		}
		DrawLines(); 
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin (angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos (angleInDegrees * Mathf.Deg2Rad));
	}

	public void displayRepChange(int faction, bool positive){
		if(card != null){
			Destroy(card.gameObject);
		}
		GameObject spawned = (GameObject)Instantiate(ui_card);
		spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		card = spawned.GetComponent<UI_GenericCard>();
		card.target = transform;
		string displayText = "";
		//0 = gov, 1= cor, 2 = reb
		switch(faction){
			case 0:
				displayText = "GOV ";
				break;
			case 1:
				displayText = "COR ";
				break;
			case 2:
				displayText = "REB ";
				break;
		}
		displayText = displayText + ((positive)?"+":"-");
		card.text = displayText;
	}

	public void DrawLines(){
		bool lineExists;
		foreach(Contestant c in visibleContestants){
			if(!c.isAlive){
				continue;
			}
			lineExists = false;
			foreach(camera_line c_l in FindObjectsOfType<camera_line>()){
				if(c_l.a_camera == this && c_l.contestant == c){
					lineExists = true;
				}
			}
			if(!lineExists){
				GameObject spawned = (GameObject)Instantiate(line, transform.position, Quaternion.identity);
				camera_line l = spawned.GetComponent<camera_line>();
				l.a_camera = this;
				l.contestant = c;
			}
		}
	}
}
