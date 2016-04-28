using UnityEngine;
using System.Collections;

public class MenuCamera : MonoBehaviour {
	/// <summary>
	/// 0 = main menu, 1 = zoomed out
	/// </summary>
	public int state;
	public Transform[] waypoints;
	public float speed;
	public AudioSource audioP;

	// Update is called once per frame
	void Update () {
		switch(state){
			case 0:
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(waypoints[0].position.x,waypoints[0].position.y,-10),speed);
				break;
			case 1:
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(waypoints[1].position.x,waypoints[1].position.y,-10),speed);
				break;
		}
	}

	public void MainMenu(){
		state = 0;
		audioP.Stop();
	}

	public void ZoomedOut(){
		state = 1;
		audioP.Play();
	}
}
