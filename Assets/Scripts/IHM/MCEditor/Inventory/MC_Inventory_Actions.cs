using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Inventory_Actions : MC_Inventory {

	// Use this for initialization
	void Start () {
		base.Start ();

		// Load Operators
		List<System.Object> actions = new List<System.Object> ();
        if (AppContextManager.instance.PrysmeEdit) {
            foreach (ActionType actionType in LumyEditorManager.instance.EditedLumy.GetComponent<AgentEntity>().getAgentActions()) {
                // filter by enable actions
                if (actionType != ActionType.None) {
                    try {
                        ABState state = new ABState(-1, actionType.ToString());
                        state.Action = ABActionFactory.CreateAction(actionType);
                        actions.Add(state);
                    }
                    catch (System.NotImplementedException ex) {
                        Debug.Log(ex);
                    }
                }
            }
            setItems(actions);
        }else
        {
            ABState state = new ABState(-1, ActionType.Lay.ToString());
            state.Action = ABActionFactory.CreateAction(ActionType.Lay);
            actions.Add(state);
            setItems(actions);
        }
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MC_Inventory

	protected override void configItem (MC_InventoryItem item)
	{
		item.Text.text = ((ABState)item.Item).Name;

		((MC_Inventory_NodeItem)item).ItemType = MC_Inventory_NodeItem.NodeItemType.Action;
	}

	public override GameObject instantiateProxy (MC_InventoryItem item)
	{
		ABState itemState = (ABState)item.Item;
		ABState newState = new ABState (itemState.Id, itemState.Name);
		newState.Action = itemState.Action.CloneEmpty();
		ProxyABAction result = MCEditor_Proxy_Factory.instantiateAction( newState );
		return result.gameObject;
	}

	protected override void Drop (GameObject proxy, MC_InventoryItem item)
	{
		ProxyABAction actionProxy = proxy.GetComponent<ProxyABAction> ();
		if (!MCEditorManager.instance.registerAction (actionProxy)) {
			Destroy (proxy);
		}
	}

	#endregion
}
