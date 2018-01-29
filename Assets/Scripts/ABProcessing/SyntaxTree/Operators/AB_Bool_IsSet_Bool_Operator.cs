public class AB_Bool_IsSet_Bool_Operator : ABOperator<ABBool>
{
    public AB_Bool_IsSet_Bool_Operator()
    {

        this.Inputs = new ABNode[1];
    }

    public override ABBool Evaluate(ABContext context)
    {
        ABBool t1 = null;
        ABNode input1 = Inputs[0];
        t1 = OperatorHelper.Instance.getBoolParam(context, input1);

        ABBool result = TypeFactory.CreateEmptyBool();
        if (t1 != null) {
            result.Value = true;
        }
        else {
            result.Value = false;
        }
        //result.Value = s1.Value + s2.Value;
        return result;
    }
}