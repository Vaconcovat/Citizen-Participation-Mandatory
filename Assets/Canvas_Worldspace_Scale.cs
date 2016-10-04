using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (UnityEngine.UI.AspectRatioFitter))]
public class Canvas_Worldspace_Scale : MonoBehaviour {

	void OnEnable(){
		GetComponent<AspectRatioFitter>().aspectRatio = (Screen.width * 1.0f) / (Screen.height * 1.0f);
	}
}
