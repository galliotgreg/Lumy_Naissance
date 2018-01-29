public class AB_Scal_Sum_Scal_Scal_Operator : ABOperator<ABScalar>
{
    public AB_Scal_Sum_Scal_Scal_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABScalar Evaluate(ABContext context)
    {
        //Retieve first arg s1
        ABScalar s1 = null;
        ABNode input1 = Inputs[0];
        s1 = OperatorHelper.Instance.getScalarParam(context, input1);

        //Retieve second arg s2
        ABScalar s2 = null;
        ABNode input2 = Inputs[1];
        s2 = OperatorHelper.Instance.getScalarParam(context, input2);

        //Build then return Result
        ABScalar result = TypeFactory.CreateEmptyScalar();
        result.Value = s1.Value + s2.Value;
        return result;
    }
}