using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoAction : MonoBehaviour {
    // Workaround for script enabling issues
    public bool activated;

    [SerializeField]
    private Vector3[] path;
    private AgentContext agentContext;

    public Vector3[] Path
    {
        get
        {
            return path;
        }

        set
        {
            path = value;
        }
    }

    // Use this for initialization
    void Start () {
        agentContext = GetComponent<AgentContext>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!activated)
        {
            return;
        }

        if (path.Length > 0)
        {
            agentContext.Self.GetComponent<AgentScript>().TrgPos = path[0];
        }

        if (Vector3.Distance(
            agentContext.Self.GetComponent<AgentScript>().TrgPos,
            agentContext.Self.GetComponent<AgentScript>().CurPos) < 0.1f)
        {
            this.transform.position = new Vector3(
                agentContext.Self.GetComponent<AgentScript>().TrgPos.x, 
                0f, 
                agentContext.Self.GetComponent<AgentScript>().TrgPos.y);
        }
        else
        {
            Vector3 dir = (
                agentContext.Self.GetComponent<AgentScript>().TrgPos 
                - agentContext.Self.GetComponent<AgentScript>().CurPos).normalized;
            float speed = 5f;
            this.transform.position +=
                Time.deltaTime * speed * new Vector3(dir.x, 0f, dir.y);
        }
    }
}
