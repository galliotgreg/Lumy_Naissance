public class Operator_Exception : Terminal_SyntaxTree_MC_Exception {
	IABOperator operatorSource;

	public Operator_Exception( IABOperator _operator, ABContext context, string msg )
		: base ( context, msg ){
		operatorSource = _operator;
	}

	#region implemented abstract members of SyntaxTree_MC_Exception

	protected override string getNodeMessage ()
	{
		return "Operator : "+MCEditor_Proxy.getNodeName ((ABNode)operatorSource) +" => "+this.Message;
	}

	#endregion
}
