public class AB_ScalTab_Get_RefTab_Txt_Operator : ABOperator<ABTable<ABScalar>>
{
    public AB_ScalTab_Get_RefTab_Txt_Operator()
    {

        this.Inputs = new ABNode[2];
        //throw new System.NotImplementedException();
    }

    public override ABTable<ABScalar> Evaluate(ABContext context)
    {
        ABTable<ABRef> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabRefParam(context, input1);

        ABText t = null;
        ABNode input2 = Inputs[1];
        t = OperatorHelper.Instance.getTextParam(context, input2);

        //Build then return Result
        ABScalar[] values = new ABScalar[tab.Values.Length];
        for (int i = 0; i < tab.Values.Length; i++) {
            values[i] = (ABScalar)tab.Values[i].GetAttr(t.Value);
        }
        ABTable<ABScalar> result = TypeFactory.CreateEmptyTable<ABScalar>();
        result.Values = values;
        return result;
        //throw new System.NotImplementedException();
    }
}
