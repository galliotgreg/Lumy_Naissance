public class AB_Bool_Equals_Vec_Vec_Operator : ABOperator<ABBool>
{
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
        if (s1.X == s2.X && s1.Y == s2.Y)
        {
            result.Value = true;
        }
        else
        {
            result.Value = false;
        }
        //result.Value = s1.Value + s2.Value;
        return result;
    }
}