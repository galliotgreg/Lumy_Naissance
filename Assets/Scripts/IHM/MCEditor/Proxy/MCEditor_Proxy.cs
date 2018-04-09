using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MCEditor_Proxy : MonoBehaviour {

	[SerializeField]
	UnityEngine.UI.Text text;
	[SerializeField]
	UnityEngine.UI.Image image;

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
			image.preserveAspect = true;

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

	#region VIEW METHODS
	public void SetNodeName(ABNode node)
	{
		UnityEngine.UI.Text operatorName = this.gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
		operatorName.text = getNodeName( node );
	}

    public void SetMacroNodeName(ABNode node)
    {
        UnityEngine.UI.Text operatorName = this.gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
        string temp = ((IABOperator)(node)).ViewName;        
        operatorName.text = temp;
    }

    public void SetProxyName(string name)
	{
		UnityEngine.UI.Text text = this.gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
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
