using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_Param_Scalar : MCEditor_DialogBox_Param {

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
		try{
			float v = float.Parse (value.text);
			//this.param.setProxyName ( v.ToString() );
			((ABScalParam)this.param.AbParam).Value.Value = v;
		}catch(System.Exception ex){
			Debug.LogError ("Value is not a SCALAR");
		}
	}

	protected override void configParam ()
	{
		Title = "Scalar";
		value.text = ((ABScalParam)this.param.AbParam).Value.Value.ToString();
	}

	protected override void cancelParam ()
	{
	}

	#endregion
}
