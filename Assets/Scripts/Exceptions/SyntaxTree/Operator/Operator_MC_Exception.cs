public class Operator_MC_Exception : Intermediate_SyntaxTree_MC_Exception {
	IABOperator operatorSource;

	public IABOperator Operator{
		get {
			return operatorSource;
		}
	}

	public Operator_MC_Exception( IABOperator _operator, SyntaxTree_MC_Exception internalException )
		: base( internalException ){
		this.operatorSource = _operator;
	}

	#region implemented abstract members of SyntaxTree_MC_Exception

	protected override string getNodeMessage ()
	{
		return "Operator : "+MCEditor_Proxy.getNodeName ((ABNode)operatorSource);
	}

	#endregion
}
