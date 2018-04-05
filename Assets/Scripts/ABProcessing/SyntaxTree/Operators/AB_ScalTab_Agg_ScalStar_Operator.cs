using System.Collections.Generic;

public class AB_ScalTab_Agg_ScalStar_Operator : ABOperator<ABTable<ABScalar>>
{
    public AB_ScalTab_Agg_ScalStar_Operator()
    {
        this.Inputs = new ABNode[32];
    }

	protected override ABTable<ABScalar> Evaluate(ABContext context)
    {
        //StackAllInputs
        IList<ABScalar> stackedNodes = new List<ABScalar>();
        foreach (ABNode input in this.Inputs)
        {
            if (input == null)
            {
                continue;
            }

            if (input is ABOperator<ABScalar>)
            {
                ABScalar scal = OperatorHelper.Instance.getScalarParam(context, input);
                stackedNodes.Add(scal);
            }
            else if (input is ABParam<ABScalar>)
            {
                ABScalar scal = OperatorHelper.Instance.getScalarParam(context, input);
                stackedNodes.Add(scal);
            }
            else if (input is ABOperator<ABTable<ABScalar>>)
            {
                ABTable<ABScalar> scalTab = OperatorHelper.Instance.getTabScalarParam(context, input);
                foreach (ABScalar scal in scalTab.Values)
                {
                    stackedNodes.Add(scal);
                }
            }
            else if (input is ABParam<ABTable<ABScalar>>)
            {
                ABTable<ABScalar> scalTab = OperatorHelper.Instance.getTabScalarParam(context, input);
                foreach (ABScalar scal in scalTab.Values)
                {
                    stackedNodes.Add(scal);
                }
            }
            else
            {
                throw new System.NotSupportedException();
            }
        }

        //Build then return Result
        ABScalar[] values = new ABScalar[stackedNodes.Count];
        for (int i = 0; i < stackedNodes.Count; i++)
        {
            values[i] = stackedNodes[i];
        }
        ABTable<ABScalar> result = TypeFactory.CreateEmptyTable<ABScalar>();
        result.Values = values;
        return result;
    }
}
