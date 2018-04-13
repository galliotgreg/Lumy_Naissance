﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

public class MCEditorManager : MonoBehaviour
{

    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static MCEditorManager instance = null;

    [SerializeField]
    private GameObject MC_Container;    // Parent for the MC
    Transform mCparent;

    private ABModel abModel;

    // Save function utils
    private int idNodeSyntTree;
    private int idNodeInputPin;
    private ABOperatorFactory opeFactory = new ABOperatorFactory();
    private ABParamFactory paramFactory = new ABParamFactory();
    private Dictionary<string, string> operatorDictionary = new Dictionary<string, string>();
    private Dictionary<string, string> paramDictionary = new Dictionary<string, string>();


    private Dictionary<ABState, ProxyABState> statesDictionnary;
    private Dictionary<ABState, ProxyABAction> actionsDictionnary;

    private List<ProxyABState> proxyStates;
    private List<ProxyABAction> proxyActions;
    private List<ProxyABTransition> proxyTransitions;
    private List<Pin> pins; //ProxyABGateOperator    
    private List<ProxyABOperator> proxyOperators;
    private List<ProxyABParam> proxyParams;
    private Text toolTipText;
    private GameObject panelToolTip;
    private Text[] toolTipTextTab;

    //ToolTip
    private List<Help> databaseOperators = new List<Help>();
    private List<Help> databaseActions_Params = new List<Help>();

    //[SerializeField]
    private string MC_OrigFilePath;

    #region PROPERTIES
    public Transform MCparent
    {
        get
        {
            return mCparent;
        }
    }
    #endregion

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
        toolTipText = GetComponentInChildren<Text>();
        panelToolTip = GameObject.Find("PanelVoletGaucheToolTip");
        toolTipTextTab = panelToolTip.GetComponentsInChildren<Text>();

        LoadActions_ParamsDatabase();
        LoadOperatorDatabase();


        /**START DO NOT COMMIT**/
        if (AppContextManager.instance.PrysmeEdit)
        {
            MC_OrigFilePath = AppContextManager.instance.ActiveSpecieFolderPath
                + AppContextManager.instance.PRYSME_FILE_NAME
                + AppContextManager.instance.CSV_EXT;

        }
        else
        {
            MC_OrigFilePath = AppContextManager.instance.ActiveBehaviorPath;
        }
        //MC_OrigFilePath = "Assets/Inputs/Test/GREG_TRANS_STATE_STATE_TEST.csv";
        /**END DO NOT COMMIT**/

        //usefull for save function
        opeFactory.CreateDictionnary();
        paramFactory.CreateDictionnary();

        operatorDictionary = opeFactory.TypeStringToString;
        paramDictionary = paramFactory.TypeStringToString;

        mCparent = (MC_Container == null ? this.transform : MC_Container.transform);

        /** LOAD MODEL AND CREATE PROXY OBJECTS **/
        SetupModel();

