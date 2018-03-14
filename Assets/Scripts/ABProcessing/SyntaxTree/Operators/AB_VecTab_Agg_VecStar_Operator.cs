using System.Collections.Generic;

public class AB_VecTab_Agg_VecStar_Operator : ABOperator<ABTable<ABVec>>
{
    public AB_VecTab_Agg_VecStar_Operator()
    {
        this.Inputs = new ABNode[32];
    }

	protected override ABTable<ABVec> Evaluate(ABContext context)
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
				ABVec vec = OperatorHelper.Instance.getVecParam ( context, input );
                stackedNodes.Add(vec);
            }
            else if (input is ABParam<ABVec>)
            {
				ABVec vec = OperatorHelper.Instance.getVecParam ( context, input );
                stackedNodes.Add(vec);
            }
            else if (input is ABOperator<ABTable<ABVec>>)
            {
				ABTable<ABVec> vecTab = OperatorHelper.Instance.getTabVecParam ( context, input );
                foreach (ABVec vec in vecTab.Values)
                {
                    stackedNodes.Add(vec);
                }
            }
            else if (input is ABParam<ABTable<ABVec>>)
            {
				ABTable<ABVec> vecTab = OperatorHelper.Instance.getTabVecParam ( context, input );
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
