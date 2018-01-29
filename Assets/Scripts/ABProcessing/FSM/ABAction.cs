using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABAction {
    protected ActionType type;
    protected IABGateOperator[] parameters;

    public ActionType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public IABGateOperator[] Parameters
    {
        get
        {
            return parameters;
        }

        set
        {
            parameters = value;
        }
    }
}
