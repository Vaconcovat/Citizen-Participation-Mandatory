using UnityEngine;
using System.Collections;

public class PlayButtonAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Audio() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play();
	}
}
