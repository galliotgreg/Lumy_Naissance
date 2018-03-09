using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_RefGate_Operator : ABGateOperator<ABRef> {
	protected override ABRef Evaluate(ABContext context)
	{
		ABRef r = null;
		ABNode input = Inputs[0];
		r = OperatorHelper.Instance.getRefParam(context, input);
		return r;
	}
}
