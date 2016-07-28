using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (AIController))]
public class FieldOfViewEditor : Editor {

	void OnSceneGUI () {
		AIController fow = (AIController)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
		Handles.color = Color.red;
		Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.detectRaduis);
		Vector3 viewAngleA = fow.DirFromAngle (-fow.viewAngle / 2, false);
		Vector3 viewAngleB = fow.DirFromAngle (fow.viewAngle / 2, false);
	
		Handles.color = Color.white;
		Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
		Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

		Handles.color = Color.blue;
		foreach (Transform visibleTarget in fow.visibleTargets) {
			Handles.DrawLine (fow.transform.position, visibleTarget.position);
		}
	}
}
