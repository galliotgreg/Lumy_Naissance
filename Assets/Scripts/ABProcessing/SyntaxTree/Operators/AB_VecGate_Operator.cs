using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_VecGate_Operator : ABGateOperator<ABVec>
{
    protected override ABVec Evaluate(ABContext context)
    {
        ABVec b = null;
        ABNode input = Inputs[0];
        b = OperatorHelper.Instance.getVecParam(context, input);
        return b;
    }
}
