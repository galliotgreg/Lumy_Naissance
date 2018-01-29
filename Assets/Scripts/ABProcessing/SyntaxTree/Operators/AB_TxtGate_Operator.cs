using System.Collections;
using System.Collections.Generic;

public class AB_TxtGate_Operator : ABGateOperator<ABText>
{
    public override ABText Evaluate(ABContext context)
    {
        ABText t = null;
        ABNode input = Inputs[0];
        t = OperatorHelper.Instance.getTextParam(context, input);
        return t;
    }
}
