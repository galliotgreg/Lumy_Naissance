using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABText : IABSimpleType
{
    private string value;

    public string Value
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
