using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_Proxy_Factory : MonoBehaviour {

	#region SINGLETON
	/// <summary>
	/// The static instance of the Singleton for external access
	/// </summary>
	public static MCEditor_Proxy_Factory instance = null;

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

	//Templates & Prefabs
	[SerializeField]
	private ProxyABState statePrefab;
	[SerializeField]
	private ProxyABAction actionPrefab;
	[SerializeField]
	private ProxyABOperator operatorPrefab;
	[SerializeField]
	private ProxyABParam parameterPrefab;
	[SerializeField]
	private ProxyABTransition transitionPrefab;
	[SerializeField]
	private Pin pinPrefab;
	[SerializeField]
	private Pin pinOutPrefab;
    [SerializeField]
	private Pin pinTransitionOutPrefab;

	void Start(){}
	void Update(){}

	#region PROPERTIES
	public ProxyABState StatePrefab {
		get {
			return statePrefab;
		}
	}

	public ProxyABAction ActionPrefab {
		get {
			return actionPrefab;
		}
	}

	public ProxyABOperator OperatorPrefab {
		get {
			return operatorPrefab;
		}
		set {
			operatorPrefab = value;
		}
	}

	public ProxyABParam ParameterPrefab {
		get {
			return parameterPrefab;
		}
	}

	public ProxyABTransition TransitionPrefab {
		get {
			return transitionPrefab;
		}
	}

	public Pin PinPrefab {
		get {
			return pinPrefab;
		}
	}

    public Pin PinOutPrefab
    {
        get
        {
            return pinOutPrefab;
        }

        set
        {
            pinOutPrefab = value;
        }
    }

	public Pin PinTransitionOutPrefab {
		get {
			return pinTransitionOutPrefab;
		}
	}

    #endregion

    #region INSTANTIATE
    public static ProxyABState instantiateState( ABState state, bool init ){
		return ProxyABState.instantiate (state, init);
	}

	public static ProxyABAction instantiateAction( ABState state ){
		return ProxyABAction.instantiate ( state );
	}

	public static ProxyABOperator instantiateOperator( IABOperator operatorObj, bool isLoaded ){
		return ProxyABOperator.instantiate (operatorObj, isLoaded);
	}

	public static ProxyABParam instantiateParam( IABParam paramObj, bool isLoaded ){
		return ProxyABParam.instantiate ( paramObj, isLoaded );
	}

	public static ProxyABTransition instantiateTransition( Pin start, Pin end, bool createCondition ){
		return ProxyABTransition.instantiate (start, end, createCondition);
	}

	public static Pin instantiatePin( Pin.PinType pinType, Vector3 position, Transform parent ){
		return Pin.instantiate ( pinType, position, parent );
	}
	#endregion
}
