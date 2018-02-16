using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggActionStub : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.H))
        {
            LumyEditorManager.instance.PushHead(5);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            LumyEditorManager.instance.PushTail(5);
        }
    }
}
