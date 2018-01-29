public class AB_Scal_Size_ScalTab_Operator : ABOperator<ABScalar>
{
    public AB_Scal_Size_ScalTab_Operator() {
        this.Inputs = new ABNode[1];
    }

    public override ABScalar Evaluate(ABContext context)
    {
        ABTable<ABScalar> tab = null;
        ABNode input1 = Inputs[0];

        tab = OperatorHelper.Instance.getTabScalarParam(context, input1);

        //Build then return Result
        ABScalar[] values = new ABScalar[tab.Values.Length];
        if (tab.Values.Length == 0) {
            return TypeFactory.CreateEmptyScalar();
        }

        int length = 0;
        length = tab.Values.Length;
        ABScalar result = TypeFactory.CreateEmptyScalar();
        result.Value = length;
        return result;
    }
}
