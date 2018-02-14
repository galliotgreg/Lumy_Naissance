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
    private GameObject operatorPrefab;
    [SerializeField]
    private GameObject parameterPrefab;
    [SerializeField]
    private ProxyABAction actionPrefab;
    [SerializeField]
    private GameObject[] mcTemplates;

    private ABModel abModel;

    private Dictionary<ABState,ProxyABState> statesDictionnary;    
    private Dictionary<ABState, ProxyABAction> actionsDictionnary;

    private List<ProxyABState> proxyStates;
    private List<ProxyABAction> proxyActions;
    private List<ProxyABTransition> proxyTransitions;
    private List<Pin> pins; //ProxyABGateOperator    
    private List<GameObject> proxyOperator;
    private List<GameObject> proxyParam;

    /** START TEST SAVE**/
    ProxyABAction abAction = null;
    ProxyABAction abAction2 = null;
    ProxyABState abState = null;
    ProxyABState abState2 = null;
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
        proxyParam = new List<GameObject>(); //ProxyABParam
        proxyOperator = new List<GameObject>();
        proxyActions = new List<ProxyABAction>();
        actionsDictionnary = new Dictionary<ABState, ProxyABAction>();
        statesDictionnary = new Dictionary<ABState, ProxyABState>();

        /** LOAD MODEL AND CREATE PROXY OBJECTS **/
        SetupModel();      
    }

    private void Update()
    {
        /**START TEST SAVE**/
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save_MC();
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            abAction = Instantiate<ProxyABAction>(actionPrefab);
            abAction2 = Instantiate<ProxyABAction>(actionPrefab);
        }else if (Input.GetKeyDown(KeyCode.Z))
        {
            CreateTransition(abAction.GetComponentInChildren<Pin>(), abAction2.GetComponentInChildren<Pin>());
        }else if (Input.GetKeyDown(KeyCode.E))
        {
            abState = Instantiate<ProxyABState>(statePrefab);
            abState2 = Instantiate<ProxyABState>(statePrefab);
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
        /**END TEST SAVE**/
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
        model = ABManager.instance.LoadABModelFromFile("Assets/Inputs/Test/siu_scoot_behavior_TEST.csv");
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

                foreach (IABGateOperator param in state.Action.Parameters) {
                    start = Instantiate<Pin>(pinPrefab);
                    start.IsGateOperator = true;
                    start.transform.parent = proxyAction.transform;
                    start.transform.position = proxyAction.transform.position;
                    float radius = proxyAction.transform.localScale.y / 2;
                    start.transform.position = new Vector3(start.transform.position.x, start.transform.position.y + radius, start.transform.position.z);
                    pins.Add(start);                     
                    foreach(ABNode node in param.Inputs)
                    {
                        Pin end = RecNodeSynthTree(node);
                        CreateTransitionSyntaxTree(start, end);
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
        GameObject proxy = null;
        Pin pin = null;
        if (node is IABOperator)
        {
            proxy = Instantiate<GameObject>(operatorPrefab);
            proxy.transform.position = new Vector3(proxy.transform.position.x + UnityEngine.Random.Range(-5, 5), proxy.transform.position.y + UnityEngine.Random.Range(-5, 5), proxy.transform.position.z);
            SetNodeName(proxy, node);

            proxyOperator.Add(proxy);
            if ( ((IABOperator)node).Inputs.Length != 0)
            {
                foreach(ABNode inputNode in ((IABOperator)node).Inputs)
                {
                    if(inputNode == null)
                    {
                        break;
                    }
                    Pin start = CreatePinSynthTree(proxy.transform, true);
                    Pin end = RecNodeSynthTree(inputNode);

                    if(end != null)
                    {
                        CreateTransitionSyntaxTree(start, end);
                    }
                } 
            }
        }
        else if (node is IABParam)
        {
            proxy = Instantiate<GameObject>(parameterPrefab);
            proxy.transform.position = new Vector3(proxy.transform.position.x + UnityEngine.Random.Range(-5, 5), proxy.transform.position.y + UnityEngine.Random.Range(-5, 5), proxy.transform.position.z);
            
            Text paramName = proxy.GetComponentInChildren<Text>();            
            if(node is ABParam<ABText>) {
                paramName.text = ((ABParam<ABText>)node).Value.ToString();
            }else if (node is ABParam<ABVec>) {
                paramName.text = ((ABParam<ABVec>)node).Value.ToString();

            }
            else if(node is ABParam<ABBool>) {
                paramName.text = ((ABParam<ABBool>)node).Value.ToString();
            }
            else if(node is ABParam<ABRef>) {
                paramName.text = ((ABParam<ABRef>)node).Value.ToString();
            }
            else if(node is ABParam<ABColor>) {
                paramName.text = ((ABParam<ABColor>)node).Value.ToString();

            }
            else if(node is ABParam<ABScalar>) {
                paramName.text = ((ABParam<ABScalar>)node).Value.ToString();
            }

            proxyParam.Add(proxy);

            }
        pin = CreatePinSynthTree(proxy.transform, true);
        return pin;
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

        pinList.Add(CreatePinState(startState.transform, false,true, curTransition));

        if (statesDictionnary.ContainsKey(AbModel.Transitions[curTransition].End)) {

            ProxyABState endState = statesDictionnary[AbModel.Transitions[curTransition].End];


            pinList.Add(CreatePinState(endState.transform, false,false, curTransition));
        }
        else if (actionsDictionnary.ContainsKey(AbModel.Transitions[curTransition].End)) {

            ProxyABAction endState = actionsDictionnary[AbModel.Transitions[curTransition].End];

            pinList.Add(CreatePinState(endState.transform, true,false));
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

    //Create pin for connect a syntaxe tree on a transition
    void CreatePinTransition(ProxyABTransition proxyABTransition)
    {
        Pin condition = Instantiate<Pin>(pinPrefab);
        condition.IsGateOperator = true;
        condition.transform.parent = proxyABTransition.transform;
        proxyABTransition.Condition = condition;
    }

    public Pin CreatePinState(Transform state,bool isAction,bool isStart, [Optional] int curTransition){
        Pin pin;
        pin = Instantiate<Pin>(pinPrefab);
        pin.transform.parent = state;
        pin.transform.position = state.position;
        float radiusState = state.localScale.y / 2;
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
                newPos = new Vector3(pin.transform.position.x + (radiusState * Mathf.Cos(curTransition * (2 * Mathf.PI) / Math.Max(1,AbModel.Transitions[curTransition].End.Outcomes.Count))), pin.transform.position.y + (radiusState * Mathf.Sin(curTransition * (2 * Mathf.PI) / Math.Max(1, AbModel.Transitions[curTransition].End.Outcomes.Count))), pin.transform.position.z);
            }
            
        }
        pin.transform.position = newPos;
        return pin;
    }

    Pin CreatePinSynthTree(Transform node, bool isOperator)
    {
        Pin pin;
        pin = Instantiate<Pin>(pinPrefab);
        pin.transform.parent = node;
        pin.transform.position = node.position;
        int childCount = node.transform.childCount;
        float radiusState = node.localScale.y / 2;
        Vector3 newPos;
        if (isOperator)
        {
            newPos = new Vector3(pin.transform.position.x + (radiusState * Mathf.Cos(childCount * (2 * Mathf.PI) / 4)), pin.transform.position.y + (radiusState * Mathf.Sin(childCount * (2 * Mathf.PI) / 4)), pin.transform.position.z);
        }
        else
        {
            newPos = new Vector3(pin.transform.position.x + (radiusState * Mathf.Cos(childCount * (2 * Mathf.PI) / 4)), pin.transform.position.y + (radiusState * Mathf.Sin(childCount * (2 * Mathf.PI) / 4)), pin.transform.position.z);            
        }
        pin.transform.position = newPos;
        return pin;
    }

    /************** SAVE FUNCTION **************/

    void Save_MC()
    {
        string csvpath = "Assets/Inputs/Test/siu_scoot_behavior_SAVE_TEST.csv";
        StringBuilder csvcontent = new StringBuilder();
        csvcontent.AppendLine("States,Name,Type");
        foreach (ABState state in abModel.States)
        {
            if(state.Action != null)
            {
                csvcontent.AppendLine(state.Id + "," + state.Name + "," + "trigger{"+state.Action.Type.ToString().ToLower()+"}");
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
        }

        File.Delete(csvpath);
        File.AppendAllText(csvpath, csvcontent.ToString());

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

        if (start.IsActionChild)
        {
            startActionParent = start.GetComponentInParent<ProxyABAction>();
            if (end.IsActionChild)
            {
                endActionParent = end.GetComponentInParent<ProxyABAction>();                
                AbModel.LinkStates(startActionParent.AbState.Name, endActionParent.AbState.Name);
            }
            else
            {
                endStateParent = end.GetComponentInParent<ProxyABState>();
                AbModel.LinkStates(startActionParent.AbState.Name, endStateParent.AbState.Name);
            }

        } else
        {
            startStateParent = start.GetComponentInParent<ProxyABState>();
            if (end.IsActionChild)
            {
                endActionParent = end.GetComponentInParent<ProxyABAction>();
                AbModel.LinkStates(startStateParent.AbState.Name, endActionParent.AbState.Name);
            }
            else
            {
                endStateParent = end.GetComponentInParent<ProxyABState>();
                AbModel.LinkStates(startStateParent.AbState.Name, endStateParent.AbState.Name);
            }
        }                                               
        CreatePinTransition(trans);

        Debug.Log(AbModel.Transitions.Count.ToString());
    }

    void Select()
    {

    }

    void DeleteTransition()
    {

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

        for (int i = 0; i < proxyOperator.Count; i++)
        {
            proxyOperator[i].transform.position = new Vector3(proxyOperator[i].transform.position.x + UnityEngine.Random.Range(-5, 5), proxyOperator[i].transform.position.y + UnityEngine.Random.Range(-5, 5), 0);
        }
    }

    void DisplayParameters()
    {
        for (int i = 0; i < proxyParam.Count; i++)
        {
            proxyParam[i].transform.position = new Vector3(proxyOperator[i].transform.position.x + UnityEngine.Random.Range(-5, 5), proxyParam[i].transform.position.y + UnityEngine.Random.Range(-5, 5), proxyParam[i].transform.position.z);
        }
    }

    void SetNodeName(GameObject proxy, ABNode node)
    {

        Text operatorName = proxy.GetComponentInChildren<Text>();
        string opeName = node.ToString();
        char splitter = '_';
        string[] newName = opeName.Split(splitter);
        string newOpeName = "";

        for (int i = 1; i < newName.Length - 1; i++)
        {

            newOpeName += newName[i];
        }
        operatorName.text = newOpeName;
    }
}
