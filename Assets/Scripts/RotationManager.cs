using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

// Serve as the interface between GoogleVR to read rotation 
// so that if GogoleVR changes, only this file needs to be changed.
public class RotationManager : MonoBehaviour {
	// I use initRotation and get eulerAngles.z to avoid Gimbal lock.
	public static float RotationAngle {
		get {
			if (instance == null || instance.numInitFrame > 0) {
				return 0f;
			}

			// rotate gvr rotation first to avoid Gimbal lock.
			Quaternion rot = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
			float rotAngle =  (rot * Quaternion.Inverse(instance.initRotation) * instance.GvrRotation()).eulerAngles.z;
			print (rotAngle);
			return rotAngle;
		}
	}

	protected static RotationManager instance = null;
	public Quaternion initRotation = Quaternion.identity;

	// need some intial number of frames to kick off.
	public int numInitFrame = 10;


	// setup the instance
	void Awake() {
		if (instance != null) {
			Destroy (gameObject); 
			return;
		}
		instance = this;
	}

	protected Quaternion GvrRotation() {
		print (transform.localRotation.eulerAngles);
		return transform.localRotation;
	}

	void LateUpdate() {
		if (numInitFrame > 0) {
			--numInitFrame;
			ResetRotation ();
			return;
		}
	}
		
	public void ResetRotation() {
		initRotation = GvrRotation ();
	}

}
