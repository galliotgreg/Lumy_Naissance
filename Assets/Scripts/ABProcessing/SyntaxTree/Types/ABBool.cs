using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABBool : IABSimpleType
{
    private bool value;

    public bool Value
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
