using System;
using System.Collections;
using System.Collections.Generic;

public class AB_ScalTab_Dist_VecTab_Vec_Operator : ABOperator<ABTable<ABScalar>> {

    public AB_ScalTab_Dist_VecTab_Vec_Operator()
    {
        this.Inputs = new ABNode[2];
    }

	protected override ABTable<ABScalar> Evaluate(ABContext context)
    {
		ABTable<ABVec> tab = OperatorHelper.Instance.getTabVecParam( context, Inputs[0] );

		ABVec v = OperatorHelper.Instance.getVecParam( context, Inputs[1] );

        //Build then return Result
        ABScalar[] values = new ABScalar[tab.Values.Length];
        for (int i = 0; i < tab.Values.Length; i++)
        {
            ABScalar distScal = TypeFactory.CreateEmptyScalar();
            float x = tab.Values[i].X - v.X;
            float y = tab.Values[i].Y - v.Y;
            distScal.Value = (float)Math.Sqrt(x * x + y * y);
            values[i] = distScal;
        }
        ABTable<ABScalar> result = TypeFactory.CreateEmptyTable<ABScalar>();
        result.Values = values;
        return result;
    }
}
