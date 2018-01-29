

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTextParam : ABParam<ABText> {
    public ABTextParam(string identifier, ABText value) : base(identifier, value)
    {
        this.identifier = identifier;
        this.value = value;
    }
}
