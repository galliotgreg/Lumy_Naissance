using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Inventory_Params : MC_Inventory {

	// Use this for initialization
	void Start () {
		base.Start ();

		// Load Params
		List<System.Object> parameters = new List<System.Object> ();

		// References
		parameters.Add ( new ABRefParam ("game", null));
		parameters.Add ( new ABRefParam ("home", null));
		parameters.Add ( new ABRefParam ("self", null));
		parameters.Add ( new ABRefParam ("allies", null));
		parameters.Add ( new ABRefParam ("enemies", null));
		parameters.Add ( new ABRefParam ("resources", null));
		parameters.Add ( new ABRefParam ("traces", null));

		// Constants
		// TODO table?
		// Scalar
		parameters.Add ( new ABScalParam ("scal", null));
		// Text
		ABText t = new ABText();
		t.Value = "text";
		parameters.Add ( new ABTextParam ("text", t ) );
		// Boolean
		ABBool abBoolTrue = new ABBool ();
		abBoolTrue.Value = true;
		parameters.Add ( new ABBoolParam ("true", abBoolTrue));
		ABBool abBoolFalse = new ABBool ();
		abBoolFalse.Value = false;
		parameters.Add ( new ABBoolParam ("false", abBoolFalse));
		// Color
		ABColor abColor_Red = new ABColor (); abColor_Red.Value = ABColor.Color.Red;
		parameters.Add ( new ABColorParam ("red", abColor_Red));
		ABColor abColor_Green = new ABColor (); abColor_Green.Value = ABColor.Color.Green;
		parameters.Add ( new ABColorParam ("green", abColor_Green));
		ABColor abColor_Blue = new ABColor (); abColor_Blue.Value = ABColor.Color.Blue;
		parameters.Add ( new ABColorParam ("blue", abColor_Blue));
		// Vector
		parameters.Add ( new ABVecParam ("vec", null));

		setItems ( parameters );
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MC_Inventory

	protected override void configItem (MC_InventoryItem item)
	{
		item.Text.text = ((IABParam)item.Item).Identifier;
	}

	public override GameObject instantiateProxy (MC_InventoryItem item)
	{
		ProxyABParam result = MCEditor_Proxy_Factory.instantiateParam( (IABParam)item.Item, false );
		UnityEngine.UI.Text paramName = result.GetComponentInChildren<UnityEngine.UI.Text>();
		paramName.text = ((IABParam)item.Item).Identifier;
		return result.gameObject;
	}

	protected override void Drop (GameObject proxy, MC_InventoryItem item)
	{
		Debug.Log ("Drop Param");
	}

	#endregion
}
