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

    }

#region Instantiate
public static ProxyABAction instantiate( ABState state ){
	return instantiate( state, calculateActionPosition( MCEditorManager.instance.MCparent ), MCEditorManager.instance.MCparent );
}
public static ProxyABAction instantiate( ABState state, Vector3 position, Transform parent){
	ProxyABAction result = Instantiate<ProxyABAction>( MCEditor_Proxy_Factory.instance.ActionPrefab, parent );
	result.IsLoaded = true;
	result.transform.position = position;

	UnityEngine.UI.Text actionName = result.GetComponentInChildren<UnityEngine.UI.Text>();
	actionName.text = state.Name;

	result.GetComponent<ProxyABAction>().AbState = state;

	// Create Pins
	if (state.Action.Parameters != null) {
		foreach (IABGateOperator param in state.Action.Parameters) {
			Pin start = Pin.instantiate (Pin.PinType.ActionParam, Pin.calculatePinPosition (Pin.PinType.ActionParam,result), result.transform);
		}
	}

	// Income
	Pin.instantiate (Pin.PinType.TransitionIn, Pin.calculatePinPosition (Pin.PinType.TransitionIn,result), result.transform);

	return result;
}

public static Vector3 calculateActionPosition( Transform parent ){
	return new Vector3(UnityEngine.Random.Range(-5, 5),UnityEngine.Random.Range(-5, 5), parent.position.z);
}
#endregion

	#region implemented abstract members of MCEditor_Proxy

	public override void click ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}