using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABColorParam : ABParam<ABColor>
{
    public ABColorParam(string identifier, ABColor value) : base(identifier, value)
    {
        this.identifier = identifier;
        this.value = value;
    }

	#region implemented abstract members of ABParam

	protected override IABParam CloneParam ()
	{
		int red = this.value.Value == ABColor.Color.Red ? 1 : 0;
		int green = this.value.Value == ABColor.Color.Green ? 1 : 0;
		int blue = this.value.Value == ABColor.Color.Blue ? 1 : 0;
		return ABParamFactory.CreateColorParam ( this.identifier, red, green, blue );
	}

	#endregion
}
