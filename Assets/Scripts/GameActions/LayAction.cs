using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayAction : GameAction {
    [SerializeField]
    private string castName;

    private int nbComposants = 0;

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

	class ResourceCost{
		public float red;
		public float green;
		public float blue;

		public ResourceCost(){
			red = 0;
			green = 0;
			blue = 0;
		}
	}

	/// <summary>
	/// Lay a unit associated to the specified childTemplate and decrease the cost from the home.
	/// </summary>
	/// <param name="childTemplate">Child template</param>
	/// <param name="cost">Cost</param>
	private void Lay( GameObject childTemplate, ResourceCost cost )
    {
        //Decrease Ressources
		DecreaseResources( cost );
        GameObject child = Instantiate(
            childTemplate, this.transform.position, this.transform.rotation);
        child.SetActive(true);
		child.name = child.GetComponent<AgentEntity> ().CastName;
        //Increment Population 
		this.agentEntity.Home.addUnit( child.GetComponent<AgentEntity>() );
    }

	/// <summary>
	/// Decreases the resources from the home.
	/// </summary>
	/// <param name="cost">Cost of the unit</param>
	private void DecreaseResources ( ResourceCost cost )
    {
		HomeScript home = agentEntity.Home;
		home.RedResAmout -= cost.red;
		home.GreenResAmout -= cost.green;
		home.BlueResAmout -= cost.blue;
    }

	/// <summary>
	/// Obtains the cost associated to a template
	/// </summary>
	/// <returns>The cost.</returns>
	/// <param name="childTemplate">Child template.</param>
	ResourceCost getCost( GameObject childTemplate ){
		//Get Cost of the child
		//Change Cost Evaluation Method
		AgentEntity child = childTemplate.GetComponent<AgentEntity>();
		AgentComponent[] agentComponents = child.gameObject.GetComponents<AgentComponent>();

		ResourceCost resultCost = new ResourceCost();

		foreach (AgentComponent component in agentComponents)
		{
			Color32 color = component.Color;
			if (color.Equals(new Color32(255, 0, 0, 1)))
				resultCost.red += component.ProdCost; 
			else if (color.Equals(new Color32(0, 255, 0, 1)))
				resultCost.green += component.ProdCost;
			else if (color.Equals(new Color32(0, 0, 255, 1)))
				resultCost.blue += component.ProdCost;
		}

		return resultCost;
	}

	/// <summary>
	/// Check if the home has enough Resource
	/// </summary>
	/// <returns><c>true</c>, if there is enough resource, <c>false</c> otherwise.</returns>
	/// <param name="childTemplate">Child template.</param>
	/// <param name="cost">Cost of the template.</param>
	private bool CheckResources ( GameObject childTemplate, ResourceCost cost )
	{
		HomeScript home = agentEntity.Home;
		// Get ressources from Home || Change the method to calculate ressources

		//Compare them and return bool
		return (cost.red <= home.RedResAmout
			&& cost.green <= home.GreenResAmout
			&& cost.blue <= home.BlueResAmout);
	}
    
	#region implemented abstract members of GameAction
	protected override void initAction ()
	{
		this.CoolDownActivate = true;
        GameObject childTemplate = GameManager.instance.GetUnitTemplate(agentEntity.Authority, castName);
	}

	protected override void executeAction ()
	{
		GameObject childTemplate = GameManager.instance.GetUnitTemplate( agentEntity.Authority, castName );

        AgentEntity child = childTemplate.GetComponent<AgentEntity>();
        AgentComponent[] agentComponents = child.getAgentComponents();
        nbComposants = agentComponents.Length;
        Debug.Log("NbComposants :" + nbComposants);
        this.CoolDownTime = (float) 0.5f * nbComposants;
        Debug.Log(CoolDownTime); 
        ResourceCost cost = getCost( childTemplate );
       

        if ( CheckResources( childTemplate, cost ) ){   
			Lay( childTemplate, cost );
		}
        else {
        }
	}
	#endregion
}
