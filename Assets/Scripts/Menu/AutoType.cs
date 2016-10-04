﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoType : MonoBehaviour {
	public bool typeOnAwake;
	public int maxLines;
	[TextArea(1,20)]
	public string[] displayedText;
	public float[] textDelays;
	public Color[] textColors;
	public GameObject finishedCall;
	public string finishedCallString;
	public AudioSource blip;
	public int charactersPerTick = 1;
	int characterCounter;
	Text textObj;
	int numlines;
	bool alternate = true;
	int characters;

	// Use this for initialization
	void Start () {
		characters = charactersPerTick;
		if(charactersPerTick < 1){
			charactersPerTick = 1;
		}
		textObj = GetComponent<Text>();
		textObj.text = "";
		if(typeOnAwake){
			StartCoroutine("TypeText", 0);
		}
		if(blip != null){
			float newVolume = PlayerPrefs.GetFloat("SFXVolume") * blip.volume;
			blip.volume = newVolume;
		}
	}

	void Update(){
		if(Input.GetKey(KeyCode.Space)){
			characters = charactersPerTick * 2;
		}
		else{
			characters = charactersPerTick;
		}
	}

	
	IEnumerator TypeText(int index){
		//Debug.Log("Coroutine Running");
		//characters = charactersPerTick;
		if(textColors[index] != new Color(0,0,0,0)){
			string preTag, postTag, content, old;
			preTag = "<color=" + ColorToHex(textColors[index]) + ">";
			postTag = "</color>";
			content = "";
			old = textObj.text;
			foreach (char letter in displayedText[index].ToCharArray()){
				content += letter;
				textObj.text = old + preTag + content + postTag;
				if(textObj.text.Split('\n').Length > maxLines){
					textObj.text = textObj.text.Substring(textObj.text.IndexOf('\n')+1);
				}
				characterCounter++;
				if(characterCounter == characters){
					characterCounter = 0;
					if (blip != null && letter != ' '){
						if(alternate){
							blip.Play();
						}
						alternate = !alternate;
					}
					yield return new WaitForSeconds(Mathf.Max(textDelays[index],0.01f));
				}
			}
		}
		else{
			foreach (char letter in displayedText[index].ToCharArray()){
				textObj.text += letter;
				if(textObj.text.Split('\n').Length > maxLines){
					textObj.text = textObj.text.Substring(textObj.text.IndexOf('\n')+1);
				}
				characterCounter++;
				if(characterCounter == characters){
					characterCounter = 0;
					if (blip != null && letter != ' '){
						if(alternate){
							blip.Play();
						}
						alternate = !alternate;
					}
					yield return new WaitForSeconds(Mathf.Max(textDelays[index],0.01f));
				}
			}
		}



		if (index < displayedText.Length - 1){
			StartCoroutine("TypeText", (index + 1));
		}
		else{
			if(finishedCall != null){
				finishedCall.SendMessage(finishedCallString,SendMessageOptions.DontRequireReceiver);
			}

		}
	}

	public void StartType(){
		characters = charactersPerTick;
		characterCounter = 0;
		textObj = GetComponent<Text>();
		textObj.text = "";
		StopCoroutine("TypeText");
		StartCoroutine("TypeText", 0);
	}

	byte ToByte(float f){
		f = Mathf.Clamp01(f);
		return (byte)(f*255);
	}

	string ColorToHex(Color c){
		return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b), ToByte(c.a));
	}


}
