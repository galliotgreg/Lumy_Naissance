using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_VecGate_Operator : ABGateOperator<ABTable<ABVec>>
{
    public override ABTable<ABVec> Evaluate(ABContext context)
    {
        ABTable<ABVec> vTab = null;
        ABNode input = Inputs[0];
        vTab = OperatorHelper.Instance.getTabVecParam(context, input);
        return vTab;
    }
}
