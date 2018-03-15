using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionParam_MC_Exception : Intermediate_SyntaxTree_MC_Exception {
	ABAction actionSource;

	public ActionParam_MC_Exception( ABAction action, SyntaxTree_MC_Exception internalException )
		: base( internalException ) {
		this.actionSource = action;
	}

	#region implemented abstract members of SyntaxTree_MC_Exception

	protected override string getNodeMessage ()
	{
		// TODO replace by the state name
		return "Action : "+actionSource.Type.ToString ();
	}

	#endregion
}
