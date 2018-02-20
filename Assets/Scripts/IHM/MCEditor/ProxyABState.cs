using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProxyABState : MonoBehaviour {
    [SerializeField]
    private string name;  
    private ABState abState;
	private Pin income;
    private List<Pin> pinList;
    private bool isLoaded = false;


    public string Name
    {
        get
        {
            return name;
        }

        set
        {
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

	public Pin Income {
		get {
			return income;
		}
		set {
			income = value;
		}
	}

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

    private void Awake()
    {
        PinList = new List<Pin>();
    }

    // Use this for initialization
    void Start()
    {
        if (IsLoaded)// when the Action is created by loading behavior file
        {            
            IsLoaded = false;
        }
        else // when the Action is created in the editor.
        {            
            Text actionName = this.GetComponentInChildren<Text>();
            actionName.text = this.Name;
            this.AbState = MCEditorManager.instance.AbModel.getState(MCEditorManager.instance.AbModel.AddState(Name, null));          

			income = MCEditorManager.getPins( this.gameObject, Pin.PinType.TransitionIn )[0];
			pinList = MCEditorManager.getPins( this.gameObject, Pin.PinType.TransitionOut );

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
