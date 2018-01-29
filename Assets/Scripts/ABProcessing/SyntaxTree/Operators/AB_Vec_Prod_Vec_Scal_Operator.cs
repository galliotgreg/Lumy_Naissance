public class AB_Vec_Prod_Vec_Scal_Operator : ABOperator<ABVec>
{
    public AB_Vec_Prod_Vec_Scal_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABVec Evaluate(ABContext context)
    {
        //Get s1
        ABVec s1 = null;
        ABNode input1 = Inputs[0];
        s1 = OperatorHelper.Instance.getVecParam(context, input1);

        //Get s2
        ABScalar s2 = null;
        ABNode input2 = Inputs[1];
        s2 = OperatorHelper.Instance.getScalarParam(context, input2);
        
        //Return
        ABVec result = TypeFactory.CreateEmptyVec();
        result.X = s1.X + s2.Value;
        result.Y = s1.Y + s2.Value;
        return result;
    }
}