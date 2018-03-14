public class AB_Scal_Div_Scal_Scal_Operator : ABOperator<ABScalar>
{
    public AB_Scal_Div_Scal_Scal_Operator()
    {
        this.Inputs = new ABNode[2];
    }

	protected override ABScalar Evaluate(ABContext context)
    {
        //Retieve first arg s1
		ABScalar s1 = OperatorHelper.Instance.getScalarParam( context, Inputs[0] );

        //Retieve second arg s2
		ABScalar s2 = OperatorHelper.Instance.getScalarParam( context, Inputs[1] );

        //Build then return Result
        ABScalar result = TypeFactory.CreateEmptyScalar();
        result.Value = s1.Value / s2.Value;
        return result;
    }
}