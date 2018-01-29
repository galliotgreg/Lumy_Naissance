using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABScalar : IABSimpleType
{
    private float value;

    public float Value
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
