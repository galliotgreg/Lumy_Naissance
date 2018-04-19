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

	/*public int Evaluate(ABContext context)
    {
        int nbTransitionFires = 0;

        curStateId = model.InitStateId;

        while (!model.HasAction(curStateId) 
			//&& curStateId != -1	)// Replaces (nbTransitionFires++ < maxTransitionFires)
            && nbTransitionFires++ < maxTransitionFires)
        {
            curStateId = model.FireTransition(curStateId, context);
        }

        return curStateId;
    }*/

	public Tracing Evaluate(ABContext context){
		return EvaluateRec (context, model.getState ( model.InitStateId ), maxTransitionFires);
	}

	public Tracing EvaluateRec(ABContext context, ABState currentState, int nbFireLeft){
        if (model.HasAction (currentState.Id)) {
			return new Terminal_Tracing (new TracingInfo (currentState));
		} else {
			int nextStateId = model.FireTransition(currentState.Id, context);

            if (nextStateId == -1) {
                // No active transition
                return new Terminal_Tracing(new TracingInfo(currentState));
            } else if (nbFireLeft == 0) {
                return new Terminal_Tracing(new TracingInfo(currentState));
            } else {
                // Next state
                ABState nextState = model.getState(nextStateId);
                return new Intermediate_Tracing(
                    EvaluateRec(context, model.getState(nextStateId), nbFireLeft - 1), 
                    new TracingInfo(currentState));
            }
		}
	}
}
