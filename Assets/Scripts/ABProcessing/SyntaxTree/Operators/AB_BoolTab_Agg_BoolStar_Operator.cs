using System.Collections.Generic;

public class AB_BoolTab_Agg_BoolStar_Operator : ABOperator<ABTable<ABBool>>
{
    public AB_BoolTab_Agg_BoolStar_Operator()
    {

        this.Inputs = new ABNode[32];
    }

    public override ABTable<ABBool> Evaluate(ABContext context)
    {
        //StackAllInputs
        IList<ABBool> stackedNodes = new List<ABBool>();
        foreach (ABNode input in this.Inputs) {
            if (input == null) {
                continue;
            }

            if (input is ABOperator<ABBool>) {
                ABOperator<ABBool> abOperator = (ABOperator<ABBool>)input;
                ABBool booleen = abOperator.Evaluate(context);
                stackedNodes.Add(booleen);
            }
            else if (input is ABParam<ABBool>) {
                ABParam<ABBool> param = (ABParam<ABBool>)input;
                ABBool booleen = param.Evaluate(context);
                stackedNodes.Add(booleen);
            }
            else if (input is ABOperator<ABTable<ABBool>>) {
                ABOperator<ABTable<ABBool>> abOperator = (ABOperator<ABTable<ABBool>>)input;
                ABTable<ABBool> boolTab = abOperator.Evaluate(context);
                foreach (ABBool booleen in boolTab.Values) {
                    stackedNodes.Add(booleen);
                }
            }
            else if (input is ABParam<ABTable<ABBool>>) {
                ABParam<ABTable<ABBool>> param = (ABParam<ABTable<ABBool>>)input;
                ABTable<ABBool> boolTab = param.Evaluate(context);
                foreach (ABBool booleen in boolTab.Values) {
                    stackedNodes.Add(booleen);
                }
            }
            else {
                throw new System.NotSupportedException();
            }
        }

        //Build then return Result
        ABBool[] values = new ABBool[stackedNodes.Count];
        for (int i = 0; i < stackedNodes.Count; i++) {
            values[i] = stackedNodes[i];
        }
        ABTable<ABBool> result = TypeFactory.CreateEmptyTable<ABBool>();
        result.Values = values;
        return result;
    }
}
