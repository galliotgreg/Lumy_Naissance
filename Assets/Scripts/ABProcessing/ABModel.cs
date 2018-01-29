using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABModel {
    private string behaviorModelIdentifier;

    private int lastStateId = 0;
    private int lastTransitionId = 0;

    private int initStateId = 0;
    private List<ABState> states = new List<ABState>();
    private List<ABTransition> transitions = new List<ABTransition>();

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

    public int InitStateId
    {
        get
        {
            return initStateId;
        }

        set
        {
            initStateId = value;
        }
    }

    //CONSTRUCTION
    public int AddState(string name, ABAction action)
    {
        int id = lastStateId++;
        ABState state = new ABState(id, name);
        if (action != null)
        {
            state.Action = action;
        }
        states.Add(state);

        return id;
    }

    public int LinkStates(string startName, string endName)
    {
        int id = lastTransitionId++;
        ABState start = FindState(startName);
        ABState end = FindState(endName);

        if (start == null || end == null)
        {
            throw new NotSupportedException();
        }

        ABTransition transition = new ABTransition(id, start, end);
        transitions.Add(transition);

        start.Outcomes.Add(transition);

        return id;
    }

    public void SetCondition(int transitionId, AB_BoolGate_Operator condition)
    {
        ABTransition transition = FindTransition(transitionId);
        transition.Condition = condition;
    }

    public IABGateOperator FindStateGate(string stateName, int id)
    {
        ABState state = FindState(stateName);
        return state.Action.Parameters[id];
    }

    //ACCESS
    public String GetStateName(int stateId)
    {
        ABState state = states[stateId];
        return state.Name;
    }

    public bool HasAction(int stateId)
    {
        ABState state = states[stateId];
        return state.Action != null;
    }

    public int FireTransition(int stateId, ABContext context)
    {
        ABState state = states[stateId];
        foreach (ABTransition transition in state.Outcomes)
        {
            if (transition.Condition == null)
            {
                return transition.End.Id;
            } else
            {
                AB_BoolGate_Operator condition = transition.Condition;
                ABBool result = condition.Evaluate(context);
                if (result.Value)
                {
                    return transition.End.Id;
                }
            }
        }

        return -1;
    }

    public ABAction GetAction(int stateId)
    {
        ABState state = states[stateId];
        return state.Action;
    }

    //INTERN UTILS
    private ABState FindState(string name)
    {
        foreach (ABState state in states)
        {
            if (state.Name == name)
            {
                return state;
            }
        }
        return null;
    }

    private ABTransition FindTransition(int id)
    {
        foreach (ABTransition transition in transitions)
        {
            if (transition.Id == id)
            {
                return transition;
            }
        }
        return null;
    }
}
