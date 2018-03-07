using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_LumyCast : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (DEBUG_Manager.instance.debugCast == true) {

            gameObject.SetActive(true);

            Renderer rend = GetComponent<Renderer>();
            //rend.material.shader = Shader.Find("Emmisive");
            string caste = gameObject.GetComponentInParent<AgentScript>().Cast;
            if (caste == "ouvrier") {

                rend.material.color = Color.green;
            }
            if (caste == "scoot") {

                rend.material.color = Color.blue;
            }
            if (caste == "soldier") {

                rend.material.color = Color.red;
            }
        }
        else {
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
