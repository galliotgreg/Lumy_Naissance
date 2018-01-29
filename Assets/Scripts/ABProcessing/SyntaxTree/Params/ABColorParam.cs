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
}
