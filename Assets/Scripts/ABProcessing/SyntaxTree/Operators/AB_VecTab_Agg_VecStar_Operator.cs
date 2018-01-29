using System.Collections.Generic;

public class AB_VecTab_Agg_VecStar_Operator : ABOperator<ABTable<ABVec>>
{
    public AB_VecTab_Agg_VecStar_Operator()
    {
        this.Inputs = new ABNode[32];
    }

    public override ABTable<ABVec> Evaluate(ABContext context)
    {
        //StackAllInputs
        IList<ABVec> stackedNodes = new List<ABVec>();
        foreach (ABNode input in this.Inputs)
        {
            if (input == null)
            {
                continue;
            }

            if (input is ABOperator<ABVec>)
            {
                ABOperator<ABVec> abOperator = (ABOperator<ABVec>)input;
                ABVec vec = abOperator.Evaluate(context);
                stackedNodes.Add(vec);
            }
            else if (input is ABParam<ABVec>)
            {
                ABParam<ABVec> param = (ABParam<ABVec>)input;
                ABVec vec = param.Evaluate(context);
                stackedNodes.Add(vec);
            }
            else if (input is ABOperator<ABTable<ABVec>>)
            {
                ABOperator<ABTable<ABVec>> abOperator = (ABOperator<ABTable<ABVec>>)input;
                ABTable<ABVec> vecTab = abOperator.Evaluate(context);
                foreach (ABVec vec in vecTab.Values)
                {
                    stackedNodes.Add(vec);
                }
            }
            else if (input is ABParam<ABTable<ABVec>>)
            {
                ABParam<ABTable<ABVec>> param = (ABParam<ABTable<ABVec>>)input;
                ABTable<ABVec> vecTab = param.Evaluate(context);
                foreach (ABVec vec in vecTab.Values)
                {
                    stackedNodes.Add(vec);
                }
            }
            else
            {
                throw new System.NotSupportedException();
            }
        }

        //Build then return Result
        ABVec[] values = new ABVec[stackedNodes.Count];
        for (int i = 0; i < stackedNodes.Count; i++)
        {
            values[i] = stackedNodes[i];
        }
        ABTable<ABVec> result = TypeFactory.CreateEmptyTable<ABVec>();
        result.Values = values;
        return result;
    }
}
