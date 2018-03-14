using System.Collections.Generic;

public class AB_BoolTab_Agg_BoolStar_Operator : ABOperator<ABTable<ABBool>>
{
    public AB_BoolTab_Agg_BoolStar_Operator()
    {

        this.Inputs = new ABNode[32];
    }

	protected override ABTable<ABBool> Evaluate(ABContext context)
    {
        //StackAllInputs
        IList<ABBool> stackedNodes = new List<ABBool>();
        foreach (ABNode input in this.Inputs) {
            if (input == null) {
                continue;
            }

            if (input is ABOperator<ABBool>) {
				ABBool booleen = OperatorHelper.Instance.getBoolParam ( context, input );
                stackedNodes.Add(booleen);
            }
            else if (input is ABParam<ABBool>) {
				ABBool booleen = OperatorHelper.Instance.getBoolParam ( context, input );
                stackedNodes.Add(booleen);
            }
            else if (input is ABOperator<ABTable<ABBool>>) {
				ABTable<ABBool> boolTab = OperatorHelper.Instance.getTabBoolParam ( context, input );
                foreach (ABBool booleen in boolTab.Values) {
                    stackedNodes.Add(booleen);
                }
            }
            else if (input is ABParam<ABTable<ABBool>>) {
				ABTable<ABBool> boolTab = OperatorHelper.Instance.getTabBoolParam ( context, input );
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
