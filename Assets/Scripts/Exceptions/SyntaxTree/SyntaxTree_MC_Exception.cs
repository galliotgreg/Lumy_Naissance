using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SyntaxTree_MC_Exception : MC_Exception {
	public SyntaxTree_MC_Exception( ABContext context )
		: base(context){
	}
}
