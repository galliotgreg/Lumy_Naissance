using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUG_mineraisStock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        if (DEBUG_Manager.instance.debugMineraiStock == true) {

            gameObject.SetActive(true);

            string stock = GetComponentInParent<ResourceScript>().Stock.ToString();
            Text stockText = gameObject.GetComponentInChildren<Text>();
            stockText.text = stock;
        }
        else {
            gameObject.SetActive(false);
        }
    }
    
}
