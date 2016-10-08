using UnityEngine;
using System.Collections;

public class TutorialMarker : MonoBehaviour {

	public enum MarkerType{CameraInfo, CameraHeaderDown, CameraHeaderLeft, BinInfo, BinHeaderRight, VendorInfo, VendorHeaderLeft, TargetInfo, TargetHeaderAbove};

	public GameObject prefab;
	public string text;
	string CustomText;
	public bool active;
	GameObject spawned;
	public MarkerType marker;
	
	// Use this for initialization
	void Start () {
		switch (marker) {
		case MarkerType.CameraHeaderDown:
			text = "CAMERA \n (Mouse Over the Info Box below for more info)";
			break;
		case MarkerType.CameraHeaderLeft:
			text = "CAMERA \n (Mouse Over the Info Box to the Left for more info)";
			break;
		case MarkerType.BinHeaderRight:
			text = "BIN \n (Mouse Over the Info Box to the Right for more info)";
			break;
		case MarkerType.VendorHeaderLeft:
			text = "VENDOR \n (Mouse Over the Info Box to the Left for more info)";
			break;
		case MarkerType.TargetHeaderAbove:
			text = "TARGET \n (Mouse Over the Info Box Above for more info)";
			break;
		case MarkerType.BinInfo:
			text = "Throw your Weapon in here \n When you are done \n By mousing over the bin \n and Pressing RMB";
			break;
		case MarkerType.CameraInfo:
			text = "This Camera will \n Follow your Every Move \n Make sure to be on Camera \n if you want your actions to count";
			break;
		case MarkerType.VendorInfo:
			text = "This Vendor will \n Dispense Weapons \n Walk up to the vendor and \n Press 'E' to Recieve a new Weapon";
			break;
		case MarkerType.TargetInfo:
			text = "Please Kill this Target by \n Mousing over them and pressing LMB";
			break;
		}
		spawned = (GameObject)Instantiate(prefab);
		spawned.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
		UI_TutorialMarker tm = spawned.GetComponent<UI_TutorialMarker>();
		tm.target = transform;
		tm.text = text;
	}

	void Update(){
		if(spawned != null){
			if(!active){
				spawned.SetActive(false);
			}
			else{
				spawned.SetActive(true);
			}
		}

	}
}
