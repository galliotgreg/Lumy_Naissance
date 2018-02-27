using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_Param_String : MCEditor_DialogBox_Param {

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

	#region implemented abstract members of MCEditor_DialogBox_Param

	protected override void confirmParam ()
	{
		this.param.setProxyName ( value.text );
		((ABTextParam)this.param.AbParam).Value.Value = value.text;
	}

	protected override void configParam ()
	{
		Title = "Text";
		value.text = ((ABTextParam)this.param.AbParam).Value.Value;
	}

	protected override void cancelParam ()
	{
	}
	#endregion
}
