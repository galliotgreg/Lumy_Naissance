using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_Param_Bool : MCEditor_DialogBox_Param {

	[SerializeField]
	BlockerDropdown_DialogBox value;

	// Use this for initialization
	void Start () {
		base.Start ();

		value.DialogBox = this;
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MCEditor_DialogBox_Param

	protected override void confirmParam ()
	{
		bool v = value.value == 0;
		((ABBoolParam)this.paramProxy.AbParam).Value.Value = v;
	}

	protected override void configParam ()
	{
		value.value = (((ABBoolParam)this.paramProxy.AbParam).Value.Value ? 0 : 1 );
	}

	protected override void deactivateParam ()
	{
		// Nothing
	}

	protected override string dialogTitle ()
	{
		return "Booléen";
	}

	#endregion
}
