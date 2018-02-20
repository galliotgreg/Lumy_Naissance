using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using System.Runtime.InteropServices;
using System.Text;

public class MCEditorManager : MonoBehaviour {

    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static MCEditorManager instance = null;
    private int curStateId;
    private int curTransId;

    //Templates & Prefabs
    [SerializeField]
    private ProxyABState statePrefab;
    [SerializeField]
    private ProxyABTransition transitionPrefab;
    [SerializeField]
    private Pin pinPrefab;
    [SerializeField]
    private ProxyABOperator operatorPrefab;
    [SerializeField]
    private ProxyABParam parameterPrefab;
    [SerializeField]
    private ProxyABAction actionPrefab;
    [SerializeField]
    private GameObject[] mcTemplates;

    private ABModel abModel;

    // Save function utils
    private int idNodeSyntTree;
    private int idNodeInputPin;
    private ABOperatorFactory opeFactory = new ABOperatorFactory();
    private ABParamFactory paramFactory = new ABParamFactory();
    private Dictionary<string, string> operatorDictionary = new Dictionary<string, string>();
    private Dictionary<string, string> paramDictionary = new Dictionary<string, string>();


    private Dictionary<ABState,ProxyABState> statesDictionnary;    
    private Dictionary<ABState, ProxyABAction> actionsDictionnary;

    private List<ProxyABState> proxyStates;
    private List<ProxyABAction> proxyActions;
    private List<ProxyABTransition> proxyTransitions;
    private List<Pin> pins; //ProxyABGateOperator    
    private List<ProxyABOperator> proxyOperators;
    private List<ProxyABParam> proxyParams;

    [SerializeField]
    private string MC_OrigFilePath = "Assets/Inputs/Test/siu_scoot_behavior_LOAD_SAVE_TEST.csv";/* siu_scoot_behavior_LOAD_SAVE_TEST.csv"; /*ref_table_Test.txt"; /*siu_scoot_behavior_LOAD_TEST.csv";*/

    /** START TEST SAVE**/
    ProxyABAction abAction = null;
    ProxyABAction abAction2 = null;
    ProxyABState abState = null;
    ProxyABState abState2 = null;
    ProxyABParam abParam = null;
    ProxyABOperator aBOperator = null;
    ProxyABOperator aBOperator2 = null;

    /** END TEST SAVE**/

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

    public ABModel AbModel
    {
        get
        {
            return abModel;
        }
    }

    // Use this for initialization
    void Start()
    {
        /** INITIALISATION **/
        proxyStates = new List<ProxyABState>();
        proxyTransitions = new List<ProxyABTransition>();
        pins = new List<Pin>(); //ProxyABGateOperator
        proxyParams = new List<ProxyABParam>(); //ProxyABParam
        proxyOperators = new List<ProxyABOperator>();
        proxyActions = new List<ProxyABAction>();
        actionsDictionnary = new Dictionary<ABState, ProxyABAction>();
        statesDictionnary = new Dictionary<ABState, ProxyABState>();
        
        //usefull for save function
        opeFactory.CreateDictionnary();
        paramFactory.CreateDictionnary();

        operatorDictionary = opeFactory.TypeStringToString;
        paramDictionary = paramFactory.TypeStringToString;

        /** LOAD MODEL AND CREATE PROXY OBJECTS **/
        SetupModel();      
    }

