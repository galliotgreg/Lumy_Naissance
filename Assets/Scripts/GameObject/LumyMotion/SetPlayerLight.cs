using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerLight : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Light light = GetComponent<Light>();
        Color color = Color.white;

        if (transform.parent != null)
        {
            AgentEntity agent = transform.parent.parent.GetComponent<AgentEntity>();
            if (agent == null)
            {
                agent = transform.parent.GetComponent<AgentEntity>();
            }
            if (agent.Home != null && agent.Authority == PlayerAuthority.Player2)
            {
                color = new Color32(230, 60, 250, 255);
            }
        }
        light.color = color;
    }
}
