using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentEntity : MonoBehaviour
{
    [SerializeField]
    private int id;

	private HomeScript home;
    [SerializeField]
    private AgentBehavior behaviour;
    [SerializeField]
    private AgentContext context;

	/// <summary>
	/// Name of the ABM the agent engages in
	/// </summary>
	[SerializeField]
	private string behaviorModelIdentifier;

	#region Properties
	public HomeScript Home {
		get {
			return home;
		}
		set {
			home = value;
			this.context.Home = value.gameObject;
		}
	}

    public AgentBehavior Behaviour
    {
        get
        {
            return behaviour;
        }
    }

    public AgentContext Context
    {
        get
        {
            return context;
        }
    }

	public string BehaviorModelIdentifier {
		get {
			return behaviorModelIdentifier;
		}
		set {
			behaviorModelIdentifier = value;
		}
	}

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

	[SerializeField]
    public PlayerAuthority Authority
    {
        get
        {
			return home.Authority;
        }
    }

	[SerializeField]
    public string CastName
    {
        get
        {
			return this.context.Model.Cast;
        }
    }

	public float VisionRange
	{
		get
		{
			float result = 0;
			foreach( AgentComponent comp in getAgentComponents() ){
				result += comp.VisionRange;
			}
			return result;
		}
	}

	public float ProdCost
	{
		get
		{
			Debug.LogWarning ( "TODO : the result must be a struct with the cost for each color" );
			float result = 0;
			foreach( AgentComponent comp in getAgentComponents() ){
				result += comp.ProdCost;
			}
			return result;
		}
	}

	public float LayTimeCost
	{
		get
		{
			return 0.5f * getAgentComponents().Length;
		}
	}

	public float BuyCost
	{
		get
		{
			float result = 0;
			foreach( AgentComponent comp in getAgentComponents() ){
				result += comp.BuyCost;
			}
			return result;
		}
	}
	#endregion

	public void setAction( ABAction action, IABType[] actionParams ){
		this.behaviour.CurAction = action;
		this.behaviour.CurActionParams = actionParams;
	}

	public AgentComponent[] getAgentComponents (){
		return this.GetComponentsInChildren<AgentComponent> ();
	}

    // Use this for initialization
    void Awake()
    {
        behaviour = this.GetComponent<AgentBehavior>();
        context = this.GetComponent<AgentContext>();
		context.Entity = this;
    }

    void Start()
    {
        ABManager.instance.RegisterAgent(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
