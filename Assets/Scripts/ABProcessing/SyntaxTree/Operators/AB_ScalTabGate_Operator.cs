using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_ScalTabGate_Operator : ABGateOperator<ABTable<ABScalar>>
{
    protected override ABTable<ABScalar> Evaluate(ABContext context)
    {
        ABTable<ABScalar> sTab = null;
        ABNode input = Inputs[0];
        sTab = OperatorHelper.Instance.getTabScalarParam(context, input);
        return sTab;
    }
}
