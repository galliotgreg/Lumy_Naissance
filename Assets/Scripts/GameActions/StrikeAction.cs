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
		this.CoolDownTime = this.agentAttr.ActSpd/(float)5;
	}

	protected override void executeAction ()
	{
		AgentScript hitObject = null;

		// Check if the player hits some other
		// Get unit's hit position
		// TODO check object rotation
		throw new System.NotImplementedException();
		if( target != null && (this.agentAttr.CurPos - target.GetComponent<AgentScript>().CurPos).magnitude <= this.agentAttr.AtkRange ){
			hitObject = target.GetComponent<AgentScript>();
		}

		if( hitObject != null ){
			// Apply damage
			float damageValue = this.agentAttr.Strength;
			float damageResult = Mathf.Max( hitObject.Vitality - damageValue, 0 );

			hitObject.Vitality = hitObject.Vitality - damageResult;
			// Kill unit
			if( hitObject.Vitality <= 0 ){
				// Reduce enemmies
				GameObject.Destroy( target );
				target = null;

				HomeScript enemyHome = GameManager.instance.GetEnemyHome(agentEntity.Authority);
				enemyHome.Population[hitObject.Cast]--;
			}
		}
	}
	#endregion
}
