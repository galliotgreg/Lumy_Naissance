public class AB_Txt_Get_Ref_Txt_Operator : ABOperator<ABText>
{
    public AB_Txt_Get_Ref_Txt_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABText Evaluate(ABContext context)
    {
        //Get ref
        ABRef reference = null;
        ABNode input1 = Inputs[0];
        reference = OperatorHelper.Instance.getRefParam(context, input1);

        //Get identifier
        ABText identifier = null;
        ABNode input2 = Inputs[1];
        identifier = OperatorHelper.Instance.getTextParam(context, input2);


        //Result
        return (ABText)reference.GetAttr(identifier.Value);
    }
}