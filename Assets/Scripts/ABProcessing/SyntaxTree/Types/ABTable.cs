using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTable<T> : IABType where T : IABSimpleType
{
    private T[] values;

    public T[] Values
    {
        get
        {
            return values;
        }

        set
        {
            this.values = value;
        }
    }
}
