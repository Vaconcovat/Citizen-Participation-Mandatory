using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuInterfaceManager : MonoBehaviour {

	public Text reputationText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		reputationText.text = 	"GOV REP: " + StaticGameStats.govRep.ToString() + "\nCOR REP: " + StaticGameStats.corRep.ToString() + "\nREB REP: " + StaticGameStats.rebRep.ToString();
	}
}
