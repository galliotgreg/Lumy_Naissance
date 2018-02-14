using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyABTransition : MonoBehaviour {    
    private ABTransition abTransition;
    public  Pin startPosition;
    public Pin endPosition;    
    
    // ABBoolGateOperator to add a syntaxe Tree
    public Pin condition;

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

    public Pin Condition {
        get {
            return condition;
        }

        set {
            condition = value;
        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(StartPosition != null && EndPosition != null ) {

            Vector3 posDepart = StartPosition.transform.position;
            Vector3 posArrivee = EndPosition.transform.position;
            GetComponent<LineRenderer>().SetPosition(0, posDepart);
            GetComponent<LineRenderer>().SetPosition(1, posArrivee);

            if(condition != null) {
                Condition.transform.position = CalculABBGOPinPosition(posDepart, posArrivee);
            }            
        }
    }

    Vector3 CalculABBGOPinPosition(Vector3 vec1, Vector3 vec2) {
        return new Vector3((vec1.x + vec2.x) / 2, (vec1.y + vec2.y) / 2, 0);
    }

    public ProxyABTransition(ABTransition transition)
    {
        this.abTransition = transition;
    }
}
