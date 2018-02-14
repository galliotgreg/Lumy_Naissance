using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAction : GameAction {    

	private void Drop()
    {
		// has item? - and remove the item from the entity
		ResourceScript _resource = this.agentAttr.removeLastResource();
		if( _resource != null ){

			// Check if the player close to the home
			// TODO What is the range?
			Debug.LogWarning( "TODO" );
            Debug.Log("DROP THE MIC"); 
			if( (this.agentAttr.CurPos - this.agentEntity.Home.Location).magnitude <= 1 ){
				// Close to home
				// Destroy object
				Destroy( _resource.gameObject );

				// Add to the home
				Color32 color = _resource.Color;

				if ( color.Equals( new Color32(255, 0, 0, 255) ) ){
					this.agentEntity.Home.RedResAmout++;
				}else if( color.Equals( new Color32(0, 255, 0, 255) ) ){
					this.agentEntity.Home.GreenResAmout++;
				}else if( color.Equals( new Color32(0, 0, 255, 255) ) ){
					this.agentEntity.Home.BlueResAmout++;
				}
				
			}
			else{
				// Wherever else
				_resource.transform.position = this.transform.position;

				Unit_GameObj_Manager.instance.dropResource( _resource );
			}
		}
    }

	#region implemented abstract members of GameAction

	protected override void initAction (){
		this.CoolDownActivate = false;
	}

	protected override void executeAction ()
	{
		return;
	}

	protected override void activateAction ()
	{
		Drop();
	}

	protected override void deactivateAction ()
	{
		return;
	}

	#endregion
}
