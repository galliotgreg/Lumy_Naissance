using System.Collections.Generic;

public class AB_TxtTab_Agg_TxtStar_Operator : ABOperator<ABTable<ABText>>
{
    public AB_TxtTab_Agg_TxtStar_Operator()
    {

        this.Inputs = new ABNode[32];
    }

    public override ABTable<ABText> Evaluate(ABContext context)
    {
        //StackAllInputs
        IList<ABText> stackedNodes = new List<ABText>();
        foreach (ABNode input in this.Inputs) {
            if (input == null) {
                continue;
            }

            if (input is ABOperator<ABText>) {
                ABOperator<ABText> abOperator = (ABOperator<ABText>)input;
                ABText reference = abOperator.Evaluate(context);
                stackedNodes.Add(reference);
            }
            else if (input is ABParam<ABText>) {
                ABParam<ABText> param = (ABParam<ABText>)input;
                ABText reference = param.Evaluate(context);
                stackedNodes.Add(reference);
            }
            else if (input is ABOperator<ABTable<ABText>>) {
                ABOperator<ABTable<ABText>> abOperator = (ABOperator<ABTable<ABText>>)input;
                ABTable<ABText> refTab = abOperator.Evaluate(context);
                foreach (ABText reference in refTab.Values) {
                    stackedNodes.Add(reference);
                }
            }
            else if (input is ABParam<ABTable<ABText>>) {
                ABParam<ABTable<ABText>> param = (ABParam<ABTable<ABText>>)input;
                ABTable<ABText> refTab = param.Evaluate(context);
                foreach (ABText reference in refTab.Values) {
                    stackedNodes.Add(reference);
                }
            }
            else {
                throw new System.NotSupportedException();
            }
        }

        //Build then return Result
        ABText[] values = new ABText[stackedNodes.Count];
        for (int i = 0; i < stackedNodes.Count; i++) {
            values[i] = stackedNodes[i];
        }
        ABTable<ABText> result = TypeFactory.CreateEmptyTable<ABText>();
        result.Values = values;
        return result;

    }
}
