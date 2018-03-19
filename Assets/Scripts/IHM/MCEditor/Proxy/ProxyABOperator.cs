using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProxyABOperator: MCEditor_Proxy, IProxyABOperator{
    [SerializeField]
    private string name;
    private string type;
    private IABOperator abOperator;
    int curPinIn = 0;
    bool isLoaded = false;
    bool isPositioned = false;
    public bool isMacroComposant = false;

    #region PROPERTIES
    public ABNode[] Inputs {
        get {
            return abOperator.Inputs;
        }

        set {
            abOperator.Inputs = value;
        }
    }

    public virtual string ViewName
    {
        get
        {
            return abOperator.ViewName;
        }

        set
        {
            throw new System.NotSupportedException();
        }
    }

    public virtual string SymbolName
    {
        get
        {
            throw new System.NotSupportedException();
        }

        set
        {
            throw new System.NotSupportedException();
        }
    }

    public Pin Outcome
    {
        get
        {
			return getPins( Pin.PinType.OperatorOut )[0];
        }
    }

    public List<Pin> Incomes
    {
        get
        {
			return getPins( Pin.PinType.OperatorIn );
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

    public bool IsPositioned
    {
        get
        {
            return isPositioned;
        }

        set
        {
            isPositioned = value;
        }
    }

    public int CurPinIn
    {
        get
        {
            return curPinIn;
        }

        set
        {
            curPinIn = value;
        }
    }

    public string ClassName
    {
        get
        {
            return abOperator.ClassName;
        }

        set
        {
            abOperator.ClassName = value;
        }
    }
    #endregion

    // Use this for initialization
    void Start () {
        if (IsLoaded)// when the Operator is created by loading behavior file
        {            
            IsLoaded = false;
        }
        else // when the OPerator is created in the editor.
        {         
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public ABNode getParamOperator( int index ){
		return this.AbOperator.Inputs [index];
	}

	#region INSTANTIATE
	public static ProxyABOperator instantiate( IABOperator operatorObj, bool isLoaded ){
		return instantiate ( operatorObj, isLoaded, calculateOperatorPosition( MCEditorManager.instance.MCparent ), MCEditorManager.instance.MCparent );
	}
	public static ProxyABOperator instantiate( IABOperator operatorObj, bool isLoaded, Vector3 position, Transform parent ){
		ProxyABOperator result = Instantiate<ProxyABOperator> (MCEditor_Proxy_Factory.instance.OperatorPrefab, parent);        
		result.IsLoaded = isLoaded;
		result.transform.position = position;
		result.AbOperator = operatorObj;

        //TODO : REFACTO avec interface IABMacroOperator
        if (operatorObj.GetType().ToString().Contains("Macro")){
            Renderer rend = result.GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", Color.red);
            result.isMacroComposant = true;
        }

        if (result.isMacroComposant)
        {
            result.SetMacroNodeName((ABNode)operatorObj);
        }
        else
        {
            result.SetNodeName((ABNode)operatorObj);
        }

        // Create Pins
        if (operatorObj.Inputs.Length <= 3)
        {
			for(int i=0; i<operatorObj.Inputs.Length; i++){
				ABNode inputNode = operatorObj.Inputs [i];
                Pin start = Pin.instantiate(Pin.PinType.OperatorIn, Pin.calculatePinPosition(result), result.transform);
				start.Pin_order.OrderPosition = i + 1;
            }
        }
        // Do not show 32 pins on an operator, TODO : aggregator cases
        else
        {
            Pin start = Pin.instantiate(Pin.PinType.OperatorIn, Pin.calculatePinPosition(result), result.transform);
			start.Pin_order.OrderPosition = 1;
        }
		

		// Outcome pin
		Pin.instantiate( Pin.PinType.OperatorOut, Pin.calculatePinPosition (result), result.transform );

		return result;
	}

    public int GetAvailablePinEnter()
    {
        bool isFind = false;
        int i = 0;
        int pinAvailable = -1;

        while (!isFind && i < this.abOperator.Inputs.Length)
        {
            if(this.abOperator.Inputs[i] == null)
            {
                pinAvailable = i;
                isFind = true;
            }
            i++;
        }

        if(pinAvailable != -1)
        {
            return pinAvailable;
        }
        else if(!isFind)
        {
            Debug.Log("No more place in input for this operator");
            return -1;
        } else
        {
            return -1;
        }

        
    }

	public static Vector3 calculateOperatorPosition( Transform parent ){
		return new Vector3(UnityEngine.Random.Range(-5, 5),UnityEngine.Random.Range(-5, 5), parent.position.z);
	}

    private void calculatePinPosition()
    {
        float radius = this.transform.localScale.y / 2;
        int outPin = 1;

        foreach (Pin pin in Incomes)
        {
            pin.transform.position = new Vector3(
                this.transform.position.x + (radius * Mathf.Cos(outPin * (2 * Mathf.PI) / Mathf.Max(1, Incomes.Count) / 2)),
                this.transform.position.y + (radius * Mathf.Sin(outPin * (2 * Mathf.PI) / Mathf.Max(1, Incomes.Count) / 2)),
                this.transform.position.z
            );
            outPin++;
        }

        Outcome.transform.position = new Vector3(
                this.transform.position.x,
                this.transform.position.y - radius,
                this.transform.position.z);
    }
    #endregion

	#region implemented abstract members of MCEditor_Proxy

	public override void doubleClick ()
	{
		// Nothing to do
	}

	public override void deleteProxy ()
	{
		MCEditorManager.instance.deleteProxy ( this );
	}

	#endregion

	public System.Type getOutcomeType ()
	{
		return this.AbOperator.getOutcomeType ();
	}

	public System.Type getIncomeType( int index )
	{
		return this.AbOperator.getIncomeType (index);
	}

	public bool acceptIncome (int index, System.Type income)
	{
		return this.AbOperator.acceptIncome (index, income);
	}
}
