using System;
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
		int red = 0;
		int green = 0;
		int blue = 0;
        if (this.value.Value == ABColor.Color.Red)
        {
            red = 255;
            green = 0;
            blue = 0;
        }
        else if (this.value.Value == ABColor.Color.Green)
        {
            red = 0;
            green = 255;
            blue = 0;
        }
        else if (this.value.Value == ABColor.Color.Blue)
        {
            red = 0;
            green = 0;
            blue = 255;
        }
        else if (this.value.Value == ABColor.Color.Yellow)
        {
            red = 255;
            green = 255;
            blue = 0;
        }
        else if (this.value.Value == ABColor.Color.Magenta)
        {
            red = 255;
            green = 0;
            blue = 255;
        }
        else if (this.value.Value == ABColor.Color.Cyan)
        {
            red = 0;
            green = 255;
            blue = 255;
        }
        else
        {
            throw new NotImplementedException();
        }
        return ABParamFactory.CreateColorParam ( this.identifier, red, green, blue );
	}

	#endregion
}
