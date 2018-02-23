using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentEntity : MonoBehaviour
{
    [SerializeField]
    private int id;

	private HomeScript home;
    private GameParamsScript gameParams;

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

    public GameParamsScript GameParams {
		get {
			return gameParams;
		}
        set {
            gameParams = value;

            this.context.GameParams = value.gameObject;

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
	#endregion

	public void setAction( ABAction action, IABType[] actionParams ){
		this.behaviour.CurAction = action;
		this.behaviour.CurActionParams = actionParams;
	}

	public AgentComponent[] getAgentComponents (){
        GameObject head = transform.Find("Head").gameObject;
        GameObject tail = transform.Find("Tail").gameObject;
        AgentComponent[] headCompos = head.GetComponentsInChildren<AgentComponent>();
        AgentComponent[] tailCompos = tail.GetComponentsInChildren<AgentComponent>();
        AgentComponent[] agentCompos = new AgentComponent[headCompos.Length + tailCompos.Length];
        int i = 0;
        foreach (AgentComponent compo in headCompos)
        {
            agentCompos[i++] = compo;
        }
        foreach (AgentComponent compo in tailCompos)
        {
            agentCompos[i++] = compo;
        }

        return agentCompos;
    }

	public List<ActionType> getAgentActions (){
		List<ActionType> result = new List<ActionType> ();

		foreach (AgentComponent comp in this.getAgentComponents()) {
			if (comp.IsAction) {
				if (comp.EnableGotoHold) {
					if (!result.Contains (ActionType.Goto)) {
						result.Add (ActionType.Goto);
					}
				}
				if (comp.EnableLay) {
					if (!result.Contains (ActionType.Lay)) {
						result.Add (ActionType.Lay);
					}
				}
				if (comp.EnablePickDrop) {
					if (!result.Contains (ActionType.Pick)) {
						result.Add (ActionType.Pick);
					}
					if (!result.Contains (ActionType.Drop)) {
						result.Add (ActionType.Drop);
					}
				}
				if (comp.EnableStrike) {
					if (!result.Contains (ActionType.Strike)) {
						result.Add (ActionType.Strike);
					}
				}
			}
		}

		return result;
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
        if (ABManager.instance != null)
        {
            ABManager.instance.RegisterAgent(this);
        }
        else
        {
            Debug.LogWarning("WARNING ! : ABManager not found");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

	void OnDestroy(){
        if (ABManager.instance != null)
        {
            ABManager.instance.UnregisterAgent(this);
        }
        else
        {
            Debug.LogWarning("WARNING ! : ABManager not found");
        }
    }
}
