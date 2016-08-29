using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUp : MonoBehaviour {
	public Image Status;

	public void OnMouseEnter()
	{
		Status.gameObject.SetActive (true);
	}

	public void OnMouseExit()
	{
		Status.gameObject.SetActive (false);
	}
}
