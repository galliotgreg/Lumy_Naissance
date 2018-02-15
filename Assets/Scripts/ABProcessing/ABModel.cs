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

    public List<ABState> States
    {
        get
        {
            return states;
        }
    }

    public List<ABTransition> Transitions
    {
        get
        {
            return transitions;
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

	public ABState getState( int stateID ){
		if (stateID < 0 || stateID >= states.Count) {
			return null;
		}
		return states [stateID];
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

	public bool UnlinkStates(string startName, string endName)
	{
		ABState start = FindState(startName);
		ABState end = FindState(endName);

		if (start == null || end == null)
		{
			throw new NotSupportedException();
		}

		ABTransition transition = FindTransition(start,end);

		// Remove from transitions
		if( !transitions.Remove(transition) ){
			return false;
		}

		// Remove from start
		if( start.Outcomes.Remove(transition) ){
			return false;
		}

		return true;
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
		ABState state = getState( stateId );
        return state.Name;
    }

    public bool HasAction(int stateId)
    {
		ABState state = getState(stateId);
		return state != null && state.Action != null;
    }

    public int FireTransition(int stateId, ABContext context)
    {
		ABState state = getState(stateId);

		if (state != null) {
			foreach (ABTransition transition in state.Outcomes) {
				if (transition.Condition == null) {
					return transition.End.Id;
				} else {
					AB_BoolGate_Operator condition = transition.Condition;
					ABBool result = condition.Evaluate (context);
					if (result.Value) {
						return transition.End.Id;
					}
				}
			}
		}

        return -1;
    }

    public ABAction GetAction(int stateId)
    {
		ABState state = getState(stateId);

		if (state == null) {
			return null;
		}

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

	private ABTransition FindTransition(ABState startState, ABState endState)
	{
		foreach (ABTransition transition in transitions)
		{
			if (transition.Start == startState && transition.End == endState)
			{
				return transition;
			}
		}
		return null;
	}
}
