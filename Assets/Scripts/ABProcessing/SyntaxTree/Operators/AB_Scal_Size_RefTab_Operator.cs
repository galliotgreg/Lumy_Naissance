public class AB_Scal_Size_RefTab_Operator : ABOperator<ABScalar>
{
    public AB_Scal_Size_RefTab_Operator()
    {
        this.Inputs = new ABNode[1];
    }

    public override ABScalar Evaluate(ABContext context)
    {
        ABTable<ABRef> tab = null;
        ABNode input1 = Inputs[0];
        tab = OperatorHelper.Instance.getTabRefParam(context, input1);

        //Build then return Result
        ABScalar result = TypeFactory.CreateEmptyScalar();
        result.Value = tab.Values.Length;
        return result;
    }
}
