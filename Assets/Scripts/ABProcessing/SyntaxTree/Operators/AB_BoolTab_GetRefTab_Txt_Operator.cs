public class AB_BoolTab_GetRefTab_Txt_Operator : ABOperator<ABTable<ABBool>>
{
    public AB_BoolTab_GetRefTab_Txt_Operator()
    {

        this.Inputs = new ABNode[2];
    }

    public override ABTable<ABBool> Evaluate(ABContext context)
    {
        ABTable<ABRef> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabRefParam(context, input1);

        ABText text = null;
        ABNode input2 = Inputs[1];
        text = OperatorHelper.Instance.getTextParam(context, input2);

        //Build then return Result

        ABTable<ABBool> result = TypeFactory.CreateEmptyTable<ABBool>();

        for (int i = 0; i < tab.Values.Length; i++) {
            if(((ABText)tab.Values[i].GetAttr(text.Value)).Value == text.Value) {

                //result.Values[i].Value = text.Value;
            }
        }
        
       // result.Value = length;
        return result;
    }
}
