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

	private bool CoolDownAuthorized = true;		// Indicates if the cooldown authorizes the execution of the action
	private bool firstExecution = true;			// Indicates the first execution of the action

	/// <summary>
	/// Lay a unit associated to the specified childTemplate and decrease the cost from the home.
	/// </summary>
	/// <param name="childTemplate">Child template</param>
	/// <param name="cost">Cost</param>
	private void Lay( GameObject childTemplate, AgentScript.ResourceCost cost )
    {
        GameObject child = Instantiate(
            childTemplate, this.transform.position, this.transform.rotation);
        child.SetActive(true);
        child.transform.parent = GameManager.instance.transform; 
		child.name = child.GetComponent<AgentEntity> ().CastName;
        //Increment Population 
		this.agentEntity.Home.addUnit( child.GetComponent<AgentEntity>() );

        //Set GameParams
        child.GetComponent<AgentEntity>().GameParams =
            GameManager.instance.GameParam.GetComponent<GameParamsScript>();
    }

	private void preLay( GameObject childTemplate, AgentScript.ResourceCost cost ){
	}

    /// <summary>
    /// TODO check if used. Remove if not
    /// </summary>
	private void Lay(){
		Lay (currentTemplate, currentCost);
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
    // activate
	// execute
	#region implemented abstract members of GameAction
	protected override void initAction ()
	{
		this.CoolDownActivate = true;
	}

	protected override void executeAction ()
	{
		CoolDownAuthorized = !firstExecution; // As the action is executed on ActivateAction, and the authorization is given by ExecuteAction (which is executed by default), it avoids the incorrect authorization during the first execution
		firstExecution = false;
	}

	protected override void activateAction ()
	{
		// this action is evaluated on activation. And it is executed if the cooldown is elapsed
		if( !CoolDownAuthorized ) {
			// Debug.LogError ( "===== NOT" );
		}
		if ( CoolDownAuthorized ) {
			this.CoolDownAuthorized = false;
			// Debug.LogError ( "* AUTH" );
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
			Invoke ("Lay", this.CoolDownTime);
		}

		return;
	}

	protected override void deactivateAction ()
	{
		// ATTENTION : TODO
		// CoolDownAuthorized = true;	// if the action is deactivated and executeAction is not called
	}
	#endregion
}
