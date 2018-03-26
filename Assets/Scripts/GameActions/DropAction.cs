using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAction : GameAction {    

	private void Drop()
    {
		// has item? - and remove the item from the entity
		List<GameObject> listResource = this.agentAttr.removeResource();
		if( listResource != null && listResource.Count != 0 ){

            // Check if the player close to the home
            // TODO What is the range?
            if ((this.agentAttr.CurPos - this.agentEntity.Home.Location).magnitude <= 1)
            {
                // Close to home
                // Destroy object
                foreach (GameObject res in listResource)
                {
                    Destroy(res.gameObject);

                    // Add to the home

                    Color32 color = res.GetComponent<ResourceScript>().Color;

                    if (color.Equals(new Color32(255, 0, 0, 255)))
                    {
                        this.agentEntity.Home.RedResAmout++;
                    }
                    else if (color.Equals(new Color32(0, 255, 0, 255)))
                    {
                        this.agentEntity.Home.GreenResAmout++;
                    }
                    else if (color.Equals(new Color32(0, 0, 255, 255)))
                    {
                        this.agentEntity.Home. BlueResAmout++;
                    }
                }


            }
            else
            {
				// Wherever else
                foreach(GameObject res in listResource)
                {
                    
                    Unit_GameObj_Manager.instance.dropResource(res.GetComponent<ResourceScript>());
                //    res.transform.position = this.transform.position;
                }
				
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
		return;
	}

	protected override void deactivateAction ()
	{
		return;
	}

	protected override void frameBeginAction ()
	{
		Drop();
	}

	protected override void frameBeginAction_CooldownAuthorized ()
	{
		return;
	}

	protected override void frameEndAction ()
	{
		return;
	}

	#endregion
}
