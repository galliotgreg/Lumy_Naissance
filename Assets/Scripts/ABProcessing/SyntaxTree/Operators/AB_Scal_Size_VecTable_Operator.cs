public class AB_Scal_Size_VecTable_Operator : ABOperator<ABScalar>
{
    public AB_Scal_Size_VecTable_Operator() {
        this.Inputs = new ABNode[1];
    }

    public override ABScalar Evaluate(ABContext context)
    {
        ABTable<ABVec> tab = null;
        ABNode input1 = Inputs[0];

        tab = OperatorHelper.Instance.getTabVecParam(context, input1);

        //Build then return Result
        ABVec[] values = new ABVec[tab.Values.Length];
        if (tab.Values.Length == 0) {
            return TypeFactory.CreateEmptyScalar();
        }

        int length = 0;
        length = tab.Values.Length;
        ABScalar result = TypeFactory.CreateEmptyScalar();
        result.Value = length;
        return result;
    }
}
