using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentContext : MonoBehaviour
{
    [BindParam(Identifier = "game")]
    [SerializeField]
    private GameObject gameParams;

    [BindParam(Identifier = "self")]
    [SerializeField]
    private GameObject self;
    [BindParam(Identifier = "home")]
    [SerializeField]
    private GameObject home;

    [BindParam(Identifier = "enemies")]
    [SerializeField]
    private GameObject[] enemies = new GameObject[0];
    [BindParam(Identifier = "allies")]
    [SerializeField]
    private GameObject[] allies = new GameObject[0];
    [BindParam(Identifier = "resources")]
    [SerializeField]
    private GameObject[] resources = new GameObject[0];
    [BindParam(Identifier = "traces")]
    [SerializeField]
    private GameObject[] traces = new GameObject[0];

	[SerializeField]
	private AgentEntity entity;
	[SerializeField]
	private AgentScript model;

	#region Properties
    public GameObject Self
    {
        get
        {
            return self;
        }

        set
        {
            self = value;
        }
    }

	public AgentScript Model {
		get {
			return model;
		}
	}

	public AgentEntity Entity {
		get {
			return entity;
		}
		set {
			entity = value;
		}
	}

    public GameObject Home
    {
        get
        {
            return home;
        }

        set
        {
            home = value;
        }
    }

    public GameObject[] Enemies
    {
        get
        {
            return enemies;
        }

        set
        {
            enemies = value;
        }
    }

    public GameObject[] Allies
    {
        get
        {
            return allies;
        }

        set
        {
            allies = value;
        }
    }

    public GameObject[] Resources
    {
        get
        {
            return resources;
        }

        set
        {
            resources = value;
        }
    }

    public GameObject[] Traces
    {
        get
        {
            return traces;
        }

        set
        {
            traces = value;
        }
    }

    public GameObject GameParams
    {
        get
        {
            return gameParams;
        }

        set
        {
            gameParams = value;
        }
    }
    #endregion

    void Awake(){
		this.model = self.GetComponent<AgentScript>();
    }

    // Use this for initialization
    void Start(){
	}

    // Update is called once per frame
    void Update()
    {
		// fill enemies and allies
		this.Allies = extractGameObj( Unit_GameObj_Manager.instance.alliesInRange( this.entity ).ToArray() );
		this.Enemies = extractGameObj( Unit_GameObj_Manager.instance.enemiesInRange( this.entity ).ToArray() );
		// fill resources
		this.Resources = extractGameObj( Unit_GameObj_Manager.instance.resourcesInRange( this.entity ).ToArray() );
		// fill traces
		this.Traces = extractGameObj( Unit_GameObj_Manager.instance.tracesInRange( this.entity ).ToArray() );
    }

	public void setModelValues(PlayerAuthority authority)
    {
		// Set Model Values based on AgentComponents
		AgentComponent[] agentComponents = this.entity.getAgentComponents();

		// vitality
		this.model.VitalityMax = 20;
		// strength
		this.model.Strength = 5;
		// stamina
		this.model.Stamina = 5;
		// actSpeed
		this.model.ActSpd = 5;
		// moveSpeed
		this.model.MoveSpd = 10;
		// nbItemMax
		this.model.NbItemMax = 20;
		// atkRange
		this.model.AtkRange = 0;
		// pickRange
		Debug.LogWarning( "TODO : pickRange = visionRange" );
		this.model.PickRange = 0;

        // ProdCost
       // ABModel behaviorModel = ABManager.instance.FindABModel(entity.BehaviorModelIdentifier);
        string playerFolder = GameManager.instance.PLAYER1_SPECIE_FOLDER;
        if (authority == PlayerAuthority.Player2)
        {
            playerFolder = GameManager.instance.PLAYER2_SPECIE_FOLDER;
        }
        string path = GameManager.instance.IntputsFolderPath
                + playerFolder
                + entity.BehaviorModelIdentifier
                + ".csv";
       // Debug.Log(path);
        ABModel behaviorModel = ABManager.instance.LoadABModelFromFile(path);
        AgentScript.ResourceCost cost = getCost( agentComponents, behaviorModel);
		this.model.ProdCost = cost.Resources;
		// layTimeCost
		this.model.LayTimeCost = getCooldown( agentComponents );
		// visionRange
		this.model.VisionRange = 0;

        foreach (AgentComponent comp in agentComponents)
        {
            this.model.VitalityMax += comp.VitalityBuff;
            this.model.Strength += comp.StrengthBuff;
            this.model.Stamina += comp.StaminaBuff;
            this.model.ActSpd += comp.ActionSpeedBuff;
            this.model.MoveSpd += comp.MoveSpeedBuff;
            this.model.PickRange += comp.PickRangeBuff;
            this.model.AtkRange += comp.AtkRangeBuff;
            this.model.VisionRange += comp.VisionRangeBuff;
        }

        this.model.Vitality = this.model.VitalityMax;
	}

	GameObject[] extractGameObj( MonoBehaviour[] list ){
		GameObject[] result = new GameObject[ list.Length ];
		for( int i = 0; i < list.Length; i++ ){
			result [i] = list [i].gameObject;
		}
		return result;
	}

	GameObject[] extractGameObj( AgentEntity[] list ){
		GameObject[] result = new GameObject[ list.Length ];
		for( int i = 0; i < list.Length; i++ ){
			result [i] = list [i].Context.Self;
		}
		return result;
	}

	/// <summary>
	/// Calculates the cooldown for laying a unit
	/// </summary>
	/// <param name="agentComponents">Agent's Components</param>
	/// <returns>The cooldown.</returns>
	float getCooldown( AgentComponent[] agentComponents ){
        return CostManager.instance.ComputeProdTime(agentComponents);
	}

	/// <summary>
	/// Obtains the cost associated to a template
	/// </summary>
	/// <param name="agentComponents">Agent's Components</param>
	/// <returns>The cost.</returns>
	AgentScript.ResourceCost getCost( AgentComponent[] agentComponents, ABModel behaviorModel ){
        return CostManager.instance.ComputeCost(agentComponents, behaviorModel);
	}
}
