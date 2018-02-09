using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using System.Runtime.InteropServices;
public class MCEditorManager : MonoBehaviour {

    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static MCEditorManager instance = null;

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
    //private Dictionary<ABAction, ProxyABAction> actionsDictionnary;
    private Dictionary<ABState, ProxyABAction> actionsDictionnary;

    private List<ProxyABState> proxyStates;
    private List<ProxyABAction> proxyActions;
    private List<ProxyABTransition> proxyTransitions;
    private List<Pin> pins; //ProxyABGateOperator
    //private List<IProxyABParam> proxyParam; //ProxyABParam
    //private List<IProxyABOperator> proxyOperator;//ProxyABOperator
    private List<GameObject> proxyOperator;
    private List<GameObject> proxyParam;


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


    private ProxyABModel proxyABModel;

    // Use this for initialization
    // Use this for initialization
    void Start()
    {
        proxyStates = new List<ProxyABState>();
        proxyTransitions = new List<ProxyABTransition>();
        pins = new List<Pin>(); //ProxyABGateOperator
        //proxyParam = new List<IProxyABParam>(); //ProxyABParam
        proxyParam = new List<GameObject>(); //ProxyABParam
        //proxyOperator = new List<IProxyABOperator>();//ProxyABOperator
        proxyOperator = new List<GameObject>();
        proxyActions = new List<ProxyABAction>();
        //actionsDictionnary = new Dictionary<ABAction, ProxyABAction>();
        actionsDictionnary = new Dictionary<ABState, ProxyABAction>();
        statesDictionnary = new Dictionary<ABState, ProxyABState>();
        SetupModel();
    }

    private void SetupModel()
    {
        abModel = LoadMC();
        CreateProxyStates();
 
        //DisplayStates();
        //DisplayOperators();
        //DisplayParameters();
        CreateProxyTransitions();
    }

    ABModel LoadMC()
    {
        ABModel model = new ABModel();
        //TODO : Récuperer le ABModel en Utilisant le AppContextManager et remplacer path
        model = ABManager.instance.LoadABModelFromFile("Assets/Inputs/Test/siu_scoot_behavior_TEST.csv");
        return model;
    }

