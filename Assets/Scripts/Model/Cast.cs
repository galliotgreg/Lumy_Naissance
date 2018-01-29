using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast {
    private string behaviorModelIdentifier;
    private List<AgentComponent> head = new List<AgentComponent>();
    private List<AgentComponent> tail = new List<AgentComponent>();

    public string BehaviorModelIdentifier
    {
        get
        {
            return behaviorModelIdentifier;
        }

        set
        {
            behaviorModelIdentifier = value;
        }
    }

    public List<AgentComponent> Head
    {
        get
        {
            return head;
        }

        set
        {
            head = value;
        }
    }

    public List<AgentComponent> Tail
    {
        get
        {
            return tail;
        }

        set
        {
            tail = value;
        }
    }
}
