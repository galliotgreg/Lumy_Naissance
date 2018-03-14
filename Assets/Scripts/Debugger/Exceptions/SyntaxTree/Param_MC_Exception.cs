using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Param_MC_Exception : Terminal_SyntaxTree_MC_Exception {
	ABNode paramSource;

	public ABNode ParamSource {
		get {
			return paramSource;
		}
	}

	public Param_MC_Exception( ABNode param, ABContext context, string msg )
		: base( context, msg ){
		this.paramSource = param;
	}

	#region implemented abstract members of SyntaxTree_MC_Exception

	protected override string getNodeMessage ()
	{
		return "Param : "+ProxyABParam.GetViewValue ((IABParam)paramSource);
	}

	#endregion
}
