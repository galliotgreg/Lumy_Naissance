using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumyCast : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Renderer rend = GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("Emmisive");
        string caste = gameObject.GetComponentInParent<AgentScript>().Cast;
        if(caste == "ouvrier") {

            rend.material.color = Color.green;
        }
        if (caste == "scoot") {

            rend.material.color = Color.blue;
        }
        if (caste == "soldier") {

            rend.material.color = Color.red;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
