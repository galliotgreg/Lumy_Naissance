public class AB_Ref_Get_Ref_Txt_Operator : ABOperator<ABRef>
{
    public AB_Ref_Get_Ref_Txt_Operator()
    {
        this.Inputs = new ABNode[2];
    }

	protected override ABRef Evaluate(ABContext context)
    {
        //Get ref
		ABRef reference = OperatorHelper.Instance.getRefParam ( context, Inputs[0] );

        //Get identifier
		ABText identifier = OperatorHelper.Instance.getTextParam ( context, Inputs[1] );

        //Result
        return (ABRef)reference.GetAttr(identifier.Value);
    }
}