using System.Collections.Generic;

public class AB_ColorTab_Agg_ColorStar_Operator : ABOperator<ABTable<ABColor>>
{
    public AB_ColorTab_Agg_ColorStar_Operator()
    {
        this.Inputs = new ABNode[32];
    }

	protected override ABTable<ABColor> Evaluate(ABContext context)
    {
        //StackAllInputs
        IList<ABColor> stackedNodes = new List<ABColor>();
        foreach (ABNode input in this.Inputs) {
            if (input == null) {
                continue;
            }

            if (input is ABOperator<ABColor>) {
				ABColor color = OperatorHelper.Instance.getColorParam( context, input );
                stackedNodes.Add(color);
            }
            else if (input is ABParam<ABColor>) {
				ABColor color = OperatorHelper.Instance.getColorParam( context, input );
                stackedNodes.Add(color);
            }
            else if (input is ABOperator<ABTable<ABColor>>) {
				ABTable<ABColor> colorTab = OperatorHelper.Instance.getTabColorParam( context, input );
                foreach (ABColor color in colorTab.Values) {
                    stackedNodes.Add(color);
                }
            }
            else if (input is ABParam<ABTable<ABColor>>) {
				ABTable<ABColor> colorTab = OperatorHelper.Instance.getTabColorParam( context, input );
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
