using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentScript : MonoBehaviour {
	// GameObject Identification
	[AttrName(Identifier = "key")]
	[SerializeField]
	private int key;

    [AttrName(Identifier = "cast")]
    [SerializeField]
    private string cast;
    [AttrName(Identifier = "curPos")]
    [SerializeField]
    private Vector2 curPos;
    [AttrName(Identifier = "trgPos")]
    [SerializeField]
    private Vector2 trgPos;
	[AttrName(Identifier = "trgPosValid")]
	[SerializeField]
	private bool trgPosValid;
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

    [AttrName(Identifier = "visionRange")]
    [SerializeField]
	private float visionRange;

    [SerializeField]
    private GameObject mineralRed;
    [SerializeField]
    private GameObject mineralGreen;
    [SerializeField]
    private GameObject mineralBlue;

    private List<GameObject> carryingResources = new List<GameObject>();
	[SerializeField]
	Transform lumyTransform;

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
			TrgPosValid = true;
        }
    }

	public bool TrgPosValid {
		get {
			return trgPosValid;
		}
		set {
			trgPosValid = value;
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

	public int Key {
		get {
			return key;
		}
	}
	#endregion

	public GameObject addResource(GameObject resourceGO ){
        //ResourceSpot
        ResourceScript resource = resourceGO.GetComponent<ResourceScript>(); 
        resource.Stock -= 1;
        //ResourceMineral 
        GameObject res = new GameObject();
        if (resource.Color == Color.red)
        {
            res = Instantiate(mineralRed);
        }
        else if(resource.Color == Color.blue)
        {
            res = Instantiate(mineralBlue);

        }
        else if (resource.Color == Color.green)
        {
            res = Instantiate(mineralGreen);

        }


        res.transform.SetParent(this.transform);
        res.transform.position = res.transform.parent.position;
 
        res.GetComponent<ResourceScript>().Stock = 1;
        res.GetComponent<ResourceScript>().Color = resource.Color;

        this.carryingResources.Add(res);
        this.nbItem = 0; 
        foreach(GameObject go in carryingResources)
        {
            int stock = go.GetComponent<ResourceScript>().Stock;
            this.nbItem += stock; 
        }

        return res; 
	}
    //Remove all Resources 
	public List<GameObject> removeResource(){
		if( this.carryingResources.Count > 0 ){
            List<GameObject> listRes =  new List<GameObject>(); 
            foreach (GameObject res in carryingResources)
            {
                listRes.Add(res); 
            }
            nbItem = 0;
            this.carryingResources.Clear();
            //ResourceScript result = this.carryingResources[ this.carryingResources.Count-1 ];
            //this.carryingResources.Remove( result );
            //this.nbItem = this.carryingResources.Count;
            return listRes;
		}
		return null;
	}

	void Awake(){
		key = this.GetHashCode();
	}

    // Use this for initialization
    void Start () {
		this.CurPos = positionFromTransform ();
        TrgPos = CurPos;
		TrgPosValid = true;
    }
	
	// Update is called once per frame
	void Update () {
		this.CurPos = positionFromTransform ();
	}

	Vector2 positionFromTransform(){
		//return new Vector2(transform.position.x, transform.position.z);
		return AgentBehavior.worldToVec2( lumyTransform.position );
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
			resources.Add( ABColor.Color.Red.ToString(), 0 );
            resources.Add(ABColor.Color.Green.ToString(), 0);
            resources.Add(ABColor.Color.Blue.ToString(), 0);
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
