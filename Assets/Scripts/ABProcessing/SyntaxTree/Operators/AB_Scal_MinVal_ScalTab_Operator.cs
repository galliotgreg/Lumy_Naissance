public class AB_Scal_MinVal_ScalTab_Operator : ABOperator<ABScalar>
{
    public AB_Scal_MinVal_ScalTab_Operator()
    {
        this.Inputs = new ABNode[1];
    }

    public override ABScalar Evaluate(ABContext context)
    {
        //Get Input
        ABTable<ABScalar> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabScalarParam(context, input1);

        //Solve empty tab case
        if (tab.Values.Length == 0)
        {
            return TypeFactory.CreateEmptyScalar();
        }

        //Find minVal
        float minVal = tab.Values[0].Value;
        for (int i = 1; i < tab.Values.Length; i++)
        {
            if (tab.Values[i].Value < minVal)
            {
                minVal = tab.Values[i].Value;
            }
        }

        //Build & return result
        ABScalar result = TypeFactory.CreateEmptyScalar();
        result.Value = minVal;
        return result;
    }
}
