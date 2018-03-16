using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_RefTabGate_Operator : ABGateOperator<ABTable<ABRef>>
{
    protected override ABTable<ABRef> Evaluate(ABContext context)
    {
        ABTable<ABRef> rTab = null;
        ABNode input = Inputs[0];
        rTab = OperatorHelper.Instance.getTabRefParam(context, input);
        return rTab;
    }
}
