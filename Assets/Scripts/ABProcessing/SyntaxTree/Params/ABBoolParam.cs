using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABBoolParam : ABParam<ABBool>
{
    public ABBoolParam(string identifier, ABBool value) : base(identifier, value)
    {
        this.identifier = identifier;
        this.value = value;
    }

	#region implemented abstract members of ABParam

	protected override IABParam CloneParam ()
	{
		return ABParamFactory.CreateBoolParam ( this.identifier, this.value.Value );
	}

	#endregion
}
