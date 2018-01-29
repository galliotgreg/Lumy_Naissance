using System;
using System.Collections;
using System.Collections.Generic;

public class AB_ScalTab_Dist_VecTab_Vec_Operator : ABOperator<ABTable<ABScalar>> {

    public AB_ScalTab_Dist_VecTab_Vec_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABTable<ABScalar> Evaluate(ABContext context)
    {
        ABTable<ABVec> tab = null;
        ABNode input1 = Inputs[0];
        if (input1 is ABOperator<ABTable<ABVec>>)
        {
            ABOperator<ABTable<ABVec>> abOperator = (ABOperator<ABTable<ABVec>>)input1;
            tab = abOperator.Evaluate(context);
        }
        else if (input1 is ABParam<ABVec>)
        {
            ABParam<ABTable<ABVec>> param = (ABParam<ABTable<ABVec>>)input1;
            tab = param.Evaluate(context);
        }
        else
        {
            throw new System.NotSupportedException();
        }

        ABVec v = null;
        ABNode input2 = Inputs[1];
        if (input2 is ABOperator<ABVec>)
        {
            ABOperator<ABVec> abOperator = (ABOperator<ABVec>)input2;
            v = abOperator.Evaluate(context);
        }
        else if (input2 is ABParam<ABVec>)
        {
            ABParam<ABVec> param = (ABParam<ABVec>)input2;
            v = param.Evaluate(context);
        }
        else
        {
            throw new System.NotSupportedException();
        }

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
