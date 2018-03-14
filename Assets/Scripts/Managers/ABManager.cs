using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class ABManager : MonoBehaviour
{
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static ABManager instance = null;

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

    //[SerializeField]
    private float fps = 1f;
    private float cooldown = -1f;
    [SerializeField]
    private string inputsFolderPath = "Inputs/";
    private int lastId = 0;

    private ABProcessor processor = new ABProcessor();
    private ABParser parser = new ABParser();

	private List<AgentEntity> agents = new List<AgentEntity>();
    private List<ABModel> models = new List<ABModel>();
    private List<ABInstance> instances = new List<ABInstance>();

	public AgentEntity Agent0 {
		get {
			return agents[0];
		}
	}

    /// <summary>
    /// If true, the agents's models will be evaluated.
    /// </summary>
    bool executeFrame = false;

	Dictionary<AgentEntity, Tracing> currentTracings;

    public string InputsFolderPath
    {
        get
        {
            return Application.dataPath + "/" + inputsFolderPath;
        }

        set
        {
            inputsFolderPath = value;
        }
    }

    // Use this for initialization
    void Start()
    {
		this.currentTracings = new Dictionary<AgentEntity, Tracing> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown < 0)
        {
            if (executeFrame)
            {
                Frame();
            }
            cooldown = 1f / fps;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }

    }

    private void Frame()
    {
        foreach (AgentEntity agent in agents)
        {
            try
            {
                // Compute Action
            	ABInstance instance = FindABInstance(agent.Id);
            	ABContext context = CreateABContextFromAgentContext(agent.Context);
				// Evaluating and Getting trace from evoluation
				Tracing tracing = processor.ProcessABInstance(instance, context);
				addCurrentTracing( agent, tracing );
				// Debug.Log( getCurrentTracing(agent).toString() );
				ABAction action = processor.actionFromTracing( instance, tracing );

                //Compute Action Parameters
                List<IABType> actionParams = new List<IABType>();

				try{
	                if (action != null && action.Parameters != null)
	                {
	                    for (int i = 0; i < action.Parameters.Length; i++)
	                    {
	                        if (action.Parameters[i] is AB_TxtGate_Operator)
	                        {
	                            IABType param =
									((AB_TxtGate_Operator)action.Parameters[i]).EvaluateOperator(context);
	                            actionParams.Add(param);
	                        }
	                        else if (action.Parameters[i] is AB_VecGate_Operator)
	                        {
	                            IABType param =
									((AB_VecGate_Operator)action.Parameters[i]).EvaluateOperator(context);
	                            actionParams.Add(param);
	                        }
	                        else if (action.Parameters[i] is AB_ColorGate_Operator)
	                        {
	                            IABType param =
									((AB_ColorGate_Operator)action.Parameters[i]).EvaluateOperator(context);
	                            actionParams.Add(param);
	                        }
	                        else if (action.Parameters[i] is AB_RefGate_Operator)
	                        {
	                            IABType param =
									((AB_RefGate_Operator)action.Parameters[i]).EvaluateOperator(context);
	                            actionParams.Add(param);
	                        }
	                        // TODO add type for each new param type
	                    }
	                }
				}
				// Exceptions on creatings params
				catch( SyntaxTree_MC_Exception syntaxEx ){
					throw new ActionParam_MC_Exception ( action, syntaxEx );
				}
				catch( System.Exception someEx ){
					throw new Action_Exception ( action, context, someEx.Message );
				}

                agent.setAction(action, actionParams.ToArray());
			}
			catch( SyntaxTree_MC_Exception syntaxEx ){
				MessagesManager.instance.LogMsg("Syntax Error : \n"+syntaxEx.getMessage());
			}
			catch (Exception e)
            {
                MessagesManager.instance.LogMsg("Something happened during your MC execution. You might miss something !! \nError is :\n" + e.Message);
            }
        }
    }

    /// <summary>
    /// Reset the ABManager.
    /// </summary>
    /// <param name="executeFrame">If set to <c>true</c>, enable frame function.</param>
    public void Reset(bool executeFrame)
    {
        this.executeFrame = executeFrame;

        // agents
        this.agents = new List<AgentEntity>();
        // models
        this.models = new List<ABModel>();
        // instances
        this.instances = new List<ABInstance>();
    }

    public ABInstance FindABInstance(int agentId)
    {
        foreach (ABInstance instance in instances)
        {
            if (instance.AgentId == agentId)
            {
                return instance;
            }
        }
        return null;
    }

    public ABModel FindABModel(string identifier)
    {
        foreach (ABModel model in models)
        {
            if (model.BehaviorModelIdentifier == identifier)
            {
                return model;
            }
        }
        return null;
    }

    /// <summary>
    /// Register the given agent in the system and provide him with a unique id
    /// </summary>
    /// <param name="agent"></param>
    public void RegisterAgent(AgentEntity agent)
    {
        agent.Id = lastId++;
        agents.Add(agent);
        ABModel model = FindABModel(agent.BehaviorModelIdentifier);
        if (model == null)
        {
            model = LoadABModelFromFile(GameManager.instance.IntputsFolderPath
                + GetInputSubFolder(agent)
                + agent.BehaviorModelIdentifier
                + ".csv");
            models.Add(model);
        }
        ABInstance instance = CreateABInstanceFromModel(model);
        instance.AgentId = agent.Id;
        instances.Add(instance);
    }

    /// <summary>
    /// Remove the given agent from the system
    /// </summary>
    /// <param name="agent"></param>
    public void UnregisterAgent(AgentEntity agent)
    {
        foreach (ABInstance instance in instances)
        {
            if (instance.AgentId == agent.Id)
            {
                instances.Remove(instance);
                break;
            }
        }
        agents.Remove(agent);
    }

    private static string GetInputSubFolder(AgentEntity agent)
    {
        string subFolder = GameManager.instance.PLAYER1_SPECIE_FOLDER;
        if (agent.Authority == PlayerAuthority.Player2)
        {
            subFolder = GameManager.instance.PLAYER2_SPECIE_FOLDER;
        }

        return subFolder;
    }

    public ABModel LoadABModelFromFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        List<string> lines = new List<string>();
        while (reader.Peek() >= 0)
        {
            lines.Add(reader.ReadLine());
        }

        return parser.Parse(lines);
    }

    private ABInstance CreateABInstanceFromModel(ABModel model)
    {
        ABInstance instance = new ABInstance();
        instance.Model = model;

        return instance;
    }

    public ABContext CreateABContextFromAgentContext(AgentContext agentContext)
    {
        ABContext context = new ABContext();
        FieldInfo[] fields = agentContext.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            object[] customAttrs = field.GetCustomAttributes(typeof(BindParam), false);
            if (customAttrs.Length > 0)
            {
                BindParam bindParamAttr = (BindParam)customAttrs[0];
                IABParam param = CreateABParam(bindParamAttr.Identifier, field, agentContext);
                context.SetParam(param);
            }
        }

        return context;
    }

    private IABParam CreateABParam(string identifier, FieldInfo field, AgentContext agentContext)
    {
        IABParam param = null;
        if (field.FieldType == typeof(bool))
        {
            bool value = (bool)field.GetValue(agentContext);
            param = ABParamFactory.CreateBoolParam(identifier, value);
        }
        else if (field.FieldType == typeof(byte))
        {
            byte value = (byte)field.GetValue(agentContext);
            param = ABParamFactory.CreateScalarParam(identifier, value);
        }
        else if (field.FieldType == typeof(short))
        {
            short value = (short)field.GetValue(agentContext);
            param = ABParamFactory.CreateScalarParam(identifier, value);
        }

        else if (field.FieldType == typeof(int))
        {
            int value = (int)field.GetValue(agentContext);
            param = ABParamFactory.CreateScalarParam(identifier, value);
        }
        else if (field.FieldType == typeof(long))
        {
            long value = (long)field.GetValue(agentContext);
            param = ABParamFactory.CreateScalarParam(identifier, value);
        }
        else if (field.FieldType == typeof(float))
        {
            float value = (float)field.GetValue(agentContext);
            param = ABParamFactory.CreateScalarParam(identifier, value);
        }
        else if (field.FieldType == typeof(double))
        {
            double value = (double)field.GetValue(agentContext);
            param = ABParamFactory.CreateScalarParam(identifier, (float)value);
        }
        else if (field.FieldType == typeof(string))
        {
            String value = (string)field.GetValue(agentContext);
            param = ABParamFactory.CreateTextParam(identifier, value);
        }
        else if (field.FieldType == typeof(Vector2))
        {
            Vector2 vetor2 = (Vector2)field.GetValue(agentContext);
            param = ABParamFactory.CreateVecParam(identifier, vetor2.x, vetor2.y);
        }
        else if (field.FieldType == typeof(Color32))
        {
            Color32 color = (Color32)field.GetValue(agentContext);
            param = ABParamFactory.CreateColorParam(identifier, color.r, color.g, color.b);
        }
        else if (field.FieldType == typeof(GameObject))
        {
            GameObject gameObject = (GameObject)field.GetValue(agentContext);
            if (gameObject != null)
            {
                MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
                param = ABParamFactory.CreateRefParam(identifier, scripts);
            }
            else
            {
                param = ABParamFactory.CreateRefParam(identifier, (MonoBehaviour[])null);
            }
        }
        else if (field.FieldType == typeof(bool[]))
        {
            bool[] values = (bool[])field.GetValue(agentContext);
            param = ABParamFactory.CreateBoolTableParam(identifier, values);
        }
        else if (field.FieldType == typeof(byte[]))
        {
            byte[] values = (byte[])field.GetValue(agentContext);
            float[] castedValues = new float[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                castedValues[i] = values[i];
            }
            param = ABParamFactory.CreateScalarTableParam(identifier, castedValues);
        }
        else if (field.FieldType == typeof(short[]))
        {
            short[] values = (short[])field.GetValue(agentContext);
            float[] castedValues = new float[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                castedValues[i] = values[i];
            }
            param = ABParamFactory.CreateScalarTableParam(identifier, castedValues);
        }

        else if (field.FieldType == typeof(int[]))
        {
            int[] values = (int[])field.GetValue(agentContext);
            float[] castedValues = new float[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                castedValues[i] = values[i];
            }
            param = ABParamFactory.CreateScalarTableParam(identifier, castedValues);
        }
        else if (field.FieldType == typeof(long[]))
        {
            long[] values = (long[])field.GetValue(agentContext);
            float[] castedValues = new float[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                castedValues[i] = values[i];
            }
            param = ABParamFactory.CreateScalarTableParam(identifier, castedValues);
        }
        else if (field.FieldType == typeof(float[]))
        {
            float[] values = (float[])field.GetValue(agentContext);
            param = ABParamFactory.CreateScalarTableParam(identifier, values);
        }
        else if (field.FieldType == typeof(double[]))
        {
            double[] values = (double[])field.GetValue(agentContext);
            float[] castedValues = new float[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                castedValues[i] = (float)values[i];
            }
            param = ABParamFactory.CreateScalarTableParam(identifier, castedValues);
        }
        else if (field.FieldType == typeof(string[]))
        {
            string[] values = (string[])field.GetValue(agentContext);
            param = ABParamFactory.CreateTextTableParam(identifier, values);
        }
        else if (field.FieldType == typeof(Vector2[]))
        {
            Vector2[] values = (Vector2[])field.GetValue(agentContext);
            float[] xValues = new float[values.Length];
            float[] yValues = new float[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                xValues[i] = values[i].x;
                yValues[i] = values[i].y;
            }
            param = ABParamFactory.CreateVecTableParam(identifier, xValues, yValues);
        }
        else if (field.FieldType == typeof(Color32[]))
        {
            Color32[] values = (Color32[])field.GetValue(agentContext);
            int[] rValues = new int[values.Length];
            int[] gValues = new int[values.Length];
            int[] bValues = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                rValues[i] = values[i].r;
                gValues[i] = values[i].g;
                bValues[i] = values[i].b;
            }
            param = ABParamFactory.CreateColorTableParam(identifier, rValues, gValues, bValues);
        }
        else if (field.FieldType == typeof(GameObject[]))
        {
            GameObject[] values = (GameObject[])field.GetValue(agentContext);
            MonoBehaviour[][] scripts = new MonoBehaviour[values.Length][];
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != null)
                {
                    scripts[i] = values[i].GetComponents<MonoBehaviour>();
                }
            }
            param = ABParamFactory.CreateRefTableParam(identifier, scripts);
        }
        else
        {
            throw new NotSupportedException(field.FieldType + " are not supported");
        }

        return param;
    }

	#region Current Tracings
	public void addCurrentTracing( AgentEntity entity, Tracing tracing ){
		if (currentTracings.ContainsKey (entity)) {
			currentTracings [entity] = tracing;
		} else {
			currentTracings.Add (entity, tracing);
		}
	}
	public Tracing getCurrentTracing( AgentEntity entity ){
		if (currentTracings.ContainsKey (entity)) {
			return currentTracings [entity];
		}
		return null;
	}
	#endregion
}
