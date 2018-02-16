using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 GameObject de branchement de transition entre deux états
 */  
public class Pin : MonoBehaviour {

    bool isGateOperator = false;
    bool isActionChild = false;
    bool isParamChild = false;
    bool isOperatorChild = false;

    public bool IsGateOperator {
        get {
            return isGateOperator;
        }

        set {
            isGateOperator = value;
        }
    }

    public bool IsActionChild
    {
        get
        {
            return isActionChild;
        }

        set
        {
            isActionChild = value;
        }
    }

    public bool IsParamChild
    {
        get
        {
            return isParamChild;
        }

        set
        {
            isParamChild = value;
        }
    }

    public bool IsOperatorChild
    {
        get
        {
            return isOperatorChild;
        }

        set
        {
            isOperatorChild = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
