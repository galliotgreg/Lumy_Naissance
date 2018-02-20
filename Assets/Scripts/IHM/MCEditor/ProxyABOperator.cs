using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProxyABOperator: MonoBehaviour, IProxyABOperator{
    [SerializeField]
    private string name;
    private string type;
    private IABOperator abOperator;
    private Pin outcome = null;
    private List<Pin> incomes;
    bool isLoaded = false;


    public ABNode[] Inputs {
        get {
            return abOperator.Inputs;
        }

        set {
            abOperator.Inputs = value;
        }
    }

    public Pin Outcome
    {
        get
        {
            return outcome;
        }

        set
        {
            outcome = value;
        }
    }

    public List<Pin> Incomes
    {
        get
        {
            return incomes;
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

    public IABOperator AbOperator
    {
        get
        {
            return abOperator;
        }

        set
        {
            abOperator = value;
        }
    }

    // Use this for initialization
    void Start () {
        if (IsLoaded)// when the Operator is created by loading behavior file
        {            
            IsLoaded = false;
        }
        else // when the OPerator is created in the editor.
        {
            Text opeName = this.GetComponentInChildren<Text>();
            opeName.text = this.Name;
            incomes = new List<Pin>();
            incomes.Add(MCEditorManager.instance.CreatePinSynthTree(this.transform, true));
            outcome = MCEditorManager.instance.CreatePinSynthTree(this.transform, true);

            ABParser abParser = new ABParser();
            abOperator = abParser.ParseOperator(type);            
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
