using System;

public class AB_Bool_IsSet_Ref_Operator : ABOperator<ABBool>
{
    public AB_Bool_IsSet_Ref_Operator()
    {
        this.Inputs = new ABNode[1];
    }

	protected override ABBool Evaluate(ABContext context)
    {
        ABNode node = Inputs[0];
        if (node is ABParam<ABRef>)
        {
			ABRef reference = OperatorHelper.Instance.getRefParam( context, node );

            ABBool result = TypeFactory.CreateEmptyBool();
            result.Value = reference != null;

            return result;
        }
        else
        {
            throw new NotSupportedException();
        }
    }
}