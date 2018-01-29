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
}
