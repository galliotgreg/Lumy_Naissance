public class AB_Bool_Equals_Scal_Scal_Operator : ABOperator<ABBool>
{
    public AB_Bool_Equals_Scal_Scal_Operator()
    {
        this.Inputs = new ABNode[2];
    }

	protected override ABBool Evaluate(ABContext context)
    {
        //Retieve first arg s1
		ABScalar s1 = OperatorHelper.Instance.getScalarParam( context, Inputs[0] );

        //Retieve second arg s2
		ABScalar s2 = OperatorHelper.Instance.getScalarParam( context, Inputs[1] );

        //Build then return Result
        ABBool result = TypeFactory.CreateEmptyBool();
        if (s1.Value == s2.Value)
        {
            result.Value = true;
        }
        else
        {
            result.Value = false;
        }

        return result;
    }
}