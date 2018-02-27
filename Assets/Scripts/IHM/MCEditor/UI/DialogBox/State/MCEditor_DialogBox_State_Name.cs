using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_State_Name : MCEditor_DialogBox_State {

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MCEditor_DialogBox_Proxy

	protected override string dialogTitle ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion

	#region implemented abstract members of MCEditor_DialogBox_State

	protected override void confirmState ()
	{
		throw new System.NotImplementedException ();
	}

	protected override void deactivateState ()
	{
		throw new System.NotImplementedException ();
	}

	protected override void configState ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
