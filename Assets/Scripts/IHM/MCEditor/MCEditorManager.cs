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

	[SerializeField]
	private GameObject MC_Container;	// Parent for the MC
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

    [SerializeField]
    private string MC_OrigFilePath = "Assets/Inputs/Test/GREG_TRANS_STATE_STATE_TEST.csv";/* siu_scoot_behavior_LOAD_SAVE_TEST.csv"; /*ref_table_Test.txt"; /*GREG_TRANS_STATE_STATE_TEST.csv /*siu_scoot_behavior_LOAD_TEST.csv";*/

    /** START TEST SAVE**/
    ProxyABAction abAction = null;
    ProxyABAction abAction2 = null;
    ProxyABState abState = null;
    ProxyABState abState2 = null;
    ProxyABParam abParam = null;
    ProxyABOperator aBOperator = null;
    ProxyABOperator aBOperator2 = null;
    /** END TEST SAVE**/

	#region PROPERTIES
	public Transform MCparent {
		get {
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
        
        //usefull for save function
        opeFactory.CreateDictionnary();
        paramFactory.CreateDictionnary();

        operatorDictionary = opeFactory.TypeStringToString;
        paramDictionary = paramFactory.TypeStringToString;

		mCparent = (MC_Container == null ? this.transform : MC_Container.transform);

        /** LOAD MODEL AND CREATE PROXY OBJECTS **/
        SetupModel();
    }

    private void Update()
    {
        /**START TEST SAVE**/
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save_MC();
        } else if (Input.GetKeyDown(KeyCode.O))
        {
			CreateOperator( new AB_BoolGate_Operator() );
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
			ABText t = new ABText ();
			t.Value = "example";
			ABTextParam par = new ABTextParam ( "const", t );

            CreateParam( par );
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
			abState = MCEditor_Proxy_Factory.instantiateState (new ABState (-1, "State_1"), false);
			registerState (abState.AbState, abState);
			abState2 = MCEditor_Proxy_Factory.instantiateState (new ABState (-1, "State_2"), false);
			registerState (abState2.AbState, abState2);
        } else if (Input.GetKeyDown(KeyCode.R))
        {
            CreateTransition(abState.GetComponentInChildren<Pin>(), abState2.GetComponentInChildren<Pin>());
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
			ABState act = new ABState ( -1, "Action_1" );
			act.Action = new ABDropAction ();

			abState = MCEditor_Proxy_Factory.instantiateState (new ABState (-1, "State_1"), false);
			abAction = MCEditor_Proxy_Factory.instantiateAction ( act );
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            CreateTransition(abState.GetComponentInChildren<Pin>(), abAction.GetComponentInChildren<Pin>());
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
			ABText t = new ABText ();
			t.Value = "example";
			ABTextParam par = new ABTextParam ( "const", t );

			abParam = MCEditor_Proxy_Factory.instantiateParam( par, false );
			aBOperator = MCEditor_Proxy_Factory.instantiateOperator( new AB_BoolGate_Operator(), false );
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            CreateTransition(abParam.GetComponentInChildren<Pin>(), aBOperator.GetComponentInChildren<Pin>());
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
			aBOperator = MCEditor_Proxy_Factory.instantiateOperator( new AB_BoolGate_Operator(), false );
			aBOperator2 = MCEditor_Proxy_Factory.instantiateOperator( new AB_BoolGate_Operator(), false );
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            CreateTransition(aBOperator.GetComponentInChildren<Pin>(), aBOperator2.GetComponentInChildren<Pin>());
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
			ABState act = new ABState ( -1, "Action_1" );
			act.Action = new ABDropAction ();

			aBOperator = MCEditor_Proxy_Factory.instantiateOperator( new AB_BoolGate_Operator(), false );
			abAction = MCEditor_Proxy_Factory.instantiateAction( act );
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

	#region LOAD MODEL FUNCTIONS
    void LoadProxyStates() {
        foreach (ABState state in this.AbModel.States)
        {
			// Actions
            if (state.Action != null)
            {
				ProxyABAction proxyAction = MCEditor_Proxy_Factory.instantiateAction( state );
				this.registerAction ( state, proxyAction );

				// Create SyntaxTrees
				List<Pin> pins = proxyAction.getPins( Pin.PinType.ActionParam );
				if (state.Action.Parameters != null) {
					for (int i = 0; i < state.Action.Parameters.Length; i++) {
						IABGateOperator param = state.Action.Parameters [i];

						Pin start = pins [i];
						registerPin (start);

						// create syntaxTree for each Param
						foreach (ABNode node in param.Inputs) {
							Pin end = RecNodeSynthTree (node);
                            if (end != null)
                            {
								MCEditor_Proxy_Factory.instantiateTransition ( start, end, false );
                            }
                        }
					}
				}
            }
			// States
            else {
				ProxyABState proxyState = MCEditor_Proxy_Factory.instantiateState (state, abModel.InitStateId == state.Id );
				this.registerState ( state, proxyState );
            }
        }
    }

    //Load syntaxe Tree in a recursive way
	// return the outcome pin
    Pin RecNodeSynthTree(ABNode node)
    {
        if (node is IABOperator)
        {
			ProxyABOperator ope = MCEditor_Proxy_Factory.instantiateOperator( (IABOperator)node, true );
			registerOperator (ope);
		    
			// Creating params trees
			List<Pin> pins = ope.getPins( Pin.PinType.OperatorIn );
			for(int i=0; i<((IABOperator)node).Inputs.Length; i++){
				ABNode inputNode = ((IABOperator)node).Inputs [i];
				Pin start = pins[i];

                if(inputNode == null)
                {
                    break;
                }

                Pin end = RecNodeSynthTree(inputNode);

                if(end != null)
                {
					MCEditor_Proxy_Factory.instantiateTransition ( start, end, false );
                }
            }

			// Outcome
			return ope.getPins( Pin.PinType.OperatorOut )[0];
        }
        else if (node is IABParam)
        {
			ProxyABParam param = MCEditor_Proxy_Factory.instantiateParam ( (IABParam)node, true );
			registerParam ( param );

			return param.getPins ( Pin.PinType.Param ) [0];
        }
		return null;
    }

    void LoadProxyTransitions()
    {
        ProxyABTransition proxyABTransition;

        for (int i = 0; i < AbModel.Transitions.Count; i++) {
			List<Pin> pinList = LoadPinsStates(i);
			proxyABTransition = MCEditor_Proxy_Factory.instantiateTransition ( pinList[0], pinList[1], true );
			// todo ATTENTION
			proxyABTransition.Transition = AbModel.Transitions [i];

            if (AbModel.Transitions[i].Condition != null) {                
                Pin end = RecNodeSynthTree(AbModel.Transitions[i].Condition.Inputs[0]);
				MCEditor_Proxy_Factory.instantiateTransition ( proxyABTransition.Condition, end, false );
            }
        }         
    }

    List<Pin> LoadPinsStates(int curTransition)
    {
        List<Pin> pinList = new List<Pin>();

        ProxyABState startState = statesDictionnary[AbModel.Transitions[curTransition].Start];        

		// Instantiate OutCome Pin in the state
		pinList.Add( MCEditor_Proxy_Factory.instantiatePin ( Pin.PinType.TransitionOut, Pin.calculatePinPosition( startState.AbState, startState.gameObject, true, curTransition ), startState.transform ) );

        if (statesDictionnary.ContainsKey(AbModel.Transitions[curTransition].End)) {
            ProxyABState endState = statesDictionnary[AbModel.Transitions[curTransition].End];

			// Recovery income Pin
			pinList.Add( endState.getPins ( Pin.PinType.TransitionIn )[0] );
        }
        else if (actionsDictionnary.ContainsKey(AbModel.Transitions[curTransition].End)) {

            ProxyABAction endState = actionsDictionnary[AbModel.Transitions[curTransition].End];

			pinList.Add( endState.getPins ( Pin.PinType.TransitionIn )[0] );
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

                syntTreeContent.AppendLine(idNodeSyntTree + ",param{" + ((IABParam)node).Identifier + " " + type + "=" + value + "},");
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
			type = ((ABParam<ABTable<ABVec>>)node).Value.ToString();
		}
		else if (node is ABParam<ABTable<ABBool>>)
		{
			type = ((ABParam<ABTable<ABBool>>)node).Value.ToString();
		}
		else if (node is ABParam<ABTable<ABScalar>>)
		{
			type = ((ABParam<ABTable<ABScalar>>)node).Value.ToString();
		}
		else if (node is ABParam<ABTable<ABText>>)
		{
			type = ((ABParam<ABTable<ABText>>)node).Value.ToString();
		}
		else if (node is ABParam<ABTable<ABColor>>)
		{
			type = ((ABParam<ABTable<ABColor>>)node).Value.ToString();
		}
		else if (node is ABParam<ABTable<ABRef>>)
		{
			type = ((ABParam<ABTable<ABRef>>)node).Value.ToString();
		}

		return type;
	}

	public static string GetParamValue(ABNode node)
	{
		string text = "";
		if (node is ABParam<ABText>)
		{
			if (((ABParam<ABText>)node).Value != null) {
				text += ((ABParam<ABText>)node).Value.Value.ToString ();
			}
		}
		else if (node is ABParam<ABVec>)
		{
			if (((ABParam<ABVec>)node).Value != null) {
				text += ((ABParam<ABVec>)node).Value.X.ToString () + ";" + ((ABParam<ABVec>)node).Value.Y.ToString ();
			}
		}
		else if (node is ABParam<ABBool>)
		{
			if (((ABParam<ABBool>)node).Value != null) {
				text += ((ABParam<ABBool>)node).Value.Value.ToString ();
			}
		}
		else if (node is ABParam<ABRef>)
		{
			if (((ABParam<ABRef>)node).Value != null) {
				text += ((ABParam<ABRef>)node).Value.ToString ();
			}
		}
		else if (node is ABParam<ABColor>)
		{
			if (((ABParam<ABColor>)node).Value != null) {
				text += ((ABParam<ABColor>)node).Value.Value.ToString ();
			}
		}
		else if (node is ABParam<ABScalar>)
		{
			if (((ABParam<ABScalar>)node).Value != null) {
				text += ((ABParam<ABScalar>)node).Value.Value.ToString ();
			}
		}
		return text;
	}
	#endregion

	#region REGISTER PROXY
	// Action
	public void registerAction( ABState state, ProxyABAction action ){
		proxyActions.Add(action);
		actionsDictionnary.Add(state, action);
	}

	// State
	public void registerState( ABState state, ProxyABState proxyState ){
		proxyStates.Add( proxyState );
		statesDictionnary.Add(state, proxyState);
	}

	// Operator
	public void registerOperator( ProxyABOperator proxyOperator ){
		proxyOperators.Add( proxyOperator );
	}

	// Param
	public void registerParam( ProxyABParam proxyParam ){
		proxyParams.Add( proxyParam );
	}

	// Pin
	public void registerPin( Pin pin ){
		pins.Add (pin);
	}
	#endregion

    #region EDITOR FUNCTIONS
    void CreateTransition(Pin start, Pin end)
	{
		ProxyABTransition trans = MCEditor_Proxy_Factory.instantiateTransition ( start, end, false );

		ProxyABAction startActionParent;
		ProxyABState startStateParent;
		ProxyABAction endActionParent;
		ProxyABState endStateParent;

		ProxyABOperator startOpeParent;
		ProxyABParam startParamParent;
		ProxyABParam endParamParent;
		ProxyABOperator endOpeParent;

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

		int transitionId = -1;

		if (start.Pin_Type == Pin.PinType.ActionParam)
        {
            startActionParent = start.GetComponentInParent<ProxyABAction>();

			if (end.Pin_Type == Pin.PinType.OperatorIn || end.Pin_Type == Pin.PinType.OperatorOut)
            {
                endOpeParent = end.GetComponentInParent<ProxyABOperator>();
				//start.IsGateOperator = true;
				start.Pin_Type = Pin.PinType.OperatorIn;
                startActionParent.AbState.Action.Parameters[0].Inputs[0] = (ABNode)endOpeParent.AbOperator;               
			} else if (end.Pin_Type == Pin.PinType.Param)
            {
                endParamParent = end.GetComponentInParent<ProxyABParam>();
                //start.IsGateOperator = true;
				start.Pin_Type = Pin.PinType.OperatorIn;
                startActionParent.AbState.Action.Parameters[0].Inputs[0] = (ABNode)endParamParent.AbParam;
            }
			else if (end.Pin_Type == Pin.PinType.ActionParam)
            {
                endActionParent = end.GetComponentInParent<ProxyABAction>();                               
				ProxyABTransition.addConditionPin ( trans );
				transitionId = AbModel.LinkStates(startActionParent.AbState.Name, endActionParent.AbState.Name);
                trans.Transition = AbModel.getTransition(transitionId);
            }
            else //State case
            {
                endStateParent = end.GetComponentInParent<ProxyABState>();                
				ProxyABTransition.addConditionPin ( trans );
				transitionId = AbModel.LinkStates(startActionParent.AbState.Name, endStateParent.AbState.Name);
                trans.Transition = AbModel.getTransition(transitionId);
            }
			// TODO why this pin is created?
			Pin newPin = MCEditor_Proxy_Factory.instantiatePin(Pin.PinType.ActionParam, Pin.calculatePinPosition( Pin.PinType.ActionParam, startActionParent ), startActionParent.transform);
        }
		else if (start.Pin_Type == Pin.PinType.OperatorIn || start.Pin_Type == Pin.PinType.OperatorOut)
        {
            startOpeParent = start.GetComponentInParent<ProxyABOperator>();
			if (end.Pin_Type == Pin.PinType.OperatorIn || end.Pin_Type == Pin.PinType.OperatorOut)
            {
                endOpeParent = end.GetComponentInParent<ProxyABOperator>();
                //start.IsGateOperator = true;
				start.Pin_Type = Pin.PinType.OperatorIn;
                startOpeParent.Inputs[startOpeParent.Inputs.Length-1] = (ABNode)endOpeParent.AbOperator;
                ((ABNode)endOpeParent.AbOperator).Output = (ABNode)startOpeParent.AbOperator;
            }
			else if (end.Pin_Type == Pin.PinType.Param)
            {
                endParamParent = end.GetComponentInParent<ProxyABParam>();
                //start.IsGateOperator = true;
				start.Pin_Type = Pin.PinType.OperatorIn;
                startOpeParent.Inputs[startOpeParent.Inputs.Length-1] = (ABNode)endParamParent.AbParam;
            }
			else if (end.Pin_Type == Pin.PinType.ActionParam)
            {
                endActionParent = end.GetComponentInParent<ProxyABAction>();
                //end.IsGateOperator = true;
				end.Pin_Type = Pin.PinType.OperatorIn;
                endActionParent.AbState.Action.Parameters[0].Inputs[0] = (ABNode)startOpeParent.AbOperator;
            }
        }
		else if (start.Pin_Type == Pin.PinType.Param)
        {
            startParamParent = start.GetComponentInParent<ProxyABParam>();
			if (end.Pin_Type == Pin.PinType.OperatorIn || end.Pin_Type == Pin.PinType.OperatorOut)
            {
                endOpeParent = end.GetComponentInParent<ProxyABOperator>();
                //start.IsGateOperator = true;
				start.Pin_Type = Pin.PinType.OperatorIn;
                endOpeParent.Inputs[endOpeParent.Inputs.Length-1] = (ABNode)startParamParent.AbParam;
                ((ABNode)startParamParent.AbParam).Output = (ABNode)endOpeParent.AbOperator;//TODO ma geule
            }
			else if (end.Pin_Type == Pin.PinType.ActionParam)
            {
                endActionParent = end.GetComponentInParent<ProxyABAction>();
                //end.IsGateOperator = true;
				end.Pin_Type = Pin.PinType.OperatorIn;                
                endActionParent.AbState.Action.Parameters[0].Inputs[0] = (ABNode)startParamParent.AbParam;
            }
        }
        else
        {
            startStateParent = start.GetComponentInParent<ProxyABState>();
			if (end.Pin_Type == Pin.PinType.ActionParam)
            {
                endActionParent = end.GetComponentInParent<ProxyABAction>();
				ProxyABTransition.addConditionPin ( trans );
                transitionId = AbModel.LinkStates(startStateParent.AbState.Name, endActionParent.AbState.Name);
                trans.Transition = AbModel.getTransition(transitionId);
            }
            else
            {
                endStateParent = end.GetComponentInParent<ProxyABState>();
				ProxyABTransition.addConditionPin ( trans );
                transitionId = AbModel.LinkStates(startStateParent.AbState.Name, endStateParent.AbState.Name);
                trans.Transition = AbModel.getTransition(transitionId);
            }
        }                                                               
    }

	ProxyABAction CreateAction( ABAction action, int nodeID, string nodeName )
    {
		ABState state = new ABState ( nodeID, nodeName );
		return MCEditor_Proxy_Factory.instantiateAction( state );
    }

	ProxyABOperator CreateOperator( IABOperator op )
    {
		ProxyABOperator ope = MCEditor_Proxy_Factory.instantiateOperator ( op, false );
        proxyOperators.Add(ope);
        return ope;
    }

	ProxyABParam CreateParam( IABParam p )
    {
		ProxyABParam param = MCEditor_Proxy_Factory.instantiateParam( p, false );
        proxyParams.Add(param);
        return param;
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
				if ( (!(transition.StartPosition.Pin_Type == Pin.PinType.OperatorIn || transition.StartPosition.Pin_Type == Pin.PinType.OperatorOut)
					|| !(transition.StartPosition.Pin_Type == Pin.PinType.Param) )
					&& (!(transition.EndPosition.Pin_Type == Pin.PinType.OperatorIn || transition.EndPosition.Pin_Type == Pin.PinType.OperatorOut)
					|| !(transition.EndPosition.Pin_Type == Pin.PinType.Param)))
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

        /*START ACTION CASES*/
        if(start.Pin_Type == Pin.PinType.ActionParam)
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
            else if(end.Pin_Type == Pin.PinType.ActionParam)
            {
                proxyAction = end.GetComponentInParent<ProxyABAction>();
                UnlinkAction_Operator(proxyAction, proxyOpeStart);
            }
        }

        /* END OPERATOR CASES */
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
        
        if(((ABNode)proxyOperator.AbOperator).Output != null)
        {
            ((ABNode)proxyOperator.AbOperator).Output = null;
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
	#endregion

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
