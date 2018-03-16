using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_TextTabGate_Operator : ABGateOperator<ABTable<ABText>>
{
    protected override ABTable<ABText> Evaluate(ABContext context)
    {
        ABTable<ABText> tTab = null;
        ABNode input = Inputs[0];
        tTab = OperatorHelper.Instance.getTabTxtParam(context, input);
        return tTab;
    }
}