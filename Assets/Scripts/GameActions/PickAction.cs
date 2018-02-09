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
        Debug.Log("Execute Pick");
		if( item is GameObject ){
			ResourceScript resource = item.GetComponent<ResourceScript>();

			if( resource != null ){
				// Check if the player close to the resource
				if( (this.agentAttr.CurPos - resource.Location).magnitude <= this.agentAttr.PickRange ){

					if (Unit_GameObj_Manager.instance.pickResource (resource)) {
						// Can carry the item?
						if( this.agentAttr.NbItem >= this.agentAttr.NbItemMax ){
							// Drop some item
							Unit_GameObj_Manager.instance.dropResource( this.agentAttr.removeLastResource() );
						}

						// Carry it
						this.agentAttr.addResource( resource );
						// Associate gameobject
						resource.transform.parent = this.agentEntity.transform;
					} else {
						throw new System.Exception ("resource not found into the manager");
					}
				}
			}
		}
	}
	#endregion
}
