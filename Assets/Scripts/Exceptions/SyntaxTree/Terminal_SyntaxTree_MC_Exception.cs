using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Terminal_SyntaxTree_MC_Exception : SyntaxTree_MC_Exception {
	public Terminal_SyntaxTree_MC_Exception( ABContext context, string msg )
		: base( context, msg ){
	}

	#region implemented abstract members of SyntaxTree_MC_Exception

	public override string getTrace ( int level )
	{
		string result = "";

		// Level markup
		for (int i = 0; i < level; i++) {
			result += "=";
		}

		return result +' '+ getNodeMessage ();
	}

	#endregion
}
