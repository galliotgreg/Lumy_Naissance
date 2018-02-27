using System.Collections;
using System.Collections.Generic;

public class AB_VecTab_Reverse_VecTab_Operator : ABOperator<ABTable<ABVec>>
{

    public AB_VecTab_Reverse_VecTab_Operator()
    {
        this.Inputs = new ABNode[1];
    }

    public override ABTable<ABVec> Evaluate(ABContext context)
    {
        ABTable<ABVec> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabVecParam(context, input1);

        //Build then return Result
        ABVec[] values = new ABVec[tab.Values.Length];
        for (int i = 0; i < tab.Values.Length; i++)
        {
            values[i] = tab.Values[tab.Values.Length - i - 1];
        }
        ABTable<ABVec> result = TypeFactory.CreateEmptyTable<ABVec>();
        result.Values = values;
        return result;
    }
}
