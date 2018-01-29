public class AB_Vec_Get_Ref_Txt_Operator : ABOperator<ABVec>
{
    public AB_Vec_Get_Ref_Txt_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABVec Evaluate(ABContext context)
    {
        //Get ref
        ABRef reference = null;
        ABNode input1 = Inputs[0];
        if (input1 is ABOperator<ABRef>)
        {
            ABOperator<ABRef> abOperator = (ABOperator<ABRef>)input1;
            reference = abOperator.Evaluate(context);
        }
        else if (input1 is ABParam<ABRef>)
        {
            ABParam<ABRef> param = (ABParam<ABRef>)input1;
            reference = param.Evaluate(context);
        }

        //Get identifier
        ABText identifier = null;
        ABNode input2 = Inputs[1];
        if (input2 is ABOperator<ABText>)
        {
            ABOperator<ABText> abOperator = (ABOperator<ABText>)input2;
            identifier = abOperator.Evaluate(context);
        }
        else if (input2 is ABParam<ABText>)
        {
            ABParam<ABText> param = (ABParam<ABText>)input2;
            identifier = param.Evaluate(context);
        }

        //Result
        return (ABVec) reference.GetAttr(identifier.Value);
    }
}