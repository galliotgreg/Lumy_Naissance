using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProxyABParam : MCEditor_Proxy, IProxyABParam{

    private IABParam abParam;
    [SerializeField]
    private string name;
    private string type;
    private bool isLoaded = false;
    private bool isPositioned = false;

    public string Identifier {
        get {
            throw new System.NotImplementedException();
        }
    }

    public string Value {
        get {
            throw new System.NotImplementedException();
        }
		set {
			type = value;
			this.setProxyName (value);
		}
    }

    public Pin Outcome
    {
        get
        {
			return getPins( Pin.PinType.Param )[0];
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

    public IABParam AbParam
    {
        get
        {
            return abParam;
        }

        set
        {
            abParam = value;
        }
    }

    public bool IsPositioned
    {
        get
        {
            return isPositioned;
        }

        set
        {
            isPositioned = value;
        }
    }

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

	public void setProxyName( string name ){
		Text paramName = this.GetComponentInChildren<Text> ();
		paramName.text = name;
	}

	#region INSTANTIATE
	public static ProxyABParam instantiate( IABParam paramObj, bool isLoaded ){
		return instantiate ( paramObj, isLoaded, calculateParamPosition( MCEditorManager.instance.MCparent ), MCEditorManager.instance.MCparent );
	}
	public static ProxyABParam instantiate( IABParam paramObj, bool isLoaded, Vector3 position, Transform parent ){
		ProxyABParam result = Instantiate<ProxyABParam> (MCEditor_Proxy_Factory.instance.ParameterPrefab, parent);
		result.IsLoaded = isLoaded;
		result.AbParam = paramObj;
		result.transform.position = position;

		result.AbParam = paramObj;

		// Set text
		if (isLoaded) {
			result.setProxyName( MCEditorManager.GetParamValue ((ABNode)paramObj) );
		} else {
			result.setProxyName( paramObj.Identifier + " : " + MCEditorManager.GetParamValue ((ABNode)paramObj) );
		}

		// Outcome pin
		Pin.instantiate( Pin.PinType.Param, Pin.calculatePinPosition (result), result.transform );

		return result;
	}

	public static Vector3 calculateParamPosition( Transform parent ){
		return new Vector3(UnityEngine.Random.Range(-5, 5),UnityEngine.Random.Range(-5, 5), parent.position.z);
	}
	#endregion

	#region implemented abstract members of MCEditor_Proxy

	public override void click ()
	{
		Vector2 pos = new Vector2 (transform.position.x + 1, transform.position.y + 1);
		MCEditor_DialogBox_Param dlg = MCEditor_DialogBoxManager.instance.instantiateValue (this, pos);
	}

	#endregion
}
