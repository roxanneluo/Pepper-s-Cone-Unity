using UnityEngine;
using System.Collections;

// Controls which scene to show. 
// Currently, it decides whether to show the calibration scene or model scene.
public class SceneController : MonoBehaviour {
	protected int curIdx = 0;
	public GameObject[] scenes;

	// show current scene and hide all the others.
	void Start() {
		for (int i = 0; i < scenes.Length; ++i) {
			scenes [i].SetActive (i == curIdx);
		}
	}

	public void showNext() {
		scenes [curIdx].SetActive (false);
		curIdx = (curIdx + 1) % scenes.Length;
		scenes [curIdx].SetActive (true);
	}
}
