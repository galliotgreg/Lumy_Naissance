using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentContext : MonoBehaviour
{
	// GameObject Identification
	[BindParam(Identifier = "key")]
	[SerializeField]
	private int key;

    [BindParam(Identifier = "self")]
    [SerializeField]
    private GameObject self;
    [BindParam(Identifier = "hive")]
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

	public int Key {
		get {
			return key;
		}
	}
	#endregion

	void Awake(){
		key = this.GetHashCode();
	}

    // Use this for initialization
    void Start()
    {
        AgentEntity agentScript = gameObject.GetComponent<AgentEntity>();
        Home = GameManager.instance.GetHome(agentScript.Authority).gameObject;

		this.model = gameObject.GetComponent<AgentScript>();

		setModelValues ();
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

	void setModelValues(){
		// Set Model Values based on AgentComponents
		AgentComponent[] agentComponents = this.entity.getAgentComponents();
		// vitality
		this.model.VitalityMax = 0;
		// strength
		this.model.Strength = 0;
		// stamina
		this.model.Stamina = 0;
		// actSpeed
		this.model.ActSpd = 0;
		// moveSpeed
		this.model.MoveSpd = 0;
		// nbItemMax
		Debug.LogError( "TODO : nbItem always 1" );
		this.model.NbItemMax = 1;
		// atkRange
		this.model.AtkRange = 0;
		// pickRange
		Debug.LogError( "TODO : pickRange = visionRange" );
		this.model.PickRange = 0;

		foreach( AgentComponent comp in agentComponents ){
			this.model.VitalityMax += comp.VitalityBuff;
			this.model.Strength += comp.StrengthBuff;
			this.model.Stamina += comp.StaminaBuff;
			this.model.ActSpd += comp.ActionSpeedBuff;
			this.model.MoveSpd += comp.MoveSpeedBuff;
			Debug.LogError( "TODO : atkRange = visionRange" );
			Debug.LogError( "TODO : difference between visionRange and visionRangeBuff" );
			this.model.AtkRange += comp.VisionRange;
			Debug.LogError( "TODO : pickRange = visionRange" );
			this.model.PickRange += comp.VisionRange;
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
}
