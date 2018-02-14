using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyABParam<T> : MonoBehaviour, IProxyABParam{

    private ABParam<T> abParam;
    private Pin outcome;

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
