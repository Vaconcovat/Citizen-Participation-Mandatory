using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationHandler : MonoBehaviour {

	public List<Animation> animations = new List<Animation>();
	int sizeofList;

	// Use this for initialization
	void Start () {
		sizeofList = animations.Count;

	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i <= sizeofList; i++) 
		{
			if (Input.GetKey (animations [i].Button)) {
				Debug.Log (animations [i].Button); //Testing Purposes right now. but here is where you would tell unity to play the animation
			}
		}
	}
}

[System.Serializable]
public class Animation
{
	[Tooltip("The Key that will need to be pressed to run the animation")]
	public KeyCode Button; 
	[Tooltip("The Animation that will be played")]
	public AnimationClip AnimationToBeRun; 
}