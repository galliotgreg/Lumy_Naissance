using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Inventory_Operators : MC_Inventory {

	// Use this for initialization
	void Start () {
		base.Start ();

		// Load Operators
		List<ABNode> operators = new List<ABNode> ();
		foreach (OperatorType operatorType in System.Enum.GetValues( typeof( OperatorType ) )) {
		//foreach (OperatorType operatorType in new List<OperatorType>(){OperatorType.BoolTab_Agg_BoolStar}) {	// test : 1 item
			if (operatorType != OperatorType.None) {
				try{
					operators.Add ( (ABNode) ABOperatorFactory.CreateOperator( operatorType ) );
				}
				catch(System.NotImplementedException ex){
					Debug.Log (ex);
				}
			}
		}
		setItems ( operators );
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MC_Inventory

	protected override void configItem (MC_InventoryItem item)
	{
		item.Text.text = MCEditorManager.getNodeName(item.Item);
	}

	public override void configProxyItem (GameObject proxy, MC_InventoryItem item)
	{
		MCEditorManager.SetNodeName (proxy, item.Item);
	}

	protected override void Drop (GameObject proxy, MC_InventoryItem item)
	{
		Debug.Log ("Drop");
	}

	#endregion
}
