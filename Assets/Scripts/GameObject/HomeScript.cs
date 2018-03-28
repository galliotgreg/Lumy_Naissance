using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScript : MonoBehaviour {
	// GameObject Identification
	[AttrName(Identifier = "key")]
	[SerializeField]
	private int key;

    [AttrName(Identifier = "pos")]
    [SerializeField]
    private Vector2 location;
    [AttrName(Identifier = "bRes")]
    [SerializeField]
    private float redResAmout;
    [AttrName(Identifier = "rRes")]
    [SerializeField]
    private float greenResAmout;
    [AttrName(Identifier = "gRes")]
    [SerializeField]
    private float blueResAmout;
    [AttrName(Identifier = "population")]
    private Dictionary<string, int> population = new Dictionary<string, int>();
	[AttrName(Identifier = "cost")]
	private Dictionary<string, Dictionary<string,int>> cost = new Dictionary<string, Dictionary<string, int>> ();

	[SerializeField]
	AgentEntity prysme;
	[SerializeField]
	Unit_GameObj_Manager gameObjectManager;
	[SerializeField]
	PlayerAuthority authority;
	Dictionary<string,List<AgentEntity>> populationGameObj;

	#region Properties
    public Vector2 Location
    {
        get
        {
            return location; 
        }
        set
        {
            location = value; 
        }
    }

    public float RedResAmout
    {
        get
        {
            return redResAmout;
        }

        set
        {
            redResAmout = value;
        }
    }

    public float GreenResAmout
    {
        get
        {
            return greenResAmout;
        }

        set
        {
            greenResAmout = value;
        }
    }

    public float BlueResAmout
    {
        get
        {
            return blueResAmout;
        }

        set
        {
            blueResAmout = value;
        }
    }

    public Dictionary<string, int> Population
    {
        get
        {
            return population;
        }
    }

	public Dictionary<string, Dictionary<string,int>> Cost {
		get {
			return cost;
		}
	}

	public PlayerAuthority Authority {
		get {
			return authority;
		}
		set {
			authority = value;
		}
	}

	public Unit_GameObj_Manager GameObjectManager {
		set {
			gameObjectManager = value;
		}
	}

	public string PrysmeName {
		get {
			return prysme.CastName;
		}
	}

	public int Key {
		get {
			return key;
		}
	}
	#endregion

	public List<AgentEntity> getPopulation(){
		List<AgentEntity> result = new List<AgentEntity>();
		foreach( List<AgentEntity> agent in this.populationGameObj.Values ){
			result.AddRange(agent);
		}
		return result;
	}

	public List<AgentEntity> getPopulation(string cast){
		return this.populationGameObj[ cast ];
	}

	public void addPrysmeToHome( AgentEntity prysme ){
		this.addUnitToHome (prysme);
		this.prysme = prysme;
	}
	public void addUnitToHome( AgentEntity unit ){
		if( !this.populationGameObj.ContainsKey( unit.Context.Model.Cast ) ){
			this.populationGameObj.Add( unit.Context.Model.Cast, new List<AgentEntity>() );
		}
		if( !this.population.ContainsKey( unit.Context.Model.Cast ) ){
			this.population.Add( unit.Context.Model.Cast, 0 );
		}
		this.populationGameObj[ unit.Context.Model.Cast ].Add( unit );
		this.population[ unit.Context.Model.Cast ]++;
		unit.Home = this;
	}

	public void removeUnit( AgentEntity unit ){
		if( this.populationGameObj.ContainsKey( unit.Context.Model.Cast ) ){
			this.populationGameObj[ unit.Context.Model.Cast ].Remove( unit );
		}
		if( this.population.ContainsKey( unit.Context.Model.Cast ) ){
			this.population[ unit.Context.Model.Cast ]--;
		}
	}

	public void fillCost( List<GameObject> units ){
		cost = new Dictionary<string, Dictionary<string, int>> ();
		foreach( GameObject unit in units ){
			AgentEntity entity = unit.GetComponent<AgentEntity> ();
			if (entity != null) {
				cost.Add (entity.CastName, entity.Context.Model.ProdCost);
			}
		}
	}

	void Awake(){
		key = this.GetHashCode();
		populationGameObj = new Dictionary<string, List<AgentEntity>>();
	}

	// Use this for initialization
	void Start () {
		this.Location = positionFromTransform ();
	}

	// Update is called once per frame
	void Update () {
		this.Location = positionFromTransform ();
	}

	Vector2 positionFromTransform(){
		return new Vector2(transform.position.x, transform.position.z);
	}
}
