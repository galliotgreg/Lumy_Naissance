using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 GameObject de branchement de transition entre deux états
 */
public class Pin : DragSelectableProxyGameObject {

	public enum PinType{
		Condition,		// Transition : condition
		TransitionIn,	// Action/State : transition in
		TransitionOut,	// State : transition out
		ActionParam,	// Action : param (gateOperator)
		OperatorIn,		// Operator : param
		OperatorOut,	// Operator : result
		Param			// Constant/Reference
	};

	[SerializeField]
	PinType pin_Type;

	MCEditor_Proxy proxyParent;
	[SerializeField]
	List<ProxyABTransition> associatedTransitions;

	[SerializeField]
	/// <summary>
	/// Prefab that implements a Transition
	/// </summary>
	GameObject transitionPrefab;

	[SerializeField]
	Pin_Order pin_order;

	Camera trackingCamera;

	#region PROPERTIES
	public PinType Pin_Type {
		get {
			return pin_Type;
		}
		set {
			pin_Type = value;
		}
	}

	public List<ProxyABTransition> AssociatedTransitions {
		get {
			return associatedTransitions;
		}
	}
		
	public MCEditor_Proxy ProxyParent {
		get {
			return proxyParent;
		}
		protected set {
			proxyParent = value;
		}
	}

	public Pin_Order Pin_order {
		get {
			return pin_order;
		}
	}
	#endregion

	protected void Awake(){
		associatedTransitions = new List<ProxyABTransition> ();
	}

    // Use this for initialization
    protected void Start () {
		trackingCamera = ( MCEditor_BringToFront_Camera.CanvasCamera != null ? MCEditor_BringToFront_Camera.CanvasCamera : Camera.main );        
	}

	// Update is called once per frame
	protected void Update () {
		base.Update ();

		handleSelectedState ();

		setOrderPanelPosition ();
	}

	public bool associateTransition( ProxyABTransition transition ){
		if (!AssociatedTransitions.Contains (transition)) {
			AssociatedTransitions.Add (transition);
			return true;
		}
		return false;
	}
	public bool desassociateTransition( ProxyABTransition transition ){
		if (AssociatedTransitions.Contains (transition)) {
			return AssociatedTransitions.Remove (transition);
		}
		return false;
	}

	void setOrderPanelPosition(){
		// Adjust Pin Order Panel position
		if( this.proxyParent != null ){
			Vector3 direction = -(this.proxyParent.transform.position - this.transform.position).normalized;
			this.Pin_order.TransitionDirection = direction;
		}
	}

	#region implemented abstract members of SelectableProxyGameObject
	protected override void select ()
	{
		if (MCEditorManager.instance.Transition_Pin_Start != null ) {
			MCEditorManager.instance.createTransition_setEndPin (this);
		} else {
			MCEditorManager.instance.createTransition_setStartPin ( this );
		}
	}

	protected override void unselect ()
	{
		if (MCEditorManager.instance.Transition_Pin_Start == this) {
			MCEditorManager.instance.createTransition_setStartPin ( null );
		}
	}

ProxyABTransition auxTransition;
	bool selectNow = false;

