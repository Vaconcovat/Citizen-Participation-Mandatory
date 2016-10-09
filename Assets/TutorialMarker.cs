using UnityEngine;
using System.Collections;

public class TutorialMarker : MonoBehaviour {

	public GameObject prefab;
	[TextArea(1,3)]
	public string text, altText;
	public bool active;
	GameObject spawned;
	
	// Use this for initialization
	void Start () {
		spawned = (GameObject)Instantiate(prefab);
		spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		UI_TutorialMarker tm = spawned.GetComponent<UI_TutorialMarker>();
		tm.target = transform;
		tm.text = text;
		tm.altText = altText;
	}

	void Update(){
		if(spawned != null){
			if(!active){
				spawned.SetActive(false);
			}
			else{
				spawned.SetActive(true);
			}
		}

	}
}
