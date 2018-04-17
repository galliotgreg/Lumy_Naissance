using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAgentLight : MonoBehaviour {

    private AgentEntity[] agents; 

	// Disable all Light in the Agents each frame
    // Used in EditCasteScene, in case the current agent is changed. 
	void Update () {

        agents = GameObject.FindObjectsOfType<AgentEntity>();

        for (int i=0; i<agents.Length; i++)
        {
            GameObject lumyLight = agents[i].GetComponentInChildren<SetPlayerLight>().gameObject;
            if (lumyLight == null)
            {
                return; 
            }
            Light light = lumyLight.GetComponent<Light>(); 
            if (light != null)  
            {
                light.intensity = 0.5f;
                light.range = 5; 
            }
        }
    }
}
