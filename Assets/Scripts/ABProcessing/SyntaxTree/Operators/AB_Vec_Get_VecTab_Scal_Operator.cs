 using System.Collections;
using System.Collections.Generic;

public class AB_Vec_Get_VecTab_Scal_Operator : ABOperator<ABVec> {
    public AB_Vec_Get_VecTab_Scal_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABVec Evaluate(ABContext context)
    {
        ABTable<ABVec> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabVecParam(context, input1);

        ABScalar s = null;
        ABNode input2 = Inputs[1];
        s = OperatorHelper.Instance.getScalarParam(context, input2);

        //Build then return Result
        return tab.Values[(int) s.Value];
    }

}
