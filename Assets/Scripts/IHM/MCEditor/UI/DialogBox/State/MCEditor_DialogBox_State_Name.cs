using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_State_Name : MCEditor_DialogBox_State {

	[SerializeField]
	UnityEngine.UI.InputField value;

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
		return "Nom de l'état";
	}

	#endregion

	#region implemented abstract members of MCEditor_DialogBox_State

	protected override void confirmState ()
	{
		if (MCEditorManager.instance.changeModelStateName (this.stateProxy, value.text)) {
			this.stateProxy.AbState.Name = value.text;
		}
	}

	protected override void deactivateState ()
	{
		// Nothing
	}

	protected override void configState ()
	{
		value.text = this.stateProxy.AbState.Name;
	}

	#endregion
}
