using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorParam_MC_Exception : Terminal_SyntaxTree_MC_Exception {
	IABOperator operatorSource;

	public IABOperator Operator{
		get {
			return operatorSource;
		}
	}

	public OperatorParam_MC_Exception( IABOperator _operator, ABContext context, string msg )
		: base( context, msg ){
		this.operatorSource = _operator;
	}

	#region implemented abstract members of SyntaxTree_MC_Exception

	protected override string getNodeMessage ()
	{
		return "Operator : "+MCEditor_Proxy.getNodeName ((ABNode)operatorSource);
	}

	#endregion
}
