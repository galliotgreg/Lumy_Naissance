public class AB_Bool_NotEquals_Color_Color_Operator : ABOperator<ABBool>
{
    public AB_Bool_NotEquals_Color_Color_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABBool Evaluate(ABContext context)
    {
        //Get s1
        ABColor s1 = null;
        ABNode input1 = Inputs[0];
        s1 = OperatorHelper.Instance.getColorParam(context, input1);

        //Get s2
        ABColor s2 = null;
        ABNode input2 = Inputs[1];
        s2 = OperatorHelper.Instance.getColorParam(context, input2);

        //Return
        ABBool result = TypeFactory.CreateEmptyBool();
        if (s1.Value != s2.Value)
        {
            result.Value = true;
        }
        else
        {
            result.Value = false;
        }
        return result;
    }
}