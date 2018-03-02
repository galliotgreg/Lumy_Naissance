using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAction : GameAction {

	private GameObject item;

	public GameObject Item {
		get {
			return item;
		}
		set {
			item = value;
		}
	}

   
	#region implemented abstract members of GameAction
	protected override void initAction ()
	{
		this.CoolDownActivate = false;
	}
	protected override void executeAction ()
	{
       
	}

	protected override void activateAction ()
	{
        if (!item is GameObject || item == null)
        {
            return;
        }
            ResourceScript resource = item.GetComponent<ResourceScript>();

        if (resource == null)
        {
            return; 
        }
        // Check if the player close to the resource
        if ((this.agentAttr.CurPos - resource.Location).magnitude > this.agentAttr.PickRange)
        {
            return;
        }

        // Can carry the item?
        if (this.agentAttr.NbItem >= this.agentAttr.NbItemMax)
        {
            return; 
        }
        
        if (Unit_GameObj_Manager.instance.pickResource(resource))
        {
            // Carry it
            
            GameObject res = this.agentAttr.addResource(item);
            // Associate gameobject
            //GameObject mineral = Instantiate(Resources.Load("Prefabs/Environment/"));
            res.transform.parent = this.agentEntity.transform;
        }
        else
        {
            throw new System.Exception("resource not found into the manager");
        }
    }

	protected override void deactivateAction ()
	{
		return;
	}
	#endregion
}
