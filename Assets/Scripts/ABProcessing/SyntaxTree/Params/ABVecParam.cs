using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABVecParam : ABParam<ABVec> {
    public ABVecParam(string identifier, ABVec value) : base(identifier, value)
    {
        this.identifier = identifier;
        this.value = value;
    }
}