    void CreateProxyStates() {

        ProxyABState proxyState;
        ProxyABAction proxyAction;
        foreach (ABState state in this.abModel.States)
        {

            Pin start;
            if (state.Action != null)
            {
                proxyAction = Instantiate<ProxyABAction>(actionPrefab);
                proxyAction.transform.position = new Vector3(proxyAction.transform.position.x + UnityEngine.Random.Range(-5, 5), proxyAction.transform.position.y + UnityEngine.Random.Range(-5, 5), proxyAction.transform.position.z);
                Text actionName = proxyAction.GetComponentInChildren<Text>();
                actionName.text = state.Name;
                proxyAction.GetComponent<ProxyABAction>().AbAction = state.Action;

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
                    //DeploySyntaxeTree(param.Inputs);
                    foreach(ABNode node in param.Inputs)
                    {
                        Pin end = RecNodeSynthTree(node);
                        CreateTransitionSyntaxTree(start, end);
                    }
                }
            }
            else {
                proxyState = Instantiate<ProxyABState>(statePrefab);
                proxyState.transform.position = new Vector3(proxyState.transform.position.x + UnityEngine.Random.Range(-5, 5), proxyState.transform.position.y + UnityEngine.Random.Range(-5, 5), proxyState.transform.position.z);
                Text stateName = proxyState.GetComponentInChildren<Text>();
                stateName.text = state.Name;
                proxyState.GetComponent<ProxyABState>().AbState = state;

                proxyStates.Add(proxyState);
                statesDictionnary.Add(state, proxyState);


            }
            if (state.Outcomes.Count != 0)
            {
                //CreatePins(state.Outcomes);
            }
        }
    }

    void SetNodeName(GameObject proxy, ABNode node) {

        Text operatorName = proxy.GetComponentInChildren<Text>();
        string opeName = node.ToString();
        char splitter = '_';
        string[] newName = opeName.Split(splitter);
        string newOpeName = "";

        for (int i = 1; i < newName.Length - 1; i++) {

            newOpeName += newName[i];
        }
        operatorName.text = newOpeName;
    }

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

            //TODO value parameter
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
            

                /*pin = CreatePinSynthTree(proxy.transform, true);
                Renderer rend = pin.GetComponent<Renderer>();
                rend.material.shader = Shader.Find("Specular");
                rend.material.SetColor("_SpecColor", Color.red);*/
            }
        pin = CreatePinSynthTree(proxy.transform, true);
        return pin;
    }


    void DeploySyntaxeTree(ABNode[] nodes)
    {
        foreach (ABNode node in nodes)
        {
            if(node is IABOperator)
            {
                GameObject ope = Instantiate<GameObject>(operatorPrefab);
                Text operatorName = ope.GetComponentInChildren<Text>();
                operatorName.text = node.Output.ToString();
                proxyOperator.Add(ope);
               
                DeploySyntaxeTree(((IABOperator)node).Inputs);

            } else if (node is IABParam)
            {
                GameObject param = Instantiate<GameObject>(parameterPrefab);
                Text paramName = param.GetComponentInChildren<Text>();
                paramName.text = node.GetType().ToString();
                proxyParam.Add(param);

            }                    
        }
    }

    void CreateTransitionSyntaxTree(Pin start, Pin end) {
        ProxyABTransition proxyABTransition = Instantiate<ProxyABTransition>(transitionPrefab);
        proxyABTransition.StartPosition = start;
        proxyABTransition.EndPosition = end;
    }

    void CreateProxyTransitions()
    {
        ProxyABTransition proxyABTransition;

        List<Pin> pinList;
        Pin condition = null;
        for (int i = 0; i < abModel.Transitions.Count; i++) {
            proxyABTransition = Instantiate<ProxyABTransition>(transitionPrefab);

            pinList = CreatePinsStates(i);

            proxyABTransition.StartPosition = pinList[0];
            proxyABTransition.EndPosition = pinList[1];

            condition = Instantiate<Pin>(pinPrefab);
            condition.IsGateOperator = true;
            condition.transform.parent = proxyABTransition.transform;
            proxyABTransition.Condition = condition;

            if(abModel.Transitions[i].Condition != null) {
                
                Pin end = RecNodeSynthTree(abModel.Transitions[i].Condition.Inputs[0]);
                CreateTransitionSyntaxTree(condition, end);
                
            }
        }         
    }

    List<Pin> CreatePinsStates(int curTransition)
    {
        List<Pin> pinList = new List<Pin>();

        ProxyABState startState = statesDictionnary[abModel.Transitions[curTransition].Start];

        //pins.Add(CreatePin(startState.transform,false,curTransition)); //TODO peut etre a jeter

        pinList.Add(CreatePinState(startState.transform, false,true, curTransition));

        if (statesDictionnary.ContainsKey(abModel.Transitions[curTransition].End)) {

            ProxyABState endState = statesDictionnary[abModel.Transitions[curTransition].End];


            pinList.Add(CreatePinState(endState.transform, false,false, curTransition));
        }
        else if (actionsDictionnary.ContainsKey(abModel.Transitions[curTransition].End)) {

            ProxyABAction endState = actionsDictionnary[abModel.Transitions[curTransition].End];

            pinList.Add(CreatePinState(endState.transform, true,false));
        }
        
        return pinList;
    }
    
    Pin CreatePinState(Transform state,bool isAction,bool isStart, [Optional] int curTransition){
        Pin pin;
        pin = Instantiate<Pin>(pinPrefab);
        pin.transform.parent = state;
        pin.transform.position = state.position;
        float radiusState = state.localScale.y / 2;
        Vector3 newPos;
        if (isAction) {
            newPos = new Vector3(pin.transform.position.x + (radiusState), pin.transform.position.y, pin.transform.position.z);
        }
        else {
            if (isStart) {
                newPos = new Vector3(pin.transform.position.x + (radiusState * Mathf.Cos(curTransition * (2 * Mathf.PI) / abModel.Transitions[curTransition].Start.Outcomes.Count)), pin.transform.position.y + (radiusState * Mathf.Sin(curTransition * (2 * Mathf.PI) / abModel.Transitions[curTransition].Start.Outcomes.Count)), pin.transform.position.z);
            }
            else {
                newPos = new Vector3(pin.transform.position.x + (radiusState * Mathf.Cos(curTransition * (2 * Mathf.PI) / abModel.Transitions[curTransition].End.Outcomes.Count)), pin.transform.position.y + (radiusState * Mathf.Sin(curTransition * (2 * Mathf.PI) / abModel.Transitions[curTransition].End.Outcomes.Count)), pin.transform.position.z);
            }
            
        }
        pin.transform.position = newPos;
        return pin;
    }

    Pin CreatePinSynthTree(Transform node, bool isOperator)
    {

        //TODO positionner les pins correctement
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
            //newPos = new Vector3(pin.transform.position.x + (radiusState), pin.transform.position.y, pin.transform.position.z);
        }
        else
        {
            newPos = new Vector3(pin.transform.position.x + (radiusState * Mathf.Cos(childCount * (2 * Mathf.PI) / 4)), pin.transform.position.y + (radiusState * Mathf.Sin(childCount * (2 * Mathf.PI) / 4)), pin.transform.position.z);
            //newPos = new Vector3(pin.transform.position.x + (radiusState), pin.transform.position.y, pin.transform.position.z);
        }
        pin.transform.position = newPos;
        return pin;
    }

    void DisplayStates() {
        for(int i=0; i< proxyStates.Count; i++) {
            proxyStates[i].transform.position = new Vector3(proxyStates[i].transform.position.x + UnityEngine.Random.Range(-5, 5), proxyStates[i].transform.position.y+ UnityEngine.Random.Range(-5,5), proxyStates[i].transform.position.z);
        }
    }

    void DisplayActions() {
        for (int i = 0; i < proxyActions.Count; i++) {
            proxyActions[i].transform.position = new Vector3(proxyActions[i].transform.position.x + UnityEngine.Random.Range(-5, 5), proxyActions[i].transform.position.y + UnityEngine.Random.Range(-5, 5), proxyActions[i].transform.position.z);
        }
    }

    void DisplayOperators() {
        
        for (int i = 0; i < proxyOperator.Count; i++) {
            proxyOperator[i].transform.position = new Vector3(proxyOperator[i].transform.position.x + UnityEngine.Random.Range(-5, 5), proxyOperator[i].transform.position.y + UnityEngine.Random.Range(-5, 5), 0);
        }
    }

    void DisplayParameters() {
        for (int i = 0; i < proxyParam.Count; i++) {
            proxyParam[i].transform.position = new Vector3(proxyOperator[i].transform.position.x + UnityEngine.Random.Range(-5, 5), proxyParam[i].transform.position.y + UnityEngine.Random.Range(-5, 5), proxyParam[i].transform.position.z);
        }
    }


    void Save_MC()
    {

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

}
