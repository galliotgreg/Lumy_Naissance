using System;
using System.Collections;
using System.Collections.Generic;

public class ABColor : IABSimpleType
{
    public enum Color
    {
        Red,
        Green,
        Blue,
    }

    private Color value;

    public Color Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;
        }
    }
}
