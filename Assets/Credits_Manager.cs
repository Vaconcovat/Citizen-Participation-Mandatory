using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Credits_Manager : MonoBehaviour {

	public AutoType Head1, Head2, Head3, Prog1, Prog2, Prog3, Des1, Des2, Sound1;
	public Text directory;
	public bool done1, done2, done3;

	// Update is called once per frame
	void Update () {
		directory.text = @"G:\GovorNet\" + StaticGameStats.instance.PlayerName + @"\CREDITS.gov";
	}
		
	public void StartHeadings(){
		Head1.StartType ();
		Head2.StartType ();
		Head3.StartType ();
		Prog1.StartType();
		Prog2.StartType();
		Prog3.StartType();
		Des1.StartType();
		Des2.StartType();
		Sound1.StartType ();
	}
}
