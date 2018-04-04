public class AB_ColorTab_Get_RefTab_Txt_Operator : ABOperator<ABTable<ABColor>>
{
    public AB_ColorTab_Get_RefTab_Txt_Operator()
    {
        this.Inputs = new ABNode[2];
    }

	protected override ABTable<ABColor> Evaluate(ABContext context)
    {
        ABTable<ABRef> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabRefParam(context, input1);

        ABText t = null;
        ABNode input2 = Inputs[1];
        t = OperatorHelper.Instance.getTextParam(context, input2);

        //Build then return Result
        ABColor[] values = new ABColor[tab.Values.Length];
        for (int i = 0; i < tab.Values.Length; i++)
        {
            values[i] = (ABColor)tab.Values[i].GetAttr(t.Value);
        }
        ABTable<ABColor> result = TypeFactory.CreateEmptyTable<ABColor>();
        result.Values = values;
        return result;
    }
}
