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

		valueX.ActivateInputField ();
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (valueX.isFocused) {
				valueY.ActivateInputField ();
			}
			if (valueY.isFocused) {
				valueX.ActivateInputField ();
			}
		}
	}

	#region implemented abstract members of MCEditor_DialogBox_Param

	protected override void confirmParam ()
	{
		try{
			float vX = float.Parse (valueX.text);
			float vY = float.Parse (valueY.text);
			((ABVecParam)this.paramProxy.AbParam).Value.X = vX;
			((ABVecParam)this.paramProxy.AbParam).Value.Y = vY;
		}catch(System.Exception ex){
			Debug.LogError ("Value is not a SCALAR");
		}
	}

	protected override void configParam ()
	{
		valueX.text = ((ABVecParam)this.paramProxy.AbParam).Value.X.ToString();
		valueY.text = ((ABVecParam)this.paramProxy.AbParam).Value.Y.ToString();
	}

	protected override void deactivateParam ()
	{
		// Nothing
	}

	protected override string dialogTitle ()
	{
		return "Vec";
	}

	#endregion
}
