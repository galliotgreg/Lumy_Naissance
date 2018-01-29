public class AB_Ref_Get_RefTab_Scal_Operator : ABOperator<ABRef>
{
    public AB_Ref_Get_RefTab_Scal_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABRef Evaluate(ABContext context)
    {
        ABTable<ABRef> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabRefParam(context, input1);

        ABScalar s = null;
        ABNode input2 = Inputs[1];
        s = OperatorHelper.Instance.getScalarParam(context, input2);

        //Build then return Result
        return tab.Values[(int)s.Value];
    }
}
