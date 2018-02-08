using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuitApplication : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(Quit);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Quit()
    {
        Application.Quit();
    }
}
