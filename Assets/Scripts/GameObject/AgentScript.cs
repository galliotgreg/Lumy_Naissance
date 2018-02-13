using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentScript : MonoBehaviour {
    [AttrName(Identifier = "cast")]
    [SerializeField]
    private string cast;
    [AttrName(Identifier = "curPos")]
    [SerializeField]
    private Vector2 curPos;
    [AttrName(Identifier = "trgPos")]
    [SerializeField]
    private Vector2 trgPos;
    [AttrName(Identifier = "vitalityMax")]
    [SerializeField]
    private float vitalityMax;
    [AttrName(Identifier = "vitality")]
    [SerializeField]
    private float vitality;
    [AttrName(Identifier = "strength")]
    [SerializeField]
    private float strength;
    [AttrName(Identifier = "stamina")]
    [SerializeField]
    private float stamina;
    [AttrName(Identifier = "actSpd")]
    [SerializeField]
    private float actSpd;
    [AttrName(Identifier = "moveSpd")]
    [SerializeField]
    private float moveSpd;
    [AttrName(Identifier = "nbItemMax")]
    [SerializeField]
    private int nbItemMax;
    [AttrName(Identifier = "nbItem")]
    [SerializeField]
    private int nbItem;
    [AttrName(Identifier = "atkRange")]
    [SerializeField]
    private float atkRange;
    [AttrName(Identifier = "pickRange")]
    [SerializeField]
	private float pickRange;

	[AttrName(Identifier = "prodCost")]
	[SerializeField]
	private Dictionary<string,int> prodCost = new Dictionary<string, int>();
	[AttrName(Identifier = "layTimeCost")]
	[SerializeField]
	private float layTimeCost;

	[SerializeField]
	private float visionRange;

	private List<ResourceScript> carryingResources = new List<ResourceScript>();

	#region Properties
    public string Cast
    {
        get
        {
            return cast;
        }

        set
        {
            cast = value;
        }
    }

    public Vector2 CurPos
    {
        get
        {
			return curPos;
        }
		set{
			this.curPos = value;
		}
    }

    public Vector2 TrgPos
    {
        get
        {
            return trgPos;
        }

        set
        {
            trgPos = value;
        }
    }

    public float VitalityMax
    {
        get
        {
            return vitalityMax;
        }

        set
        {
            vitalityMax = value;
        }
    }

    public float Vitality
    {
        get
        {
            return vitality;
        }

        set
        {
            vitality = value;
        }
    }

    public float Strength
    {
        get
        {
            return strength;
        }

        set
        {
            strength = value;
        }
    }

    public float Stamina
    {
        get
        {
            return stamina;
        }

        set
        {
            stamina = value;
        }
    }

    public float ActSpd
    {
        get
        {
            return actSpd;
        }

        set
        {
            actSpd = value;
        }
    }

    public float MoveSpd
    {
        get
        {
            return moveSpd;
        }

        set
        {
            moveSpd = value;
        }
    }

    public int NbItemMax
    {
        get
        {
            return nbItemMax;
        }
		set
		{
			this.nbItemMax = value;
		}
    }

    public int NbItem
    {
        get
        {
			return this.nbItem;
        }
    }

    public float AtkRange
    {
        get
        {
            return atkRange;
        }

        set
        {
            atkRange = value;
        }
    }

	public float PickRange
    {
        get
        {
            return pickRange;
        }

        set
        {
            pickRange = value;
        }
    }

	public float LayTimeCost {
		get {
			return layTimeCost;
		}
		set {
			layTimeCost = value;
		}
	}

	public Dictionary<string,int> ProdCost {
		get {
			return prodCost;
		}
		set {
			prodCost = value;
		}
	}

	public float VisionRange {
		get {
			return visionRange;
		}
		set {
			visionRange = value;
		}
	}
	#endregion

	public void addResource( ResourceScript resource ){
		this.carryingResources.Add( resource );
		this.nbItem = this.carryingResources.Count;
	}
	public ResourceScript removeLastResource(){
		if( this.carryingResources.Count > 0 ){
			ResourceScript result = this.carryingResources[ this.carryingResources.Count-1 ];
			this.carryingResources.Remove( result );
			this.nbItem = this.carryingResources.Count;
			return result;
		}
		return null;
	}

	void Awake(){
	}

    // Use this for initialization
    void Start () {
		this.CurPos = positionFromTransform ();
        TrgPos = CurPos;
    }
	
	// Update is called once per frame
	void Update () {
		this.CurPos = positionFromTransform ();
	}

	Vector2 positionFromTransform(){
		return new Vector2(transform.position.x, transform.position.z);
	}

	public class ResourceCost{
		Dictionary<string,int> resources;

		public Dictionary<string, int> Resources {
			get {
				return resources;
			}
		}

		public int getResourceByColor( ABColor.Color color ){
			return resources [color.ToString()];
		}

		public ResourceCost(){
			resources = new Dictionary<string, int>();
			foreach( ABColor.Color color in System.Enum.GetValues( typeof( ABColor.Color ) ) ){
				resources.Add( color.ToString(), 0 );
			}
		}
		public ResourceCost( Dictionary<string, int> newResources ){
			this.resources = newResources;
		}

		public void addResource( ABColor.Color color, int amount ){
			if (resources.ContainsKey (color.ToString())) {
				resources [color.ToString()] += amount;
			}
		}

		public void reduceResource( ABColor.Color color, int amount ){
			if (resources.ContainsKey (color.ToString())) {
				resources [color.ToString()] = Mathf.Max( resources [color.ToString()]-amount, 0 );
			}
		}
	}
}
