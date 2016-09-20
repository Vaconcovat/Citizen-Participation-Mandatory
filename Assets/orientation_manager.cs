using UnityEngine;
using System.Collections;

public class orientation_manager : MonoBehaviour {

	public AutoType at;
	[TextArea(2,5)]
	public string[] lines;
	public GameObject prompt;
	bool done;
	int i;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Delete)) {
			FindObjectOfType<MenuCamera> ().ZoomedOut ();
		}
		if (Input.GetKey (KeyCode.Space)){
			AutoType.charactersPerTick = 3;
		} else {
			AutoType.charactersPerTick = 1;
		}
		if(done){
			if(Input.GetKeyDown(KeyCode.X)){
				if(i == lines.Length -1){
					FindObjectOfType<MenuCamera>().Questionaire();
				}
				else{
					TypeNext();
				}
			}
		}


	}

	public void Orientation(){
		i = -1;
		TypeNext();
	}

	public void Done(){
		done = true;
		prompt.SetActive(true);
	}

	void TypeNext(){
		prompt.SetActive(false);
		i++;
		done = false;
		AutoType.charactersPerTick = 1;
		at.displayedText[1] = lines[i];
		at.StartType();
	}

}
