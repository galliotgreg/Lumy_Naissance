using System.Collections.Generic;

public class AB_RefTab_Agg_RefStar_Operator : ABOperator<ABTable<ABRef>>
{
    public AB_RefTab_Agg_RefStar_Operator()
    {
        this.Inputs = new ABNode[32];
    }

    public override ABTable<ABRef> Evaluate(ABContext context)
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
                ABOperator<ABRef> abOperator = (ABOperator<ABRef>)input;
                ABRef reference = abOperator.Evaluate(context);
                stackedNodes.Add(reference);
            }
            else if (input is ABParam<ABRef>)
            {
                ABParam<ABRef> param = (ABParam<ABRef>)input;
                ABRef reference = param.Evaluate(context);
                stackedNodes.Add(reference);
            }
            else if (input is ABOperator<ABTable<ABRef>>)
            {
                ABOperator<ABTable<ABRef>> abOperator = (ABOperator<ABTable<ABRef>>)input;
                ABTable<ABRef> refTab = abOperator.Evaluate(context);
                foreach (ABRef reference in refTab.Values) {
                    stackedNodes.Add(reference);
                }
            }
            else if (input is ABParam<ABTable<ABRef>>)
            {
                ABParam<ABTable<ABRef>> param = (ABParam<ABTable<ABRef>>)input;
                ABTable<ABRef> refTab = param.Evaluate(context);
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
