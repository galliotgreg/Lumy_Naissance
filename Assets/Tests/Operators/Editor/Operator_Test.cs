using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public abstract class Operator_Test<OperatorType> where OperatorType:IABOperator {
	/// <summary>
	/// Create an operator based on the list of ABParams
	/// </summary>
	/// <returns>The operator</returns>
	/// <param name="operatorSymbol">string that represents the operator</param>
	/// <param name="parameters">ABParam parameters that will compose the operator</param>
	public static OperatorType getOperator_ABParams( string operatorSymbol, ABContext ctx, params ABNode[] parameters ){

		//Build test operator
		OperatorType ope = (OperatorType)ABOperatorFactory.CreateOperator(operatorSymbol);
		ope.Inputs = parameters;

		Operator_Test<OperatorType>.fillContext( ctx, parameters );

		return ope;
	}

	/// <summary>
	/// Insert the parameters into the context
	/// </summary>
	/// <param name="ctx">Context</param>
	/// <param name="parameters">Parameters to be added</param>
	public static void fillContext( ABContext ctx, ABNode[] parameters ){
		foreach( IABParam p in parameters ){
			if( p != null ){
				ctx.SetParam( p );
			}
		}
	}
}
