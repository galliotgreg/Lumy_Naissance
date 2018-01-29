using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_BoolGate_Operator : ABGateOperator<ABBool>
{
    public override ABBool Evaluate(ABContext context)
    {
        ABBool b = null;
        ABNode input = Inputs[0];
        b = OperatorHelper.Instance.getBoolParam(context, input);
        return b;
    }
}
