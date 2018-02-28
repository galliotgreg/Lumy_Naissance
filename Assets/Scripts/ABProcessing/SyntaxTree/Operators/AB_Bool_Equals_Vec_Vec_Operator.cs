using System;

public class AB_Bool_Equals_Vec_Vec_Operator : ABOperator<ABBool>
{
    private const float EPS = 0.3f;

    public AB_Bool_Equals_Vec_Vec_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABBool Evaluate(ABContext context)
    {
        //Get s1
        ABVec s1 = null;
        ABNode input1 = Inputs[0];
        s1 = OperatorHelper.Instance.getVecParam(context, input1);

        //Get s2
        ABVec s2 = null;
        ABNode input2 = Inputs[1];
        s2 = OperatorHelper.Instance.getVecParam(context, input2);

        //Return

        ABBool result = TypeFactory.CreateEmptyBool();
        float x = s2.X - s1.X;
        float y = s2.Y - s1.Y;
        float dist = (float)Math.Sqrt(x * x + y * y);
        result.Value = !(dist > EPS);

        return result;
    }
}