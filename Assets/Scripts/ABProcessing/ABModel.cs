using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABModel {
    private string behaviorModelIdentifier;

    private int lastStateId = 0;
    private int lastTransitionId = 0;

    public int initStateId = 0;
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
		foreach( ABState state in states ){
			if (state.Id == stateID) {
				return state;
			}
		}
		return null;
		// ATTENTION : does not work due the deletion
		/*
		if (stateID < 0 || stateID >= states.Count) {
			return null;
		}
		return 
		return states [stateID];
		*/
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
            //throw new NotSupportedException();
            return false;
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

	public ABTransition getTransition(int id){
		foreach(ABTransition t in transitions ){
			if (t.Id == id) {
				return t;
			}
		}
		return null;
	}

    public ABTransition shiftIDTransition(int id)
    {
        lastTransitionId--;
        //decrement the ID of the following transitions
        foreach (ABTransition t in transitions)
        {
            if (t.Id > id)
            {
                t.Id--;
            }
        }
        return null;
    }

	public void exchangeTransitionPositions(ABTransition transA, ABTransition transB) {
		int indexA = -1;
		int indexB = -1;

		// Find transition indexes
		for( int i=0; i < transitions.Count; i++ )
		{
			if (transitions[i] == transA)
			{
				indexA = i;
			}
			if (transitions[i] == transB)
			{
				indexB = i;
			}
			if (indexA >= 0 && indexB >= 0) {
				break;
			}
		}

		if (indexA >= 0 && indexB >= 0) {
			transitions [indexA] = transB;
			transitions [indexB] = transA;
		}
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
                if (transition.Start.Name == "spawn scoot" && transition.End.Name == "lay scoot")
                {
                    Debug.Log("TODO Remove this debug stub");
                }
				if (transition.Condition == null) {
					return transition.End.Id;
				} else {
					try{
						AB_BoolGate_Operator condition = transition.Condition;
						ABBool result = condition.EvaluateOperator (context);

						if (result.Value) {
							return transition.End.Id;
						}
					}catch( SyntaxTree_MC_Exception syntaxEx ){
						throw new Condition_MC_Exception( transition, syntaxEx );
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

	#region DELETE
	public bool delete( ABState _state ){
		ABState state = getState ( _state.Id );
		if( state != null ){
			return states.Remove ( state );
		}
		return false;
	}
	public bool delete( ABTransition _transition ){
		ABState start = getState ( _transition.Start.Id );
		ABState end = getState ( _transition.End.Id );
		return UnlinkStates (start.Name, end.Name);
	}
	#endregion

	public static System.Type ParamTypeToType( ParamType type ){
		switch ( type ) {
		case ParamType.Bool:
			return typeof(ABBool);
		case ParamType.Color:
			return typeof(ABColor);
		case ParamType.Ref:
			return typeof(ABRef);
		case ParamType.Scalar:
			return typeof(ABScalar);
		case ParamType.Text:
			return typeof(ABText);
		case ParamType.Vec:
			return typeof(ABVec);
		case ParamType.BoolTable:
			return typeof(ABTable<ABBool>);
		case ParamType.ColorTable:
			return typeof(ABTable<ABColor>);
		case ParamType.RefTable:
			return typeof(ABTable<ABRef>);
		case ParamType.ScalarTable:
			return typeof(ABTable<ABScalar>);
		case ParamType.TextTable:
			return typeof(ABTable<ABText>);
		case ParamType.VecTable:
			return typeof(ABTable<ABVec>);
		}
		return null;
	}
}
