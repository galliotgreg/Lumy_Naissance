using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingAction : GameAction {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region implemented abstract members of GameAction

	protected override void initAction ()
	{
		throw new System.NotImplementedException ();
	}

	protected override void executeAction ()
	{
		throw new System.NotImplementedException ();
	}

	protected override void frameBeginAction ()
	{
		throw new System.NotImplementedException ();
	}

	protected override void frameBeginAction_CooldownAuthorized ()
	{
		throw new System.NotImplementedException ();
	}

	protected override void frameEndAction ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
