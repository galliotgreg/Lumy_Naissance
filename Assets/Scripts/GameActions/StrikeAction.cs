using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeAction : GameAction {

	private GameObject target;

	public GameObject Target {
		get {
			return target;
		}
		set {
			target = value;
		}
	}

	#region implemented abstract members of GameAction
	protected override void initAction ()
	{
		this.CoolDownActivate = true;
		if (this.agentAttr.ActSpd <= 0) {
			this.CoolDownTime = 0;
		} else {
			this.CoolDownTime = 1/(this.agentAttr.ActSpd/5);
		}
	}

	protected override void executeAction ()
	{
		if( target is GameObject ){
			AgentEntity targetAgent = target.GetComponent<AgentEntity>();

			if( targetAgent != null ){
				AgentScript targetModel = targetAgent.Context.Model;

				// Check if the player hits some other
				if( (this.agentAttr.CurPos - targetModel.CurPos).magnitude <= this.agentAttr.AtkRange ){
					// Apply damage
					float damageValue = this.agentAttr.Strength;

					float damageCaused = Unit_GameObj_Manager.instance.strikeUnit( targetAgent, damageValue );
				}
			}
		}
	}
	#endregion
}
