public class AB_Bool_IsSet_Vec_Operator : ABOperator<ABBool>
{
    public AB_Bool_IsSet_Vec_Operator()
    {

        this.Inputs = new ABNode[1];
    }

    public override ABBool Evaluate(ABContext context)
    {
        ABVec s1 = null;
        ABNode input1 = Inputs[0];
        s1 = OperatorHelper.Instance.getVecParam(context, input1);

        ABBool result = TypeFactory.CreateEmptyBool();
        if (s1 != null) {
            result.Value = true;
        }
        else {
            result.Value = false;
        }
        //result.Value = s1.Value + s2.Value;
        return result;
    }
}