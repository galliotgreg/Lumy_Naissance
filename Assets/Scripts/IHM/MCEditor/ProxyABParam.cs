using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyABParam<T> : MonoBehaviour, IProxyABParam{

    private ABParam<T> abParam;

    public string Identifier {
        get {
            throw new System.NotImplementedException();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public ProxyABParam(ABParam<T> param) {
        AbParam = param;
    }
}