    private void Update()
    {
		// adjust camera
		/*List<GameObject> proxies = new List<GameObject>();
		foreach (MonoBehaviour b in proxyStates) {
			proxies.Add (b.gameObject);
		}
		foreach (MonoBehaviour b in proxyActions) {
			proxies.Add (b.gameObject);
		}
        proxies.AddRange ( proxyParams );
        proxies.AddRange ( proxyOperators );
        // TODO create global list

		MC_Camera camera = Camera.main.GetComponent<MC_Camera>();
        if (camera != null) {
			camera.adjustCamera (proxies);
		}*/

        /**START TEST SAVE**/
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save_MC();
        } else if (Input.GetKeyDown(KeyCode.O))
        {
            CreateOperator();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            CreateParam();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            abState = Instantiate<ProxyABState>(statePrefab);
			proxyStates.Add (abState);
            abState2 = Instantiate<ProxyABState>(statePrefab);
			proxyStates.Add (abState2);
        } else if (Input.GetKeyDown(KeyCode.R))
        {
            CreateTransition(abState.GetComponentInChildren<Pin>(), abState2.GetComponentInChildren<Pin>());
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            abState = Instantiate<ProxyABState>(statePrefab);
            abAction = Instantiate<ProxyABAction>(actionPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            CreateTransition(abState.GetComponentInChildren<Pin>(), abAction.GetComponentInChildren<Pin>());
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            abParam = Instantiate<ProxyABParam>(parameterPrefab);
            aBOperator = Instantiate<ProxyABOperator>(operatorPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            CreateTransition(abParam.GetComponentInChildren<Pin>(), aBOperator.GetComponentInChildren<Pin>());
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            aBOperator = Instantiate<ProxyABOperator>(operatorPrefab);
            aBOperator2 = Instantiate<ProxyABOperator>(operatorPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            CreateTransition(aBOperator.GetComponentInChildren<Pin>(), aBOperator2.GetComponentInChildren<Pin>());
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            aBOperator = Instantiate<ProxyABOperator>(operatorPrefab);
            abAction = Instantiate<ProxyABAction>(actionPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            CreateTransition(aBOperator.GetComponentInChildren<Pin>(), abAction.GetComponentInChildren<Pin>());
        }

        /**END TEST SAVE**/
		/**Delete Transition**/
		else if (Input.GetKeyDown(KeyCode.D))
		{
			this.deleteSelectedTransition ();
		}
    }

    private void SetupModel()
    {
        abModel = LoadMC();
        LoadProxyStates();
        LoadProxyTransitions();
    }
    
    ABModel LoadMC()
    {
        ABModel model = new ABModel();

        /**** START TODO ****/
        //TODO : Récuperer le ABModel en Utilisant le AppContextManager et remplacer path
		model = ABManager.instance.LoadABModelFromFile(MC_OrigFilePath);
        /**** END TODO ****/

        return model;
    }

    /************** LOAD MODEL FUNCTIONS  **************/

    void LoadProxyStates() {

        ProxyABState proxyState;
        ProxyABAction proxyAction;
        foreach (ABState state in this.AbModel.States)
        {

            Pin start;
            if (state.Action != null)
            {
                proxyAction = Instantiate<ProxyABAction>(actionPrefab);
                proxyAction.IsLoaded = true;
                proxyAction.transform.position = new Vector3(proxyAction.transform.position.x + UnityEngine.Random.Range(-5, 5), proxyAction.transform.position.y + UnityEngine.Random.Range(-5, 5), proxyAction.transform.position.z);
                Text actionName = proxyAction.GetComponentInChildren<Text>();
                actionName.text = state.Name;                

                proxyActions.Add(proxyAction);
                actionsDictionnary.Add(state, proxyAction);

				if (state.Action.Parameters != null) {
					foreach (IABGateOperator param in state.Action.Parameters) {
						start = Instantiate<Pin> (pinPrefab);
						start.IsGateOperator = true;
						start.transform.parent = proxyAction.transform;
						start.transform.position = proxyAction.transform.position;
						float radius = proxyAction.transform.localScale.y / 2;
						start.transform.position = new Vector3 (start.transform.position.x, start.transform.position.y + radius, start.transform.position.z);
						pins.Add (start);                     
						foreach (ABNode node in param.Inputs) {
							Pin end = RecNodeSynthTree (node);
                            if (end != null)
                            {
                                CreateTransitionSyntaxTree (start, end);
                            }
                        }
					}
				}
            }
            else {
                proxyState = Instantiate<ProxyABState>(statePrefab);
                proxyState.IsLoaded = true;
                proxyState.transform.position = new Vector3(proxyState.transform.position.x + UnityEngine.Random.Range(-5, 5), proxyState.transform.position.y + UnityEngine.Random.Range(-5, 5), proxyState.transform.position.z);
                Text stateName = proxyState.GetComponentInChildren<Text>();
                stateName.text = state.Name;
                proxyState.GetComponent<ProxyABState>().AbState = state;

                proxyStates.Add(proxyState);
                statesDictionnary.Add(state, proxyState);
            }
            if (state.Outcomes.Count != 0)
            {                
            }
        }
    }

    //Load syntaxe Tree in a recursive way
    Pin RecNodeSynthTree(ABNode node)
    {
        ProxyABOperator ope = null;
        ProxyABParam param = null;

        Pin pin = null;
        if (node is IABOperator)
        {
            ope = Instantiate<ProxyABOperator>(operatorPrefab);
            ope.IsLoaded = true;
            ope.AbOperator = (IABOperator)node;
            ope.transform.position = new Vector3(ope.transform.position.x + UnityEngine.Random.Range(-5, 5), ope.transform.position.y + UnityEngine.Random.Range(-5, 5), ope.transform.position.z);
            SetNodeName(ope.gameObject, node);

            proxyOperators.Add(ope);
            if ( ((IABOperator)node).Inputs.Length != 0)
            {
                foreach(ABNode inputNode in ((IABOperator)node).Inputs)
                {
                    if(inputNode == null)
                    {
                        break;
                    }
                    Pin start = CreatePinSynthTree(ope.transform, true);
                    Pin end = RecNodeSynthTree(inputNode);

                    if(end != null)
                    {
                        CreateTransitionSyntaxTree(start, end);
                    }
                } 
            }
            pin = CreatePinSynthTree(ope.transform, true);
        }
        else if (node is IABParam)
        {
            param = Instantiate<ProxyABParam>(parameterPrefab);
            param.IsLoaded = true;
            param.AbParam = (IABParam)node;
            param.transform.position = new Vector3(param.transform.position.x + UnityEngine.Random.Range(-5, 5), param.transform.position.y + UnityEngine.Random.Range(-5, 5), param.transform.position.z);
            
            Text paramName = param.GetComponentInChildren<Text>();            
            paramName.text = GetParamValue(node);
            proxyParams.Add(param);

            pin = CreatePinSynthTree(param.transform, false);
        }
        return pin;
    }

    string GetParamType(ABNode node)
    {
        string type = "";        
        if (node is ABParam<ABText>)
        {
            type = ((ABParam<ABText>)node).Value.ToString();
        }
        else if (node is ABParam<ABVec>)
        {
            type = ((ABParam<ABVec>)node).Value.ToString();
        }
        else if (node is ABParam<ABBool>)
        {
            type = ((ABParam<ABBool>)node).Value.ToString();
        }
        else if (node is ABParam<ABRef>)
        {
            type = ((ABParam<ABRef>)node).Value.ToString();
        }
        else if (node is ABParam<ABColor>)
        {
            type = ((ABParam<ABColor>)node).Value.ToString();
        }
        else if (node is ABParam<ABScalar>)
        {
            type = ((ABParam<ABScalar>)node).Value.ToString();
        }

        else if (node is ABParam<ABTable<ABVec>>)
        {
            type = "ABTable<ABVec>";
            //type = ((ABParam<ABTable<ABVec>>)node).Value.ToString();
        }
        else if (node is ABParam<ABTable<ABBool>>)
        {
            type = "ABTable<ABBool>";
            //type = ((ABParam<ABTable<ABBool>>)node).Value.ToString();
        }
        else if (node is ABParam<ABTable<ABScalar>>)
        {
            type = "ABTable<ABScalar>";
            //type = ((ABParam<ABTable<ABScalar>>)node).Value.ToString();
        }
        else if (node is ABParam<ABTable<ABText>>)
        {
            type = "ABTable<ABText>";
            //type = ((ABParam<ABTable<ABText>>)node).Value.ToString();
        }
        else if (node is ABParam<ABTable<ABColor>>)
        {
            type = "ABTable<ABColor>";
            //type = ((ABParam<ABTable<ABColor>>)node).Value.ToString();
        }
        else if (node is ABParam<ABTable<ABRef>>)
        {
            type = "ABTable<ABRef>";
            //type = ((ABParam<ABTable<ABRef>>)node).Value.ToString();
        }

        return type;
    }

    string GetParamValue(ABNode node)
    {
        string text = "";
        if (node is ABParam<ABText>)
        {
            text = ((ABParam<ABText>)node).Value.Value.ToString();
        }
        else if (node is ABParam<ABVec>)
        {
            text = ((ABParam<ABVec>)node).Value.X.ToString() +";"+ ((ABParam<ABVec>)node).Value.Y.ToString();
        }
        else if (node is ABParam<ABBool>)
        {
            text = ((ABParam<ABBool>)node).Value.Value.ToString();
        }
        else if (node is ABParam<ABRef>)
        {
            text = ((ABParam<ABRef>)node).Value.ToString();
        }
        else if (node is ABParam<ABColor>)
        {
            text = ((ABParam<ABColor>)node).Value.Value.ToString();
        }
        else if (node is ABParam<ABScalar>)
        {
            text = ((ABParam<ABScalar>)node).Value.Value.ToString();
        }
        return text;
    }

    void LoadProxyTransitions()
    {
        ProxyABTransition proxyABTransition;

        List<Pin> pinList;
        Pin condition = null;
        for (int i = 0; i < AbModel.Transitions.Count; i++) {
            proxyABTransition = Instantiate<ProxyABTransition>(transitionPrefab);

            pinList = LoadPinsStates(i);

            proxyABTransition.StartPosition = pinList[0];
            proxyABTransition.EndPosition = pinList[1];
			proxyABTransition.Transition = AbModel.Transitions [i];

            CreatePinTransition(proxyABTransition);

            if (AbModel.Transitions[i].Condition != null) {                
                Pin end = RecNodeSynthTree(AbModel.Transitions[i].Condition.Inputs[0]);
                CreateTransitionSyntaxTree(proxyABTransition.Condition, end);                
            }
        }         
    }

    List<Pin> LoadPinsStates(int curTransition)
    {
        List<Pin> pinList = new List<Pin>();

        ProxyABState startState = statesDictionnary[AbModel.Transitions[curTransition].Start];        

		pinList.Add(CreatePinState(startState.AbState, startState.transform, false,true, curTransition));

        if (statesDictionnary.ContainsKey(AbModel.Transitions[curTransition].End)) {

            ProxyABState endState = statesDictionnary[AbModel.Transitions[curTransition].End];


			pinList.Add(CreatePinState(startState.AbState, endState.transform, false,false, curTransition));
        }
        else if (actionsDictionnary.ContainsKey(AbModel.Transitions[curTransition].End)) {

            ProxyABAction endState = actionsDictionnary[AbModel.Transitions[curTransition].End];

			pinList.Add(CreatePinState(startState.AbState, endState.transform, true,false));
        }
        
        return pinList;
    }

    /************** PROXY CREATION FUNCTIONS **************/

    // In a syntaxe tree a transition don't need a pin ( only for connect syntaxe tree to action or transition between state and action/state)
    void CreateTransitionSyntaxTree(Pin start, Pin end)
    {
        ProxyABTransition proxyABTransition = Instantiate<ProxyABTransition>(transitionPrefab);
        proxyABTransition.StartPosition = start;
        proxyABTransition.EndPosition = end;
    }

    Pin CreatePin(Transform transfo)
    {
        Pin pin;
        pin = Instantiate<Pin>(pinPrefab);
        pin.transform.parent = transfo;
        pin.transform.position = transfo.position;

        return pin;
    }

    //Create pin for connect a syntaxe tree on a transition
    void CreatePinTransition(ProxyABTransition proxyABTransition)
    {
        Pin condition = CreatePin(proxyABTransition.transform);
        condition.IsGateOperator = true;
        proxyABTransition.Condition = condition;
    }

	public Pin CreatePinState(ABState state, Transform state_transform,bool isAction,bool isStart, [Optional] int curTransition){
        Pin pin;
        pin = CreatePin(state_transform);
        float radiusState = state_transform.localScale.y / 2;
        Vector3 newPos;

        if (isAction) {
            pin.IsActionChild = true;
            newPos = new Vector3(pin.transform.position.x + (radiusState), pin.transform.position.y, pin.transform.position.z);
        }
        else {
            pin.IsActionChild = false;
            if (isStart) {
                newPos = new Vector3(pin.transform.position.x + (radiusState * Mathf.Cos(curTransition * (2 * Mathf.PI) / Math.Max(1,AbModel.Transitions[curTransition].Start.Outcomes.Count))), pin.transform.position.y + (radiusState * Mathf.Sin(curTransition * (2 * Mathf.PI) / Math.Max(1, AbModel.Transitions[curTransition].Start.Outcomes.Count))), pin.transform.position.z);
            }
            else {
                newPos = new Vector3(
					pin.transform.position.x + (radiusState * Mathf.Cos(curTransition * (2 * Mathf.PI) / Math.Max(1, state.Outcomes.Count))),
					pin.transform.position.y + (radiusState * Mathf.Sin(curTransition * (2 * Mathf.PI) / Math.Max(1, state.Outcomes.Count))),
					pin.transform.position.z);
            }
            
        }
        pin.transform.position = newPos;
        return pin;
    }

    public Pin CreatePinSynthTree(Transform node, bool isOperator)
    {
        Pin pin;
        pin = CreatePin(node);
        int childCount = node.transform.childCount;
        float radiusState = node.localScale.y / 2;
        Vector3 newPos;
        if (isOperator)
        {
            newPos = new Vector3(pin.transform.position.x + (radiusState * Mathf.Cos(childCount * (2 * Mathf.PI) / 4)), pin.transform.position.y + (radiusState * Mathf.Sin(childCount * (2 * Mathf.PI) / 4)), pin.transform.position.z);
            pin.IsOperatorChild = true;
        }
        else
        {
            newPos = new Vector3(pin.transform.position.x + (radiusState * Mathf.Cos(childCount * (2 * Mathf.PI) / 4)), pin.transform.position.y + (radiusState * Mathf.Sin(childCount * (2 * Mathf.PI) / 4)), pin.transform.position.z);
            pin.IsParamChild = true;
        }
        pin.transform.position = newPos;
        
        return pin;
    }

    /************** SAVE FUNCTION **************/

    void Save_Ope_Param(int idNodeInput, int idNodeInputPin, ABNode node, StringBuilder syntTreeContent)
    {
        int idParentnode = idNodeSyntTree;       
        if (idNodeSyntTree == 0)
        {
            if (node is IABOperator)
            {
                string type = operatorDictionary[((IABOperator)node).GetType().ToString()];

                syntTreeContent.AppendLine(idNodeSyntTree + ",operator{" + type + "},");
                idNodeSyntTree++;
                idNodeInputPin = 0;
                foreach (ABNode input in ((IABOperator)node).Inputs)
                {
                    /**Recursive function**/
                    Save_Ope_Param(idParentnode, idNodeInputPin, input, syntTreeContent);
                    idNodeInputPin++;
                }
            }
            else if (node is IABParam)
            {
                syntTreeContent.AppendLine(idNodeSyntTree + ",param{" + ((IABParam)node).Identifier + "},");
                idNodeSyntTree++;
            }
        }
        else
        {            
            if (node is IABOperator)
            {
                string type = "";
                if (!operatorDictionary.ContainsKey(((IABOperator)node).GetType().ToString()))
                {
                    Debug.LogError(((IABOperator)node).GetType().ToString() + " n'est pas dans la le dictionnaire des opérateurs. Vérifier l'orthographe Dans le fichier ABOperatorFactory");
                } else
                {
                    type = operatorDictionary[((IABOperator)node).GetType().ToString()];
                }
                syntTreeContent.AppendLine(idParentnode + ",operator{" + type + "}" + "," + idNodeInput + "->" + idNodeInputPin);
                idNodeSyntTree++;
                idNodeInputPin = 0;
                foreach (ABNode input in ((IABOperator)node).Inputs)
                {
                    /**Recursive function**/
                    Save_Ope_Param(idParentnode, idNodeInputPin, input, syntTreeContent);
                    idNodeInputPin++;
                }
            }
            else if (node is IABParam)
            {
                string value = GetParamValue(node);
                string type = "";


                if (!paramDictionary.ContainsKey(GetParamType(node)))
                {
                    Debug.LogError(GetParamType(node) + " n'est pas dans la le dictionnaire des parameters. Vérifier l'orthographe Dans le fichier ABParamFactory");
                } else
                {
                    type = paramDictionary[GetParamType(node)];
                }

                if (((IABParam)node).Identifier != "const")
                {
                    syntTreeContent.AppendLine(idNodeSyntTree + ",param{" + type + ":" + ((IABParam)node).Identifier + "}" + "," + idNodeInput + "->" + idNodeInputPin);
                }
                else
                {
                    syntTreeContent.AppendLine(idNodeSyntTree + ",param{" + ((IABParam)node).Identifier + " " + type + "=" + value + "}" + "," + idNodeInput + "->" + idNodeInputPin);
                    idNodeSyntTree++;
                }
            }
        }              
    }

    void Save_MC()
    {
        string csvpath = "Assets/Inputs/Test/siu_scoot_behavior_SAVE_LOAD_SAVE_TEST.csv";
        StringBuilder csvcontent = new StringBuilder();
        List<StringBuilder> syntTrees = new List<StringBuilder>();

        csvcontent.AppendLine("States,Name,Type");
        foreach (ABState state in abModel.States)
        {
            if(state.Action != null)
            {
                csvcontent.AppendLine(state.Id + "," + state.Name + "," + "trigger{"+state.Action.Type.ToString().ToLower()+"}");                
                if (state.Action.Parameters[0].Inputs[0] != null)
                {
                    StringBuilder syntTreeContent = new StringBuilder();
                    syntTreeContent.AppendLine("Syntax Tree,output,");
                    syntTreeContent.AppendLine("1,"+state.Name +"->0"+",");
                    syntTreeContent.AppendLine("Nodes,Type,output (Node -> Input)");

                    idNodeSyntTree = 0;
                    foreach(ABNode node in state.Action.Parameters[0].Inputs)
                    {
                        Save_Ope_Param(idNodeSyntTree, idNodeInputPin, node, syntTreeContent);
                    }
                    syntTreeContent.AppendLine(",,");
                    syntTrees.Add(syntTreeContent);
                }
            }
            else
            {
                if (state.Id == 0)
                {
                    csvcontent.AppendLine(state.Id + "," + state.Name + ",Init");
                } else
                {
                    csvcontent.AppendLine(state.Id + "," + state.Name + ",Inter");
                }                
            }               
        }
        csvcontent.AppendLine(",,");
        csvcontent.AppendLine("Transitions,Start State,End State");
        foreach(ABTransition trans in abModel.Transitions)
        {
            csvcontent.AppendLine(trans.Id + "," + trans.Start.Name + "," + trans.End.Name);
            if (trans.Condition != null)
            {
                if (trans.Condition.Inputs[0] != null)
                {
                    StringBuilder syntTreeContent = new StringBuilder();
                    syntTreeContent.AppendLine("Syntax Tree,output,");
                    syntTreeContent.AppendLine("1," + trans.Id + ",");
                    syntTreeContent.AppendLine("Nodes,Type,output (Node -> Input)");

                    idNodeSyntTree = 0;
                    foreach (ABNode node in trans.Condition.Inputs)
                    {
                        Save_Ope_Param(idNodeSyntTree, idNodeInputPin, node, syntTreeContent);
                    }
                    syntTreeContent.AppendLine(",,");
                    syntTrees.Add(syntTreeContent);
                }
            }
        }
        csvcontent.AppendLine(",,");
        File.Delete(csvpath);
        File.AppendAllText(csvpath, csvcontent.ToString());

        foreach (StringBuilder content in syntTrees)
        {
            File.AppendAllText(csvpath, content.ToString());
        }
        Debug.Log("Save MC");
    }

    /************** EDITOR FUNCTIONS **************/

    void CreateTransition(Pin start, Pin end)
    {
        ProxyABTransition trans = Instantiate<ProxyABTransition>(transitionPrefab);
        trans.StartPosition = start;
        trans.EndPosition = end;

        ProxyABAction startActionParent;
        ProxyABState startStateParent;
        ProxyABAction endActionParent;
        ProxyABState endStateParent;

        ProxyABOperator startOpeParent;
        ProxyABParam startParamParent;
        ProxyABParam endParamParent;
        ProxyABOperator endOpeParent;
        

		int transitionId = -1;
        if (start.IsActionChild)
        {
            startActionParent = start.GetComponentInParent<ProxyABAction>();

            if (end.IsOperatorChild)
            {
                endOpeParent = end.GetComponentInParent<ProxyABOperator>();
                start.IsGateOperator = true;
                startActionParent.AbState.Action.Parameters[0].Inputs[0] = (ABNode)endOpeParent.AbOperator;               
            } else if (end.IsParamChild)
            {
                endParamParent = end.GetComponentInParent<ProxyABParam>();
                start.IsGateOperator = true;
                startActionParent.AbState.Action.Parameters[0].Inputs[0] = (ABNode)endParamParent.AbParam;
            }
            else if (end.IsActionChild)
            {
                endActionParent = end.GetComponentInParent<ProxyABAction>();                
                AbModel.LinkStates(startActionParent.AbState.Name, endActionParent.AbState.Name);
                CreatePinTransition(trans);
				transitionId = AbModel.LinkStates(startActionParent.AbState.Name, endActionParent.AbState.Name);
            }
            else //State case
            {
                endStateParent = end.GetComponentInParent<ProxyABState>();
                AbModel.LinkStates(startActionParent.AbState.Name, endStateParent.AbState.Name);
                CreatePinTransition(trans);
				transitionId = AbModel.LinkStates(startActionParent.AbState.Name, endStateParent.AbState.Name);
            }

        }
        else if (start.IsOperatorChild)
        {
            startOpeParent = start.GetComponentInParent<ProxyABOperator>();
            if (end.IsOperatorChild)
            {
                endOpeParent = end.GetComponentInParent<ProxyABOperator>();
                start.IsGateOperator = true;
                startOpeParent.Inputs[startOpeParent.Inputs.Length-1] = (ABNode)endOpeParent.AbOperator;
                ((ABNode)endOpeParent.AbOperator).Output = (ABNode)startOpeParent.AbOperator;
            }
            else if (end.IsParamChild)
            {
                endParamParent = end.GetComponentInParent<ProxyABParam>();
                start.IsGateOperator = true;
                startOpeParent.Inputs[startOpeParent.Inputs.Length] = (ABNode)endParamParent.AbParam;
            }
            else if (end.IsActionChild)
            {
                endActionParent = end.GetComponentInParent<ProxyABAction>();
                end.IsGateOperator = true;
                endActionParent.AbState.Action.Parameters[0].Inputs[0] = (ABNode)startOpeParent.AbOperator;
            }
        }
        else if (start.IsParamChild)
        {
            startParamParent = start.GetComponentInParent<ProxyABParam>();
            if (end.IsOperatorChild)
            {
                endOpeParent = end.GetComponentInParent<ProxyABOperator>();
                start.IsGateOperator = true;
                endOpeParent.Inputs[endOpeParent.Inputs.Length-1] = (ABNode)startParamParent.AbParam;
                ((ABNode)startParamParent.AbParam).Output = (ABNode)endOpeParent.AbOperator;//TODO ma geule
            }
            else if (end.IsActionChild)
            {
                endActionParent = end.GetComponentInParent<ProxyABAction>();
                end.IsGateOperator = true;
                endActionParent.AbState.Action.Parameters[0].Inputs[0] = (ABNode)startParamParent.AbParam;
            }
        }
        else
        {
            startStateParent = start.GetComponentInParent<ProxyABState>();
            if (end.IsActionChild)
            {
                endActionParent = end.GetComponentInParent<ProxyABAction>();
				transitionId = AbModel.LinkStates(startStateParent.AbState.Name, endActionParent.AbState.Name);
            }
            else
            {
                endStateParent = end.GetComponentInParent<ProxyABState>();
				transitionId = AbModel.LinkStates(startStateParent.AbState.Name, endStateParent.AbState.Name);
            }
        }                                                               
    }

    ProxyABAction CreateAction()
    {
        ProxyABAction action = Instantiate<ProxyABAction>(actionPrefab);
            return action;
    }

    ProxyABOperator CreateOperator()
    {
        ProxyABOperator ope;
        ope = Instantiate<ProxyABOperator>(operatorPrefab);
        proxyOperators.Add(ope);
        return ope;
    }

    ProxyABParam CreateParam()
    {
        ProxyABParam param;
        param = Instantiate<ProxyABParam>(parameterPrefab);
        proxyParams.Add(param);
        return param;

		//trans.Transition = AbModel.getTransition( transitionId );

        Debug.Log(AbModel.Transitions.Count.ToString());
    }

    void Select()
    {

    }

	void DeleteTransition( ProxyABTransition transition )
    {
		if (transition != null) {

            // Transition between Action/State and Action/State
            if (transition.Condition != null)
            {
                if (!transition.StartPosition.IsOperatorChild || !transition.StartPosition.IsOperatorChild && !transition.EndPosition.IsOperatorChild || !transition.EndPosition.IsOperatorChild)
                {
                    AbModel.UnlinkStates(transition.Transition.Start.Name, transition.Transition.End.Name);
                }
            } else
            {
                RemoveTransitionSyntTree(transition);
            }
            // Unlink            
 		               
			// Remove Pin
			// Destroy( transition.Condition.gameObject );
			// Destroy Object
			Destroy (transition.gameObject);
		}
    }

    void Move()
    {

    }
     void Copy()
    {

    }

    void Cut()
    {

    }

    void Undo()
    {

    }

    void Redo()
    {

    }

    /************** REMOVE TRANSTION FUNCTIONS **************/

    private void RemoveTransitionSyntTree(ProxyABTransition transition)
    {
        Pin start = transition.StartPosition;
        Pin end = transition.EndPosition;

        ProxyABOperator proxyOpeStart = null;
        ProxyABOperator proxyOpeEnd = null;
        ProxyABParam proxyParam = null;

        if (start.IsOperatorChild)
        {
            proxyOpeStart = start.GetComponentInParent<ProxyABOperator>();
            if (end.IsParamChild)
            {
                proxyParam = end.GetComponentInParent<ProxyABParam>();
                UnlinkOperator_Param(proxyOpeStart, proxyParam);
            }
            else if (end.IsOperatorChild)
            {
                proxyOpeEnd = end.GetComponentInParent<ProxyABOperator>();
                UnlinkOperator_Operator(proxyOpeStart, proxyOpeEnd);
            }
        }
        else if (end.IsOperatorChild)
        {
            proxyOpeEnd = end.GetComponentInParent<ProxyABOperator>();
            if (start.IsParamChild)
            {
                proxyParam = start.GetComponentInParent<ProxyABParam>();
                UnlinkOperator_Param(proxyOpeStart, proxyParam);
            }
            else if (start.IsOperatorChild)
            {
                proxyOpeEnd = start.GetComponentInParent<ProxyABOperator>();
                UnlinkOperator_Operator(proxyOpeStart, proxyOpeEnd);
            }
        }        
    }

    private void UnlinkOperator_Operator(ProxyABOperator proxyOpeStart, ProxyABOperator proxyOpeEnd)
    {
        for (int i = 0; i < proxyOpeStart.Inputs.Length; i++)
        {
            if (proxyOpeStart.Inputs[i] == ((ABNode)(proxyOpeEnd.AbOperator)))
            {
                proxyOpeStart.Inputs[i] = null;
            }
        }
        for (int i = 0; i < proxyOpeEnd.Inputs.Length; i++)
        {
            if (proxyOpeEnd.Inputs[i] == ((ABNode)(proxyOpeStart.AbOperator)))
            {
                proxyOpeEnd.Inputs[i] = null;
            }
        }
    }

    private void UnlinkOperator_Param(ProxyABOperator proxyOpeStart, ProxyABParam proxyParam)
    {

        string idRemoveObject = proxyParam.AbParam.Identifier;
        for (int i = 0; i < proxyOpeStart.AbOperator.Inputs.Length; i++)
        {
            ABNode node = proxyOpeStart.AbOperator.Inputs[i];
            if(node != null)
            {
                if (((IABParam)(node)).Identifier == idRemoveObject)
                {
                    node.Output = null;
                    proxyOpeStart.AbOperator.Inputs[i] = null;
                }
            }            
        }
    }

    /************** DISPLAY FUNCTIONS **************/

    void DisplayStates()
    {
        for (int i = 0; i < proxyStates.Count; i++)
        {
            proxyStates[i].transform.position = new Vector3(proxyStates[i].transform.position.x + UnityEngine.Random.Range(-5, 5), proxyStates[i].transform.position.y + UnityEngine.Random.Range(-5, 5), proxyStates[i].transform.position.z);
        }
    }

    void DisplayActions()
    {
        for (int i = 0; i < proxyActions.Count; i++)
        {
            proxyActions[i].transform.position = new Vector3(proxyActions[i].transform.position.x + UnityEngine.Random.Range(-5, 5), proxyActions[i].transform.position.y + UnityEngine.Random.Range(-5, 5), proxyActions[i].transform.position.z);
        }
    }

    void DisplayOperators()
    {

        for (int i = 0; i < proxyOperators.Count; i++)
        {
            proxyOperators[i].transform.position = new Vector3(proxyOperators[i].transform.position.x + UnityEngine.Random.Range(-5, 5), proxyOperators[i].transform.position.y + UnityEngine.Random.Range(-5, 5), 0);
        }
    }

    void DisplayParameters()
    {
        for (int i = 0; i < proxyParams.Count; i++)
        {
            proxyParams[i].transform.position = new Vector3(proxyOperators[i].transform.position.x + UnityEngine.Random.Range(-5, 5), proxyParams[i].transform.position.y + UnityEngine.Random.Range(-5, 5), proxyParams[i].transform.position.z);
        }
    }

    public static void SetNodeName(GameObject proxy, ABNode node)
    {
        Text operatorName = proxy.GetComponentInChildren<Text>();
		operatorName.text = getNodeName( node );
    }

	public static string getNodeName( ABNode node ){
		string opeName = node.ToString();
		char splitter = '_';
		string[] newName = opeName.Split(splitter);
		string newOpeName = "";

		for (int i = 1; i < newName.Length - 1; i++)
		{
			newOpeName += newName[i];
		}

		return newOpeName;
	}

	#region Transition Create Delete
	Pin transition_Pin_Start = null;
	ProxyABTransition transition_Selected = null;

	public Pin Transition_Pin_Start {
		get {
			return transition_Pin_Start;
		}
	}
	public ProxyABTransition Transition_Selected {
		get {
			return transition_Selected;
		}
	}

	public void createTransition_setStartPin( Pin pin ){
		this.transition_Pin_Start = pin;
	}
	public void createTransition_setEndPin( Pin pin ){
		if (this.transition_Pin_Start != null && this.transition_Pin_Start != pin) {
			this.CreateTransition ( this.transition_Pin_Start, pin );
			this.transition_Pin_Start = null;
		}
	}

	// delete
	public void selectTransition( ProxyABTransition transition ){
		this.transition_Selected = transition;
	}
	void deleteSelectedTransition(){
		if (this.transition_Selected != null) {
			this.DeleteTransition (this.transition_Selected);
			this.transition_Selected = null;
		}
	}
	#endregion
}
