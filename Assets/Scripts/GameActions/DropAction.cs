using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAction : MonoBehaviour {

    public bool activated;
    [SerializeField]
    private GameObject ressource;

    private AgentEntity agentEntity; 
    private AgentContext agentContext; 

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

	// Use this for initialization
	void Start () {
        agentContext = GetComponent<AgentContext>();
        agentEntity = GetComponent<AgentEntity>();
    }
	
	// Update is called once per frame
	void Update () {
		
        if(!activated)
        {
            return; 
        }

        if(agentContext.Self.GetComponent<AgentScript>().NbItem > 0 && ressource != null)
        {
            Drop(); 
        }
	}

    private void Drop()
    {
        //Get the ref of the ressources || Passed in args ? --> Add the Getter and Setter to the
        //ResourcesScript ? 
        ressource.GetComponent<ResourceScript>();
        //Destroy if close to home and increment the amount
        //Increase the amount ok ! Get the reference from the pick action 
        //What is the range ? Now hard coded : 10
        HomeScript home = GameManager.instance.GetHome(agentEntity.Authority);
        if (Vector3.Distance(
            agentContext.Self.GetComponent<AgentScript>().CurPos,
            agentContext.Home.GetComponent<Transform>().position) < 10)
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
        else
        {
            Instantiate(gameObject, this.transform);
        }
        //Decrement the number of item
        if(agentContext.Self.GetComponent<AgentScript>().NbItem > 0)
            agentContext.Self.GetComponent<AgentScript>().NbItem -= 1; 


    }
}
