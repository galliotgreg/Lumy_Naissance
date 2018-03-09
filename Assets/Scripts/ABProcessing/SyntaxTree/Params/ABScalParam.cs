using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABScalParam : ABParam<ABScalar>
{
    public ABScalParam(string identifier, ABScalar value) : base(identifier, value)
    {
        this.identifier = identifier;
        this.value = value;
    }

	#region implemented abstract members of ABParam

	protected override IABParam CloneParam ()
	{
		return ABParamFactory.CreateScalarParam ( this.identifier, this.value.Value );
	}

	#endregion
}