	protected void handleSelectedState(){
		if (Selected) {
			if (!selectNow) {
				auxTransition = Instantiate (transitionPrefab).GetComponent<ProxyABTransition> ();
				auxTransition.Collider.enabled = false;

				selectNow = true;
			}

			if (auxTransition != null) {
				// Move
				auxTransition.LineRenderer.SetPosition( 0, this.transform.position );
				Vector3 mouse = trackingCamera.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, -trackingCamera.transform.position.z));
				//mouse.z = this.transform.position.z;

				auxTransition.LineRenderer.SetPosition( 1, mouse );
			}
		} else {
			selectNow = false;
			if (auxTransition != null) {
				Destroy (auxTransition.gameObject);
			}
		}
	}
	#endregion

	#region INSTANTIATE
	public static Pin instantiate( Pin.PinType pinType, Vector3 position, Transform parent ){
        Pin result;
		if (pinType == PinType.TransitionOut || pinType==PinType.OperatorOut || pinType == PinType.Param)
        {
            result = Instantiate<Pin>(MCEditor_Proxy_Factory.instance.PinOutPrefab, parent);
        } else
        {
            result = Instantiate<Pin>(MCEditor_Proxy_Factory.instance.PinPrefab, parent);
        }                    
		result.Pin_Type = pinType;
		result.transform.position = position;
		result.ProxyParent = parent.gameObject.GetComponent<MCEditor_Proxy> ();

        if(result.Pin_Type != PinType.ActionParam)
        {
            result.SetPinColor();
        }

        return result;
	}

    public void SetPinColor()
    {
        Color color = new Color();
        string type = "";

        if (this.Pin_Type == Pin.PinType.OperatorOut)
        {
            ProxyABOperator parent = this.GetComponentInParent<ProxyABOperator>();
            string opeParentType = parent.AbOperator.ClassName;
            type = opeParentType.Split('_')[1];            

            this.regularColor = PinColor.GetColorPinFromType(type);            

            if (parent.getOutcomeType().ToString().Contains("Tab"))
            {
                SetTableColor();
            }
            //((ProxyABAction)start.ProxyParent).getParamOperator(start.Pin_order.OrderPosition - 1).getOutcomeType();
        }
        else if (this.Pin_Type == Pin.PinType.OperatorIn)
        {
            ProxyABOperator parent = this.GetComponentInParent<ProxyABOperator>();
            int curPinIn = parent.CurPinIn;
            parent.CurPinIn++;
            type = parent.AbOperator.getIncomeType(curPinIn).ToString();
            this.regularColor = PinColor.GetColorPinFromType(type);            
            if (parent.getIncomeType(pin_order.OrderPosition).ToString().Contains("Tab"))
            {
                SetTableColor();
            }
        }
        else if(this.Pin_Type == Pin.PinType.Param)
        {
            ProxyABParam parent = this.GetComponentInParent<ProxyABParam>();
            type = parent.AbParam.GetType().ToString();
            this.regularColor = PinColor.GetColorPinFromType(type);           
        }
        else if (this.pin_Type == Pin.PinType.Condition)
        {
            type = "Bool";
            this.regularColor = PinColor.GetColorPinFromType(type);
        }
        else if (this.pin_Type == Pin.PinType.ActionParam)
        {
            if (this.GetComponentInParent<ProxyABAction>().AbState.Action.Parameters.Length > 0)
            {
                Debug.Log("Pin order : " + (pin_order.OrderPosition));
                type = this.GetComponentInParent<ProxyABAction>().AbState.Action.Parameters[pin_order.OrderPosition-1].GetType().ToString();
                this.regularColor = PinColor.GetColorPinFromType(type);                
            }            
        }
        else
        {
            this.regularColor = Color.white;
        }

        // Set toolTipsText
        if (type.Contains("Bool"))
        {
            base.toolTipText = "Bool";
        }
        else if (type.Contains("Scal"))
        {
            base.toolTipText = "Scal";
        }
        else if (type.Contains("Text") || type.Contains("Txt"))
        {
            base.toolTipText = "Text";
        }
        else if (type.Contains("Color"))
        {
            base.toolTipText = "Color";
        }
        else if (type.Contains("Ref"))
        {
            base.toolTipText = "Ref";
        }
        else if (type.Contains("Vec"))
        {
            base.toolTipText = "Vec";
        }
        else
        {
            base.toolTipText = "";
        }
        if (type.Contains("Tab"))
        {
            base.toolTipText += "[]";
        }
    }

    public void SetTableColor()
    {
        /*Renderer rend = this.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        //Gold reflection
        rend.material.SetColor("_SpecColor", new Color(0.85f, 0.65f, 0.12f));
        float shininess = Mathf.PingPong(Time.time, 1.0F);
        rend.material.SetFloat("_Shininess", shininess);*/
    }
    /*public void FixedUpdate()
    {
        if(this.AssociatedTransitions.Count != 0)
        {
            Renderer rend = this.GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            //Gold reflection
            rend.material.SetColor("_SpecColor", new Color(0.85f, 0.65f, 0.12f));
            float shininess = Mathf.PingPong(Time.time * 2f, 1F);
            rend.material.SetFloat("_Shininess", shininess);
        }        
    }*/

    // Pin : Action : fixed number of pins
    public static Vector3 calculatePinPosition( ProxyABAction action, Pin.PinType pinType, ProxyABAction parent ){
		float radius = parent.transform.localScale.y / 2;
		if (pinType == Pin.PinType.TransitionIn) {
			// Income
			return new Vector3 (parent.transform.position.x, parent.transform.position.y + radius, parent.transform.position.z);
		} else {
			// Param
			// +1 income
			int childCount = parent.AllPins.Count + 1;
			int totalPins = parent.AbState.Action.Parameters.Length + 1;
			float angle = childCount * (2 * Mathf.PI) / (float)totalPins;
			return new Vector3 (
				parent.transform.position.x + (radius * Mathf.Sin ( angle ) ),
				parent.transform.position.y + (radius * Mathf.Cos ( angle ) ),
				parent.transform.position.z
			);
		}
	}

	// Pin : State : Outcome pins is variant
	public static Vector3 calculatePinPosition( ProxyABState parent ){
		float radius = parent.transform.localScale.y / 2;
		return new Vector3 (parent.transform.position.x, parent.transform.position.y + radius, parent.transform.position.z);
	}

	public static Vector3 calculatePinPosition( ABState state, GameObject stateParent, bool transitionOut, int curTransition = 0 ){
		float radius = stateParent.transform.localScale.y / 2;
		if (transitionOut) {
			return new Vector3 (
				stateParent.transform.position.x + (radius * Mathf.Cos (curTransition * (2 * Mathf.PI) / Mathf.Max (1, state.Outcomes.Count))),
				stateParent.transform.position.y + (radius * Mathf.Sin (curTransition * (2 * Mathf.PI) / Mathf.Max (1, state.Outcomes.Count))),
				stateParent.transform.position.z
			);
		} else {
			return new Vector3(
				stateParent.transform.position.x + (radius * Mathf.Cos (curTransition * (2 * Mathf.PI) / Mathf.Max (1, state.Outcomes.Count))),
				stateParent.transform.position.y + (radius * Mathf.Sin (curTransition * (2 * Mathf.PI) / Mathf.Max (1, state.Outcomes.Count))),
				stateParent.transform.position.z
			);
		}
	}

	public static Vector3 calculatePinPosition( ProxyABTransition parent ){
		return parent.transform.position;
	}

	public static Vector3 calculatePinPosition( ProxyABOperator parent ){
		int childCount = parent.transform.childCount;
		float radius = parent.transform.localScale.y / 2;
		return new Vector3(parent.transform.position.x + (radius * Mathf.Cos(childCount * (2 * Mathf.PI) / 4)),
			parent.transform.position.y + (radius* Mathf.Sin(childCount * (2 * Mathf.PI) / 4)),
			parent.transform.position.z
		);
	}

	// Pin : Param : fixed number of pins
	public static Vector3 calculatePinPosition( ProxyABParam parent ){
		float radius = parent.transform.localScale.y / 2;
		return new Vector3 (parent.transform.position.x, parent.transform.position.y - radius, parent.transform.position.z);
	}
	#endregion
}
