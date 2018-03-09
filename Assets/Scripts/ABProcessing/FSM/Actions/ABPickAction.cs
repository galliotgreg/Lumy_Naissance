using System.Collections;
using System.Collections.Generic;

public class ABPickAction : ABAction {
	/// <summary>
	/// Initializes a new instance of the <see cref="ABPickAction"/> class.
	/// </summary>
	public ABPickAction()
	{
		this.type = ActionType.Pick;
		this.parameters = new IABGateOperator[1];
		this.parameters[0] = new AB_RefGate_Operator();
	}

	#region implemented abstract members of ABAction

	public override ABAction CloneEmpty ()
	{
		return ABActionFactory.CreateAction (ActionType.Pick);
	}

	#endregion
}
