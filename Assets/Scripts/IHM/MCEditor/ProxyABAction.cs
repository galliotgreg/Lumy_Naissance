using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProxyABAction : MonoBehaviour {

    [SerializeField]
    private string name;
    [SerializeField]
    private string action;
    private ABState abState;
    private List<Pin> pinList;
    private bool isLoaded = false;

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

    public List<Pin> PinList
    {
        get
        {
            return pinList;
        }

        set
        {
            pinList = value;
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

    private void Awake()
    {
        pinList = new List<Pin>();
    }

    // Use this for initialization
    void Start () {
        if (IsLoaded)// when the Action is created by loading behavior file
        {            
            isLoaded = false;
        }
        else // when the Action is created in the editor.
        {
            ABAction abAction = ABActionFactory.CreateAction(action.ToLower());
            Text actionName = this.GetComponentInChildren<Text>();
            actionName.text = this.name;             
            this.abState = MCEditorManager.instance.AbModel.getState(MCEditorManager.instance.AbModel.AddState(name, abAction));            
            pinList.Add(MCEditorManager.instance.CreatePinState(this.transform, true, false));                        
			pinList.Add(MCEditorManager.instance.CreatePinState(AbState, this.transform, true, false));

            Debug.Log(MCEditorManager.instance.AbModel.States.Count);
        }
    }
	
    public void AddPin(Pin pin)
    {
        PinList.Add(pin);
    }

	// Update is called once per frame
	void Update () {
                
    }
}
