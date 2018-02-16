using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    [SerializeField]
    private string name;
    private List<Pin> incomes;
    bool isLoaded = false;


    public ABNode[] Inputs {
        get {
        }

        set {
        }
    }

    public Pin Outcome
    {
        get
        {
        }

        set
        {
        }
    }

    public List<Pin> Incomes
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
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

    // Use this for initialization
    void Start () {
        if (IsLoaded)// when the Operator is created by loading behavior file
            IsLoaded = false;
        }
        else // when the OPerator is created in the editor.
        {
            Text opeName = this.GetComponentInChildren<Text>();
            opeName.text = this.Name;
            incomes.Add(MCEditorManager.instance.CreatePinSynthTree(this.transform, true));

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
