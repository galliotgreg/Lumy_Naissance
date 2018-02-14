using System;
using System.Collections;
using System.Collections.Generic;

public class AB_ColorGate_Operator : ABGateOperator<ABColor>
{
    public override ABColor Evaluate(ABContext context)
    {
		ABNode input = Inputs[0];
		return OperatorHelper.Instance.getColorParam(context, input);
    }
}
