public class AB_Color_Get_Ref_Txt_Operator : ABOperator<ABColor>
{
    public AB_Color_Get_Ref_Txt_Operator()
    {

        this.Inputs = new ABNode[2];
    }

    public override ABColor Evaluate(ABContext context)
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
        return (ABColor)reference.GetAttr(identifier.Value);
    }
}