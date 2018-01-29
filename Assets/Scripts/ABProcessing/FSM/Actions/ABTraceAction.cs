using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTraceAction : ABAction
{
    public ABTraceAction()
    {
        this.type = ActionType.Trace;
        this.parameters = new IABGateOperator[2];
        this.parameters[0] = new AB_ColorGate_Operator();
        this.parameters[1] = new AB_VecGate_Operator();
    }
}
