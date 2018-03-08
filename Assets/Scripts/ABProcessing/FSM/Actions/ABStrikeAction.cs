using System.Collections;
using System.Collections.Generic;

public class ABStrikeAction : ABAction {
	/// <summary>
	/// Initializes a new instance of the <see cref="ABStrikeAction"/> class.
	/// </summary>
	public ABStrikeAction()
	{
		this.type = ActionType.Strike;
		this.parameters = new IABGateOperator[1];
		this.parameters[0] = new AB_RefGate_Operator();
	}

	#region implemented abstract members of ABAction

	public override ABAction CloneEmpty ()
	{
		return ABActionFactory.CreateAction (ActionType.Strike);
	}

	#endregion
}
