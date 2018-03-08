using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Color color = Color.white;

        if (transform.parent != null)
        {
            AgentEntity agent = transform.parent.parent.GetComponent<AgentEntity>();
            if (agent.Home != null && agent.Authority == PlayerAuthority.Player2)
            {
                color = new Color32(50, 0, 90, 255);
            }
        }
        meshRenderer.material.color = color;
    }
}
