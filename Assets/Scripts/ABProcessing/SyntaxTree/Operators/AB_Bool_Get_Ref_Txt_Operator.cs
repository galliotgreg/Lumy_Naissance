using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB_Bool_Get_Ref_Txt_Operator : ABOperator<ABBool>
{
    public AB_Bool_Get_Ref_Txt_Operator()
    {
		this.Inputs = new ABNode[2];
    }

    public override ABBool Evaluate(ABContext context)
    {
		//Get s1
		ABNode input1 = Inputs[0];
		ABRef s1 = OperatorHelper.Instance.getRefParam(context, input1);

		//Get s2
		ABNode input2 = Inputs[1];
		ABText s2 = OperatorHelper.Instance.getTextParam(context, input2);

		if( s1 == null || s2 == null ){
			throw new System.ArgumentNullException();
		}

		ABBool call = ((ABBool)s1.GetAttr( s2.Value ));

		if( call != null ){
			ABBool result = TypeFactory.CreateEmptyBool();
			result.Value = call.Value;
			return result;
		}
		else{
			throw new System.Exception( "key not found" );
		}
    }
}