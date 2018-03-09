using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_Action_Name : MCEditor_DialogBox_Action {

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
		return "Nom de l'action";
	}

	#endregion

	#region implemented abstract members of MCEditor_DialogBox_Action

	protected override void confirmAction ()
	{
		if (MCEditorManager.instance.changeModelActionName (this.actionProxy, value.text)) {
			this.actionProxy.AbState.Name = value.text;
		}
	}

	protected override void deactivateAction ()
	{
		// Nothing
	}

	protected override void configAction ()
	{
		value.text = this.actionProxy.AbState.Name;
	}

	#endregion
}