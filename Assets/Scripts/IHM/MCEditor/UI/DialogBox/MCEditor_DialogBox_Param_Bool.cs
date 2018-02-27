using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_Param_Bool : MCEditor_DialogBox_Param {

	[SerializeField]
	BlockerDropdown value;

	// Use this for initialization
	void Start () {
		base.Start ();
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MCEditor_DialogBox_Param

	protected override void confirmParam ()
	{
		bool v = value.value == 0;
		this.param.setProxyName ( v.ToString() );
		//this.param.setProxyName ( value.options[value.value].text );
		((ABBoolParam)this.param.AbParam).Value.Value = v;
	}

	protected override void configParam ()
	{
		Title = "Booléen";
		value.value = (((ABBoolParam)this.param.AbParam).Value.Value ? 0 : 1 );
	}

	protected override void cancelParam ()
	{
		Destroy (value.gameObject);
	}

	#endregion
}
