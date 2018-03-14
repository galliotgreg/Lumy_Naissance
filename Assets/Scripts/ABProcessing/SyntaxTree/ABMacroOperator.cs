using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABMacroOperator<T> : ABOperator<T>
{
    private string className;

    public override string ClassName
    {
        get
        {
            return className;
        }

        set
        {
            className = value;
        }
    }

    public override T Evaluate(ABContext context)
    {
        throw new System.NotImplementedException();
    }

    public void AllocInputs(int nbInputs)
    {
        inputs = new ABNode[nbInputs];
    }
}
