public class AB_Scal_Prod_Scal_Scal_Operator : ABOperator<ABScalar>
{
    public AB_Scal_Prod_Scal_Scal_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABScalar Evaluate(ABContext context)
    {
        //Retieve first arg s1
        ABScalar s1 = null;
        ABNode input1 = Inputs[0];
        //input node is an scalar operator (computed from sub-tree)
        if (input1 is ABOperator<ABScalar>)
        {
            ABOperator<ABScalar> abOperator = (ABOperator<ABScalar>)input1;
            s1 = abOperator.Evaluate(context);
        }
        //input node is an scalar parameter (user set constante)
        else if (input1 is ABParam<ABScalar>)
        {
            ABParam<ABScalar> param = (ABParam<ABScalar>)input1;
            s1 = param.Evaluate(context);
        }
        //input is not correct (wrong type maybe)
        else
        {
            throw new System.NotSupportedException();
        }

        //Retieve second arg s2
        ABScalar s2 = null;
        ABNode input2 = Inputs[1];
        //input node is an scalar operator (computed from sub-tree)
        if (input2 is ABOperator<ABScalar>)
        {
            ABOperator<ABScalar> abOperator = (ABOperator<ABScalar>)input2;
            s2 = abOperator.Evaluate(context);
        }
        //input node is an scalar parameter (user set constante)
        else if (input2 is ABParam<ABScalar>)
        {
            ABParam<ABScalar> param = (ABParam<ABScalar>)input2;
            s2 = param.Evaluate(context);
        }
        //input is not correct (wrong type maybe)
        else
        {
            throw new System.NotSupportedException();
        }

        //Build then return Result
        ABScalar result = TypeFactory.CreateEmptyScalar();
        result.Value = s1.Value * s2.Value;
        return result;
    }
}
