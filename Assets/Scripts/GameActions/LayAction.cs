using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayAction : GameAction {
    [SerializeField]
    private string castName;

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

	private GameObject currentTemplate;
	private AgentScript.ResourceCost currentCost;
	private bool layDemand = false;

	/// <summary>
	/// Lay a unit associated to the specified childTemplate and decrease the cost from the home.
	/// </summary>
	/// <param name="childTemplate">Child template</param>
	/// <param name="cost">Cost</param>
	private void Lay( GameObject childTemplate, AgentScript.ResourceCost cost )
    {
        GameObject child = Instantiate(
            childTemplate, this.transform.position + (transform.forward*2) , this.transform.rotation);
        child.SetActive(true);
        AgentScript selfTemplate= childTemplate.GetComponent<AgentContext>().Self.GetComponent<AgentScript>();
        AgentScript self = child.GetComponent<AgentContext>().Self.GetComponent<AgentScript>();
        self.ProdCost = selfTemplate.ProdCost; 
        child.transform.parent = GameManager.instance.transform; 
		AgentEntity childEntity = child.GetComponent<AgentEntity> ();
		child.name = childEntity.CastName;
        
		//childEntity.Context.setModelValues (this.agentEntity.Authority);
        
        //Increment Population 
		Unit_GameObj_Manager.instance.addUnit( childEntity, this.agentEntity.Home );

        //Set GameParams
		childEntity.GameParams =
            GameManager.instance.GameParam.GetComponent<GameParamsScript>();
    }

	private void preLay( GameObject childTemplate, AgentScript.ResourceCost cost ){
	}

	/// <summary>
	/// Decreases the resources from the home.
	/// </summary>
	/// <param name="cost">Cost of the unit</param>
	private void DecreaseResources ( AgentScript.ResourceCost cost )
    {
		HomeScript home = agentEntity.Home;
		home.RedResAmout -= cost.getResourceByColor( ABColor.Color.Red );
		home.GreenResAmout -= cost.getResourceByColor( ABColor.Color.Green );
		home.BlueResAmout -= cost.getResourceByColor( ABColor.Color.Blue );
    }

	/// <summary>
	/// Check if the home has enough Resource
	/// </summary>
	/// <returns><c>true</c>, if there is enough resource, <c>false</c> otherwise.</returns>
	/// <param name="childTemplate">Child template.</param>
	/// <param name="cost">Cost of the template.</param>
	private bool CheckResources ( GameObject childTemplate, AgentScript.ResourceCost cost )
	{
		HomeScript home = agentEntity.Home;
		// Get ressources from Home || Change the method to calculate ressources

		//Compare them and return bool
		return (cost.getResourceByColor( ABColor.Color.Red ) <= home.RedResAmout
			&& cost.getResourceByColor( ABColor.Color.Green ) <= home.GreenResAmout
			&& cost.getResourceByColor( ABColor.Color.Blue ) <= home.BlueResAmout);
	}

	#region implemented abstract members of GameAction
	protected override void initAction ()
	{
		this.CoolDownActivate = true;
	}

	protected override bool executeAction (){
		if (!this.layDemand) {
			this.CoolDownTime = 0.01f;
		}
		return true; // consider new cooldown when reset
	}

	protected override void activateAction (){}

	protected override void deactivateAction (){
		this.CoolDownTime = 0.01f;
	}

	protected override void frameBeginAction (){}

	protected override void frameBeginAction_CooldownAuthorized ()
	{
		if (castName != agentEntity.Home.PrysmeName) {
			currentTemplate = GameManager.instance.GetUnitTemplate (agentEntity.Authority, castName);
			AgentEntity unitEntity = currentTemplate.GetComponent<AgentEntity> ();

			currentCost = new AgentScript.ResourceCost (unitEntity.Context.Model.ProdCost);
			if (!CheckResources (currentTemplate, currentCost))
				return;
			if (this.agentEntity.Home.getPopulation ().Count >= SwapManager.instance.GetPlayerNbLumy ())
				return; 

			DecreaseResources (currentCost);

			this.CoolDownTime = unitEntity.Context.Model.LayTimeCost;

			// wait for cooldownTime
			layDemand = true;
		} else {
			throw new System.Exception ("Lay : cannot lay Prysme.");
		}
	}

	protected override void cooldownFinishAction ()
	{
		if (layDemand) {
			Lay (currentTemplate, currentCost);
			layDemand = false;
		}
	}

	protected override void frameEndAction (){}
	#endregion
}
