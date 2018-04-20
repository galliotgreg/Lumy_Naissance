using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBoxManager : MonoBehaviour {

    #region SINGLETON
    // The static instance of the Singleton for external access
    public static MCEditor_DialogBoxManager instance = null;

    // Enforce Singleton properties
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
    #endregion

    [SerializeField]
    RectTransform container;
    
    //Pin
    [SerializeField]
    MCEditor_DialogBox_ChangePin changePin_Prefab;
	
    

    // States
    [SerializeField]
    MCEditor_DialogBox_State_Name stateName_Prefab;
    // Actions
    [SerializeField]
    MCEditor_DialogBox_Action_Name actionName_Prefab;
    // Params
    [SerializeField]
    MCEditor_DialogBox_Param_String paramText_Prefab;
    [SerializeField]
    MCEditor_DialogBox_Param_Scalar paramScalar_Prefab;
    [SerializeField]
    MCEditor_DialogBox_Param_Bool paramBool_Prefab;
    [SerializeField]
    MCEditor_DialogBox_Param_Color paramColor_Prefab;
    [SerializeField]
    MCEditor_DialogBox_Param_Vec paramVec_Prefab;
    [SerializeField]
    GameObject toolTip_action_prefab;
    [SerializeField]
    GameObject toolTip_param_prefab;
    [SerializeField]
    GameObject toolTip_operator_prefab;

	[SerializeField]
	ZoomingScript camHandler;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public MCEditor_DialogBox_Param instantiateValue( ProxyABParam param, Vector2 position ){
		Vector2 relativePosition = positionRelativeToBorder( param.gameObject );
		Vector3 pos3D = new Vector3 ( position.x + relativePosition.x, position.y + relativePosition.y, container.position.z );
		if (param.AbParam is ABTextParam) {
			relativePosition = positionRelativeToBorder( param.gameObject, 3.0f );
			pos3D = new Vector3 ( position.x + relativePosition.x, position.y + relativePosition.y, container.position.z );
			return MCEditor_DialogBox_Param_String.instantiate( param, paramText_Prefab, pos3D, container);
		}
		else if (param.AbParam is ABScalParam) {
			return MCEditor_DialogBox_Param_Scalar.instantiate( param, paramScalar_Prefab, pos3D, container);
		}
		else if (param.AbParam is ABBoolParam) {
			return MCEditor_DialogBox_Param_Bool.instantiate( param, paramBool_Prefab, pos3D, container);
		}
		else if (param.AbParam is ABColorParam) {
			return MCEditor_DialogBox_Param_Color.instantiate( param, paramColor_Prefab, pos3D, container);
		}
		else if (param.AbParam is ABVecParam) {
			return MCEditor_DialogBox_Param_Vec.instantiate( param, paramVec_Prefab, pos3D, container);
		}
		return null;
	}

	public MCEditor_DialogBox_State_Name instantiateStateName( ProxyABState state, Vector2 position ){
		Vector2 relativePosition = positionRelativeToBorder( state.gameObject );
		Vector3 pos3D = new Vector3 ( position.x + relativePosition.x, position.y + relativePosition.y, container.position.z );
		return (MCEditor_DialogBox_State_Name)MCEditor_DialogBox_State.instantiate( state, stateName_Prefab, pos3D, container );
	}
	public MCEditor_DialogBox_Action_Name instantiateActionName( ProxyABAction action, Vector2 position ){
		Vector2 relativePosition = positionRelativeToBorder( action.gameObject );
		Vector3 pos3D = new Vector3 ( position.x + relativePosition.x, position.y + relativePosition.y, container.position.z );
		return (MCEditor_DialogBox_Action_Name)MCEditor_DialogBox_Action.instantiate( action, actionName_Prefab, pos3D, container );
	}
	public MCEditor_DialogBox_ChangePin instantiateChangePin( Pin pin, Vector2 position ){
		Vector2 relativePosition = positionRelativeToBorder( pin.gameObject );
		Vector3 pos3D = new Vector3 ( position.x + relativePosition.x, position.y + relativePosition.y, container.position.z );
		return (MCEditor_DialogBox_ChangePin)MCEditor_DialogBox_ChangePin.instantiate( pin, changePin_Prefab, pos3D, container );
	}

    public void instantiateToolTip(Vector3 position, string type, MCEditor_Proxy proxy)
    {
        MCEditorManager.instance.instantiateToolTip(position, type, proxy);        
    }

    public void instantiateToolTip(Vector3 position, System.Object item)
    {
        MCEditorManager.instance.instantiateToolTip(position, item);
    }

	#region Position
	/// <summary>
	/// Calculate the position of the dialogBox relative to the Borders
	/// </summary>
	/// <returns>The relative position to border. [(1,1) is the default position. If it is close to the border, (1,-1), (-1,1) or (-1,-1)]</returns>
	/// <param name="obj">Object.</param>
	Vector2 positionRelativeToBorder( GameObject obj, float dialogSize = 1.0f ){
		Vector2 dialogDimension = new Vector2( dialogSize, dialogSize );
		float distanceFactor = 0.7f; // factor that stablishes the distance from the dialog to the object
		float camFactor = camHandler.CurrentHeight / camHandler.InitialHeight * distanceFactor;
		Vector2 dialogActualDimension = camFactor * dialogDimension;

		Vector3 camPosition = camHandler.gameObject.transform.position;
		Vector3 objPosition = obj.transform.position;
		float objRadius = obj.transform.localScale.x/2.0f;

		// Verify Top Border
		bool TopOk = true;
		if( objPosition.y + camFactor+objRadius + dialogActualDimension.y > camPosition.y + camHandler.CurrentHeight/2.0f ){
			TopOk = false;
		}
		// Verify Right Border
		bool RigthOk = true;
		if( objPosition.x + camFactor+objRadius + dialogActualDimension.x > camPosition.x + camHandler.CurrentWidth/2.0f ){
			RigthOk = false;
		}

		Vector2 direction = new Vector2 ((RigthOk ? 1 : -1), (TopOk ? 1 : -1)).normalized;
		Vector2 objCenter = Vector2.Scale (dialogActualDimension, new Vector2 (0.5f, 0.5f));
		Vector2 objRadiusV = new Vector2( camFactor+objRadius , camFactor+objRadius );
		return Vector2.Scale( direction , objCenter+objRadiusV );
	}
	#endregion
}
