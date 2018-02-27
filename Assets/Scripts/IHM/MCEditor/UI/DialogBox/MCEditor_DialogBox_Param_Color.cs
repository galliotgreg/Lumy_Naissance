using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_Param_Color : MCEditor_DialogBox_Param {

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
		ABColor.Color v = (ABColor.Color)System.Enum.GetValues( typeof( ABColor.Color ) ).GetValue( value.value );
		//this.param.setProxyName ( v.ToString() );
		((ABColorParam)this.param.AbParam).Value.Value = v;
	}

	protected override void configParam ()
	{
		Title = "Couleur";
		ABColor.Color v = ((ABColorParam)this.param.AbParam).Value.Value;
		System.Array colors = System.Enum.GetValues (typeof(ABColor.Color));
		int index = 0;
		for (int i=0; i<colors.Length; i++){
			if( (ABColor.Color)colors.GetValue(i) == v ){
				index = i;
			}
		}
		value.value = index;
	}

	protected override void cancelParam ()
	{
	}

	#endregion
}
