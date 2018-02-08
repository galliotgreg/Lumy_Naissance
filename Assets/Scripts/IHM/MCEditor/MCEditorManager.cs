using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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
 
        DisplayStates();
        DisplayOperators();
        DisplayParameters();
        CreateProxyTransitions();
        //CreateProxyTransitions();
        //
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

            Pin pin;
            if (state.Action != null)
            {
                proxyAction = Instantiate<ProxyABAction>(actionPrefab);
                Text actionName = proxyAction.GetComponentInChildren<Text>();
                actionName.text = state.Name;
                proxyAction.GetComponent<ProxyABAction>().AbAction = state.Action;

                proxyActions.Add(proxyAction);
                actionsDictionnary.Add(state, proxyAction);

                foreach (IABGateOperator param in state.Action.Parameters) {
                    pin = Instantiate<Pin>(pinPrefab);
                    pin.transform.parent = proxyAction.transform;
                    pin.transform.position = proxyAction.transform.position;
                    float radius = proxyAction.transform.localScale.y / 2;
                    pin.transform.position = new Vector3(pin.transform.position.x, pin.transform.position.y + radius, pin.transform.position.z);
                    pins.Add(pin); 
                    DeploySyntaxeTree(param.Inputs);                
                }
            }
            else {
                proxyState = Instantiate<ProxyABState>(statePrefab);
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

    void CreateProxySyntaxTree() {

    }

    void CreateProxyTransitions()
    {
        ProxyABTransition proxyABTransition;

        List<Pin> pinList;

        for(int i =0;i< abModel.Transitions.Count; i++) {
            proxyABTransition = Instantiate<ProxyABTransition>(transitionPrefab);

            pinList = CreatePins(i);
            proxyABTransition.GetComponent<LineRenderer>().SetPosition(0, pinList[0].transform.position);
            proxyABTransition.GetComponent<LineRenderer>().SetPosition(1, pinList[1].transform.position);

        }
        
        
    }

    List<Pin> CreatePins(int i)
    {
        Pin startPin;
        Pin endPin;
        List<Pin> pinList = new List<Pin>();

        ProxyABState startState = statesDictionnary[abModel.Transitions[i].Start];

        startPin = Instantiate<Pin>(pinPrefab);
        startPin.transform.parent = startState.transform;
        startPin.transform.position = startState.transform.position;
        float radiusStartPin = startState.transform.localScale.y / 2;

        startPin.transform.position = new Vector3(startPin.transform.position.x + (radiusStartPin * Mathf.Cos(i * (2 * Mathf.PI) / abModel.Transitions[i].Start.Outcomes.Count)), startPin.transform.position.y + (radiusStartPin * Mathf.Sin(i * (2 * Mathf.PI) / abModel.Transitions[i].Start.Outcomes.Count)), startPin.transform.position.z);
        pins.Add(startPin);
        pinList.Add(startPin);

        if (statesDictionnary.ContainsKey(abModel.Transitions[i].End)) {

            ProxyABState endState = statesDictionnary[abModel.Transitions[i].End];

            endPin = Instantiate<Pin>(pinPrefab);
            endPin.transform.parent = endState.transform;
            endPin.transform.position = endState.transform.position;
            float radiusEndPin = endState.transform.localScale.y / 2;
            endPin.transform.position = new Vector3(endPin.transform.position.x + (radiusEndPin * Mathf.Cos(i * (2 * Mathf.PI) / abModel.Transitions[i].End.Outcomes.Count)), endPin.transform.position.y + (radiusEndPin * Mathf.Sin(i * (2 * Mathf.PI) / abModel.Transitions[i].End.Outcomes.Count)), endPin.transform.position.z);

            pins.Add(endPin);
            pinList.Add(endPin);
        }
        else if (actionsDictionnary.ContainsKey(abModel.Transitions[i].End)) {

            ProxyABAction endState = actionsDictionnary[abModel.Transitions[i].End];

            endPin = Instantiate<Pin>(pinPrefab);
            endPin.transform.parent = endState.transform;
            endPin.transform.position = endState.transform.position;
            float radiusEndPin = endState.transform.localScale.y / 2;
            endPin.transform.position = new Vector3(endPin.transform.position.x + (radiusEndPin), endPin.transform.position.y, endPin.transform.position.z);
            pins.Add(endPin);
            pinList.Add(endPin);
        }
        
        return pinList;
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
