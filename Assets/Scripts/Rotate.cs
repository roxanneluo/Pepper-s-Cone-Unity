using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	public int axis = 1; // rotate around which axis
	// Update is called once per frame
	void LateUpdate () {
		Vector3 rot_euler = Vector3.zero;
		rot_euler [axis] = RotationManager.RotationAngle;
		transform.localEulerAngles = rot_euler;
	}
}
