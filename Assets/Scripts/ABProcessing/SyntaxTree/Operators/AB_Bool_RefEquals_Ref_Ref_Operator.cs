public class AB_Bool_RefEquals_Ref_Ref_Operator : ABOperator<ABBool>
{
    public AB_Bool_RefEquals_Ref_Ref_Operator()
    {

        this.Inputs = new ABNode[2];
    }

    public override ABBool Evaluate(ABContext context)
    {
        //Retieve first arg s1
        ABRef r1 = null;
        ABNode input1 = Inputs[0];
        r1 = OperatorHelper.Instance.getRefParam(context, input1);

        //Retieve second arg s2
        ABRef r2 = null;
        ABNode input2 = Inputs[1];
        r2 = OperatorHelper.Instance.getRefParam(context, input2);

        //Build then return Result
        ABBool result = TypeFactory.CreateEmptyBool();
        if (r1 == r2) {
            result.Value = true;
        }
        else {
            result.Value = false;
        }
        //result.Value = s1.Value + s2.Value;
        return result;
    }
}