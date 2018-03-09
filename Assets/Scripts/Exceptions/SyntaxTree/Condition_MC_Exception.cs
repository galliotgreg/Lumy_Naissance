using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_MC_Exception : Intermediate_SyntaxTree_MC_Exception {
	ABTransition transitionSource;

	public Condition_MC_Exception( ABTransition _transition, SyntaxTree_MC_Exception internalException )
		: base( internalException ){
		this.transitionSource = _transition;
	}
}
