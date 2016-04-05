using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PostArenaInterface : MonoBehaviour {

	//an easy way to find all the text objects
	public Text govRepText;
	public Text govChangeText;
	public Text corRepText;
	public Text corChangeText;
	public Text rebRepText;
	public Text rebChangeText;

	/// <summary>
	/// Number of seconds it takes for the counters to reach their full value
	/// </summary>
	public float counterTime = 5.0f;

	//storing how much the influence changed
	float govChange;
	float corChange;
	float rebChange;

	//used by the lerp system to remember what we started on
	float govPointer;
	float corPointer;
	float rebPointer;

	string changeText = "Change: ";

	// Use this for initialization
	void Start () {
		//calculate all the changes
		govChange = StaticGameStats.govRep - StaticGameStats.oldgovRep;
		corChange = StaticGameStats.corRep - StaticGameStats.oldcorRep;
		rebChange = StaticGameStats.rebRep - StaticGameStats.oldrebRep;

		//set all the change texts
		govChangeText.text = changeText + govChange.ToString();
		corChangeText.text = changeText + corChange.ToString();
		rebChangeText.text = changeText + rebChange.ToString();

		//store the initial values as pointers for the lerp function
		govPointer = StaticGameStats.oldgovRep;
		corPointer = StaticGameStats.oldcorRep;
		rebPointer = StaticGameStats.oldrebRep;
	}
	
	// Update is called once per frame
	void Update () {

		//LERP IT UP
		StaticGameStats.oldgovRep = Mathf.Lerp(govPointer,StaticGameStats.govRep, Time.timeSinceLevelLoad / counterTime);
		StaticGameStats.oldcorRep = Mathf.Lerp(corPointer,StaticGameStats.corRep, Time.timeSinceLevelLoad / counterTime);
		StaticGameStats.oldrebRep = Mathf.Lerp(rebPointer,StaticGameStats.rebRep, Time.timeSinceLevelLoad / counterTime);

		//update the strings
		govRepText.text = "Government Reputation: " + StaticGameStats.oldgovRep.ToString("F2") + "%";
		corRepText.text = "Corporate Reputation: " + StaticGameStats.oldcorRep.ToString("F2") + "%";
		rebRepText.text = "Rebel Reputation: " + StaticGameStats.oldrebRep.ToString("F2") + "%";
	}
}
