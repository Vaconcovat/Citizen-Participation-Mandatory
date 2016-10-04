using UnityEngine;
using System.Collections;

public class FirstReseter : MonoBehaviour {

	public void TurnFirstRunOff()
	{
		if (StaticGameStats.instance.NumTimesClicked == 0) {
			StaticGameStats.instance.FirstRun = true;
		} else {
			StaticGameStats.instance.FirstRun = false;
		}
		StaticGameStats.instance.NumTimesClicked = StaticGameStats.instance.NumTimesClicked + 1;
		Debug.Log ("First Run is " + StaticGameStats.instance.FirstRun);
	}
}
