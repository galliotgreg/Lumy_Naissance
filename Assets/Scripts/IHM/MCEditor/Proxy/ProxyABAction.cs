using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProxyABAction : MCEditor_Proxy {
	[SerializeField]
	private string name;
	[SerializeField]
	private string action;
	private ABState abState;
	private bool isLoaded = false;


    #region PROPERTIES
    public bool IsLoaded
	{
		get
{
	return isLoaded;
}

set
{
	isLoaded = value;
}
}

	public string Name {
		get {
			return name;
		}
		set {
			UnityEngine.UI.Text text = GetComponentInChildren<UnityEngine.UI.Text> ();
			text.text = value;
			abState.Name = value;
			name = value;
		}
	}

public ABState AbState
{
	get
	{
		return abState;
	}

	set
	{
		abState = value;
		if (value != null) {
			Name = value.Name;
		}
	}
}

public List<Pin> ActionParams
{
	get
	{
		return this.getPins(Pin.PinType.ActionParam);
	}
}

public Pin Income {
	get {
		return this.getPins(Pin.PinType.TransitionIn)[0];
	}
}
#endregion

	void Awake()
	{
		//pinList = new List<Pin>();
	}

	// Use this for initialization
	void Start () {
		if (IsLoaded)// when the Action is created by loading behavior file
		{            
			isLoaded = false;
		}
		else // when the Action is created in the editor.
		{
			/*ABAction abAction = ABActionFactory.CreateAction(action.ToLower());
		            Text actionName = this.GetComponentInChildren<Text>();
		            actionName.text = this.name;*/

			// TODO ATTENTION
			//this.abState = MCEditorManager.instance.AbModel.getState(MCEditorManager.instance.AbModel.AddState(name, abAction));
		}
		calculatePinPosition ();
	}

	public void calculatePinPosition() 
	{ 
		int childCount = AllPins.Count; 
		float radius = this.transform.localScale.y / 2; 
		int i = 1; 
		foreach (Pin pin in AllPins) 
		{ 
			pin.transform.position = new Vector3(this.transform.position.x + (radius * Mathf.Cos(childCount * (i * Mathf.PI) / 4)), 
				this.transform.position.y + (radius * Mathf.Sin(childCount * (i * Mathf.PI) / 4)), 
				this.transform.position.z); 
			i++; 
		} 
	} 

	// Update is called once per frame 
	void Update () {
        if (timeOnHover > 0 && timeOnHover + timeOnHoverWait < Time.time )
        {
            if (!toolTipIsCreated 
                && !(MCToolManager.instance.CurrentTool == MCToolManager.ToolType.Hand))
            {
                toolTipIsCreated = true;
                toolTip = MCEditor_DialogBoxManager.instance.instantiateToolTip(this.transform.position, this.GetType().ToString(), this);
            }            
        }
    }

	public Pin getParamPin( int index ){
		List<Pin> pins = getPins (Pin.PinType.ActionParam);
		foreach (Pin p in pins) {
			if (p.Pin_order.OrderPosition == index + 1) {
				return p;
			}
		}
		return null;
	}

	public IABOperator getParamOperator( int index ){
		return this.AbState.Action.Parameters [index];
	}

#region Instantiate
public static ProxyABAction instantiate( ABState state ){
	return instantiate( state, calculateActionPosition( MCEditorManager.instance.MCparent ), MCEditorManager.instance.MCparent );
}
public static ProxyABAction instantiate( ABState state, Transform parent ){
	return instantiate( state, calculateActionPosition( parent ), parent );
}
public static ProxyABAction instantiate( ABState state, Vector3 position, Transform parent){
	ProxyABAction result = Instantiate<ProxyABAction>( MCEditor_Proxy_Factory.instance.ActionPrefab, parent );
	result.IsLoaded = true;
	result.transform.position = position;

	result.AbState = state;

	// Create Pins
	if (state.Action.Parameters != null) {
		for (int i = 0; i < state.Action.Parameters.Length; i++) {
			IABGateOperator param = state.Action.Parameters [i];
				Pin p = Pin.instantiate (Pin.PinType.ActionParam, Pin.calculatePinPosition (result, Pin.PinType.ActionParam, result), result.transform);
				p.Pin_order.OrderPosition = i+1;
                p.SetPinColor();
		}
	}

	// Income
	Pin.instantiate (Pin.PinType.TransitionIn, Pin.calculatePinPosition (result,Pin.PinType.TransitionIn,result), result.transform).Pin_order.OrderPosition = 0;

	return result;
}

public static Vector3 calculateActionPosition( Transform parent ){
	return new Vector3(UnityEngine.Random.Range(-5, 5),UnityEngine.Random.Range(-5, 5), parent.position.z);
}
#endregion

	#region implemented abstract members of MCEditor_Proxy

	public override void doubleClick ()
	{
		Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
		MCEditor_DialogBoxManager.instance.instantiateActionName (this, pos);
	}

	public override void deleteProxy ()
	{
		MCEditorManager.instance.deleteProxy ( this );
	}

	#endregion
}