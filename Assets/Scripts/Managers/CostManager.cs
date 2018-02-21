using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour
{
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

    //TWEAKABLE VARS
    [SerializeField]
    private float phy_rate = 1.1f;
    [SerializeField]
    private float mc_param_fixed_cost = 10f;
    [SerializeField]
    private float mc_inter_fixed_cost = 10f;
    [SerializeField]
    private float mc_final_fixed_cost = 10f;
    [SerializeField]
    private float mc_operator_fixed_cost = 10f;
    [SerializeField]
    private float mc_node_fixed_cost = 10f;
    [SerializeField]
    private float mc_param_coef = 1f;
    [SerializeField]
    private float mc_inter_coef = 0f;
    [SerializeField]
    private float mc_final_coef = 0f;
    [SerializeField]
    private float mc_operator_coef = 1.5f;
    [SerializeField]
    private float mc_node_coef = 0.75f;

    public AgentScript.ResourceCost ComputeCost(
        AgentComponent[] agentComponents, ABModel behaviorModel)
    {
        AgentScript.ResourceCost resultCost = new AgentScript.ResourceCost();

        //Handle Component Cost
        FloatResourceCost compoCost = ComputeCompoCost(agentComponents);

        //Handle Behavior Cost
        float behaviorCost = ComputeBehaviorCost(behaviorModel);

        //Merge costs
        string[] keys = new string[3];
        int ind = 0;
        foreach(string color in resultCost.Resources.Keys)
        {
            keys[ind++] = color;
        }

        for (int i=0; i < keys.Length; i++)
        {
            string color = keys[i];
            if (ABColor.Color.Red.ToString() == color)
            {
                resultCost.Resources[color] = (int) MergeCosts(
                        compoCost.Red,
                        behaviorCost);
            }
            else if (ABColor.Color.Green.ToString() == color)
            {
                resultCost.Resources[color] = (int)MergeCosts(
                       compoCost.Green,
                       behaviorCost);
            }
            else if (ABColor.Color.Blue.ToString() == color)
            {
                resultCost.Resources[color] = (int)MergeCosts(
                       compoCost.Blue,
                       behaviorCost);
            }

        }

        return resultCost;
    }

    private class FloatResourceCost
    {
        private float red, green, blue;

        public float Red
        {
            get
            {
                return red;
            }

            set
            {
                red = value;
            }
        }

        public float Green
        {
            get
            {
                return green;
            }

            set
            {
                green = value;
            }
        }

        public float Blue
        {
            get
            {
                return blue;
            }

            set
            {
                blue = value;
            }
        }
    }

    private FloatResourceCost ComputeCompoCost(AgentComponent[] agentComponents)
    {
        FloatResourceCost compoCost = new FloatResourceCost();

        //Build head and tail
        IList<AgentComponent> headCompos = new List<AgentComponent>();
        IList<AgentComponent> tailCompos = new List<AgentComponent>();
        foreach (AgentComponent component in agentComponents)
        {
            string parentName = component.transform.parent.name;
            if (parentName == "Head")
            {
                headCompos.Add(component);
            }
            else if (parentName == "Tail")
            {
                tailCompos.Add(component);
            }
        }
        ((List<AgentComponent>)headCompos).Reverse();

        //Compute actual costs
        int rank = 0;
        float coef;
        foreach (AgentComponent component in headCompos)
        {
            coef = ComputePhyCoef(rank++);
            Color32 color = component.Color;
            if (color.Equals(new Color32(255, 0, 0, 255)))
                compoCost.Red += component.ProdCost * coef;
            else if (color.Equals(new Color32(0, 255, 0, 255)))
                compoCost.Green += component.ProdCost * coef;
            else if (color.Equals(new Color32(0, 0, 255, 255)))
                compoCost.Blue += component.ProdCost * coef;
            else
                Debug.LogWarning("Component has no good color TODO Implement new strategy");
        }

        rank = 0;
        foreach (AgentComponent component in tailCompos)
        {
            coef = ComputePhyCoef(rank++);
            Color32 color = component.Color;
            if (color.Equals(new Color32(255, 0, 0, 255)))
                compoCost.Red += component.ProdCost * coef;
            else if (color.Equals(new Color32(0, 255, 0, 255)))
                compoCost.Green += component.ProdCost * coef;
            else if (color.Equals(new Color32(0, 0, 255, 255)))
                compoCost.Blue += component.ProdCost * coef;
            else
                Debug.LogWarning("Component has no good color TODO Implement new strategy");
        }

        return compoCost;
    }

    private float ComputePhyCoef(int rank)
    {
        return Mathf.Pow(phy_rate, (float) rank);
    }

    private float ComputeBehaviorCost(ABModel behaviorModel)
    {
        float mc_cost;

        //TODO SOMETHING

        return 1f;
    }

    private float MergeCosts(float phys, float mc)
    {
        //TODO compute real MC_0
        float mc_0 = mc;

        return phys * (mc * mc / mc_0);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
