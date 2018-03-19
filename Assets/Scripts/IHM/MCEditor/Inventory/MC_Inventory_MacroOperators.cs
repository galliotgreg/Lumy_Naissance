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
		item.Text.text = ((IABOperator)item.Item).ViewName;

		((MC_Inventory_NodeItem)item).ItemType = MC_Inventory_NodeItem.NodeItemType.MacroOperator;
	}

	#endregion
}
