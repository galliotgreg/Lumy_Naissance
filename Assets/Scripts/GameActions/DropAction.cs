using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAction : GameAction {    
    //Ressource Ref
    [SerializeField]
    private GameObject ressource;

    public GameObject Ressource
    {
        get
        {
            return ressource; 
        }
        set
        {
            ressource = value;
        }
    }

    private void Drop()
    {
        HomeScript home = GameManager.instance.GetHome(agentEntity.Authority);

        //Get the ressources script of the ressource ref
        //What to return if null ? 
        if (ressource.GetComponent<ResourceScript>() == null) {
            return; 
        } 
        ressource.GetComponent<ResourceScript>(); 
        //Destroy if close to home and increment the amount
        //What is the range ? Now hard coded : 10
        if(Vector3.Distance(
			agentAttr.CurPos,
			agentEntity.Context.Home.GetComponent<Transform>().position) < 10)
        {
           Color32 color = ressource.GetComponent<ResourceScript>().Color;

            if (color.Equals(new Color32(255, 0, 0, 1)))
                home.RedResAmout += 1;
            else if (color.Equals(new Color32(0, 255, 0, 1)))
                home.GreenResAmout += 1;
            else if (color.Equals(new Color32(0, 0, 255, 1)))
                home.BlueResAmout += 1;
            //else ?? What to do for incolore ?  
            Destroy(ressource);
        }
        
        //Instantiate ressources if not close to home
        else
        {
            Instantiate(gameObject, this.transform);
        }
        
        //Decrement the number of item
		if(agentEntity.Context.Self.GetComponent<AgentScript>().NbItem >0)
			agentEntity.Context.Self.GetComponent<AgentScript>().NbItem -= 1; 
    }

	#region implemented abstract members of GameAction

	protected override void initAction (){
		this.CoolDownActivate = false;
	}

	protected override void executeAction ()
	{
		if(agentAttr.NbItem > 0 && ressource != null)
		{
			Drop(); 
		}
	}

	#endregion
}
