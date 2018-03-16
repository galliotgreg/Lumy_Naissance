using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_BoolTabGate_Operator : ABGateOperator<ABTable<ABBool>>
{
    protected override ABTable<ABBool> Evaluate(ABContext context)
    {
        ABTable<ABBool> bTab = null;
        ABNode input = Inputs[0];
        bTab = OperatorHelper.Instance.getTabBoolParam(context, input);
        return bTab;
    }
}
