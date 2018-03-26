using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAction : GameAction {

	private GameObject item;

	public GameObject Item {
		get {
			return item;
		}
		set {
			item = value;
		}
	}

	void Pick(){
		// Check if item exists
		if (!item is GameObject || item == null)
		{
			return;
		}
		ResourceScript resource = item.GetComponent<ResourceScript>();

		if (resource == null)
		{
			return; 
		}
		// Check if the player close to the resource
		if ((this.agentAttr.CurPos - resource.Location).magnitude > this.agentAttr.PickRange)
		{
			return;
		}

		// Can carry the item?
		if (this.agentAttr.NbItem >= this.agentAttr.NbItemMax)
		{
			return; 
		}

		if (Unit_GameObj_Manager.instance.pickResource(resource))
		{
			// Carry it
			GameObject res = this.agentAttr.addResource(item);
			res.transform.parent = this.agentEntity.transform;
		}
		else
		{
			throw new System.Exception("resource not found into the manager");
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

	//float lastPick = 0; // TEST : store time of last pick
	protected override void executeAction ()
	{
		/*Debug.LogError ( "***********SPEED = "+this.agentAttr.ActSpd );
		Debug.LogError ( "COOLDOWN = "+this.CoolDownTime );
		Debug.LogError ( Time.time - lastPick );
		lastPick = Time.time;*/
		Pick ();
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
	#endregion
}
