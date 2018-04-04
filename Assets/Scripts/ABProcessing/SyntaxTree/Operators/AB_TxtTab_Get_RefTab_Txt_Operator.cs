public class AB_TxtTab_Get_RefTab_Txt_Operator : ABOperator<ABTable<ABText>>
{
    public AB_TxtTab_Get_RefTab_Txt_Operator()
    {
        this.Inputs = new ABNode[2];
    }

	protected override ABTable<ABText> Evaluate(ABContext context)
    {
        ABTable<ABRef> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabRefParam(context, input1);

        ABText t = null;
        ABNode input2 = Inputs[1];
        t = OperatorHelper.Instance.getTextParam(context, input2);

        //Build then return Result
        ABText[] values = new ABText[tab.Values.Length];
        for (int i = 0; i < tab.Values.Length; i++)
        {
            values[i] = (ABText)tab.Values[i].GetAttr(t.Value);
        }
        ABText<ABScalar> result = TypeFactory.CreateEmptyTable<ABText>();
        result.Values = values;
        return result;
    }
}
