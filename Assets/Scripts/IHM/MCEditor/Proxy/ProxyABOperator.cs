using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProxyABOperator: MCEditor_Proxy, IProxyABOperator{
    [SerializeField]
    private string name;
    private string type;
    private IABOperator abOperator;
    bool isLoaded = false;

	#region PROPERTIES
    public ABNode[] Inputs {
        get {
            return abOperator.Inputs;
        }

        set {
            abOperator.Inputs = value;
        }
    }

    public Pin Outcome
    {
        get
        {
			return getPins( Pin.PinType.OperatorOut )[0];
        }
    }

    public List<Pin> Incomes
    {
        get
        {
			return getPins( Pin.PinType.OperatorIn );
        }
    }

    public bool IsLoaded
    {
        get
        {
            return isLoaded;
        }

        set
        {
            isLoaded = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public IABOperator AbOperator
    {
        get
        {
            return abOperator;
        }

        set
        {
            abOperator = value;
        }
    }
	#endregion

    // Use this for initialization
    void Start () {
        if (IsLoaded)// when the Operator is created by loading behavior file
        {            
            IsLoaded = false;
        }
        else // when the OPerator is created in the editor.
        {         
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	#region INSTANTIATE
	public static ProxyABOperator instantiate( IABOperator operatorObj, bool isLoaded ){
		return instantiate ( operatorObj, isLoaded, calculateOperatorPosition( MCEditorManager.instance.MCparent ), MCEditorManager.instance.MCparent );
	}
	public static ProxyABOperator instantiate( IABOperator operatorObj, bool isLoaded, Vector3 position, Transform parent ){
		ProxyABOperator result = Instantiate<ProxyABOperator> (MCEditor_Proxy_Factory.instance.OperatorPrefab, parent);
		result.IsLoaded = isLoaded;
		result.transform.position = position;
		result.AbOperator = operatorObj;
		result.SetNodeName( (ABNode)operatorObj );

		// Create Pins
		foreach(ABNode inputNode in operatorObj.Inputs)
		{
			Pin start = Pin.instantiate (Pin.PinType.OperatorIn, Pin.calculatePinPosition (result), result.transform);
		}

		// Outcome pin
		Pin.instantiate( Pin.PinType.OperatorOut, Pin.calculatePinPosition (result), result.transform );

		return result;
	}

	public static Vector3 calculateOperatorPosition( Transform parent ){
		return new Vector3(UnityEngine.Random.Range(-5, 5),UnityEngine.Random.Range(-5, 5), parent.position.z);
	}
	#endregion
}
