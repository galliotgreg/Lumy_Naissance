using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static CostManager instance = null;

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public AgentScript.ResourceCost ComputeCost(AgentComponent[] agentComponents)
    {
        // TODO Change Cost Evaluation Method
        AgentScript.ResourceCost resultCost = new AgentScript.ResourceCost();

        foreach (AgentComponent component in agentComponents)
        {
            Color32 color = component.Color;
            if (color.Equals(new Color32(255, 0, 0, 255)))
                resultCost.addResource(ABColor.Color.Red, component.ProdCost);
            else if (color.Equals(new Color32(0, 255, 0, 255)))
                resultCost.addResource(ABColor.Color.Green, component.ProdCost);
            else if (color.Equals(new Color32(0, 0, 255, 255)))
                resultCost.addResource(ABColor.Color.Blue, component.ProdCost);
            else
                Debug.LogWarning("Component has no good color TODO Implement new strategy");
        }

        return resultCost;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
