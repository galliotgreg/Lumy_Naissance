using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intermediate_SyntaxTree_MC_Exception : SyntaxTree_MC_Exception {
	SyntaxTree_MC_Exception internalException;

	public Intermediate_SyntaxTree_MC_Exception( SyntaxTree_MC_Exception internalException )
		: base( internalException.ContextSource ) {
	}
}
