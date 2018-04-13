using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MCEditor_Proxy : MonoBehaviour {  

	[SerializeField]
	protected UnityEngine.UI.Text text;
	[SerializeField]
	protected UnityEngine.UI.Image image;

	public UnityEngine.UI.Text Text {
		get {
			return text;
		}
		set {
			text = value;
		}
	}

	public Sprite Image {
		get {
			return image.sprite;
		}
		set {
			image.sprite = value;

			// Choose the element which will be activated
			if (value == null) {
				image.gameObject.SetActive (false);
			} else {
				text.gameObject.SetActive (false);
			}
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

    private void OnMouseEnter()
    {
        MCEditor_DialogBoxManager.instance.instantiateToolTip(this.transform.position, this.GetType().ToString(), this );
    }

    private void OnMouseExit()
    {
                  
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
		UnityEngine.UI.Text text = GetComponentInChildren<UnityEngine.UI.Text> ();
		text.text = name;
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

			Debug.Log ( "MCEditor : Selected proxy => "+this.GetProxyName() );
			doubleClicked = this;
		}
		lastClick = Time.time;
	}

	#endregion
}
