public class AB_Bool_GreaterThan_Scal_Scal_Operator : ABOperator<ABBool>
{
    public AB_Bool_GreaterThan_Scal_Scal_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABBool Evaluate(ABContext context)
    {

        //Get s1
        ABScalar s1 = null;
        ABNode input1 = Inputs[0];
        s1 = OperatorHelper.Instance.getScalarParam(context, input1);

        //Get s2
        ABScalar s2 = null;
        ABNode input2 = Inputs[1];
        s2 = OperatorHelper.Instance.getScalarParam(context, input2);

        //Return
        ABBool result = TypeFactory.CreateEmptyBool();
        result.Value = s1.Value > s2.Value;
        return result;
    }
}