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
}
