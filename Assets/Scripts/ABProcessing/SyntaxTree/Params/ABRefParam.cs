using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABRefParam : ABParam<ABRef>
{
    public ABRefParam(string identifier, ABRef value) : base(identifier, value)
    {
        this.identifier = identifier;
        this.value = value;
    }

	#region implemented abstract members of ABParam

	protected override IABParam CloneParam ()
	{
		return new ABRefParam ( this.identifier, this.value );
	}

	#endregion
}
