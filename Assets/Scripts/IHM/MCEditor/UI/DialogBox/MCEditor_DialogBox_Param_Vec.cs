using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_Param_Vec : MCEditor_DialogBox_Param {

	[SerializeField]
	UnityEngine.UI.InputField valueX;
	[SerializeField]
	UnityEngine.UI.InputField valueY;

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
			float vX = float.Parse (valueX.text);
			float vY = float.Parse (valueY.text);
			//this.param.setProxyName ( "("+vX.ToString()+","+vY.ToString()+")" );
			((ABVecParam)this.param.AbParam).Value.X = vX;
			((ABVecParam)this.param.AbParam).Value.Y = vY;
		}catch(System.Exception ex){
			Debug.LogError ("Value is not a SCALAR");
		}
	}

	protected override void configParam ()
	{
		Title = "Vec";
		valueX.text = ((ABVecParam)this.param.AbParam).Value.X.ToString();
		valueY.text = ((ABVecParam)this.param.AbParam).Value.Y.ToString();
	}

	protected override void cancelParam ()
	{
	}

	#endregion
}
