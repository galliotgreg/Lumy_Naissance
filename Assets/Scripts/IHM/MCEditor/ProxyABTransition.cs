using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyABTransition : MonoBehaviour {    
    private ABTransition abTransition;
    private Pin startPosition;
    private Pin endPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public ProxyABTransition(ABTransition transition)
    {
        this.abTransition = transition;
    }
}
