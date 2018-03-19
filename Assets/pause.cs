using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	    if(Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0; 
            
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Time.timeScale = 1; 
        }
	}
}
