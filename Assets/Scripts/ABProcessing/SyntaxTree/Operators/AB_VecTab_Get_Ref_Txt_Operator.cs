using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_VecTab_Get_Ref_Txt_Operator : ABOperator<ABTable<ABVec>> {

	public AB_VecTab_Get_Ref_Txt_Operator()
	{
		this.Inputs = new ABNode[2];
	}

	public override ABTable<ABVec> Evaluate(ABContext context)
	{
		ABRef r = null;
		ABNode input1 = Inputs[0];
		r = OperatorHelper.Instance.getRefParam(context, input1);        

		ABText t = null;
		ABNode input2 = Inputs[1];
		t = OperatorHelper.Instance.getTextParam(context, input2);        

		//Build then return Result
		ABTable<ABVec> v = (ABTable<ABVec>) r.GetAttr( t.Value );

		ABTable<ABVec> result = TypeFactory.CreateEmptyTable<ABVec>();
		result.Values = v.Values;
		return result;
	}
}
