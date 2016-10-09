using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialTrigger : MonoBehaviour {
	public static bool FromTutorial = false;

	public enum TriggerType{Vendor, Exit};
	public TriggerType triggerType;
	public bool triggered = false;

	TutorialManager tm;

	void Start(){
		tm = FindObjectOfType<TutorialManager>();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Contestant") {
			if(!triggered){
				Trigger();
			}
		}
	}

	void Trigger(){
		triggered = true;
		switch(triggerType){
			case TriggerType.Vendor:
				_Vendor();
				break;
			case TriggerType.Exit:
				Exit();
				break;
		}
	}

	void Exit(){
		StaticGameStats.instance.tutorialDone = true;
		RoundManager.roundNumber = 1;
		FromTutorial = true;
		StaticGameStats.instance.Save();
		SceneManager.LoadScene(0);
	}

	void _Vendor(){
		tm._Vendor();
	}
}
