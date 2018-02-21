﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProxyABState : MCEditor_Proxy {
    [SerializeField]
    private string name;  
    private ABState abState;
    private bool isLoaded = false;

	#region PROPERTIES
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

    public ABState AbState
    {
        get
        {
            return abState;
        }

        set
        {
            abState = value;
        }
    }

    public List<Pin> Outcomes
    {
        get
        {
			return this.getPins(Pin.PinType.TransitionOut);
        }
    }

	public Pin Income {
		get {
			return this.getPins(Pin.PinType.TransitionIn)[0];
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
	#endregion

    private void Awake(){}

    // Use this for initialization
    void Start()
    {
        if (IsLoaded)// when the Action is created by loading behavior file
        {            
            IsLoaded = false;
        }
        else // when the Action is created in the editor.
        {            
            //this.AbState = MCEditorManager.instance.AbModel.getState(MCEditorManager.instance.AbModel.AddState(Name, null));          

			//income = MCEditorManager.getPins( this.gameObject, Pin.PinType.TransitionIn )[0];
			//pinList = MCEditorManager.getPins( this.gameObject, Pin.PinType.TransitionOut );

            //Debug.Log(MCEditorManager.instance.AbModel.States.Count);
		}
    }

    // Update is called once per frame
    void Update () {
		
	}

	#region INSTANTIATE
	public static ProxyABState instantiate( ABState state, bool init ){
		return instantiate (state, init, calculateStatePosition (MCEditorManager.instance.MCparent), MCEditorManager.instance.MCparent);
	}
	public static ProxyABState instantiate( ABState state, bool init, Vector3 position, Transform parent ){
		ProxyABState result = Instantiate<ProxyABState>( MCEditor_Proxy_Factory.instance.StatePrefab, parent);
		result.IsLoaded = true;
		result.transform.position = position;
		result.AbState = state;

		UnityEngine.UI.Text stateName = result.GetComponentInChildren<UnityEngine.UI.Text>();
		stateName.text = state.Name;

		result.GetComponent<ProxyABState>().AbState = state;

		// Income Pin
		if (!init) {
			Pin.instantiate (Pin.PinType.TransitionIn, Pin.calculatePinPosition (result), result.transform);
		}

		// Outcome Pin 
		foreach(ABTransition transition in state.Outcomes) 
		{ 
			Pin pin = MCEditor_Proxy_Factory.instantiatePin(Pin.PinType.TransitionOut, Pin.calculatePinPosition(result), result.transform); 
		}

		return result;
	}

	public static Vector3 calculateStatePosition( Transform parent ){
		return new Vector3(UnityEngine.Random.Range(-5, 5),UnityEngine.Random.Range(-5, 5), parent.position.z);
	}
	#endregion
}
