﻿using UnityEngine;
using System.Collections;

public class RagdollController : MonoBehaviour {

	public Component[] bones;
	public Animator anim;
	public bool isDead;

	// Use this for initialization
	void Start () 
	{
		bones = gameObject.GetComponentsInChildren<Rigidbody> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isDead) 
		{
			killRagdoll ();
		}
	
	}

	void killRagdoll () 
	{
		foreach (Rigidbody ragdoll in bones) 
		{
			ragdoll.isKinematic = false;
		}
		anim.enabled = false;
	}
}
