using System.Collections.Generic;

public class AB_RefTab_Agg_RefStar_Operator : ABOperator<ABTable<ABRef>>
{
    public AB_RefTab_Agg_RefStar_Operator()
    {
        this.Inputs = new ABNode[32];
    }

	protected override ABTable<ABRef> Evaluate(ABContext context)
    {
        //StackAllInputs
        IList<ABRef> stackedNodes = new List<ABRef>();
        foreach (ABNode input in this.Inputs)
        {
            if (input == null)
            {
                continue;
            }

            if (input is ABOperator<ABRef>)
            {
				ABRef reference = OperatorHelper.Instance.getRefParam ( context, input );
                stackedNodes.Add(reference);
            }
            else if (input is ABParam<ABRef>)
            {
				ABRef reference = OperatorHelper.Instance.getRefParam ( context, input );
                stackedNodes.Add(reference);
            }
            else if (input is ABOperator<ABTable<ABRef>>)
            {
				ABTable<ABRef> refTab = OperatorHelper.Instance.getTabRefParam ( context, input );
                foreach (ABRef reference in refTab.Values) {
                    stackedNodes.Add(reference);
                }
            }
            else if (input is ABParam<ABTable<ABRef>>)
            {
				ABTable<ABRef> refTab = OperatorHelper.Instance.getTabRefParam ( context, input );
                foreach (ABRef reference in refTab.Values)
                {
                    stackedNodes.Add(reference);
                }
            }
            else
            {
                throw new System.NotSupportedException();
            }
        }

        //Build then return Result
        ABRef[] values = new ABRef[stackedNodes.Count];
        for (int i = 0; i < stackedNodes.Count; i++)
        {
            values[i] = stackedNodes[i];
        }
        ABTable<ABRef> result = TypeFactory.CreateEmptyTable<ABRef>();
        result.Values = values;
        return result;
    }
}
