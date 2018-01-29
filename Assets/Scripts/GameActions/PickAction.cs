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
		ResourceScript resource = null;

		// Item is in the range
		// TODO Resource script?
		if( item != null && (this.agentAttr.CurPos - item.GetComponent<ResourceScript>().Location).magnitude <= this.agentAttr.PickRange ){
			resource = item.GetComponent<ResourceScript>();
		}
			
		if( resource != null ){
			// Can carry the item?
			if( this.agentAttr.NbItem >= this.agentAttr.NbItemMax ){
				// Drop some item
				// TODO which one?
				// TODO how to drop?
				this.agentAttr.NbItem--;
			}

			// Carry it
			this.agentEntity.Context.Resources[ this.agentAttr.NbItem ] = item;
			this.agentAttr.NbItem++;

			// TODO Remove from map
		}
	}
	#endregion
}
