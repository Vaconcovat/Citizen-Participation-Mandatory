using UnityEngine;
using System.Collections;

public class Advertisement : MonoBehaviour {

	public Material[] AdvertisementTextures;
	public bool autoSwap = false;
	public float timer = 0;
	public float readyTime;

	// Update is called once per frame
	void Update () {
		if(autoSwap){
			if(timer > 0){
				timer -= Time.deltaTime;
			}
			else{
				timer = readyTime;
				SwapAdvert ();
			}
		}
		else{
			if(Vector3.Distance(FindObjectOfType<PlayerController>().transform.position, transform.position) < 5 && !autoSwap){
				if(Input.GetKeyDown(KeyCode.E)){
					SwapAdvert();
				}
			}
		}
	}

	public void SwapAdvert(){
		int x = Random.Range (1, AdvertisementTextures.Length);
		GetComponent<Renderer> ().material = AdvertisementTextures [x];
		}
}
