public class AB_Vec_TermProd_Vec_Vec_Operator : ABOperator<ABVec>
{
    public AB_Vec_TermProd_Vec_Vec_Operator()
    {        
        this.Inputs = new ABNode[2];
    }

    public override ABVec Evaluate(ABContext context)
    {
        //Retieve first arg s1
        ABVec v1 = null;
        ABNode input1 = Inputs[0];
        v1 = OperatorHelper.Instance.getVecParam(context, input1);

        //Retieve second arg s2
        ABVec v2 = null;
        ABNode input2 = Inputs[1];
        v2 = OperatorHelper.Instance.getVecParam(context, input2);

        //Build then return Result
        ABVec result = TypeFactory.CreateEmptyVec();
        result.X = v1.X * v2.X;
        result.Y = v1.Y * v2.Y;
        return result;
    }
}