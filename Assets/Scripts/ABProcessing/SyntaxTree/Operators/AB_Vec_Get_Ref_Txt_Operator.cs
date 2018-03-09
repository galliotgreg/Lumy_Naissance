public class AB_Vec_Get_Ref_Txt_Operator : ABOperator<ABVec>
{
    public AB_Vec_Get_Ref_Txt_Operator()
    {
        this.Inputs = new ABNode[2];
    }

	protected override ABVec Evaluate(ABContext context)
    {
        //Get ref
		ABRef reference = OperatorHelper.Instance.getRefParam( context, Inputs[0] );

        //Get identifier
		ABText identifier = OperatorHelper.Instance.getTextParam( context, Inputs[1] );

        //Result
        return (ABVec) reference.GetAttr(identifier.Value);
    }
}