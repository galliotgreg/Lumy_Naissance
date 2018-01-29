public class AB_Bool_Equals_Bool_Bool_Operator : ABOperator<ABBool>
{
    public AB_Bool_Equals_Bool_Bool_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABBool Evaluate(ABContext context)
    {        
        //Get s1
        ABBool s1 = null;
        ABNode input1 = Inputs[0];
        s1 = OperatorHelper.Instance.getBoolParam(context, input1);

        //Get s2
        ABBool s2 = null;
        ABNode input2 = Inputs[1];
        s2 = OperatorHelper.Instance.getBoolParam(context, input2);

        //Return
        ABBool result = TypeFactory.CreateEmptyBool();
        result.Value = s1.Value == s2.Value;
        return result;
    }
}