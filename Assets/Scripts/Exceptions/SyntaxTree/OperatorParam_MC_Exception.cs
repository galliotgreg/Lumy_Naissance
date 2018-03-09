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

	public OperatorParam_MC_Exception( IABOperator _operator, ABContext context )
		: base( context ){
		this.operatorSource = _operator;
	}
}
