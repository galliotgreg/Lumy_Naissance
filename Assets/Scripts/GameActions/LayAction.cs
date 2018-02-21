﻿using System.Collections;
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
		child.name = child.GetComponent<AgentEntity> ().CastName;
        //Increment Population 
		this.agentEntity.Home.addUnit( child.GetComponent<AgentEntity>() );

        //Set GameParams
        child.GetComponent<AgentEntity>().GameParams =
            GameManager.instance.GameParam.GetComponent<GameParamsScript>();
    }

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
    
	#region implemented abstract members of GameAction
	protected override void initAction ()
	{
		this.CoolDownActivate = true;
	}


	protected override void executeAction ()
	{
		currentTemplate = GameManager.instance.GetUnitTemplate( agentEntity.Authority, castName );
		AgentEntity unitEntity = currentTemplate.GetComponent<AgentEntity> ();

		currentCost = new AgentScript.ResourceCost( unitEntity.Context.Model.ProdCost );
		if ( CheckResources( currentTemplate, currentCost ) ){
			DecreaseResources( currentCost );

			this.CoolDownTime = unitEntity.Context.Model.LayTimeCost;
			// wait for cooldownTime
			Invoke( "Lay", this.CoolDownTime );
		}
	}

	protected override void activateAction ()
	{
		return;
	}

	protected override void deactivateAction ()
	{
		return;
	}
	#endregion
}
