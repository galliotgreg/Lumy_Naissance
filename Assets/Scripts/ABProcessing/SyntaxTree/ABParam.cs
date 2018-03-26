using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABParam<T> : ABNode, IABParam
{
    protected string identifier;
    protected T value;

	public ABParam(string identifier, T value)
    {
        this.identifier = identifier;
        this.value = value;
    }

    public string Identifier
    {
        get
        {
            return identifier;
        }
    }

    public T Value
    {
        get
        {
            return value;
        }
    }

    public T Evaluate(ABContext context)
    {
		try {
	        if (identifier == "const")
	        {
	            return value;
	        }
	        else
	        {
	            ABParam<T> valuedParam = (ABParam<T>)context.GetParam(identifier);
	            if (valuedParam == null)
	                Debug.LogError(identifier + " not found");
	            return valuedParam.Value;
	        }
		}
		catch( System.Exception ex ){
			throw new Param_MC_Exception ( this, context, ex.Message );
		}
    }

	protected abstract IABParam CloneParam ();

	public IABParam Clone ()
	{
		return CloneParam();
	}

	public System.Type getOutcomeType ()
	{
		return typeof(T);
	}
}
