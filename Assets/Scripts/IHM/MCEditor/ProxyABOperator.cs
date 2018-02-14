using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyABOperator<T> : MonoBehaviour, IProxyABOperator{
    private ABOperator<T> abOperator;
    private Pin outcome;
    private List<Pin> incomes;


    public ABNode[] Inputs {
        get {
            throw new System.NotImplementedException();
        }

        set {
            throw new System.NotImplementedException();
        }
    }

    public Pin Outcome
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }

    public List<Pin> Incomes
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
