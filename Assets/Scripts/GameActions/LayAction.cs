using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayAction : MonoBehaviour {
    // Workaround for script enabling issues
    public bool activated;

    [SerializeField]
    private string castName;

    private bool coolDownElapsed = true;

    private GameObject childTemplate;

    private HomeScript home;

    private AgentEntity agentEntity;

    //All Ressources 
    private float unitRedCost = 0;
    private float unitBlueCost = 0;
    private float unitGreenCost = 0;
    private float unitIncoCost = 0;


    public string CastName
    {
        get
        {
            return castName;
        }

        set
        {
            castName = value;
        }
    }

    // Use this for initialization
    void Start () {
        agentEntity = GetComponent<AgentEntity>();
    }
	
	// Update is called once per frame
	void Update () {
		if (!activated)
        {
            return;
        }

        if (coolDownElapsed && CheckRes())
        {     
            //Decrease Ressources & Drop new Instance
            Lay();
            coolDownElapsed = false;
            // Get CoolDown 
            Invoke("EndCooldown", 0.3f);            
        }
    }

    private void Lay()
    {
        //Decrease Ressources
        DecreaseAmount();
        GameObject child = Instantiate(
            childTemplate, this.transform.position, this.transform.rotation);
        child.SetActive(true);
        //Increment Population 
        home.Population[castName]++;
    }

    private void EndCooldown()
    {
        coolDownElapsed = true;
    }

    private bool CheckRes ()
    {
         childTemplate = GameManager.instance.GetUnitTemplate(
         agentEntity.Authority, castName);
        //Get Cost of the child  
        //Change Cost Evaluation Method 
        AgentContext childContext = childTemplate.GetComponent<AgentContext>();
        AgentComponent[] agentComponents = childContext.GetComponents<AgentComponent>();


        foreach (AgentComponent component in agentComponents)
        {
            Color32 color = component.Color;
            if (color.Equals(new Color32(255, 0, 0, 1)))
                unitRedCost += component.ProdCost; 
            else if (color.Equals(new Color32(0, 255, 0, 1)))
                unitBlueCost += component.ProdCost;
            else if (color.Equals(new Color32(0, 0, 255, 1)))
                unitGreenCost += component.ProdCost;
            else
                unitIncoCost += component.ProdCost; 
        }

        unitIncoCost += unitRedCost + unitGreenCost + unitBlueCost; 

        home = GameManager.instance.GetHome(agentEntity.Authority);
        //Get ressources from Home || Change the method to calculate ressources 
        float resAmount = home.RedResAmout + home.GreenResAmout + home.BlueResAmout;

        //Compare them and return bool
        if (unitRedCost <= home.RedResAmout
            && unitGreenCost <= home.GreenResAmout
            && unitBlueCost <= home.BlueResAmout
            && unitIncoCost <= resAmount)  
        {
            return true; 
        }
        unitBlueCost = 0;
        unitGreenCost = 0;
        unitIncoCost = 0;
        unitRedCost = 0;
        return false; 
    }

    private void DecreaseAmount ()
    {
       
     
        //How to decrease the incolore Amount ? 
        home.GreenResAmout -= unitGreenCost;
        home.RedResAmout -= unitRedCost;
        home.BlueResAmout -= unitBlueCost;

        unitBlueCost = 0;
        unitGreenCost = 0;
        unitIncoCost = 0;
        unitRedCost = 0;
    }

  
    
}
