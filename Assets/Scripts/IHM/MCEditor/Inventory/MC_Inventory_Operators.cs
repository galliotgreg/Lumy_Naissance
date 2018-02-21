using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Inventory_Operators : MC_Inventory {

	// Use this for initialization
	void Start () {
		base.Start ();

		// Load Operators
		List<System.Object> operators = new List<System.Object> ();
		foreach (OperatorType operatorType in System.Enum.GetValues( typeof( OperatorType ) )) {
		//foreach (OperatorType operatorType in new List<OperatorType>(){OperatorType.BoolTab_Agg_BoolStar}) {	// test : 1 item
			if (operatorType != OperatorType.None) {
				try{
					operators.Add ( ABOperatorFactory.CreateOperator( operatorType ) );
				}
				catch(System.NotImplementedException ex){
					Debug.Log (ex);
				}
			}
		}
		setItems ( operators );
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MC_Inventory

	protected override void configItem (MC_InventoryItem item)
	{
		item.Text.text = MCEditor_Proxy.getNodeName((ABNode)item.Item);
	}

	public override GameObject instantiateProxy (MC_InventoryItem item)
	{
		return MCEditor_Proxy_Factory.instantiateOperator((IABOperator)item.Item, false).gameObject;
	}

	protected override void Drop (GameObject proxy, MC_InventoryItem item)
	{
		Debug.Log ("Drop");
	}

	#endregion
}
