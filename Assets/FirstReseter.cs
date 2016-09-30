using UnityEngine;
using System.Collections;

public class FirstReseter : MonoBehaviour {

	public void TurnFirstRunOff()
	{
		if (StaticGameStats.NumTimesClicked == 0) {
			StaticGameStats.FirstRun = true;
		} else {
			StaticGameStats.FirstRun = false;
		}
		StaticGameStats.NumTimesClicked = StaticGameStats.NumTimesClicked + 1;
		Debug.Log ("First Run is " + StaticGameStats.FirstRun);
	}
}
