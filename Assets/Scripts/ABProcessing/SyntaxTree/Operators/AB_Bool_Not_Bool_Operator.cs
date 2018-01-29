public class AB_Bool_Not_Bool_Operator : ABOperator<ABBool>
{
    public AB_Bool_Not_Bool_Operator()
    {
        this.Inputs = new ABNode[1];
    }

    public override ABBool Evaluate(ABContext context)
    {        
        //Get s1
        ABBool s1 = null;
        ABNode input1 = Inputs[0];
        s1 = OperatorHelper.Instance.getBoolParam(context, input1);

        //Return
        ABBool result = TypeFactory.CreateEmptyBool();
        result.Value = !s1.Value;
        return result;
    }
}