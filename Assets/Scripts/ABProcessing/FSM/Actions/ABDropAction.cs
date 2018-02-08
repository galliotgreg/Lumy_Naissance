using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABDropAction : ABAction {
	public ABDropAction()
	{
		this.type = ActionType.Drop;
		// No parameters
		/*this.parameters = new IABGateOperator[1];
		this.parameters[0] = new AB_RefGate_Operator();*/
	}
}
