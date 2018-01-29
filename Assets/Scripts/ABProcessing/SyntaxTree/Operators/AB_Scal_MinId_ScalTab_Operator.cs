public class AB_Scal_MinId_ScalTab_Operator : ABOperator<ABScalar> {

    public AB_Scal_MinId_ScalTab_Operator()
    {
        this.Inputs = new ABNode[1];
    }

    public override ABScalar Evaluate(ABContext context)
    {
        ABTable<ABScalar> tab = null;
        ABNode input1 = Inputs[0];

        tab = OperatorHelper.Instance.getTabScalarParam(context, input1);

        //Build then return Result
        ABScalar[] values = new ABScalar[tab.Values.Length];
        if (tab.Values.Length == 0)
        {
            return TypeFactory.CreateEmptyScalar();
        }

        int minId = 0;
        float minValue = tab.Values[0].Value;
        for (int i = 1; i < tab.Values.Length; i++)
        {
            if (tab.Values[i].Value < minValue)
            {
                minValue = tab.Values[i].Value;
                minId = i;
            }
        }
        ABScalar result = TypeFactory.CreateEmptyScalar();
        result.Value = minId;
        return result;
    }
}
