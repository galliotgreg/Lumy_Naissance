using System;

public class AB_Scal_Dist_Vec_Vec_Operator : ABOperator<ABScalar>
{
    public AB_Scal_Dist_Vec_Vec_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABScalar Evaluate(ABContext context)
    {
        //Get v1
        ABVec v1 = null;
        ABNode input1 = Inputs[0];    
        v1 = OperatorHelper.Instance.getVecParam(context, input1);

        //Get v2
        ABVec v2 = null;
        ABNode input2 = Inputs[1];
        v2 = OperatorHelper.Instance.getVecParam(context, input2);

        //Result
        ABScalar result = TypeFactory.CreateEmptyScalar();
        float x = v2.X - v1.X;
        float y = v2.Y - v1.Y;
        result.Value = (float) Math.Sqrt(x * x + y * y);
        return result;
    }
}