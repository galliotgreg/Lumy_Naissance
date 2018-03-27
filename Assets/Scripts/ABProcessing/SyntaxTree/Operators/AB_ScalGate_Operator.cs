using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_ScalGate_Operator : ABGateOperator<ABScalar>
{
	protected override ABScalar Evaluate(ABContext context)
    {
		ABScalar s = null;
		ABNode input = Inputs[0];
		s = OperatorHelper.Instance.getScalarParam(context, input);
		return s;
    }
}
