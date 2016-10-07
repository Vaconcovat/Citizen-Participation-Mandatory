using UnityEngine;
using System.Collections;

public class camera_line : MonoBehaviour {
	public Arena_Camera a_camera;
	public Contestant contestant;

	LineRenderer ln;

	// Use this for initialization
	void Start () {
		ln = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(a_camera == null || contestant == null || a_camera.enabled == false){
			Destroy(gameObject);
		}
		else{
			ln.SetPosition(0, a_camera.transform.position);
			ln.SetPosition(1, contestant.transform.position);

			if(!contestant.onCameras.Contains(a_camera)){
				Destroy(gameObject);
			}
	
			if(!contestant.isAlive){
				Destroy(gameObject);
			}

		}

	}
}
