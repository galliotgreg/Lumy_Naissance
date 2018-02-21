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

			pinList = MCEditorManager.getPins ( this.gameObject, Pin.PinType.ActionParam );

            Debug.Log(MCEditorManager.instance.AbModel.States.Count);
            
        }
    }
	
    public void AddPin(Pin pin)
    {
        PinList.Add(pin);
        calculatePinPosition();
    }

    public void calculatePinPosition()
    {
        int childCount = pinList.Count;
        float radius = this.transform.localScale.y / 2;
        int i = 1;
        foreach (Pin pin in pinList)
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
}
