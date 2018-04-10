using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Inventory_MacroOperators : MC_Inventory_Operators {

	// Use this for initialization
	void Start () {
		base.Start();

		// Load Operators
		allOperators = new List<IABOperator> ();
		foreach (IABOperator macro in ABManager.instance.Macros.Values) {
			try{
				allOperators.Add ( macro );
			}
			catch(System.NotImplementedException ex){
				Debug.Log (ex);
			}
		}
		setItems ( listToObject<IABOperator>( allOperators ));

		loadReturnTypeDropdown ();

		returnTypeDropdown.onValueChanged.AddListener (changeReturnTypeDropdown);
		changeReturnTypeDropdown ( returnTypeDropdown.value );
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MC_Inventory

	protected override void configItem (MC_InventoryItem item)
	{
		item.TextItem.text = ((IABOperator)item.Item).ViewName;
		IABOperator op = ((IABOperator)item.Item);
		// Setting return type as title
		// item.Title = MCEditor_Proxy.getNodeName((ABNode)item.Item);
		item.Title = MCEditor_Proxy.typeToString( op.getOutcomeType() );
		// Setting param type as subtitle
		string subTitle = "";
		for(int i=0; i<op.Inputs.Length; i++){
			subTitle += (i>0?"\n":"") + MCEditor_Proxy.typeToString( op.getIncomeType(i) );
			if (ABStar<ABBool>.isStar (op.getIncomeType (i))) {
				break;
			}
		}
		item.SubTitle = subTitle;

		((MC_Inventory_NodeItem)item).ItemType = MC_Inventory_NodeItem.NodeItemType.MacroOperator;
	}

	#endregion
}
