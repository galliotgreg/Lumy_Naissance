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

		this.model = gameObject.GetComponent<AgentScript>();
    }

    // Use this for initialization
    void Start(){}

    // Update is called once per frame
    void Update()
    {
		/* // the home is not set immediatly
		if (Home == null) {
			if (this.entity.Home != null) {
				Home = this.entity.Home.gameObject;
			}
		}
		*/

		// TODO fill enemies and allies ?
		Debug.LogWarning( "TODO" );
    }
}
