public class AB_Txt_Get_TxtTab_Scal_Operator : ABOperator<ABText>
{
    public AB_Txt_Get_TxtTab_Scal_Operator()
    {


        this.Inputs = new ABNode[2];
    }

    public override ABText Evaluate(ABContext context)
    {
        ABTable<ABText> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabTxtParam(context, input1);

        //Retieve second arg s2
        ABScalar s2 = null;
        ABNode input2 = Inputs[1];
        s2 = OperatorHelper.Instance.getScalarParam(context, input2);

        //Build then return Result
        ABText result = TypeFactory.CreateEmptyText();
        result = tab.Values[(int)s2.Value];
        return result;
    }
}
