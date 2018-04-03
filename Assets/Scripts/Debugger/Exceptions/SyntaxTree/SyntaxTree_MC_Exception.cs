using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SyntaxTree_MC_Exception : MC_Exception {
	public SyntaxTree_MC_Exception( ABContext context, string msg = "" )
		: base(context, msg){
	}

	public abstract string getTrace ( int level );
	protected abstract string getNodeMessage ();

	#region implemented abstract members of MC_Exception

	public override string Title {
		get {
			return Cast;
		}
	}

	public override string getMessage(){
		string trace = getTrace ( 0 );
		return trace;
	}

	#endregion
}
