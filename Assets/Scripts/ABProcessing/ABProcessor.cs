using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABProcessor {
    /*
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
    }*/

	/// <summary>
	/// Process the given ABInstance
	/// </summary>
	/// <param name="behaviorInstance"></param>
	/// <param name="context"></param>
	/// <returns></returns>
	public Tracing ProcessABInstance(ABInstance behaviorInstance, ABContext context)
	{
		return behaviorInstance.Evaluate(context);
	}

	public ABAction actionFromTracing( ABInstance behaviorInstance, Tracing processedTracing ){
		int lastStateId = processedTracing.getLastTracingInfo ().State.Id;
		return behaviorInstance.Model.GetAction (lastStateId);
	}
}
