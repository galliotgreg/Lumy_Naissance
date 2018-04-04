public class AB_RefTab_Get_RefTab_Txt_Operator : ABOperator<ABTable<ABRef>>
{
    public AB_RefTab_Get_RefTab_Txt_Operator()
    {
        this.Inputs = new ABNode[2];
    }

	protected override ABTable<ABRef> Evaluate(ABContext context)
    {
        ABTable<ABRef> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabRefParam(context, input1);

        ABText t = null;
        ABNode input2 = Inputs[1];
        t = OperatorHelper.Instance.getTextParam(context, input2);

        //Build then return Result
        ABRef[] values = new ABRef[tab.Values.Length];
        for (int i = 0; i < tab.Values.Length; i++)
        {
            values[i] = (ABRef)tab.Values[i].GetAttr(t.Value);
        }
        ABTable<ABRef> result = TypeFactory.CreateEmptyTable<ABRef>();
        result.Values = values;
        return result;
    }
}
