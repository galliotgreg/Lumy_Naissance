public class AB_Scal_Get_ScalTab_Scal_Operator : ABOperator<ABScalar>
{
    public AB_Scal_Get_ScalTab_Scal_Operator()
    {

        this.Inputs = new ABNode[2];
    }

    public override ABScalar Evaluate(ABContext context)
    {
        ABTable<ABScalar> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabScalarParam(context, input1);

        //Retieve second arg s2
        ABScalar s2 = null;
        ABNode input2 = Inputs[1];
        s2 = OperatorHelper.Instance.getScalarParam(context, input2);

        //Build then return Result
        ABScalar result = TypeFactory.CreateEmptyScalar();
        result = tab.Values[(int)s2.Value];
        return result;
    }
}
