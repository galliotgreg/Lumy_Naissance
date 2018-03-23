using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTest : MonoBehaviour {

    [SerializeField]
    private Toggle toggle;

    private bool boolTest;
	// Use this for initialization
	void Start () {
        boolTest = true;

        toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggle); });

	}

    private void ToggleValueChanged(Toggle toggle)
    {
        boolTest = !boolTest;
        Debug.Log(boolTest);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
