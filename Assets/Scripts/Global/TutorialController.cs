using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {
	public bool typeOnAwake;
	public GameObject[] textBoxes;
	public GameObject[] triggers;
	public GameObject player;
	[TextArea(1,20)]
	public string[] displayedText;
	public float[] textDelays;
	public int charactersPerTick = 1;
	int characterCounter;
	public int maxLines = 15;
	int numlines;
	public GameObject[] guards;
	//int health;

	// Use this for initialization
	void Start () {
		//textBoxes [0].GetComponent<TextMesh> ().text = "Message Log:\n<Steve>   Hello there new recruit!\n\tAre you ready to give your life \n\tfor your country's entertainment?";
		textBoxes [0].GetComponent<TextMesh> ().text = "";
		//health = 999;
		for (int i = 1; i < textBoxes.Length; i++) {
			textBoxes [i].GetComponent<TextMesh> ().text = "Wait for\nnew text";
		}
		if(charactersPerTick < 1){
			charactersPerTick = 1;
		}
		if(typeOnAwake){
			StartCoroutine("TypeText", 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (triggers [0].GetComponent<spotlightTrigger> ().activated == true) {
			//textBoxes[1].GetComponent<TextMesh> ().text = "First Checkpoint\n   Reached";

		}
		for (int i = 1; i < guards.Length; i++) {
			if (guards [i].GetComponent<Contestant> ().health < guards [i].GetComponent<Contestant> ().maxHealth) {
				guardStateChange ();
			}
		}
		if(player.GetComponent<Contestant>().isAlive == false){
			SceneManager.LoadScene(2);
		}
	}


	IEnumerator TypeText(int index){
		Debug.Log("Coroutine Running");
		foreach (char letter in displayedText[index].ToCharArray()){
			textBoxes [0].GetComponent<TextMesh> ().text += letter;
			/*if (blip != null){
				blip.Play();
			}*/
			if(textBoxes [0].GetComponent<TextMesh> ().text.Split('\n').Length > maxLines){
				textBoxes [0].GetComponent<TextMesh> ().text = textBoxes [0].GetComponent<TextMesh> ().text.Substring(textBoxes [0].GetComponent<TextMesh> ().text.IndexOf('\n')+1);
			}
			characterCounter++;
			if(characterCounter == charactersPerTick){
				characterCounter = 0;
				yield return new WaitForSeconds(textDelays[index]);
			}
		}
		if (index < displayedText.Length - 1){
			StartCoroutine("TypeText", (index + 1));
		}
		else{
			/*if(finishedCall != null){
				finishedCall.SendMessage(finishedCallString,SendMessageOptions.DontRequireReceiver);
			}*/

		}
	}
	public void StartType(){
		textBoxes [0].GetComponent<TextMesh> ().text = "";
		StopCoroutine("TypeText");
		StartCoroutine("TypeText", 0);
	}

	void guardStateChange () {
		for (int i = 1; i < guards.Length; i++) {
			guards [i].GetComponent<AI_GuardController> ().job = AI_GuardController.Job.EndRound;
		}
	}
}
