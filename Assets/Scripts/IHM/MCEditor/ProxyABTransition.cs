using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyABTransition : SelectableProxyGameObject {    
	private ABTransition transition;
    public  Pin startPosition;
    public Pin endPosition;    
    
    // ABBoolGateOperator to add a syntaxe Tree
    public Pin condition;

	public ABTransition Transition {
		get {
			return transition;
		}
		set{
			transition = value;
		}
	}

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
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();

        if(StartPosition != null && EndPosition != null ) {

            Vector3 posDepart = StartPosition.transform.position;
            Vector3 posArrivee = EndPosition.transform.position;
            GetComponent<LineRenderer>().SetPosition(0, posDepart);
            GetComponent<LineRenderer>().SetPosition(1, posArrivee);

			adjustCollider ();

            if(condition != null) {
                Condition.transform.position = CalculABBGOPinPosition(posDepart, posArrivee);
            }            
        }
    }

    Vector3 CalculABBGOPinPosition(Vector3 vec1, Vector3 vec2) {
        return new Vector3((vec1.x + vec2.x) / 2, (vec1.y + vec2.y) / 2, 0);
    }

	#region implemented abstract members of SelectableProxyGameObject

	protected override void select ()
	{
		MCEditorManager.instance.selectTransition (this);
	}

	protected override void unselect ()
	{
		if (MCEditorManager.instance.Transition_Selected == this) {
			MCEditorManager.instance.selectTransition (null);
		}
	}

	#endregion

	#region Collider

	[SerializeField]
	CapsuleCollider collider;
	[SerializeField]
	LineRenderer lineRenderer;

	private void adjustCollider()
	{
		Vector3 startPoint = lineRenderer.GetPosition (0);
		Vector3 endPoint = lineRenderer.GetPosition (1);

		collider.height = Vector3.Distance (startPoint, endPoint);
		collider.radius = lineRenderer.startWidth;

		Vector3 midPoint = (endPoint-startPoint)/2 + startPoint;
		collider.transform.position = midPoint;

		float tang = (Mathf.Abs (endPoint.y - startPoint.y) / Mathf.Abs (endPoint.x - startPoint.x));
		if((startPoint.y<endPoint.y && startPoint.x>endPoint.x) || (endPoint.y<startPoint.y && endPoint.x>startPoint.x))
		{
			tang*=-1;
		}
		float angle = Mathf.Rad2Deg * Mathf.Atan (tang);
		collider.transform.rotation = Quaternion.Euler( new Vector3 (0, 0, 90+angle) );
	}
	#endregion
}
