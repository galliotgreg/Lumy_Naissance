public class AB_Bool_Get_BoolTab_Scal_Operator : ABOperator<ABBool>
{
    public AB_Bool_Get_BoolTab_Scal_Operator()
    {

        this.Inputs = new ABNode[2];
    }

    public override ABBool Evaluate(ABContext context) {

        ABTable<ABBool> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabBoolParam(context, input1);

        //Retieve second arg s2
        ABScalar s2 = null;
        ABNode input2 = Inputs[1];
        s2 = OperatorHelper.Instance.getScalarParam(context, input2);

        //Build then return Result
        ABBool result = TypeFactory.CreateEmptyBool();
        result = tab.Values[(int)s2.Value];
        return result;
    }
}
