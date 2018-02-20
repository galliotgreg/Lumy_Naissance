using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Inventory_Actions : MC_Inventory {

	// Use this for initialization
	void Start () {
		base.Start ();

		// Load Operators
		/*List<System.Object> actions = new List<System.Object> ();
		foreach (ActionType actionType in System.Enum.GetValues( typeof( ActionType ) )) {
			if (actionType != ActionType.None) {
				try{
					actions.Add (ABActionFactory.CreateAction( actionType ));
				}
				catch(System.NotImplementedException ex){
					Debug.Log (ex);
				}
			}
		}
		setItems ( actions );*/
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MC_Inventory

	protected override void configItem (MC_InventoryItem item)
	{
		//item.Text.text = MCEditorManager.getNodeName(item.Item);
	}

	public override GameObject instantiateProxy (MC_InventoryItem item)
	{
		return null;
		//return MCEditorManager.instantiateAction ( (ABAction)item.Item ).gameObject;
	}

	protected override void Drop (GameObject proxy, MC_InventoryItem item)
	{
		Debug.Log ("Drop Action");
	}

	#endregion
}
