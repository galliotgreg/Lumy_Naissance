using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProxyABTransition : IsolatedSelectableProxyGameObject {    
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

		adjustTransitionPosition ();
    }

	public void adjustTransitionPosition(){
		if(StartPosition != null && EndPosition != null ) {

			adjustPinPosition();

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
		return Pin.calculatePinPosition ( this );
    }

	void OnDestroy(){
		if (StartPosition != null) {
			StartPosition.desassociateTransition (this);
		}
		if (EndPosition != null) {
			EndPosition.desassociateTransition (this);
		}
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

	public LineRenderer LineRenderer {
		get {
			return lineRenderer;
		}
	}

	public CapsuleCollider Collider {
		get {
			return collider;
		}
	}

	private void adjustCollider()
	{
		Vector3 startPoint = lineRenderer.GetPosition (0);
		Vector3 endPoint = lineRenderer.GetPosition (1);

		collider.height = Vector3.Distance (startPoint, endPoint);
		collider.radius = lineRenderer.startWidth/2;

		Vector3 midPoint = (endPoint-startPoint)/2 + startPoint;
		collider.transform.position = midPoint;

		float tang = (Mathf.Abs (endPoint.y - startPoint.y) / Mathf.Abs (endPoint.x - startPoint.x));
		if((startPoint.y<endPoint.y && startPoint.x>endPoint.x) || (endPoint.y<startPoint.y && endPoint.x>startPoint.x))
		{
			tang*=-1;
		}
		float angle = Mathf.Rad2Deg * Mathf.Atan (tang);

		//collider.transform.rotation = Quaternion.Euler( new Vector3 (0, 0, 90+angle) );
		if (!float.IsNaN (angle)) {
			collider.transform.localEulerAngles = new Vector3 (0, 0, 90 + angle);
		}
	}
	#endregion

	#region INSTANTIATE
	public static ProxyABTransition instantiate( Pin start, Pin end, bool createCondition ){
		return instantiate ( start, end, createCondition, MCEditor_Proxy_Factory.instance.InteractableZone );
	}
	public static ProxyABTransition instantiate( Pin start, Pin end, bool createCondition, DropArea selectableZone ){
		return instantiate ( start, end, createCondition, selectableZone, MCEditorManager.instance.MCparent );
	}
	public static ProxyABTransition instantiate( Pin start, Pin end, bool createCondition, DropArea selectableZone, Transform parent ){
		ProxyABTransition proxyABTransition = Instantiate<ProxyABTransition>(MCEditor_Proxy_Factory.instance.TransitionPrefab, parent);
		proxyABTransition.selectZone = selectableZone;
		proxyABTransition.transform.position = start.transform.position + (start.transform.position - end.transform.position)/2;

		/*if(start.Pin_Type == Pin.PinType.TransitionOut) 
		{ 
			ProxyABState stateParent = start.GetComponentInParent<ProxyABState>(); 
			Pin pin = MCEditor_Proxy_Factory.instantiatePin(Pin.PinType.TransitionOut, Pin.calculatePinPosition(stateParent), stateParent.transform); 
		} 

		if(end.Pin_Type == Pin.PinType.TransitionOut) 
		{ 
			ProxyABState stateParent = end.GetComponentInParent<ProxyABState>(); 
			Pin pin = MCEditor_Proxy_Factory.instantiatePin(Pin.PinType.TransitionOut, Pin.calculatePinPosition(stateParent), stateParent.transform); 
		}*/

		proxyABTransition.StartPosition = start;
		proxyABTransition.EndPosition = end;

		start.associateTransition (proxyABTransition);
		end.associateTransition (proxyABTransition);

        if(start.GetColor() == end.GetColor())
        {
            proxyABTransition.regularColor = start.GetColor();
        }


		if (createCondition) {
			addConditionPin ( proxyABTransition );
		}

		proxyABTransition.adjustTransitionPosition ();
		// TODO register?
		return proxyABTransition;
	}

	public static void addConditionPin( ProxyABTransition proxyABTransition ){
		Pin conditionPin = Pin.instantiate( Pin.PinType.Condition, Pin.calculatePinPosition( proxyABTransition ), proxyABTransition.transform );
		proxyABTransition.Condition = conditionPin;
	}

    private void computePinPositionWithParentRadius(GameObject parent, Pin pin, Vector3 direction)
    {
        float radius = parent.transform.localScale.y / 2;

        pin.transform.position = new Vector3(
            parent.transform.position.x + (radius * direction.x),
            parent.transform.position.y + (radius * direction.y),
            parent.transform.position.z
            );
    }

    //Minimise distance between two pin
    private void adjustPinPosition()
    {
		GameObject startParent = GetParent(startPosition);
		GameObject endParent = GetParent(endPosition);
        Vector3 direction = new Vector3();
        direction = computeDirection(startParent.transform.position, endParent.transform.position);

		if (!(startPosition.Pin_Type == Pin.PinType.Condition) && startPosition.AssociatedTransitions.IndexOf(this) == 0 )
        {
            computePinPositionWithParentRadius(startParent, startPosition, direction);            
        }
		if (!(endPosition.Pin_Type == Pin.PinType.Condition) && endPosition.AssociatedTransitions.IndexOf(this) == 0 )
        {
            computePinPositionWithParentRadius(endParent, endPosition, -direction);
        }
    }
    private GameObject GetParent(Pin pin)
    {
        GameObject parent = null;

		if (pin.Pin_Type == Pin.PinType.Condition) {
			parent = pin.GetComponentInParent<ProxyABTransition> ().gameObject;
		} else {
			parent = pin.ProxyParent.gameObject;
		}
        /*else if (pin.Pin_Type == Pin.PinType.OperatorIn || pin.Pin_Type == Pin.PinType.OperatorOut)
        {
            parent = pin.GetComponentInParent<ProxyABOperator>().gameObject;
        }
        else if (pin.Pin_Type == Pin.PinType.Param)
        {
            parent = pin.GetComponentInParent<ProxyABParam>().gameObject;
        }
        else if (pin.Pin_Type == Pin.PinType.ActionParam)
        {
            parent = pin.GetComponentInParent<ProxyABAction>().gameObject;
        }
        else if (pin.Pin_Type == Pin.PinType.TransitionIn || pin.Pin_Type == Pin.PinType.TransitionOut)
        {
            ProxyABState parentTemp = pin.GetComponentInParent<ProxyABState>();
            if (!parentTemp)
            {
                parent = pin.GetComponentInParent<ProxyABAction>().gameObject;
            } else
            {
                parent = parentTemp.gameObject;
            }
        } */                       
        return parent;
    }

    private float computeNorme(Vector3 vec)
    {
        float norme = (float)Math.Sqrt(vec.x * vec.x + vec.y * vec.y);
        if (norme == 0)
            norme = 1;
        return norme;        
    }

    private Vector3 computeDirection(Vector3 vec1, Vector3 vec2)
    {
        Vector3 res = new Vector3(vec2.x - vec1.x, vec2.y - vec1.y, 0);
        float norme = computeNorme(res);
        float res_norm_x = res.x / norme;
        float res_norm_y = res.y / norme;

        res = new Vector3(res_norm_x, res_norm_y, 0);
        return res;
    }

    #endregion

	#region Outils
	public MCEditor_Proxy oppositeSide( MCEditor_Proxy oneSide ){
		return oppositePin( oneSide ).ProxyParent;
	}
	public Pin oppositePin( MCEditor_Proxy oneSide ){
		if (this.StartPosition.ProxyParent == oneSide) {
			return this.EndPosition;
		}
		if (this.EndPosition.ProxyParent == oneSide) {
			return this.StartPosition;
		}
		return null;
	}
	#endregion
}
