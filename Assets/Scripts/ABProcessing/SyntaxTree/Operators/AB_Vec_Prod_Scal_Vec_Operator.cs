public class AB_Vec_Prod_Scal_Vec_Operator : ABOperator<ABVec>
{
    public AB_Vec_Prod_Scal_Vec_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABVec Evaluate(ABContext context)
    {
        //Get s1
        ABScalar s1 = null;
        ABNode input1 = Inputs[0];
        s1 = OperatorHelper.Instance.getScalarParam(context, input1);

        //Get s2
        ABVec s2 = null;
        ABNode input2 = Inputs[1];
        s2 = OperatorHelper.Instance.getVecParam(context, input2);

        //Return
        ABVec result = TypeFactory.CreateEmptyVec();
        result.X = s1.Value + s2.X;
        result.Y = s1.Value + s2.Y;
        return result;
    }
}