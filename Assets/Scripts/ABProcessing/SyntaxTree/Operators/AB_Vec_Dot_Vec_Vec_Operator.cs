public class AB_Scal_Dot_Vec_Vec_Operator : ABOperator<ABScalar>
{
    public AB_Scal_Dot_Vec_Vec_Operator()
    {

        this.Inputs = new ABNode[2];
    }

    public override ABScalar Evaluate(ABContext context)
    {
        //Get s1
        ABVec v1 = null;
        ABNode input1 = Inputs[0];
        v1 = OperatorHelper.Instance.getVecParam(context, input1);

        //Get s2
        ABVec v2 = null;
        ABNode input2 = Inputs[1];
        v2 = OperatorHelper.Instance.getVecParam(context, input2);

        //Return
        ABScalar result = TypeFactory.CreateEmptyScalar();
        float resultX;
        float resultY;
        resultX = v1.X * v2.X;
        resultY = v1.Y * v2.Y;
        result.Value = resultX + resultY;
        return result;
    }
}