public class AB_Color_Get_ColorTab_Scal_Operator : ABOperator<ABColor>
{
    public AB_Color_Get_ColorTab_Scal_Operator()
    {

        this.Inputs = new ABNode[2];
    }

    public override ABColor Evaluate(ABContext context)
    {
        ABTable<ABColor> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabColorParam(context, input1);

        //Retieve second arg s2
        ABScalar s2 = null;
        ABNode input2 = Inputs[1];
        s2 = OperatorHelper.Instance.getScalarParam(context, input2);

        //Build then return Result
        ABColor result = TypeFactory.CreateEmptyColor();
        result = tab.Values[(int)s2.Value];
        return result;
    }
}
