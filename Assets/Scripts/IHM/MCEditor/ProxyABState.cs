using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyABState : MonoBehaviour {
    private ABState abState;
    private List<Pin> pinList;

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public ProxyABState(ABState state)
    {
        this.AbState = state;
    }
}
