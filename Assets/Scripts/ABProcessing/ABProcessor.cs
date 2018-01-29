using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABProcessor {
    /// <summary>
    /// Process the given ABInstance
    /// </summary>
    /// <param name="behaviorInstance"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public ABAction ProcessABInstance(ABInstance behaviorInstance, ABContext context)
    {
        int finalStateId = behaviorInstance.Evaluate(context);
        return behaviorInstance.Model.GetAction(finalStateId);
    }
}
