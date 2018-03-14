using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABMacroOperator<T> : ABOperator<T>
{
    private string className;
    private string viewName;

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

    public string ViewName
    {
        get
        {
            return viewName;
        }

        set
        {
            viewName = value;
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
