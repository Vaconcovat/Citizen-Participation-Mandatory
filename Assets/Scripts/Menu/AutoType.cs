﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoType : MonoBehaviour {
	public bool typeOnAwake;
	public int maxLines;
	[TextArea(1,20)]
	public string[] displayedText;
	public float[] textDelays;
	public GameObject finishedCall;
	public string finishedCallString;
	Text textObj;
	int numlines;

	// Use this for initialization
	void Start () {
		textObj = GetComponent<Text>();
		textObj.text = "";
		if(typeOnAwake){
			StartCoroutine(TypeText(0));
		}
	}
	
	IEnumerator TypeText(int index){
		foreach (char letter in displayedText[index].ToCharArray()){
			textObj.text += letter;
			if(textObj.text.Split('\n').Length > maxLines){
				textObj.text = textObj.text.Substring(textObj.text.IndexOf('\n')+1);
			}
			yield return new WaitForSeconds(textDelays[index]);
		}
		if (index < displayedText.Length - 1){
			StartCoroutine(TypeText(index + 1));
		}else{
			if(finishedCall != null){
				finishedCall.SendMessage(finishedCallString,SendMessageOptions.DontRequireReceiver);
			}

		}
	}

	public void StartType(){
		StartCoroutine(TypeText(0));
	}
}