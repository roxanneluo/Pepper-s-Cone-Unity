using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// Control displaying child models.
// traverse through all its childern when showNextModel is called.
public class ModelController : MonoBehaviour {

    public GameObject [] models = null;
    int curIdx = 0;
	// Use this for initialization
	void Start () {
        models = new GameObject[transform.childCount];
        int cnt = 0;
        foreach (Transform child in transform)
        {
            if (child != transform)
            {
                models[cnt++] = child.gameObject;
				child.gameObject.SetActive (false);
            }
        }
        Debug.Assert(cnt == transform.childCount);

		showModel (curIdx);
	}

    public void ShowNextModel()
    {
		models [curIdx].SetActive (false);
        curIdx = (curIdx + 1) % models.Length;
		models [curIdx].SetActive (true);
    }

    protected void showModel(int idx)
    {
        for (int i = 0; i < models.Length; ++i)
        {
			models[i].SetActive(i == idx);
        }
    }
}
