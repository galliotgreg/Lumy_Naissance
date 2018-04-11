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
		parameters.Add ( new ABRefParam ("game", TypeFactory.CreateEmptyRef() ));
		parameters.Add ( new ABRefParam ("home", TypeFactory.CreateEmptyRef() ));
		parameters.Add ( new ABRefParam ("self", TypeFactory.CreateEmptyRef() ));
		parameters.Add ( new ABTableParam<ABRef> ("allies", TypeFactory.CreateEmptyTable<ABRef>() ));
		parameters.Add ( new ABTableParam<ABRef> ("enemies", TypeFactory.CreateEmptyTable<ABRef>() ));
		parameters.Add ( new ABTableParam<ABRef> ("resources", TypeFactory.CreateEmptyTable<ABRef>() ));
		parameters.Add ( new ABTableParam<ABRef> ("traces", TypeFactory.CreateEmptyTable<ABRef>() ));

		// Constants
		// TODO table?
		// Scalar
		ABScalar s = new ABScalar();
		s.Value = 0;
		parameters.Add ( new ABScalParam ("const", s));
		// Text
		ABText t = new ABText();
		t.Value = "text";
		parameters.Add ( new ABTextParam ("const", t ) );
		// Boolean
		ABBool abBoolTrue = new ABBool ();
		abBoolTrue.Value = true;
		parameters.Add ( new ABBoolParam ("const", abBoolTrue));
		ABBool abBoolFalse = new ABBool ();
		abBoolFalse.Value = false;
		parameters.Add ( new ABBoolParam ("const", abBoolFalse));
		// Color
		ABColor abColor_Red = new ABColor (); abColor_Red.Value = ABColor.Color.Red;
		parameters.Add ( new ABColorParam ("const", abColor_Red));
		ABColor abColor_Green = new ABColor (); abColor_Green.Value = ABColor.Color.Green;
		parameters.Add ( new ABColorParam ("const", abColor_Green));
		ABColor abColor_Blue = new ABColor (); abColor_Blue.Value = ABColor.Color.Blue;
		parameters.Add ( new ABColorParam ("const", abColor_Blue));
		// Vector
		ABVec v = new ABVec();
		v.X = 0;
		v.Y = 0;
		parameters.Add ( new ABVecParam ("const", v));

		setItems ( parameters );
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MC_Inventory

	protected override void configItem (MC_InventoryItem item)
	{
		if (item.Item is ABScalParam) {
			item.TextItem.text = "Scalar";
			item.Title = item.TextItem.text;
		} else if (item.Item is ABVecParam) {
			item.TextItem.text = "Vec";
			item.Title = item.TextItem.text;
		} else if (item.Item is ABTextParam) {
			item.TextItem.text = "Text";
			item.Title = item.TextItem.text;
		} else {
			if (item.Item is ABBoolParam) {
				item.Title = "Bool";
			} else if (item.Item is ABColorParam) {
				item.Title = "Color";
			} else if (item.Item is ABRefParam) {
				item.Title = "Reference";
			} else if (item.Item is ABTableParam<ABRef>) {
				item.Title = "Array of References";
			} else {
				item.Title = "";
			}
			item.TextItem.text = ProxyABParam.GetViewValue ((IABParam)item.Item);
		}
		item.AddContent( ProxyABParam.GetViewValue ((IABParam)item.Item) );

		((MC_Inventory_NodeItem)item).ItemType = MC_Inventory_NodeItem.NodeItemType.Param;
	}

	public override GameObject instantiateProxy (MC_InventoryItem item)
	{
		IABParam newParam = ((IABParam)item.Item).Clone ();
		ProxyABParam result = MCEditor_Proxy_Factory.instantiateParam( newParam, false );
		return result.gameObject;
	}

	protected override void Drop (GameObject proxy, MC_InventoryItem item)
	{
		MCEditorManager.instance.registerParam( proxy.GetComponent<ProxyABParam>() );
	}

	#endregion
}
