using System.Collections.Generic;

public class AB_ColorTab_Agg_CStar_Operator : ABOperator<ABTable<ABColor>>
{
    public AB_ColorTab_Agg_CStar_Operator()
    {
        this.Inputs = new ABNode[32];
    }

    public override ABTable<ABColor> Evaluate(ABContext context)
    {
        //StackAllInputs
        IList<ABColor> stackedNodes = new List<ABColor>();
        foreach (ABNode input in this.Inputs) {
            if (input == null) {
                continue;
            }

            if (input is ABOperator<ABColor>) {
                ABOperator<ABColor> abOperator = (ABOperator<ABColor>)input;
                ABColor color = abOperator.Evaluate(context);
                stackedNodes.Add(color);
            }
            else if (input is ABParam<ABColor>) {
                ABParam<ABColor> param = (ABParam<ABColor>)input;
                ABColor color = param.Evaluate(context);
                stackedNodes.Add(color);
            }
            else if (input is ABOperator<ABTable<ABColor>>) {
                ABOperator<ABTable<ABColor>> abOperator = (ABOperator<ABTable<ABColor>>)input;
                ABTable<ABColor> colorTab = abOperator.Evaluate(context);
                foreach (ABColor color in colorTab.Values) {
                    stackedNodes.Add(color);
                }
            }
            else if (input is ABParam<ABTable<ABColor>>) {
                ABParam<ABTable<ABColor>> param = (ABParam<ABTable<ABColor>>)input;
                ABTable<ABColor> colorTab = param.Evaluate(context);
                foreach (ABColor color in colorTab.Values) {
                    stackedNodes.Add(color);
                }
            }
            else {
                throw new System.NotSupportedException();
            }
        }

        //Build then return Result
        ABColor[] values = new ABColor[stackedNodes.Count];
        for (int i = 0; i < stackedNodes.Count; i++) {
            values[i] = stackedNodes[i];
        }
        ABTable<ABColor> result = TypeFactory.CreateEmptyTable<ABColor>();
        result.Values = values;
        return result;
    }
}
