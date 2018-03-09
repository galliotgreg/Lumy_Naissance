using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorParam_MC_Exception : Terminal_SyntaxTree_MC_Exception {
	IABOperator operatorSource;
	int paramIndex;

	public IABOperator Operator{
		get {
			return operatorSource;
		}
	}

	public OperatorParam_MC_Exception( IABOperator _operator, int paramIndex, ABContext context, string msg )
		: base( context, msg ){
		this.operatorSource = _operator;
		this.paramIndex = paramIndex;
	}

	#region implemented abstract members of SyntaxTree_MC_Exception

	protected override string getNodeMessage ()
	{
		return "Param ["+this.paramIndex+"] - Operator : "+MCEditor_Proxy.getNodeName ((ABNode)operatorSource);
	}

	#endregion
}
