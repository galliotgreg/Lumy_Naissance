using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABInstance {
    private int agentId;
    private ABModel model;
    private int curStateId;
    private int maxTransitionFires = 1000;

    public int AgentId
    {
        get
        {
            return agentId;
        }

        set
        {
            agentId = value;
        }
    }

    public ABModel Model
    {
        get
        {
            return model;
        }

        set
        {
            model = value;
        }
    }

    public int Evaluate(ABContext context)
    {
        int nbTransitionFires = 0;

        curStateId = model.InitStateId;
        while (!model.HasAction(curStateId) 
            && nbTransitionFires++ < maxTransitionFires)
        {
            curStateId = model.FireTransition(curStateId, context);
        }

        return curStateId;
    }
}
