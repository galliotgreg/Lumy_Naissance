public class ABRoamingAction : ABAction
{
	public ABRoamingAction()
	{
		this.type = ActionType.Roaming;
		this.parameters = new IABGateOperator[2];
		this.parameters[0] = new AB_ScalGate_Operator();	// angle
		this.parameters[1] = new AB_ScalGate_Operator();	// dist
	}

	#region implemented abstract members of ABAction

	public override ABAction CloneEmpty ()
	{
		return ABActionFactory.CreateAction (ActionType.Roaming);
	}

	#endregion
}