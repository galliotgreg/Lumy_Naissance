using System.Collections;
using System.Collections.Generic;

public class AB_VecTab_Get_RefTab_Txt_Operator : ABOperator<ABTable<ABVec>> {

    public AB_VecTab_Get_RefTab_Txt_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABTable<ABVec> Evaluate(ABContext context)
    {
        ABTable<ABRef> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabRefParam(context, input1);        

        ABText t = null;
        ABNode input2 = Inputs[1];
        t = OperatorHelper.Instance.getTextParam(context, input2);        

        //Build then return Result
        ABVec[] values = new ABVec[tab.Values.Length];
        for (int i = 0; i < tab.Values.Length; i++)
        {
            values[i] = (ABVec) tab.Values[i].GetAttr(t.Value);
        }
        ABTable<ABVec> result = TypeFactory.CreateEmptyTable<ABVec>();
        result.Values = values;
        return result;
    }
}
