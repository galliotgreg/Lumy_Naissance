using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeAction : GameAction {

	private GameObject target;
    public GameObject Projectile_J1;
    public GameObject Projectile_J2;

    public GameObject Target {
		get {
			return target;
		}
		set {
			target = value;
		}
	}

	void Strike(){
		if( target is GameObject && target != null ){
			AgentEntity targetAgent = target.GetComponent<AgentEntity>();

			//add vfx
			GameObject projectile;

			if (this.agentEntity.Authority == PlayerAuthority.Player2)
			{
				projectile = Instantiate(Projectile_J2, this.transform.position, Quaternion.identity);
				projectile.transform.SetParent(GameManager.instance.transform);
			}
			else
			{
				projectile = Instantiate(Projectile_J1, this.transform.position, Quaternion.identity);
				projectile.transform.SetParent(GameManager.instance.transform);
			}

            if(InGameUIController.instance != null && InGameUIController.instance.Self == this.agentAttr)
            {
                SoundManager.instance.PlayLumyAttackSFX(); 
            }

			projectile.GetComponent<Rigidbody>().velocity = this.transform.forward * 5.0f;
			projectile.GetComponent<Rigidbody>().MovePosition(targetAgent.Context.Model.transform.position);
			Destroy(projectile, projectile.GetComponentInChildren<ParticleSystem>().duration);
			//add vfx end

			if ( targetAgent != null ){
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

	#region implemented abstract members of GameAction
	protected override void initAction ()
	{
		this.CoolDownActivate = true;
		if (this.agentAttr.ActSpd <= 0) {
			this.CoolDownTime = 0;	// No action is performed
		} else {
			this.CoolDownTime = 1f/(this.agentAttr.ActSpd/5f);
		}
	}

	//float lastAction = 0; // TEST : store time of last action
	protected override bool executeAction ()
	{
		/*
		// TEST
		Debug.LogError ( "***********SPEED = "+this.agentAttr.ActSpd );
		Debug.LogError ( "COOLDOWN = "+this.CoolDownTime );
		Debug.LogError ( Time.time - lastAction );
		lastAction = Time.time;*/

		Strike ();
		return true;
	}

	protected override void activateAction ()
	{
		return;
	}

	protected override void deactivateAction ()
	{
		return;
	}

	protected override void frameBeginAction ()
	{
		return;
	}

	protected override void frameBeginAction_CooldownAuthorized ()
	{
		return;
	}

	protected override void frameEndAction ()
	{
		return;
	}

	protected override void cooldownFinishAction (){}

	#endregion
}
