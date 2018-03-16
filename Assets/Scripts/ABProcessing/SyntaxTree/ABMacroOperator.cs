using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABMacroOperator<T> : ABOperator<T>
{
    private string className;
    private string viewName;
    private string symbolName;

    private ABOperator<T> wrappedTree;

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

    public string SymbolName
    {
        get
        {
            return symbolName;
        }

        set
        {
            symbolName = value;
        }
    }

    public ABOperator<T> WrappedTree
    {
        get
        {
            return wrappedTree;
        }

        set
        {
            wrappedTree = value;
        }
    }

    protected override T Evaluate(ABContext context)
    {
        ABContext subContext = ComputeSubContext(context);
        return wrappedTree.EvaluateOperator(subContext);
    }

    private ABContext ComputeSubContext(ABContext context)
    {
        throw new NotImplementedException();
    }

    public void AllocInputs(int nbInputs)
    {
        inputs = new ABNode[nbInputs];
    }
}
