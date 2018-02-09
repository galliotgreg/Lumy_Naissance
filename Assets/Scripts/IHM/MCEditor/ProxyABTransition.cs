using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyABTransition : MonoBehaviour {    
    private ABTransition abTransition;
    private Pin startPosition;
    private Pin endPosition;

    public Pin StartPosition {
        get {
            return startPosition;
        }

        set {
            startPosition = value;
        }
    }

    public Pin EndPosition {
        get {
            return endPosition;
        }

        set {
            endPosition = value;
        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(StartPosition != null && EndPosition != null) {

            Vector3 posDepart = StartPosition.transform.position;
            Vector3 posArrivee = EndPosition.transform.position;
            GetComponent<LineRenderer>().SetPosition(0, posDepart);
            GetComponent<LineRenderer>().SetPosition(1, posArrivee);
        }
    }

    public ProxyABTransition(ABTransition transition)
    {
        this.abTransition = transition;
    }
}
