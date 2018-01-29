public class AB_Vec_Sub_Vec_Vec_Operator : ABOperator<ABVec>
{
    public AB_Vec_Sub_Vec_Vec_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABVec Evaluate(ABContext context)
    {
        //Retieve first arg s1
        ABVec s1 = null;
        ABNode input1 = Inputs[0];
        s1 = OperatorHelper.Instance.getVecParam(context, input1);

        //Retieve second arg s2
        ABVec s2 = null;
        ABNode input2 = Inputs[1];
        s2 = OperatorHelper.Instance.getVecParam(context, input2);

        //Build then return Result
        ABVec result = TypeFactory.CreateEmptyVec();
        result.X = s1.X - s2.X;
        result.Y = s1.Y - s2.Y;
        return result;
    }
}