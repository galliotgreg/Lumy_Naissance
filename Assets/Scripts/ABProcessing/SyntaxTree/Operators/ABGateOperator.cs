using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABGateOperator<T> : ABOperator<T>, IABGateOperator
{
    public ABGateOperator()
    {
        this.Inputs = new ABNode[1];
    }
}
