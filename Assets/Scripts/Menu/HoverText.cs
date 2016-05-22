using UnityEngine;
using System.Collections;

public class HoverText : MonoBehaviour {

	public AutoType StatusBar;
	[TextArea(1,4)]
	public string tooltip;
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Display(){
		StatusBar.displayedText = new string[1];
		StatusBar.displayedText[0] = tooltip;
		StatusBar.textDelays = new float[1];
		StatusBar.textDelays[0] = 0;
		StatusBar.StartType();
	}
}
