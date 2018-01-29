using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABOperator<T> : ABNode, IABOperator
{
    protected ABNode[] inputs;

    public ABNode[] Inputs
    {
        get
        {
            return inputs;
        }

        set
        {
            inputs = value;
        }
    }

    public abstract T Evaluate(ABContext context);
}
