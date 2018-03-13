using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_MC_Exception : Intermediate_SyntaxTree_MC_Exception {
	ABTransition transitionSource;

	public Condition_MC_Exception( ABTransition _transition, SyntaxTree_MC_Exception internalException )
		: base( internalException ){
		this.transitionSource = _transition;
	}

	#region implemented abstract members of SyntaxTree_MC_Exception

	protected override string getNodeMessage ()
	{
		string result = "Condition : ";
		if (transitionSource != null) {
			if (transitionSource.Start != null) {
				result += transitionSource.Start.Name;
			}
			result += " => ";
			if (transitionSource.End != null) {
				result += transitionSource.End.Name;
			}
		}
		return result;
	}

	#endregion
}
