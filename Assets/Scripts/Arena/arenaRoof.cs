using UnityEngine;
using System.Collections;

public class arenaRoof : MonoBehaviour {

public GameObject roof, outerRoof;
public bool inArena = true;

	// Use this for initialization
	void Start () {
		if(!inArena){
					roof.SetActive(true);
					outerRoof.SetActive(false);
				}else{
					roof.SetActive(false);
					outerRoof.SetActive(true);
				}
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Contestant"){
			if(col.gameObject.GetComponent<Contestant>().type == Contestant.ContestantType.Player){
				if(inArena){
					roof.SetActive(true);
					outerRoof.SetActive(false);
				}else{
					roof.SetActive(false);
					outerRoof.SetActive(true);
				}
				inArena = !inArena;


			}
		}
	}
}
