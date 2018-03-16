using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_ColorTabGate_Operator : ABGateOperator<ABTable<ABColor>>
{
    protected override ABTable<ABColor> Evaluate(ABContext context)
    {
        ABTable<ABColor> cTab = null;
        ABNode input = Inputs[0];
        cTab = OperatorHelper.Instance.getTabColorParam(context, input);
        return cTab;
    }
}
