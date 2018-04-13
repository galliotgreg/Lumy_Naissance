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
			this.SetProxyName (value);
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

	public static string GetViewValue( IABParam param )
	{
		string text = "";
		if (param is ABParam<ABRef>) {
			if (((ABParam<ABRef>)param).Value != null) {
				text += param.Identifier.ToString ();
			}
		} else if (param is ABTableParam<ABRef>) {
			if (((ABTableParam<ABRef>)param).Value != null) {
				text += param.Identifier.ToString ();
			}
		} else {
			text += MCEditorManager.GetParamValue ((ABNode)param);
		}
		return text;
	}

	public System.Type getOutcomeType ()
	{
		return this.AbParam.getOutcomeType ();
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
		result.SetProxyName( GetViewValue( result.AbParam ) );

		// Outcome pin
		Pin.instantiate( Pin.PinType.Param, Pin.calculatePinPosition (result), result.transform );

		return result;
	}

	public static Vector3 calculateParamPosition( Transform parent ){
		return new Vector3(UnityEngine.Random.Range(-5, 5),UnityEngine.Random.Range(-5, 5), parent.position.z);
	}
	#endregion

	#region implemented abstract members of MCEditor_Proxy

	public override void doubleClick ()
	{
		Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
		MCEditor_DialogBoxManager.instance.instantiateValue (this, pos);
	}

	public override void deleteProxy ()
	{
		MCEditorManager.instance.deleteProxy ( this );
	}

	#endregion

	public IABParam Clone ()
	{
		throw new System.NotImplementedException ();
	}
}
