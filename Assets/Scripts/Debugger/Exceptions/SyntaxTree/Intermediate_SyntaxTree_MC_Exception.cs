using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Intermediate_SyntaxTree_MC_Exception : SyntaxTree_MC_Exception {
	SyntaxTree_MC_Exception internalException;

	public Intermediate_SyntaxTree_MC_Exception( SyntaxTree_MC_Exception internalException )
		: base( internalException.ContextSource ) {
		this.internalException = internalException;
	}
		
	#region implemented abstract members of SyntaxTree_MC_Exception
	public override string getTrace ( int level )
	{
		string result = "";

		// Level markup
		for (int i = 0; i < level; i++) {
			result += "=";
		}

		result += getNodeMessage ();

		if (internalException != null) {
			result += '\n' + internalException.getTrace ( level+1 );
		}

		return result;
	}
	#endregion
}
