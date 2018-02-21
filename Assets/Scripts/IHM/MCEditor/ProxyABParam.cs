using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProxyABParam : MonoBehaviour, IProxyABParam{

    private IABParam abParam;
    [SerializeField]
    private string name;
    private string type = "const scal=5";
    private Pin outcome = null;
    private bool isLoaded = false;

    public string Identifier {
        get {
            throw new System.NotImplementedException();
        }
    }

    public string Value {
        get {
            throw new System.NotImplementedException();
        }
    }

    public Pin Outcome
    {
        get
        {
            return outcome;
        }

        set
        {
            outcome = value;
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

    // Use this for initialization
    void Start () {
        if (IsLoaded)// when the Operator is created by loading behavior file
        {            
            IsLoaded = false;
        }
        else // when the OPerator is created in the editor.
        {
            // Created on MCEDITORMANAGER
			outcome = MCEditorManager.getPins( this.gameObject, Pin.PinType.Param )[0];                        
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
