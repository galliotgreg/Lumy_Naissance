using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MCEditor_Proxy : MonoBehaviour {  

	[SerializeField]
	protected UnityEngine.UI.Text text;

	public UnityEngine.UI.Text Text {
		get {
			return text;
		}
		set {
			text = value;
		}
	}

	public List<Pin> AllPins{
		get{
			return new List<Pin> (this.gameObject.GetComponentsInChildren<Pin> ());
		}
	}

	public List<Pin> getPins( Pin.PinType pinType ){
		List<Pin> result = new List<Pin> ();

		Pin[] pins = this.gameObject.GetComponentsInChildren<Pin>();

		foreach (Pin p in pins) {
			if (p.Pin_Type == pinType) {
				result.Add ( p );
			}
		}

		return result;
	}

	bool isHover = false;
	// Update is called once per frame 
	protected void Update()
	{
		// When the moude is hover the Proxy and the moude is in the Interactable Zone, throw tooltip
		if( isHover && MCEditor_Proxy_Factory.instance.InteractableZone.IsHover ){
			MCEditor_DialogBoxManager.instance.instantiateToolTip(this.transform.position, this.GetType().ToString(), this );
			isHover = false;
		}
	}

    private void OnMouseEnter()
    {
		isHover = true;
    }

    private void OnMouseExit()
    {
		isHover = false;          
    }

    #region VIEW METHODS
    public void SetNodeName(ABNode node)
	{
		SetProxyName( getNodeName( node ) );
	}

    public void SetMacroNodeName(ABNode node)
    {
		SetProxyName( ((IABOperator)(node)).ViewName );
    }

    public void SetProxyName(string name)
	{
		GameObject picto = setImage();

		// Avoid Images on States
		/*if( ! (this is ProxyABState) ){
			picto = MCPictFactory.instance.InstanciatePict(name);
		}*/
        
        if (picto != null)
        {
			text.gameObject.SetActive (false);
        }
        else
        {
            Text.text = name;
       	}
	}

	public GameObject setImage(){
		return MCEditor_ProxyIcon_Manager.instance.getProxyImage(this);
	}

	public string GetProxyName()
	{
		if (this is ProxyABState) {
			return ((ProxyABState)this).AbState.Name;
		}
		else if (this is ProxyABAction) {
			//return ((ProxyABAction)this).AbState.Action.Type.ToString();
			return ((ProxyABAction)this).AbState.Name;
		}
		else if (this is ProxyABOperator) {
            if (((ProxyABOperator)this).isMacroComposant)
            {               
                return ((ProxyABOperator)this).ViewName;
            }
            else
            {
                return getNodeName((ABNode)((ProxyABOperator)this).AbOperator);
            }                 			
		}
		else if (this is ProxyABParam) {
			return ProxyABParam.GetViewValue( ((ProxyABParam)this).AbParam );
		}
		return "NODE";
	}

	public static string getNodeName( ABNode node ){
        string opeName = node.ToString();
        char splitter = '_';
        string[] newName = opeName.Split(splitter);
        string newOpeName = "";

        for (int i = 1; i < newName.Length - 1; i++)
        {
            newOpeName += newName[i];
        }

        return newOpeName;
    }

	public static string typeToString( System.Type type ){
		if( type == typeof(ABBool) ) 	return "Bool";
		if( type == typeof(ABScalar) )	return "Scalar";
		if( type == typeof(ABText) )	return "Text";
		if( type == typeof(ABVec) )		return "Vec";
		if( type == typeof(ABColor) )	return "Color";
		if( type == typeof(ABRef) ) 	return "Ref";
		if( type == typeof(ABTable<ABBool>) )	return "Bool[]";
		if( type == typeof(ABTable<ABScalar>) )	return "Scalar[]";
		if( type == typeof(ABTable<ABText>) )	return "Text[]";
		if( type == typeof(ABTable<ABVec>) )	return "Vec[]";
		if( type == typeof(ABTable<ABColor>) )	return "Color[]";
		if( type == typeof(ABTable<ABRef>) )	return "Ref[]";
		if( type == typeof(ABStar<ABBool>) )	return "Bool*";
		if( type == typeof(ABStar<ABScalar>) )	return "Scalar*";
		if( type == typeof(ABStar<ABText>) )	return "Text*";
		if( type == typeof(ABStar<ABVec>) )		return "Vec*";
		if( type == typeof(ABStar<ABColor>) )	return "Color*";
		if( type == typeof(ABStar<ABRef>) )		return "Ref*";
		return "";
	}
	#endregion

	public static MCEditor_Proxy doubleClicked = null;
	public abstract void deleteProxy ();
	public abstract void doubleClick ();

	#region IPointerClickHandler implementation

	public const float doubleClickIntervalMseconds = 200;

	float lastClick = -1;
	public void OnMouseDown ()
	{
		if (lastClick > 0 && Time.time - lastClick < doubleClickIntervalMseconds/1000f) {
			doubleClick ();
			doubleClicked = this;
		}
		lastClick = Time.time;
	}

	#endregion

	#region IsConnected
	public bool isConnected(){
		// TODO consider operator loop

		// is Init or null
		bool isState = containsStateProxy ();
		if (!isState) {
			System.Object connectedTo = resultConnection ();

			return (connectedTo != null);
		} else {
			return true;
		}
	}
	bool containsStateProxy(){
		return this is ProxyABState || this is ProxyABAction;
	}

	// Return proxy or conditionTransition
	System.Object resultConnection ()
	{
		Pin pin = resultPin ();
		if (pin != null){
			if (pin.AssociatedTransitions.Count > 0) {
				MCEditor_Proxy proxy = pin.AssociatedTransitions [0].oppositeSide (this);
				// Proxy or Condition
				if (proxy != null) {
					return proxy;
				} else {
					// Search Condition
					Pin condition = pin.AssociatedTransitions [0].oppositePin (this);
					if (condition.AssociatedTransitions.Count > 0) {
						return condition.AssociatedTransitions [0];
					}
				}
			}
		}
		return null;
	}
	/// <summary>
	/// Gets the pin that represents the result of the proxy
	/// </summary>
	/// <returns>The pin that represents the result of the proxy</returns>
	protected abstract Pin resultPin ();
	#endregion
}
