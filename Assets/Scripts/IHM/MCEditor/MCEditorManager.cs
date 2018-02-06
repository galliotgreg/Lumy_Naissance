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
    private IProxyABOperator operatorPrefab;
    [SerializeField]
    private IProxyABParam parameterPrefab;
    [SerializeField]
    private GameObject actionPrefab;
    [SerializeField]
    private GameObject[] mcTemplates;

    private ABModel abModel;
    private List<ProxyABState> proxyStates;
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
            ProxyABState proxyState = Instantiate<ProxyABState>(statePrefab);
            //this.proxyStates.Add( (ProxyABModel) proxyState);
            if (state.Action != null)
            {

            }
            if (state.Outcomes.Count != 0)
            {
                CreatePins(state.Outcomes);
            }            
        }
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
