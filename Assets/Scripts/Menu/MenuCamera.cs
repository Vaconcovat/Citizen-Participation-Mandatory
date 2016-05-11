using UnityEngine;
using System.Collections;

public class MenuCamera : MonoBehaviour {
	/// <summary>
	/// 0 = main menu, 1 = zoomed out, 2 = post
	/// </summary>
	public int state;
	public Transform[] waypoints;
	public float speed;
	public AudioSource audioP;

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(waypoints[state].position.x,waypoints[state].position.y,-10),speed);
	}

	public void MainMenu(){
		state = 0;
		audioP.Stop();
	}

	public void ZoomedOut(){
		state = 1;
		audioP.Play();
	}

	public void Post(){
		state = 2;
	}
}
