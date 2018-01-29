using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABRefParam : ABParam<ABRef>
{
    public ABRefParam(string identifier, ABRef value) : base(identifier, value)
    {
        this.identifier = identifier;
        this.value = value;
    }
}