        //Temporary_Save_MC_Behavior(AppContextManager.instance.ActiveCast.Name + "_behavior", "0");
        MCToolManager.instance.TemporarySave();
    }

    private void Update()
    {
        /**START TEST SAVE**/
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            Save_MC();
        }
        else if (Input.GetKeyDown(KeyCode.Delete))
        {
            this.deleteSelectedTransition();
            MCToolManager.instance.hasBeenAdded = false;
            MCToolManager.instance.TemporarySave();
        }

    }

    private void OnDestroy()
    {
        AppContextManager.instance.PrysmeEdit = false;
    }

    public void SetupModel()
    {
        abModel = LoadMC();
        LoadProxyStates();
        LoadProxyTransitions();
        LoadMC_Position();
    }

    public void TemporarySetupModel(string cast_name, string id)
    {
        abModel = LoadMCFromTemporary(cast_name, id);
        LoadProxyStates();
        LoadProxyTransitions();
        LoadMC_PositionFromTemporary(cast_name, id);
    }

    void setProxyPositionOnLoad(string nameProxy, bool isStateBlock, bool isActionBlock, bool isOperatorBlock, bool isParameterBlock, float x, float y, float z)
    {

        if (isStateBlock)
        {
            foreach (ProxyABState proxy in proxyStates)
            {
                if (proxy.AbState.Name == nameProxy)
                {
                    proxy.transform.position = new Vector3(x, y, z);
                }
            }
        }
        else if (isActionBlock)
        {
            foreach (ProxyABAction proxy in proxyActions)
            {
                if (proxy.AbState.Name == nameProxy)
                {
                    proxy.transform.position = new Vector3(x, y, z);
                }
            }
        }
        else if (isOperatorBlock)
        {
            foreach (ProxyABOperator proxy in proxyOperators)
            {
                if (proxy.isMacroComposant)
                {
                    if (proxy.AbOperator.SymbolName == nameProxy)
                    {
                        if (!proxy.IsPositioned)
                        {
                            proxy.transform.position = new Vector3(x, y, z);
                            proxy.IsPositioned = true;
                            return;
                        }
                    }
                }
                else if (operatorDictionary[((IABOperator)proxy.AbOperator).ClassName] == nameProxy)
                {
                    if (!proxy.IsPositioned)
                    {
                        proxy.transform.position = new Vector3(x, y, z);
                        proxy.IsPositioned = true;
                        return;
                    }
                }
            }
        }
        else if (isParameterBlock)
        {
            foreach (ProxyABParam proxy in proxyParams)
            {

                string value = GetParamValue((ABNode)proxy.AbParam);
                string type = paramDictionary[GetParamType((ABNode)proxy.AbParam)];
                if (nameProxy.Contains("const"))
                {
                    if ((((IABParam)proxy.AbParam).Identifier + " " + type + "=" + value) == nameProxy)
                    {
                        if (!proxy.IsPositioned)
                        {
                            proxy.transform.position = new Vector3(x, y, z);
                            proxy.IsPositioned = true;
                            return;
                        }
                    }
                }
                else
                {
                    if ((type + ":" + ((IABParam)proxy.AbParam).Identifier) == nameProxy)
                    {

                        if (!proxy.IsPositioned)
                        {
                            proxy.transform.position = new Vector3(x, y, z);
                            proxy.IsPositioned = true;
                            return;
                        }
                    }
                }
            }
        }
    }

    public void LoadMC_Position()
    {
        string path = MC_OrigFilePath.Split('.')[0] + "_POSITION.csv";
        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);

            List<string> lines = new List<string>();

            bool isStateBlock = false;
            bool isActionBlock = false;
            bool isOperatorBlock = false;
            bool isParameterBlock = false;

            while (reader.Peek() >= 0)
            {
                lines.Add(reader.ReadLine());
            }
            foreach (string line in lines)
            {
                if (line != ",,")
                {
                    String[] tokens = line.Split(',');
                    if (tokens[0] == "States")
                    {
                        isStateBlock = true;
                        continue;
                    }
                    else if (tokens[0] == "Actions")
                    {
                        isStateBlock = false;
                        isActionBlock = true;
                        continue;
                    }
                    else if (tokens[0] == "Operators")
                    {
                        isStateBlock = false;
                        isActionBlock = false;
                        isOperatorBlock = true;
                        continue;
                    }
                    else if (tokens[0] == "Parameters")
                    {
                        isStateBlock = false;
                        isActionBlock = false;
                        isOperatorBlock = false;
                        isParameterBlock = true;
                        continue;
                    }

                    string name = tokens[0];

                    string x_string = tokens[1];
                    string y_string = tokens[2];

                    float x = float.Parse(x_string);
                    float y = float.Parse(y_string);
                    float z = 0;

                    setProxyPositionOnLoad(name, isStateBlock, isActionBlock, isOperatorBlock, isParameterBlock, x, y, z);
                }
            }
            reader.Close();
        }
    }
    public void LoadMC_PositionFromTemporary(string cast_name, string id)
    {
        string csvpath = Application.dataPath + @"\Inputs/TemporaryBackup/" + cast_name + "_POSITION_" + id + ".csv";
        //string path = MC_OrigFilePath.Split('.')[0] + "_POSITION.csv";
        if (File.Exists(csvpath))
        {
            StreamReader reader = new StreamReader(csvpath);

            List<string> lines = new List<string>();

            bool isStateBlock = false;
            bool isActionBlock = false;
            bool isOperatorBlock = false;
            bool isParameterBlock = false;

            while (reader.Peek() >= 0)
            {
                lines.Add(reader.ReadLine());
            }
            foreach (string line in lines)
            {
                if (line != ",,")
                {
                    String[] tokens = line.Split(',');
                    if (tokens[0] == "States")
                    {
                        isStateBlock = true;
                        continue;
                    }
                    else if (tokens[0] == "Actions")
                    {
                        isStateBlock = false;
                        isActionBlock = true;
                        continue;
                    }
                    else if (tokens[0] == "Operators")
                    {
                        isStateBlock = false;
                        isActionBlock = false;
                        isOperatorBlock = true;
                        continue;
                    }
                    else if (tokens[0] == "Parameters")
                    {
                        isStateBlock = false;
                        isActionBlock = false;
                        isOperatorBlock = false;
                        isParameterBlock = true;
                        continue;
                    }

                    string name = tokens[0];

                    string x_string = tokens[1];
                    string y_string = tokens[2];

                    float x = float.Parse(x_string);
                    float y = float.Parse(y_string);
                    float z = 0;

                    setProxyPositionOnLoad(name, isStateBlock, isActionBlock, isOperatorBlock, isParameterBlock, x, y, z);
                }
            }
            reader.Close();
        }
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
    ABModel LoadMCFromTemporary(string cast_name, string id)
    {
        string csvpath = Application.dataPath + @"\Inputs/TemporaryBackup/" + cast_name + "_" + id + ".csv";

        ABModel model = new ABModel();

        /**** START TODO ****/
        //TODO : Récuperer le ABModel en Utilisant le AppContextManager et remplacer path        
        model = ABManager.instance.LoadABModelFromFile(csvpath);
        /**** END TODO ****/

        return model;
    }

    #region LOAD MODEL FUNCTIONS
    public void LoadProxyStates()
    {
        foreach (ABState state in this.AbModel.States)
        {
            // Actions
            if (state.Action != null)
            {
                ProxyABAction proxyAction = MCEditor_Proxy_Factory.instantiateAction(state);
                this.registerAction(proxyAction);

                // Create SyntaxTrees
                List<Pin> pins = proxyAction.getPins(Pin.PinType.ActionParam);
                if (state.Action.Parameters != null)
                {
                    for (int i = 0; i < state.Action.Parameters.Length; i++)
                    {
                        IABGateOperator param = state.Action.Parameters[i];

                        Pin start = pins[i];
                        registerPin(start);

                        // create syntaxTree for each Param
                        foreach (ABNode node in param.Inputs)
                        {
                            Pin end = RecNodeSynthTree(node);
                            if (end != null)
                            {
                                MCEditor_Proxy_Factory.instantiateTransition(start, end, false);
                            }
                        }
                    }
                }
            }
            // States
            else
            {
                ProxyABState proxyState = MCEditor_Proxy_Factory.instantiateState(state, abModel.InitStateId == state.Id);
                this.registerState(proxyState);
            }
        }
    }

    //Load syntaxe Tree in a recursive way
    // return the outcome pin
    Pin RecNodeSynthTree(ABNode node)
    {
        if (node is IABOperator)
        {
            ProxyABOperator ope = MCEditor_Proxy_Factory.instantiateOperator((IABOperator)node, true);
            registerOperator(ope);

            // Creating params trees
            List<Pin> pins = ope.getPins(Pin.PinType.OperatorIn);
            Pin start;
            for (int i = 0; i < ((IABOperator)node).Inputs.Length; i++)
            {
                ABNode inputNode = ((IABOperator)node).Inputs[i];
                if (((IABOperator)node).Inputs.Length <= 3)
                {
                    start = pins[i];
                }
                else
                {
                    start = pins[0];
                }

                if (inputNode == null)
                {
                }

                Pin end = RecNodeSynthTree(inputNode);

                if (end != null)
                {
                    MCEditor_Proxy_Factory.instantiateTransition(start, end, false);
                }
            }

            // Outcome
            return ope.getPins(Pin.PinType.OperatorOut)[0];
        }
        else if (node is IABParam)
        {
            ProxyABParam param = MCEditor_Proxy_Factory.instantiateParam((IABParam)node, true);
            registerParam(param);

            return param.getPins(Pin.PinType.Param)[0];
        }
        return null;
    }

    public void LoadProxyTransitions()
    {
        ProxyABTransition proxyABTransition;

        for (int i = 0; i < AbModel.Transitions.Count; i++)
        {
            List<Pin> pinList = LoadPinsStates(i);
            proxyABTransition = MCEditor_Proxy_Factory.instantiateTransition(pinList[0], pinList[1], true);
            proxyABTransition.Transition = AbModel.Transitions[i];

            // Check pin for the transition states
            if (proxyABTransition.EndPosition.ProxyParent is ProxyABState)
            {
                ((ProxyABState)proxyABTransition.EndPosition.ProxyParent).checkPins();
            }
            if (proxyABTransition.StartPosition.ProxyParent is ProxyABState)
            {
                ((ProxyABState)proxyABTransition.StartPosition.ProxyParent).checkPins();
            }

            if (AbModel.Transitions[i].Condition != null)
            {
                Pin end = RecNodeSynthTree(AbModel.Transitions[i].Condition.Inputs[0]);
                MCEditor_Proxy_Factory.instantiateTransition(proxyABTransition.Condition, end, false);
            }
        }
    }

    List<Pin> LoadPinsStates(int curTransition)
    {
        List<Pin> pinList = new List<Pin>();

        ProxyABState startState = statesDictionnary[AbModel.Transitions[curTransition].Start];

        // Instantiate OutCome Pin in the state
        //pinList.Add( MCEditor_Proxy_Factory.instantiatePin ( Pin.PinType.TransitionOut, Pin.calculatePinPosition( startState.AbState, startState.gameObject, true, curTransition ), startState.transform ) );
        pinList.Add(startState.ExtraPin);

        if (statesDictionnary.ContainsKey(AbModel.Transitions[curTransition].End))
        {
            ProxyABState endState = statesDictionnary[AbModel.Transitions[curTransition].End];

            // Recovery income Pin
            if (endState.getPins(Pin.PinType.TransitionIn).Count > 0)
            {
                pinList.Add(endState.getPins(Pin.PinType.TransitionIn)[0]);
            }
        }
        else if (actionsDictionnary.ContainsKey(AbModel.Transitions[curTransition].End))
        {

            ProxyABAction endState = actionsDictionnary[AbModel.Transitions[curTransition].End];

            pinList.Add(endState.getPins(Pin.PinType.TransitionIn)[0]);
        }

        return pinList;
    }
    #endregion

    #region SAVE FUNCTION
    void Save_Ope_Param(int idNodeInput, int idNodeInputPin, ABNode node, StringBuilder syntTreeContent)
    {
        int idParentnode = idNodeSyntTree;
        if (idNodeSyntTree == 0)
        {

            if (node is IABOperator)
            {
                string type = "";
                if (node.GetType().ToString().Contains("Macro"))
                {
                    type = ((IABOperator)node).SymbolName;
                    syntTreeContent.AppendLine(idParentnode + ",macro{" + type + "}" + ",");
                }
                else
                {
                    type = operatorDictionary[((IABOperator)node).ClassName];
                    if (!operatorDictionary.ContainsKey(((IABOperator)node).ClassName))
                    {
                        Debug.LogError(((IABOperator)node).GetType().ToString() + " n'est pas dans la le dictionnaire des opérateurs. Vérifier l'orthographe Dans le fichier ABOperatorFactory");
                    }
                    else
                    {
                        syntTreeContent.AppendLine(idNodeSyntTree + ",operator{" + type + "},");
                    }
                }
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
                }
                else
                {
                    type = paramDictionary[GetParamType(node)];
                }
                if (((IABParam)node).Identifier != "const")
                {
                    syntTreeContent.AppendLine(idNodeSyntTree + ",param{" + type + ":" + ((IABParam)node).Identifier + "}" + ",");
                }
                else
                {
                    syntTreeContent.AppendLine(idNodeSyntTree + ",param{" + ((IABParam)node).Identifier + " " + type + "=" + value + "},");
                }
                idNodeSyntTree++;
            }
        }
        else
        {
            if (node is IABOperator)
            {
                string type = "";
                if (node.GetType().ToString().Contains("Macro"))
                {
                    type = ((IABOperator)node).SymbolName;
                    syntTreeContent.AppendLine(idParentnode + ",macro{" + type + "}" + "," + idNodeInput + "->" + idNodeInputPin);
                }
                else
                {
                    if (!operatorDictionary.ContainsKey(((IABOperator)node).ClassName))
                    {
                        Debug.LogError(((IABOperator)node).GetType().ToString() + " n'est pas dans la le dictionnaire des opérateurs. Vérifier l'orthographe Dans le fichier ABOperatorFactory");
                    }
                    else
                    {
                        type = operatorDictionary[((IABOperator)node).ClassName];
                        syntTreeContent.AppendLine(idParentnode + ",operator{" + type + "}" + "," + idNodeInput + "->" + idNodeInputPin);
                    }
                }
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
                }
                else
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
                }
                idNodeSyntTree++;
            }
        }
    }
    void Save_MC_Position()
    {
        SortOperatorProxies();

        string csvpath = MC_OrigFilePath.Split('.')[0] + "_POSITION.csv";
        StringBuilder csvcontent = new StringBuilder();
        List<StringBuilder> syntTrees = new List<StringBuilder>();
        csvcontent.AppendLine("States,Position");
        foreach (ProxyABState state in proxyStates)
        {
            csvcontent.AppendLine(state.AbState.Name + ", " + state.transform.position.x.ToString() + ", "
                                                            + state.transform.position.y.ToString() + ", "
                                                            + state.transform.position.z.ToString());
        }
        csvcontent.AppendLine(",,");
        csvcontent.AppendLine("Actions,Position");
        foreach (ProxyABAction action in proxyActions)
        {
            csvcontent.AppendLine(action.AbState.Name + ", " + action.transform.position.x.ToString() + ", "
                                                            + action.transform.position.y.ToString() + ", "
                                                            + action.transform.position.z.ToString());
        }
        csvcontent.AppendLine(",,");
        csvcontent.AppendLine("Operators,Position");
        foreach (ProxyABOperator ope in proxyOperators)
        {
            string type = "";
            if (ope.AbOperator.GetType().ToString().Contains("Macro"))
            {
                type = ope.AbOperator.SymbolName;
            }
            else
            {
                type = operatorDictionary[((IABOperator)ope).ClassName];
            }
            csvcontent.AppendLine(type + ", " + ope.transform.position.x.ToString() + ", "
                                                            + ope.transform.position.y.ToString() + ", "
                                                            + ope.transform.position.z.ToString());
        }
        csvcontent.AppendLine(",,");
        csvcontent.AppendLine("Parameters,Position");
        foreach (ProxyABParam param in proxyParams)
        {
            string value = GetParamValue((ABNode)param.AbParam);
            string type = paramDictionary[GetParamType((ABNode)param.AbParam)];
            if (((IABParam)param.AbParam).Identifier != "const")
            {
                csvcontent.AppendLine(type + ":" + ((IABParam)param.AbParam).Identifier + "," + param.transform.position.x.ToString() + ", "
                                                                                        + param.transform.position.y.ToString() + ", "
                                                                                         + param.transform.position.z.ToString());
            }
            else
            {
                csvcontent.AppendLine(((IABParam)param.AbParam).Identifier + " " + type + "=" + value + "," + param.transform.position.x.ToString() + ", "
                                                                                                    + param.transform.position.y.ToString() + ", "
                                                                                                    + param.transform.position.z.ToString());
            }
        }
        if (File.Exists(csvpath))
        {
            File.WriteAllText(csvpath, csvcontent.ToString());
        }
        else
        {
            File.AppendAllText(csvpath, csvcontent.ToString());
        }
        Debug.Log("Save MC Position");
    }

    private void SortOperatorProxies()
    {
        List<ProxyABOperator> proxyOperatorsTMP = new List<ProxyABOperator>();
        List<ProxyABParam> proxyParamsTMP = new List<ProxyABParam>();

        //Fetch action trees nodes
        foreach (ABState state in abModel.States)
        {
            if (state.Action != null)
            {
                if (state.Action.Parameters != null)
                {
                    for (int i = 0; i < state.Action.Parameters.Length; i++)
                    {
                        if (state.Action.Parameters[i].Inputs[0] != null)
                        {
                            foreach (ABNode node in state.Action.Parameters[i].Inputs)
                            {
                                RecSweepTree(node, proxyOperatorsTMP, proxyParamsTMP);
                            }
                        }
                    }
                }
            }
        }

        //Fetch transition trees nodes
        foreach (ABTransition trans in abModel.Transitions)
        {
            if (trans.Condition != null)
            {
                if (trans.Condition.Inputs[0] != null)
                {
                    foreach (ABNode node in trans.Condition.Inputs)
                    {
                        RecSweepTree(node, proxyOperatorsTMP, proxyParamsTMP);
                    }
                }

            }
        }

        proxyOperators.Clear();
        proxyParams.Clear();
        proxyOperators.AddRange(proxyOperatorsTMP);
        proxyParams.AddRange(proxyParamsTMP);
    }

    void RecSweepTree(ABNode node, List<ProxyABOperator> proxyOperatorsTMP, List<ProxyABParam> proxyParamsTMP)
    {
        if (node is IABOperator)
        {
            ProxyABOperator proxyNode = FindProxyOperatorFromABOperator(node);
            proxyOperatorsTMP.Add(proxyNode);

            foreach (ABNode input in ((IABOperator)node).Inputs)
            {
                RecSweepTree(input, proxyOperatorsTMP, proxyParamsTMP);
            }
        }
        else if (node is IABParam)
        {
            ProxyABParam proxyNode = FindProxyParamFromABParam(node);
            proxyParamsTMP.Add(proxyNode);
        }
    }

    private ProxyABOperator FindProxyOperatorFromABOperator(ABNode node)
    {
        foreach (ProxyABOperator proxyOperator in proxyOperators)
        {
            if (proxyOperator.AbOperator == node)
            {
                return proxyOperator;
            }
        }
        throw new NotSupportedException();
    }

    private ProxyABParam FindProxyParamFromABParam(ABNode node)
    {
        foreach (ProxyABParam proxyParam in proxyParams)
        {
            if (proxyParam.AbParam == node)
            {
                return proxyParam;
            }
        }
        throw new NotSupportedException();
    }

    public void Temporary_Save_MC_Position(string cast_name, string id)
    {
        string csvpath = Application.dataPath + @"\Inputs/TemporaryBackup/" + cast_name + "_POSITION_" + id + ".csv";
        StringBuilder csvcontent = new StringBuilder();
        List<StringBuilder> syntTrees = new List<StringBuilder>();
        csvcontent.AppendLine("States,Position");
        foreach (ProxyABState state in proxyStates)
        {
            csvcontent.AppendLine(state.AbState.Name + ", " + state.transform.position.x.ToString() + ", "
                                                            + state.transform.position.y.ToString() + ", "
                                                            + state.transform.position.z.ToString());
        }
        csvcontent.AppendLine(",,");
        csvcontent.AppendLine("Actions,Position");
        foreach (ProxyABAction action in proxyActions)
        {
            csvcontent.AppendLine(action.AbState.Name + ", " + action.transform.position.x.ToString() + ", "
                                                            + action.transform.position.y.ToString() + ", "
                                                            + action.transform.position.z.ToString());
        }
        csvcontent.AppendLine(",,");
        csvcontent.AppendLine("Operators,Position");
        foreach (ProxyABOperator ope in proxyOperators)
        {
            string type = "";
            if (ope.AbOperator.GetType().ToString().Contains("Macro"))
            {
                type = ope.AbOperator.SymbolName;
            }
            else
            {
                type = operatorDictionary[((IABOperator)ope).ClassName];
            }
            csvcontent.AppendLine(type + ", " + ope.transform.position.x.ToString() + ", "
                                                            + ope.transform.position.y.ToString() + ", "
                                                            + ope.transform.position.z.ToString());
        }
        csvcontent.AppendLine(",,");
        csvcontent.AppendLine("Parameters,Position");
        foreach (ProxyABParam param in proxyParams)
        {
            string value = GetParamValue((ABNode)param.AbParam);
            string type = paramDictionary[GetParamType((ABNode)param.AbParam)];
            if (((IABParam)param.AbParam).Identifier != "const")
            {
                csvcontent.AppendLine(type + ":" + ((IABParam)param.AbParam).Identifier + "," + param.transform.position.x.ToString() + ", "
                                                                                        + param.transform.position.y.ToString() + ", "
                                                                                         + param.transform.position.z.ToString());
            }
            else
            {
                csvcontent.AppendLine(((IABParam)param.AbParam).Identifier + " " + type + "=" + value + "," + param.transform.position.x.ToString() + ", "
                                                                                                    + param.transform.position.y.ToString() + ", "
                                                                                                    + param.transform.position.z.ToString());
            }
        }
        if (File.Exists(csvpath))
        {
            File.WriteAllText(csvpath, csvcontent.ToString());
        }
        else
        {
            File.AppendAllText(csvpath, csvcontent.ToString());
        }

        Debug.Log("Temporary Saved MC Position");
    }
    public void Temporary_Save_MC_Behavior(string cast_name, string id)
    {
        string csvpath = Application.dataPath + @"\Inputs/TemporaryBackup/" + cast_name + "_" + id + ".csv";
        StringBuilder csvcontent = new StringBuilder();
        List<StringBuilder> syntTrees = new List<StringBuilder>();

        csvcontent.AppendLine("States,Name,Type");
        foreach (ABState state in abModel.States)
        {
            if (state.Action != null)
            {
                csvcontent.AppendLine(state.Id + "," + state.Name + "," + "trigger{" + state.Action.Type.ToString().ToLower() + "}");

                if (state.Action.Parameters != null)
                {
                    for (int i = 0; i < state.Action.Parameters.Length; i++)
                    {
                        if (state.Action.Parameters[i].Inputs[0] != null)
                        {
                            StringBuilder syntTreeContent = new StringBuilder();
                            syntTreeContent.AppendLine("Syntax Tree,output,");
                            syntTreeContent.AppendLine("1," + state.Name + "->" + i + ",");
                            syntTreeContent.AppendLine("Nodes,Type,output (Node -> Input)");

                            idNodeSyntTree = 0;
                            foreach (ABNode node in state.Action.Parameters[i].Inputs)
                            {
                                Save_Ope_Param(idNodeSyntTree, idNodeInputPin, node, syntTreeContent);
                            }
                            syntTreeContent.AppendLine(",,");
                            syntTrees.Add(syntTreeContent);
                        }
                    }
                }
            }
            else
            {
                if (state.Id == 0)
                {
                    csvcontent.AppendLine(state.Id + "," + state.Name + ",Init");
                }
                else
                {
                    csvcontent.AppendLine(state.Id + "," + state.Name + ",Inter");
                }
            }
        }
        csvcontent.AppendLine(",,");
        csvcontent.AppendLine("Transitions,Start State,End State");
        int last_id = 0;
        foreach (ABTransition trans in abModel.Transitions)
        {
            //trans.Id = last_id;
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
                    //last_id++;
                }

            }
        }
        csvcontent.AppendLine(",,");
        File.WriteAllText(csvpath, csvcontent.ToString());

        foreach (StringBuilder content in syntTrees)
        {
            File.AppendAllText(csvpath, content.ToString());
        }
        Debug.Log("Save MC");
        Temporary_Save_MC_Position(cast_name, id);
    }
    public void Save_MC()
    {
        string csvpath = MC_OrigFilePath;
        StringBuilder csvcontent = new StringBuilder();
        List<StringBuilder> syntTrees = new List<StringBuilder>();

        csvcontent.AppendLine("States,Name,Type");
        foreach (ABState state in abModel.States)
        {
            if (state.Action != null)
            {
                csvcontent.AppendLine(state.Id + "," + state.Name + "," + "trigger{" + state.Action.Type.ToString().ToLower() + "}");

                if (state.Action.Parameters != null)
                {
                    for (int i = 0; i < state.Action.Parameters.Length; i++)
                    {
                        if (state.Action.Parameters[i].Inputs[0] != null)
                        {
                            StringBuilder syntTreeContent = new StringBuilder();
                            syntTreeContent.AppendLine("Syntax Tree,output,");
                            syntTreeContent.AppendLine("1," + state.Name + "->" + i + ",");
                            syntTreeContent.AppendLine("Nodes,Type,output (Node -> Input)");

                            idNodeSyntTree = 0;
                            foreach (ABNode node in state.Action.Parameters[i].Inputs)
                            {
                                Save_Ope_Param(idNodeSyntTree, idNodeInputPin, node, syntTreeContent);
                            }
                            syntTreeContent.AppendLine(",,");
                            syntTrees.Add(syntTreeContent);
                        }
                    }
                }
            }
            else
            {
                if (state.Id == 0)
                {
                    csvcontent.AppendLine(state.Id + "," + state.Name + ",Init");
                }
                else
                {
                    csvcontent.AppendLine(state.Id + "," + state.Name + ",Inter");
                }
            }
        }
        csvcontent.AppendLine(",,");
        csvcontent.AppendLine("Transitions,Start State,End State");
        int last_id = 0;
        foreach (ABTransition trans in abModel.Transitions)
        {
            //trans.Id = last_id;
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
                    //last_id++;
                }

            }
        }
        csvcontent.AppendLine(",,");
        File.WriteAllText(csvpath, csvcontent.ToString());

        foreach (StringBuilder content in syntTrees)
        {
            File.AppendAllText(csvpath, content.ToString());
        }
        Debug.Log("Save MC");
        Save_MC_Position();
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
        }
        else if (node is ABParam<ABTable<ABBool>>)
        {
            type = "ABTable<ABBool>";
        }
        else if (node is ABParam<ABTable<ABScalar>>)
        {
            type = "ABTable<ABScalar>";
        }
        else if (node is ABParam<ABTable<ABText>>)
        {
            type = "ABTable<ABText>";
        }
        else if (node is ABParam<ABTable<ABColor>>)
        {
            type = "ABTable<ABColor>";
        }
        else if (node is ABParam<ABTable<ABRef>>)
        {
            type = "ABTable<ABRef>";
        }

        return type;
    }

    public static string GetParamValue(ABNode node)
    {
        string text = "";
        if (node is ABParam<ABText>)
        {
            if (((ABParam<ABText>)node).Value != null)
            {
                text += ((ABParam<ABText>)node).Value.Value.ToString();
            }
        }
        else if (node is ABParam<ABVec>)
        {
            if (((ABParam<ABVec>)node).Value != null)
            {
                text += ((ABParam<ABVec>)node).Value.X.ToString() + ";" + ((ABParam<ABVec>)node).Value.Y.ToString();
            }
        }
        else if (node is ABParam<ABBool>)
        {
            if (((ABParam<ABBool>)node).Value != null)
            {
                text += ((ABParam<ABBool>)node).Value.Value.ToString();
            }
        }
        else if (node is ABParam<ABRef>)
        {
            if (((ABParam<ABRef>)node).Value != null)
            {
                text += ((ABParam<ABRef>)node).Value.ToString();
            }
        }
        else if (node is ABParam<ABColor>)
        {
            if (((ABParam<ABColor>)node).Value != null)
            {
                text += ((ABParam<ABColor>)node).Value.Value.ToString();
            }
        }
        else if (node is ABParam<ABScalar>)
        {
            if (((ABParam<ABScalar>)node).Value != null)
            {
                text += ((ABParam<ABScalar>)node).Value.Value.ToString();
            }
        }
        return text;
    }
    #endregion

    #region REGISTER PROXY
    // Action
    // return the success of the operation [if necessary]
    public bool registerAction(ProxyABAction proxyAction)
    {
        // check disponibility
        //Alter name if exists
        proxyAction.Name = getAvailableStateName(proxyAction.AbState.Name);

        if (AbModel.getState(proxyAction.AbState.Id) == null)
        {
            proxyAction.AbState.Id = AbModel.AddState(proxyAction.AbState.Name, proxyAction.AbState.Action);
            proxyAction.AbState = AbModel.getState(proxyAction.AbState.Id);
        }
        proxyActions.Add(proxyAction);
        actionsDictionnary.Add(proxyAction.AbState, proxyAction);

        return true;
    }

    // State
    // return the success of the operation [if necessary]
    public bool registerState(ProxyABState proxyState)
    {
        // check disponibility
        //Alter name if exists
        proxyState.Name = getAvailableStateName(proxyState.AbState.Name);

        if (AbModel.getState(proxyState.AbState.Id) == null)
        {
            proxyState.AbState.Id = AbModel.AddState(proxyState.AbState.Name, proxyState.AbState.Action);
            proxyState.AbState = AbModel.getState(proxyState.AbState.Id);
        }
        proxyStates.Add(proxyState);
        statesDictionnary.Add(proxyState.AbState, proxyState);

        return true;
    }
    public bool changeModelStateName(ProxyABState proxyState, string newValue)
    {
        // check disponibility
        bool available = stateAvailable(newValue);

        if (available)
        {
            // Change value in the model
            if (AbModel.getState(proxyState.AbState.Id) != null)
            {
                AbModel.getState(proxyState.AbState.Id).Name = newValue;
            }
        }
        return available;
    }
    public bool changeModelActionName(ProxyABAction proxyAction, string newValue)
    {
        // check disponibility
        bool available = stateAvailable(newValue);

        if (available)
        {
            // Change value in the model
            if (AbModel.getState(proxyAction.AbState.Id) != null)
            {
                AbModel.getState(proxyAction.AbState.Id).Name = newValue;
            }
        }
        return available;
    }

    public string getAvailableStateName(string name)
    {
        bool available = stateAvailable(name);

        if (available)
        {
            return name;
        }

        int suffixId = 0;
        while (true)
        {
            string candidateName = name + "_" + suffixId++;
            if (stateAvailable(candidateName))
            {
                return candidateName;
            }
        }
    }
    public bool stateAvailable(string name)
    {
        // check disponibility
        bool available = true;
        foreach (ABState s in statesDictionnary.Keys)
        {
            if (s.Name == name)
            {
                available = false;
            }
        }
        foreach (ABState s in actionsDictionnary.Keys)
        {
            if (s.Name == name)
            {
                available = false;
            }
        }
        return available;
    }

    // Operator
    public void registerOperator(ProxyABOperator proxyOperator)
    {
        proxyOperators.Add(proxyOperator);
    }

    // Param
    public void registerParam(ProxyABParam proxyParam)
    {
        proxyParams.Add(proxyParam);
    }

    // Pin
    public void registerPin(Pin pin)
    {
        pins.Add(pin);
    }
    #endregion

    #region POSITIONING
    public static void positioningProxy(MCEditor_Proxy proxy)
    {
        if (proxy != null)
        {
            Vector3 curPosition = proxy.transform.position;
            proxy.transform.position = new Vector3(Mathf.Round(curPosition.x), Mathf.RoundToInt(curPosition.y), curPosition.z);

            /*float gridDimension = 0.75f;

			float newPosX = 0;
			float newPosY = 0;
			// Check if the position is closer to previous or next grid point
			float diffX = curPosition.x % gridDimension;
			int xIt = Mathf.FloorToInt( curPosition.x / gridDimension );
			if (Mathf.Abs (diffX) > gridDimension / 2f) {
				newPosX = gridDimension * (xIt + 1);
			} else {
				newPosX = gridDimension*xIt;
			}

			float diffY = curPosition.y % gridDimension;
			int yIt = Mathf.FloorToInt( curPosition.y / gridDimension );
			if (Mathf.Abs (diffY) > gridDimension / 2f) {
				newPosY = gridDimension * (yIt + 1);
			} else {
				newPosY = gridDimension*yIt;
			}

			proxy.transform.position = new Vector3 ( newPosX, newPosY, curPosition.z );*/
        }
    }
    #endregion

    private void LinkGateOperator_Operator(Pin start, Pin end)
    {
        ProxyABTransition startTransitionParent;
        ProxyABOperator endOpeParent;

        startTransitionParent = start.GetComponentInParent<ProxyABTransition>();
        endOpeParent = end.GetComponentInParent<ProxyABOperator>();
        startTransitionParent.Transition.Condition = new AB_BoolGate_Operator();
        startTransitionParent.Transition.Condition.Inputs[0] = (ABNode)endOpeParent.AbOperator;
    }

    private void LinkGateOperator_Param(Pin start, Pin end)
    {
        ProxyABTransition startTransitionParent;
        ProxyABParam endParamParent;

        startTransitionParent = start.GetComponentInParent<ProxyABTransition>();
        endParamParent = end.GetComponentInParent<ProxyABParam>();
        startTransitionParent.Transition.Condition = new AB_BoolGate_Operator();
        startTransitionParent.Transition.Condition.Inputs[0] = (ABNode)endParamParent.AbParam;
    }

    private void LinkAction_Operator(Pin start, Pin end)
    {
        ProxyABAction startActionParent;
        ProxyABOperator endOpeParent;

        startActionParent = start.GetComponentInParent<ProxyABAction>();
        endOpeParent = end.GetComponentInParent<ProxyABOperator>();
        startActionParent.AbState.Action.Parameters[start.Pin_order.OrderPosition - 1].Inputs[0] = (ABNode)endOpeParent.AbOperator;
    }

    private void LinkAction_Param(Pin start, Pin end)
    {
        ProxyABAction startActionParent;
        ProxyABParam endParamParent;

        startActionParent = start.GetComponentInParent<ProxyABAction>();
        endParamParent = end.GetComponentInParent<ProxyABParam>();
        startActionParent.AbState.Action.Parameters[start.Pin_order.OrderPosition - 1].Inputs[0] = (ABNode)endParamParent.AbParam;
    }

    private void LinkOperator_Operator(Pin income, Pin outcome)
    {
        ProxyABOperator incomeOpeParent;
        ProxyABOperator outcomeOpeParent;

        incomeOpeParent = income.GetComponentInParent<ProxyABOperator>();
        outcomeOpeParent = outcome.GetComponentInParent<ProxyABOperator>();

        int availeblePin = incomeOpeParent.GetAvailablePinEnter();

        if (availeblePin != -1)
        {
            if (incomeOpeParent.Inputs.Length <= 3)
            {
                incomeOpeParent.Inputs[income.Pin_order.OrderPosition - 1] = (ABNode)outcomeOpeParent.AbOperator;
            }
            else
            {
                incomeOpeParent.Inputs[availeblePin] = (ABNode)outcomeOpeParent.AbOperator;
            }

            ((ABNode)outcomeOpeParent.AbOperator).Output = (ABNode)incomeOpeParent.AbOperator;
        }
    }

    private void LinkOperator_Param(Pin ope, Pin param)
    {
        ProxyABOperator opeParent;
        ProxyABParam paramParent;

        opeParent = ope.GetComponentInParent<ProxyABOperator>();
        paramParent = param.GetComponentInParent<ProxyABParam>();

        int availeblePin = opeParent.GetAvailablePinEnter();
        if (availeblePin != -1)
        {
            if (opeParent.Inputs.Length <= 3)
            {
                opeParent.Inputs[ope.Pin_order.OrderPosition - 1] = (ABNode)paramParent.AbParam;
            }
            else
            {
                opeParent.Inputs[availeblePin] = (ABNode)paramParent.AbParam;
            }
        }
        opeParent.CurPinIn++;
        ((ABNode)paramParent.AbParam).Output = (ABNode)opeParent.AbOperator;
    }

    private ABTransition LinkState_State(Pin stateIn, Pin stateOut)
    {
        ProxyABState inParent;
        ProxyABState outParent;

        inParent = stateIn.GetComponentInParent<ProxyABState>();
        outParent = stateOut.GetComponentInParent<ProxyABState>();

        int transitionId = AbModel.LinkStates(outParent.AbState.Name, inParent.AbState.Name);
        return AbModel.getTransition(transitionId);
    }

    private ABTransition LinkState_Action(Pin state, Pin action)
    {
        ProxyABState stateParent;
        ProxyABAction actionParent;

        stateParent = state.GetComponentInParent<ProxyABState>();
        actionParent = action.GetComponentInParent<ProxyABAction>();

        int transitionId = AbModel.LinkStates(stateParent.AbState.Name, actionParent.AbState.Name);
        return AbModel.getTransition(transitionId);
    }

    #region EDITOR FUNCTIONS
    void CreateTransition(Pin start, Pin end)
    {
        ProxyABAction startActionParent;
        ProxyABState startStateParent;
        ProxyABState endStateParent;
        ABTransition createdTransition = null;  // Stores the ABtransition between states

        // Action; State; Param; Operator; Transition

        // IN
        // action 	<- state | operator | param(gate)
        // state 	<- state
        // operator	<- param | operator
        // param 	<- 
        // transition <- operator | param(gate)

        // OUT
        // action 	->
        // state 	-> action | state
        // operator	-> action | operator | transition
        // param	-> operator | action(gate) | transition(gate)
        // transition -> 

        bool validTransition = false;
        bool invalidTransition_HasTransition = false;
        bool invalidTransitionType = false;

        bool activateTypeValidation = true; // Flag used to activate TYPE VALIDATION

        // Verify if Pin has already a transition (only ABStar is accepted)
        //if (
        //    // Start
        //    (
        //        start.AssociatedTransitions.Count > 0 &&                                                                                                                                    // Existing Transition
        //        !(start.Pin_Type == Pin.PinType.OperatorIn && ABStar<ABBool>.isStar(((ProxyABOperator)start.ProxyParent).AbOperator.getIncomeType(start.Pin_order.OrderPosition - 1)))  // ABStar
        //    ) ||
        //    (
        //        end.AssociatedTransitions.Count > 0 &&                                                                                                                                  // Existing Transition
        //        !(end.Pin_Type == Pin.PinType.OperatorIn && ABStar<ABBool>.isStar(((ProxyABOperator)end.ProxyParent).AbOperator.getIncomeType(end.Pin_order.OrderPosition - 1)))    // ABStar
        //    )
        //)
        //{
        //    invalidTransition_HasTransition = true;
        //}

        if (!invalidTransition_HasTransition)
        {
            if (start.Pin_Type == Pin.PinType.Condition)
            {
                if (end.Pin_Type == Pin.PinType.OperatorOut)
                {
                    // Check type
                    // Condition accepts only bool values
                    System.Type opType = ((ProxyABOperator)end.ProxyParent).getOutcomeType();
                    if (!activateTypeValidation || opType == typeof(ABBool))
                    {
                        LinkGateOperator_Operator(start, end);
                        validTransition = true;
                    }
                    else
                    {
                        invalidTransitionType = true;
                    }
                }
                else if (end.Pin_Type == Pin.PinType.Param)
                {
                    // Check type
                    // Condition accepts only bool values
                    System.Type paramType = ((ProxyABParam)end.ProxyParent).getOutcomeType();
                    if (!activateTypeValidation || paramType == typeof(ABBool))
                    {
                        LinkGateOperator_Param(start, end);
                        validTransition = true;
                    }
                    else
                    {
                        invalidTransitionType = true;
                    }
                }
                else
                {
                    Debug.LogError("Un Pin Bool Gate Operator ne prend pas en entrée un pin de type " + end.Pin_Type.ToString());
                }
            }
            else if (start.Pin_Type == Pin.PinType.ActionParam)
            {
                System.Type actionParamType = ((ProxyABAction)start.ProxyParent).getParamOperator(start.Pin_order.OrderPosition - 1).getOutcomeType();
                if (end.Pin_Type == Pin.PinType.OperatorOut)
                {
                    // Check type
                    System.Type opType = ((ProxyABOperator)end.ProxyParent).getOutcomeType();
                    if (!activateTypeValidation || actionParamType == opType)
                    {
                        LinkAction_Operator(start, end);
                        validTransition = true;
                    }
                    else
                    {
                        invalidTransitionType = true;
                    }
                }
                else if (end.Pin_Type == Pin.PinType.Param)
                {
                    // Check type
                    System.Type paramType = ((ProxyABParam)end.ProxyParent).getOutcomeType();
                    if (!activateTypeValidation || actionParamType == paramType)
                    {
                        LinkAction_Param(start, end);
                        validTransition = true;
                    }
                    else
                    {
                        invalidTransitionType = true;
                    }
                }
                else
                {
                    Debug.LogError("Un Pin Gate Operator ne prend pas en entrée un pin de type " + end.Pin_Type.ToString());
                }
            }
            else if (start.Pin_Type == Pin.PinType.OperatorIn)
            {
                // Check type
                //System.Type opStartType = ((ProxyABOperator)start.ProxyParent).AbOperator.getIncomeType( start.Pin_order.OrderPosition-1 );
                if (end.Pin_Type == Pin.PinType.OperatorOut)
                {
                    // Check type
                    System.Type opEndType = ((ProxyABOperator)end.ProxyParent).getOutcomeType();
                    if (!activateTypeValidation || ((ProxyABOperator)start.ProxyParent).AbOperator.acceptIncome(start.Pin_order.OrderPosition - 1, opEndType))
                    {
                        LinkOperator_Operator(start, end);
                        validTransition = true;
                    }
                    else
                    {
                        invalidTransitionType = true;
                    }
                }
                else if (end.Pin_Type == Pin.PinType.Param)
                {
                    System.Type paramType = ((ProxyABParam)end.ProxyParent).getOutcomeType();
                    if (!activateTypeValidation || ((ProxyABOperator)start.ProxyParent).AbOperator.acceptIncome(start.Pin_order.OrderPosition - 1, paramType))
                    {
                        LinkOperator_Param(start, end);
                        validTransition = true;
                    }
                    else
                    {
                        invalidTransitionType = true;
                    }
                }
                else
                {
                    Debug.LogError("Un Pin OperatorIn ne prend pas en entrée un pin de type " + end.Pin_Type.ToString());
                }
            }
            else if (start.Pin_Type == Pin.PinType.OperatorOut)
            {
                // Check type
                System.Type opStartType = ((ProxyABOperator)start.ProxyParent).getOutcomeType();
                if (end.Pin_Type == Pin.PinType.OperatorIn)
                {
                    // Check type
                    if (!activateTypeValidation || ((ProxyABOperator)end.ProxyParent).AbOperator.acceptIncome(end.Pin_order.OrderPosition - 1, opStartType))
                    {
                        LinkOperator_Operator(end, start);
                        validTransition = true;
                    }
                    else
                    {
                        invalidTransitionType = true;
                    }
                }
                else if (end.Pin_Type == Pin.PinType.Condition)
                {
                    // Check type
                    // Condition accepts only bool values
                    if (!activateTypeValidation || opStartType == typeof(ABBool))
                    {
                        LinkGateOperator_Operator(end, start);
                        validTransition = true;
                    }
                    else
                    {
                        invalidTransitionType = true;
                    }
                }
                else if (end.Pin_Type == Pin.PinType.ActionParam)
                {
                    System.Type actionParamType = ((ProxyABAction)end.ProxyParent).getParamOperator(end.Pin_order.OrderPosition - 1).getOutcomeType();
                    if (!activateTypeValidation || opStartType == actionParamType)
                    {
                        LinkAction_Operator(end, start);
                        validTransition = true;
                    }
                    else
                    {
                        invalidTransitionType = true;
                    }
                }
                else
                {
                    Debug.LogError("Un Pin OperatorOut ne prend pas en entrée un pin de type " + end.Pin_Type.ToString());
                }
            }
            else if (start.Pin_Type == Pin.PinType.Param)
            {
                System.Type paramType = ((ProxyABParam)start.ProxyParent).getOutcomeType();
                if (end.Pin_Type == Pin.PinType.OperatorIn)
                {
                    if (!activateTypeValidation || ((ProxyABOperator)end.ProxyParent).AbOperator.acceptIncome(end.Pin_order.OrderPosition - 1, paramType))
                    {
                        LinkOperator_Param(end, start);
                        validTransition = true;
                    }
                    else
                    {
                        invalidTransitionType = true;
                    }
                }
                else if (end.Pin_Type == Pin.PinType.Condition)
                {
                    // Check type
                    // Condition accepts only bool values
                    if (!activateTypeValidation || paramType == typeof(ABBool))
                    {
                        LinkGateOperator_Param(end, start);
                        validTransition = true;
                    }
                    else
                    {
                        invalidTransitionType = true;
                    }
                }
                else if (end.Pin_Type == Pin.PinType.ActionParam)
                {
                    System.Type actionParamType = ((ProxyABAction)end.ProxyParent).getParamOperator(end.Pin_order.OrderPosition - 1).getOutcomeType();
                    if (!activateTypeValidation || paramType == actionParamType)
                    {
                        LinkAction_Param(end, start);
                        validTransition = true;
                    }
                    else
                    {
                        invalidTransitionType = true;
                    }
                }
                else
                {
                    Debug.LogError("Un Pin Param ne prend pas en entrée un pin de type " + end.Pin_Type.ToString());
                }
            }
            else if (start.Pin_Type == Pin.PinType.TransitionIn)
            {
                if (end.Pin_Type == Pin.PinType.TransitionOut)
                {
                    startStateParent = start.GetComponentInParent<ProxyABState>();
                    // ACTION -> 
                    if (!startStateParent)
                    {
                        startActionParent = start.GetComponentInParent<ProxyABAction>();
                        endStateParent = end.GetComponentInParent<ProxyABState>();
                        // ACTION -> ACTION : IMPOSSIBLE
                        if (!endStateParent)
                        {
                            Debug.LogError("Action -> Action n'existe pas");
                        }
                        // ACTION -> STATE
                        else
                        {
                            createdTransition = LinkState_Action(end, start);
                            validTransition = true;
                        }
                    }
                    // STATE -> 
                    else
                    {
                        endStateParent = end.GetComponentInParent<ProxyABState>();
                        // STATE -> ACTION
                        if (!endStateParent)
                        {
                            Debug.LogError("State -> Action n'existe pas");
                        }
                        // STATE -> STATE
                        else
                        {
                            createdTransition = LinkState_State(start, end);
                            validTransition = true;
                        }
                    }
                }
            }
            // STATE ->
            else if (start.Pin_Type == Pin.PinType.TransitionOut)
            {
                if (end.Pin_Type == Pin.PinType.TransitionIn)
                {
                    startStateParent = start.GetComponentInParent<ProxyABState>();
                    endStateParent = end.GetComponentInParent<ProxyABState>();
                    // STATE -> STATE
                    if (endStateParent)
                    {
                        createdTransition = LinkState_State(end, start);
                        validTransition = true;
                    }
                    // STATE -> ACTION
                    else
                    {
                        createdTransition = LinkState_Action(start, end);
                        validTransition = true;
                    }
                }
                else
                {
                    Debug.LogError("Un Pin TransitionOut ne prend pas en entrée un pin de type " + end.Pin_Type.ToString());
                }
            }
        }

        // Create transition
        if (validTransition
            && !invalidTransitionType
            && !invalidTransition_HasTransition
        )
        {
            // create transition proxy
            ProxyABTransition trans = MCEditor_Proxy_Factory.instantiateTransition(start, end, false);


            // Associate transition
            if (createdTransition != null)
            {
                trans.Transition = createdTransition;
                ProxyABTransition.addConditionPin(trans);
            }

            // Checking pins in states
            if (start.ProxyParent is ProxyABState)
            {
                ((ProxyABState)start.ProxyParent).checkPins();
            }
            if (end.ProxyParent is ProxyABState)
            {
                ((ProxyABState)end.ProxyParent).checkPins();
            }
            MCToolManager.instance.TemporarySave();
        }
        else
        {
            if (invalidTransitionType)
            {
                Debug.LogError("Throw Exception : transition not authorized due the types of pins");
            }
            else if (invalidTransition_HasTransition)
            {
                Debug.LogError("Throw Exception : transition not authorized : trying to add transition to a pin which already has one");
            }
            else
            {
                Debug.LogError("Throw Exception : transition not authorized due the IN/OUT status of pins");
            }
        }
    }

    ProxyABAction CreateAction(ABAction action, int nodeID, string nodeName)
    {
        ABState state = new ABState(nodeID, nodeName);
        return MCEditor_Proxy_Factory.instantiateAction(state);
    }

    ProxyABOperator CreateOperator(IABOperator op)
    {
        ProxyABOperator ope = MCEditor_Proxy_Factory.instantiateOperator(op, false);
        proxyOperators.Add(ope);
        return ope;
    }

    ProxyABParam CreateParam(IABParam p)
    {
        ProxyABParam param = MCEditor_Proxy_Factory.instantiateParam(p, false);
        proxyParams.Add(param);
        return param;
    }

    void Select()
    {

    }
    public void exchangeTransitionPosition(ABTransition transitionA, ABTransition transitionB)
    {
        abModel.exchangeTransitionPositions(transitionA, transitionB);
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
    #endregion

    #region DISPLAY FUNCTIONS
    private void RemoveTransitionSyntTree(ProxyABTransition transition)
    {
        Pin start = transition.StartPosition;
        Pin end = transition.EndPosition;

        ProxyABOperator proxyOpeStart = null;
        ProxyABOperator proxyOpeEnd = null;
        ProxyABParam proxyParam = null;
        ProxyABAction proxyAction = null;
        ProxyABTransition proxyTransition = null;

        /* START TRANSITION CASES */
        if (start.Pin_Type == Pin.PinType.Condition)
        {
            proxyTransition = start.GetComponentInParent<ProxyABTransition>();

            /* TRANSITION -> OPERATOR */
            if (end.Pin_Type == Pin.PinType.OperatorOut)
            {
                proxyOpeEnd = end.GetComponentInParent<ProxyABOperator>();
                UnlinkTransition_Operator(proxyTransition, proxyOpeEnd);
            }
            /* TRANSITION -> PARAM */
            else if (end.Pin_Type == Pin.PinType.Param)
            {
                proxyParam = end.GetComponentInParent<ProxyABParam>();
                UnlinkTransition_Param(proxyTransition, proxyParam);
            }
        }
        /*START ACTION CASES*/
        else if (start.Pin_Type == Pin.PinType.ActionParam)
        {
            proxyAction = start.GetComponentInParent<ProxyABAction>();
            /*ACTION -> PARAM */
            if (end.Pin_Type == Pin.PinType.Param)
            {
                proxyParam = end.GetComponentInParent<ProxyABParam>();
                UnlinkAction_Param(proxyAction, proxyParam);
            }
            /*ACTION -> OPERATOR */
            else if (end.Pin_Type == Pin.PinType.OperatorIn || end.Pin_Type == Pin.PinType.OperatorOut)
            {
                proxyOpeEnd = end.GetComponentInParent<ProxyABOperator>();
                UnlinkAction_Operator(proxyAction, proxyOpeEnd);
            }
        }
        /* START OPERATOR CASES */
        else if (start.Pin_Type == Pin.PinType.OperatorIn || start.Pin_Type == Pin.PinType.OperatorOut)
        {
            proxyOpeStart = start.GetComponentInParent<ProxyABOperator>();
            /* OPERATOR -> PARAM */
            if (end.Pin_Type == Pin.PinType.Param)
            {
                proxyParam = end.GetComponentInParent<ProxyABParam>();
                UnlinkOperator_Param(proxyOpeStart, proxyParam);
            }
            /* OPERATOR -> OPERATOR */
            else if (end.Pin_Type == Pin.PinType.OperatorIn || end.Pin_Type == Pin.PinType.OperatorOut)
            {
                proxyOpeEnd = end.GetComponentInParent<ProxyABOperator>();
                UnlinkOperator_Operator(proxyOpeStart, proxyOpeEnd);
            }
            /* OPERATOR -> ACTION */
            else if (end.Pin_Type == Pin.PinType.ActionParam)
            {
                proxyAction = end.GetComponentInParent<ProxyABAction>();
                UnlinkAction_Operator(proxyAction, proxyOpeStart);
            }
            /* OPERATOR -> TRANSITION */
            else if (end.Pin_Type == Pin.PinType.Condition)
            {
                proxyTransition = end.GetComponentInParent<ProxyABTransition>();
                UnlinkTransition_Operator(proxyTransition, proxyOpeStart);
            }
        }
        /* START PARAM CASES */
        else if (start.Pin_Type == Pin.PinType.Param)
        {
            proxyParam = start.GetComponentInParent<ProxyABParam>();
            /* PARAM -> OPERATOR */
            if (end.Pin_Type == Pin.PinType.OperatorIn)
            {
                proxyOpeEnd = end.GetComponentInParent<ProxyABOperator>();
                UnlinkOperator_Param(proxyOpeEnd, proxyParam);

            }
            /* PARAM -> TRANSITION */
            else if (end.Pin_Type == Pin.PinType.Condition)
            {
                proxyTransition = end.GetComponentInParent<ProxyABTransition>();
                UnlinkTransition_Param(proxyTransition, proxyParam);
            }
        }         /* END OPERATOR CASES */
        else if (end.Pin_Type == Pin.PinType.OperatorIn || end.Pin_Type == Pin.PinType.OperatorOut)
        {
            proxyOpeEnd = end.GetComponentInParent<ProxyABOperator>();
            /* PARAM -> OPERATOR */
            if (start.Pin_Type == Pin.PinType.Param)
            {
                proxyParam = start.GetComponentInParent<ProxyABParam>();
                UnlinkOperator_Param(proxyOpeEnd, proxyParam);
            }
            /* OPERATOR -> OPERATOR */
            else if (start.Pin_Type == Pin.PinType.OperatorIn || start.Pin_Type == Pin.PinType.OperatorOut)
            {
                proxyOpeEnd = start.GetComponentInParent<ProxyABOperator>();
                UnlinkOperator_Operator(proxyOpeStart, proxyOpeEnd);
            }
            /* ACTION -> OPERATOR */
            else if (start.Pin_Type == Pin.PinType.ActionParam)
            {
                proxyAction = end.GetComponentInParent<ProxyABAction>();
                UnlinkAction_Operator(proxyAction, proxyOpeEnd);
            }
        }
    }

    private void UnlinkTransition_Operator(ProxyABTransition proxyTransition, ProxyABOperator proxyOpe)
    {
        for (int i = 0; i < proxyTransition.Transition.Condition.Inputs.Length; i++)
        {
            if (proxyTransition.Transition.Condition.Inputs[i] == (ABNode)proxyOpe.AbOperator)
            {
                proxyTransition.Transition.Condition.Inputs[i] = null;
            }
        }
    }

    private void UnlinkTransition_Param(ProxyABTransition proxyTransition, ProxyABParam proxyParam)
    {
        for (int i = 0; i < proxyTransition.Transition.Condition.Inputs.Length; i++)
        {
            if (proxyTransition.Transition.Condition.Inputs[i] == (ABNode)proxyParam.AbParam)
            {
                proxyTransition.Transition.Condition.Inputs[i] = null;
            }
        }
    }

    private void UnlinkAction_Param(ProxyABAction proxyAction, ProxyABParam proxyParam)
    {
        for (int j = 0; j < proxyAction.AbState.Action.Parameters.Length; j++)
        {
            for (int i = 0; i < proxyAction.AbState.Action.Parameters[j].Inputs.Length; i++)
            {
                if (proxyAction.AbState.Action.Parameters[j].Inputs[i] == ((ABNode)proxyParam.AbParam))
                {
                    proxyAction.AbState.Action.Parameters[j].Inputs[i] = null;
                }
            }
        }

        if (((ABNode)proxyParam.AbParam).Output != null)
        {
            ((ABNode)proxyParam.AbParam).Output = null;
        }
    }

    private void UnlinkAction_Operator(ProxyABAction proxyAction, ProxyABOperator proxyOperator)
    {
        for (int j = 0; j < proxyAction.AbState.Action.Parameters.Length; j++)
        {

            for (int i = 0; i < proxyAction.AbState.Action.Parameters[j].Inputs.Length; i++)
            {
                if (proxyAction.AbState.Action.Parameters[j].Inputs[i] == ((ABNode)proxyOperator.AbOperator))
                {
                    proxyAction.AbState.Action.Parameters[j].Inputs[i] = null;
                }
            }
        }

        if (((ABNode)proxyOperator.AbOperator).Output != null)
        {
            ((ABNode)proxyOperator.AbOperator).Output = null;
        }
    }

    private void UnlinkOperator_Operator(ProxyABOperator proxyOpeStart, ProxyABOperator proxyOpeEnd)
    {
        bool startIsChanged = false;
        bool endIsChanged = false;

        for (int i = 0; i < proxyOpeStart.Inputs.Length; i++)
        {
            if (proxyOpeStart.Inputs[i] == ((ABNode)(proxyOpeEnd.AbOperator)))
            {
                proxyOpeStart.Inputs[i] = null;
                startIsChanged = true;
            }
        }
        for (int i = 0; i < proxyOpeEnd.Inputs.Length; i++)
        {
            if (proxyOpeEnd.Inputs[i] == ((ABNode)(proxyOpeStart.AbOperator)))
            {
                proxyOpeEnd.Inputs[i] = null;
                endIsChanged = true;
            }
        }
        if (startIsChanged)
        {
            if (proxyOpeStart.AbOperator.GetType().ToString().Contains("Agg"))
            {
                proxyOpeStart.AbOperator.Inputs = RebuiltInputTableForAggOperator(proxyOpeStart.AbOperator.Inputs);
            }
        }
        if (endIsChanged)
        {
            if (proxyOpeEnd.AbOperator.GetType().ToString().Contains("Agg"))
            {
                proxyOpeEnd.AbOperator.Inputs = RebuiltInputTableForAggOperator(proxyOpeEnd.AbOperator.Inputs);
            }
        }
    }

    private void UnlinkOperator_Param(ProxyABOperator proxyOpeStart, ProxyABParam proxyParam)
    {
        for (int i = 0; i < proxyOpeStart.AbOperator.Inputs.Length; i++)
        {
            ABNode node = proxyOpeStart.AbOperator.Inputs[i];
            if (node != null)
            {
                if (node is IABParam)
                {
                    if (((IABParam)(node)) == proxyParam.AbParam)
                    {
                        node.Output = null;
                        proxyOpeStart.AbOperator.Inputs[i] = null;
                    }
                }
            }
        }
        if (proxyOpeStart.AbOperator.GetType().ToString().Contains("Agg"))
        {
            proxyOpeStart.AbOperator.Inputs = RebuiltInputTableForAggOperator(proxyOpeStart.AbOperator.Inputs);
        }
    }

    private ABNode[] RebuiltInputTableForAggOperator(ABNode[] inputs)
    {
        ABNode[] result = new ABNode[32];
        int i = 0;
        foreach (ABNode node in inputs)
        {
            if (node != null)
            {
                result[i] = node;
                i++;
            }
        }
        return result;
    }

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

    public void DisplayParameters()
    {
        for (int i = 0; i < proxyParams.Count; i++)
        {
            proxyParams[i].transform.position = new Vector3(proxyOperators[i].transform.position.x + UnityEngine.Random.Range(-5, 5), proxyParams[i].transform.position.y + UnityEngine.Random.Range(-5, 5), proxyParams[i].transform.position.z);
        }
    }
    #endregion

    #region Delete Model
    void DeleteTransition(ProxyABTransition transition)
    {
        if (transition != null)
        {
            int id_transition_to_remove = 0;
            if (transition.Transition != null)
            {
                id_transition_to_remove = transition.Transition.Id;
            }
            // Transition between Action/State and Action/State
            if (transition.Condition != null)
            {
                if (!(transition.StartPosition.Pin_Type == Pin.PinType.OperatorIn || transition.StartPosition.Pin_Type == Pin.PinType.OperatorOut || transition.StartPosition.Pin_Type == Pin.PinType.Param)
                    && !(transition.EndPosition.Pin_Type == Pin.PinType.OperatorIn || transition.EndPosition.Pin_Type == Pin.PinType.OperatorOut || transition.EndPosition.Pin_Type == Pin.PinType.Param))
                {
                    AbModel.UnlinkStates(transition.Transition.Start.Name, transition.Transition.End.Name);
                    // Update Pins
                    if (transition.StartPosition.ProxyParent is ProxyABState && transition.StartPosition.Pin_Type == Pin.PinType.TransitionOut)
                    {
                        ((ProxyABState)transition.StartPosition.ProxyParent).checkPins();
                    }
                    if (transition.EndPosition.ProxyParent is ProxyABState && transition.EndPosition.Pin_Type == Pin.PinType.TransitionOut)
                    {
                        ((ProxyABState)transition.EndPosition.ProxyParent).checkPins();
                    }
                    // Delete Condition transition
                    if (transition.Condition.AssociatedTransitions.Count > 0)
                    {
                        foreach (ProxyABTransition trans in transition.Condition.AssociatedTransitions)
                        {
                            DeleteTransition(trans);
                        }
                    }
                }
                abModel.shiftIDTransition(id_transition_to_remove);
            }
            else
            {
                RemoveTransitionSyntTree(transition);
            }
            // Unlink            

            // Remove Pin
            // Destroy( transition.Condition.gameObject );
            // Destroy Object
            Destroy(transition.gameObject);

        }
    }

    void deleteTransitions(MCEditor_Proxy _proxy)
    {
        // remove transitions
        List<Pin> pins = _proxy.AllPins;
        foreach (Pin _pin in pins)
        {
            foreach (ProxyABTransition _transition in _pin.AssociatedTransitions)
            {
                DeleteTransition(_transition);
            }
        }
    }

    public void deleteProxy(ProxyABState _state)
    {
        // check init
        if (_state.AbState.Id != AbModel.InitStateId)
        {
            ABState state = AbModel.getState(_state.AbState.Id);

            // remove transitions
            deleteTransitions(_state);

            // Remove from model
            if (state != null)
            {
                AbModel.delete(state);
                proxyStates.Remove(_state);
                statesDictionnary.Remove(state);
            }

            Destroy(_state.gameObject);
        }
    }

    public void forcedeleteProxy(ProxyABState _state)
    {

        ABState state = AbModel.getState(_state.AbState.Id);

        // remove transitions
        deleteTransitions(_state);

        // Remove from model
        if (state != null)
        {
            AbModel.delete(state);
            proxyStates.Remove(_state);
            statesDictionnary.Remove(state);
        }

        Destroy(_state.gameObject);

    }
    public void deleteProxy(ProxyABAction _action)
    {
        ABState action = AbModel.getState(_action.AbState.Id);

        // remove transitions
        deleteTransitions(_action);

        // Remove from model
        if (action != null)
        {
            AbModel.delete(action);
            proxyActions.Remove(_action);
            actionsDictionnary.Remove(action);
        }

        Destroy(_action.gameObject);
    }
    public void deleteProxy(ProxyABOperator _operator)
    {
        // remove transitions
        deleteTransitions(_operator);

        // Remove from model
        proxyOperators.Remove(_operator);

        Destroy(_operator.gameObject);
    }
    public void deleteProxy(ProxyABParam _param)
    {
        // remove transitions
        deleteTransitions(_param);

        // Remove from model
        proxyParams.Remove(_param);

        Destroy(_param.gameObject);
    }
    #endregion

    #region Transition UI Create Delete
    Pin transition_Pin_Start = null;
    ProxyABTransition transition_Selected = null;

    public Pin Transition_Pin_Start
    {
        get
        {
            return transition_Pin_Start;
        }
    }
    public ProxyABTransition Transition_Selected
    {
        get
        {
            return transition_Selected;
        }
    }

    public void createTransition_setStartPin(Pin pin)
    {
        this.transition_Pin_Start = pin;
    }
    public void createTransition_setEndPin(Pin pin)
    {
        if (this.transition_Pin_Start != null && this.transition_Pin_Start != pin)
        {
            this.CreateTransition(this.transition_Pin_Start, pin);
            this.transition_Pin_Start = null;
        }
    }

    // delete
    public void selectTransition(ProxyABTransition transition)
    {
        this.transition_Selected = transition;
    }
    void deleteSelectedTransition()
    {
        if (this.transition_Selected != null)
        {
            this.DeleteTransition(this.transition_Selected);
            this.transition_Selected = null;
        }
    }
    #endregion

    #region Delete UI Proxy
    public void deleteSelectedProxies(List<MCEditor_Proxy> selectedProxies)
    {
        foreach (MCEditor_Proxy p in selectedProxies)
        {
            if (p != null)
            {
                p.deleteProxy();
            }
        }
    }
    #endregion

    #region ToolTips

    public void LoadActions_ParamsDatabase()
    {
        string path = Application.dataPath + @"/Inputs/HelpFiles/" + "Modele_Comportemental" + ".json";
        using (StreamReader stream = new StreamReader(path))
        {
            string json = stream.ReadToEnd();
            databaseActions_Params = JsonConvert.DeserializeObject<List<Help>>(json);
            stream.Close();
        }
    }

    public void LoadOperatorDatabase()
    {
        databaseOperators = new List<Help>();

        string path = Application.dataPath + @"/Inputs/HelpFiles/" + "Liste_operateurs" + ".json";
        using (StreamReader stream = new StreamReader(path))
        {
            string json = stream.ReadToEnd();
            foreach (Help paramHelp in JsonConvert.DeserializeObject<List<Help>>(json))
            {
                databaseOperators.Add(paramHelp);
            }
            stream.Close();
        }
        path = Application.dataPath + @"/Inputs/HelpFiles/" + "Liste_composites" + ".json";
        using (StreamReader stream = new StreamReader(path))
        {
            string json = stream.ReadToEnd();
            foreach (Help paramHelp in JsonConvert.DeserializeObject<List<Help>>(json))
            {
                databaseOperators.Add(paramHelp);
            }
            stream.Close();
        }
    }

    // ToolTip for Pin
    public void ShowToolTip(string text, Vector3 pos)
    {
        toolTipText.gameObject.transform.position = new Vector3(pos.x, pos.y, -2);
        toolTipText.text = text;
    }

    // ToolTip for Node
    public void instantiateToolTip(Vector3 position, string type, MCEditor_Proxy proxy)
    {
        toolTipTextTab[0].text = "";
        toolTipTextTab[1].text = "";
        toolTipTextTab[2].text = "";
        toolTipTextTab[3].text = "";
        toolTipTextTab[4].text = "";

        if (type.Contains("Action"))
        {
            FillActionToolTip(((ProxyABAction)proxy).AbState);
        }
        else if (type.Contains("Param"))
        {
            FillParamToolTip((IABParam)((ProxyABParam)proxy).AbParam);
        }
        else if (type.Contains("Operator"))
        {
            FillOperatorToolTip((IABOperator)((ProxyABOperator)proxy).AbOperator);
        }
    }

    public void instantiateToolTip(Vector3 position, System.Object item)
    {
        toolTipTextTab[0].text = "";
        toolTipTextTab[1].text = "";
        toolTipTextTab[2].text = "";
        toolTipTextTab[3].text = "";
        toolTipTextTab[4].text = "";

        if (item is ABState)
        {
            if (((ABState)item).Action != null)
                FillActionToolTip((ABState)item);
        }
        else if (item is IABParam)
        {
            FillParamToolTip((IABParam)item);
        }
        else if (item is IABOperator)
        {
            FillOperatorToolTip((IABOperator)item);
        }
    }

    private void FillActionToolTip(ABState proxy)
    {
        int index_i = -1;
        int index_j = -1;
        for (int i = 0; i < databaseActions_Params.Count; i++)
        {
            if (databaseActions_Params[i].Title == "Actions")
            {
                for (int j = 0; j < databaseActions_Params[i].Content.Count; j++)
                {
                    if (proxy.Action.GetType().ToString().Contains("Goto"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "goto")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                    else if (proxy.Action.GetType().ToString().Contains("Strike"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "strike")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                    else if (proxy.Action.GetType().ToString().Contains("Pick"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "pick")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                    else if (proxy.Action.GetType().ToString().Contains("Drop"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "drop")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                    else if (proxy.Action.GetType().ToString().Contains("Lay"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "lay")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                    else if (proxy.Action.GetType().ToString().Contains("Trace"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "trace")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                    else if (proxy.Action.GetType().ToString().Contains("Roaming"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "random roaming")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                }
            }
        }
        if (index_i > -1 && index_j > -1)
        {            
            toolTipTextTab[0].text = databaseActions_Params[index_i].Content[index_j].SubTitle;
            toolTipTextTab[1].text = databaseActions_Params[index_i].Content[index_j].Content;
        }
    }

    private void FillRefToolTip(IABParam proxy)
    {
        int index_j = -1;
        int index_i = -1;

        for (int i = 0; i < databaseActions_Params.Count; i++)
        {
            if (databaseActions_Params[i].Title == "Références")
            {
                for (int j = 0; j < databaseActions_Params[i].Content.Count; j++)
                {
                    if (((IABParam)proxy).Identifier.Contains("game"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "Game")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                    else if (((IABParam)proxy).Identifier.Contains("home"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "Home")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                    else if (((IABParam)proxy).Identifier.Contains("self"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "Self")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                    else if (((IABParam)proxy).Identifier.Contains("allies"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "Allies[]")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                    else if (((IABParam)proxy).Identifier.Contains("enemies"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "Enemies[]")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                    else if (((IABParam)proxy).Identifier.Contains("trace"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "Trace[]")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                    else if (((IABParam)proxy).Identifier.Contains("resources"))
                    {
                        if (databaseActions_Params[i].Content[j].SubTitle == "Ressources[]")
                        {
                            index_j = j;
                            index_i = i;
                        }
                    }
                }
            }
        }
        if (index_i > -1 && index_j > -1)
        {
            toolTipTextTab[0].text = databaseActions_Params[index_i].Content[index_j].SubTitle;
            toolTipTextTab[1].text = databaseActions_Params[index_i].Content[index_j].Content;
        }
    }

    private void FillParamToolTip(IABParam proxy)
    {
        int index_i = -1;
        int index_j = -1;

        if (proxy.GetType().ToString().Contains("Ref"))
        {
            FillRefToolTip(proxy);
        }
        else
        {
            for (int i = 0; i < databaseActions_Params.Count; i++)
            {
                if (databaseActions_Params[i].Title == "Paramètres")
                {
                    for (int j = 0; j < databaseActions_Params[i].Content.Count; j++)
                    {
                        if (proxy.GetType().ToString().Contains("Scal"))
                        {
                            if (databaseActions_Params[i].Content[j].SubTitle == "Scalaire")
                            {
                                index_j = j;
                                index_i = i;
                            }
                        }
                        else if (proxy.GetType().ToString().Contains("Bool"))
                        {
                            if (databaseActions_Params[i].Content[j].SubTitle == "Booléen")
                            {
                                index_j = j;
                                index_i = i;
                            }
                        }
                        else if (proxy.GetType().ToString().Contains("Color"))
                        {
                            if (databaseActions_Params[i].Content[j].SubTitle == "Couleur")
                            {
                                index_j = j;
                                index_i = i;
                            }
                        }
                        else if (proxy.GetType().ToString().Contains("Vec"))
                        {
                            if (databaseActions_Params[i].Content[j].SubTitle == "Vecteur")
                            {
                                index_j = j;
                                index_i = i;
                            }
                        }
                        else if (proxy.GetType().ToString().Contains("Text") || (proxy.GetType().ToString().Contains("Txt")))
                        {
                            if (databaseActions_Params[i].Content[j].SubTitle == "Texte")
                            {
                                index_j = j;
                                index_i = i;
                            }
                        }
                    }
                }
            }
        }

        if (index_i > -1 && index_j > -1)
        {
            toolTipTextTab[0].text = databaseActions_Params[index_i].Content[index_j].SubTitle;
            toolTipTextTab[1].text = databaseActions_Params[index_i].Content[index_j].Content;
        }
    }

    private void FillOperatorToolTip(IABOperator proxy)
    {
        string proxyType = ((IABOperator)proxy).ClassName.Substring(3, ((IABOperator)proxy).ClassName.Length - 12);
        string proxyCompositeType = proxyType.Split('_')[1];
        bool isMacro = false;
        bool isContainingArg = false;

        foreach (Help categorie in databaseOperators)
        {
            foreach (SubHelp opeDescription in categorie.Content)
            {
                if (opeDescription.SubTitle == proxyType || opeDescription.SubTitle.ToLower() == proxyCompositeType.ToLower())
                {
                    toolTipTextTab[0].text = opeDescription.SubTitle;
                    string[] tabSplit = opeDescription.Content.Split('\n');
                    int nbArg = 0;

                    //Retour
                    toolTipTextTab[1].text = tabSplit[0];

                    //Symbol
                    if (tabSplit[1].Contains("Symbole"))
                    {
                        toolTipTextTab[2].text = tabSplit[1];
                    }
                    else
                    {
                        toolTipTextTab[2].text = "Macro Operateur";
                        isMacro = true;
                    }
                    //Arguments
                    toolTipTextTab[3].text = "";
                    foreach (string arg in tabSplit)
                    {
                        if (arg.Contains("Argument"))
                        {
                            nbArg++;
                            toolTipTextTab[3].text += " " + arg;
                            isContainingArg = true;
                        }
                    }
                    //Description
                    if (isMacro)
                    {
                        if (isContainingArg)
                        {
                            toolTipTextTab[4].text = tabSplit[1 + nbArg];
                        }
                        else
                        {
                            toolTipTextTab[4].text = tabSplit[1];
                        }
                    }
                    else
                    {
                        toolTipTextTab[4].text = tabSplit[2 + nbArg];
                    }
                }
            }
        }
    }
    #endregion
}
