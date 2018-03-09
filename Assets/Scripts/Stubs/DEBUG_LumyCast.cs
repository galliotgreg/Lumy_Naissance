using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUG_LumyCast : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (DEBUG_Manager.instance.debugCast == true) {

            gameObject.SetActive(true);

            Renderer rend = GetComponent<Renderer>();
            //rend.material.shader = Shader.Find("Emmisive");
            string caste = gameObject.GetComponentInParent<AgentScript>().Cast;

            Text castName = gameObject.GetComponentInChildren<Text>();
            castName.text = caste;
        }
        else {
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
