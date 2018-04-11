using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Inventory_States : MC_Inventory {

	// Use this for initialization
	void Start () {
		base.Start ();

		// Load States
		List<System.Object> states = new List<System.Object> ();

		// References
		//states.Add ( new ABState(0, "Init") );
		states.Add ( new ABState(-1, "State") );

		setItems ( states );
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MC_Inventory

	protected override void configItem (MC_InventoryItem item)
	{
		item.TextItem.text = ((ABState)item.Item).Name;
		item.Title = "State";

		((MC_Inventory_NodeItem)item).ItemType = MC_Inventory_NodeItem.NodeItemType.State;
	}

	public override GameObject instantiateProxy (MC_InventoryItem item)
	{
		ABState itemState = (ABState)item.Item;
		ProxyABState result = MCEditor_Proxy_Factory.instantiateState( new ABState( itemState.Id, itemState.Name) , ((ABState)item.Item).Id == 0 );
		return result.gameObject;
	}

	protected override void Drop (GameObject proxy, MC_InventoryItem item)
	{
		ProxyABState stateProxy = proxy.GetComponent<ProxyABState> ();
		if (!MCEditorManager.instance.registerState (stateProxy)) {
			Destroy (proxy);
		}
	}

	#endregion
}
