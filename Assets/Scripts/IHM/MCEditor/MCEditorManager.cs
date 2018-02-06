using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
    //private IProxyABParam parameterPrefab;
    private GameObject parameterPrefab;
    [SerializeField]
    private ProxyABAction actionPrefab;
    [SerializeField]
    private GameObject[] mcTemplates;

    private ABModel abModel;
    private List<ProxyABState> proxyStates;
    private List<ProxyABAction> proxyActions;
    private List<ProxyABTransition> proxyTransitions;
    private List<Pin> pins; //ProxyABGateOperator
    private List<IProxyABParam> proxyParam; //ProxyABParam
    private List<IProxyABOperator> proxyOperator;//ProxyABOperator


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
        proxyParam = new List<IProxyABParam>(); //ProxyABParam
        proxyOperator = new List<IProxyABOperator>();//ProxyABOperator
        SetupModel();
    }

    private void SetupModel()
    {
        abModel = LoadMC();
        CreateProxyStates();
        //CreateProxyTransitions();
    }

    ABModel LoadMC()
    {
        ABModel model = new ABModel();
        //TODO : Récuperer le ABModel en Utilisant le AppContextManager et remplacer path
        model = ABManager.instance.LoadABModelFromFile("Assets/Inputs/Test/siu_scoot_behavior_TEST.csv");
        return model;
    }

    void CreateProxyStates()

    {
        foreach (ABState state in this.abModel.States)
        {
            ProxyABState proxyState;
            GameObject proxyAction;
            Pin pin;            if (state.Action != null)
            {
                proxyAction.GetComponent<ProxyABAction>().AbAction = state.Action;
                proxyActions.Add(proxyAction);
                foreach (IABGateOperator param in state.Action.Parameters) {
                    pin = Instantiate<Pin>(pinPrefab);
                    pin.transform.position = proxyAction.transform.position;
                    float radius = proxyAction.transform.localScale.y / 2;
                    pin.transform.position = new Vector3(pin.transform.position.x, pin.transform.position.y + radius, pin.transform.position.z);
                    pins.Add(pin);
                    DeploySyntaxeTree(param.Inputs);                     
                }
            }
            else {
                proxyState = Instantiate<ProxyABState>(statePrefab);
                proxyState.GetComponent<ProxyABState>().AbState = state;
                proxyStates.Add(proxyState);                
            }
            if (state.Outcomes.Count != 0)
            {
                CreatePins(state.Outcomes);
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

                DeploySyntaxeTree(((IABOperator)node).Inputs);

            } else if (node is IABParam)
            {
                GameObject param = Instantiate<GameObject>(parameterPrefab);

            }                    
        }
    }

    void CreateProxySyntaxTree() {

    }

    void CreateProxyTransitions()
    {
        foreach (ABTransition transition in this.abModel.Transitions)
        {
            ProxyABTransition proxyTransition = new ProxyABTransition(transition);
            this.proxyTransitions.Add(proxyTransition);
        }
    }

    void CreatePins(List<ABTransition> transitions)
    {
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
