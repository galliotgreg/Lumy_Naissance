using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProxyABState : MCEditor_Proxy {
    [SerializeField]
    private string name;  
    private ABState abState;
    private bool isLoaded = false;

	private Pin extraPin;

	#region PROPERTIES
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

    public List<Pin> Outcomes
    {
        get
        {
			return this.getPins(Pin.PinType.TransitionOut);
        }
    }

	public Pin Income {
		get {
			return this.getPins(Pin.PinType.TransitionIn)[0];
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

	public Pin ExtraPin {
		get {
			return extraPin;
		}
		protected set {
			extraPin = value;
			extraPin.Pin_order.OrderPosition = this.AbState.Outcomes.Count + 1;
		}
	}
	#endregion

    private void Awake(){}

    // Use this for initialization
    void Start()
    {
        if (IsLoaded)// when the Action is created by loading behavior file
        {            
            IsLoaded = false;
        }
        else // when the Action is created in the editor.
        {            
            //this.AbState = MCEditorManager.instance.AbModel.getState(MCEditorManager.instance.AbModel.AddState(Name, null));          

			//income = MCEditorManager.getPins( this.gameObject, Pin.PinType.TransitionIn )[0];
			//pinList = MCEditorManager.getPins( this.gameObject, Pin.PinType.TransitionOut );

            //Debug.Log(MCEditorManager.instance.AbModel.States.Count);
		}
    }

    // Update is called once per frame
    void Update () {
        
    }

	/// <summary>
	/// Create Pin for transitions (if necessary) and set pin orderPosition
	/// </summary>
	public void checkPins(){
		
		List<Pin> pins = this.getPins(Pin.PinType.TransitionOut);

		// Check if transition has pin : create if necessary
		/*Debug.Log(this.AbState.Outcomes.Count);
		for (int j = 0; j < this.AbState.Outcomes.Count; j++) {
			// Search for the pin that contains the transition
			bool transitionFound = false;
			for ( int i = 0; i < pins.Count; i++ ){
				bool pinFound = pins[i].AssociatedTransitions.Count > 0 && this.AbState.Outcomes.Contains( pins[i].AssociatedTransitions[0] );

				if (pinFound) {
					transitionFound = true;
				}
			}

			if (!transitionFound) {
				// Create pin
				Pin_TransitionOut pin = Pin_TransitionOut.instantiate( -1, Pin.calculatePinPosition( this.AbState, this.gameObject, true, pins.Count+1 ), this.transform );
				pins.Add (pin);
				pin.AssociatedTransitions.Add ( this.AbState.Outcomes[j] );
			}
		}*/

		// Check if pin has transition : set orderPosition
		bool extraPinFound = false;
		for ( int i = 0; i < pins.Count; i++ ){
			bool transitionFound = pins[i].AssociatedTransitions.Count > 0 && this.AbState.Outcomes.Contains( pins[i].AssociatedTransitions[0].Transition );

			if (transitionFound) {
				pins[i].Pin_order.OrderPosition = this.AbState.Outcomes.IndexOf( pins[i].AssociatedTransitions[0].Transition )+1;
			}else{
				//pins [i].AssociatedTransitions.Clear ();
				if (extraPinFound) {
					// There is another extra pin : destroy this one
					Destroy( pins[i].gameObject );
				} else {
					// This is the extra Pin
					ExtraPin = pins [i];

					extraPinFound = true;
				}
			}
		}

		if (!extraPinFound) {
			// No extra pin was found : create it
			ExtraPin = MCEditor_Proxy_Factory.instantiatePin( Pin.PinType.TransitionOut, Pin.calculatePinPosition( this.AbState, this.gameObject, true, this.AbState.Outcomes.Count+1 ), this.transform );
		}
	}

	#region INSTANTIATE
	public static ProxyABState instantiate( ABState state, bool init ){
		return instantiate (state, init, calculateStatePosition (MCEditorManager.instance.MCparent), MCEditorManager.instance.MCparent);
	}
	public static ProxyABState instantiate( ABState state, bool init, Vector3 position, Transform parent ){
		ProxyABState result = Instantiate<ProxyABState>( MCEditor_Proxy_Factory.instance.StatePrefab, parent);
		result.IsLoaded = true;
		result.transform.position = position;
		result.AbState = state;

		UnityEngine.UI.Text stateName = result.GetComponentInChildren<UnityEngine.UI.Text>();
		stateName.text = state.Name;

		result.GetComponent<ProxyABState>().AbState = state;

		// Income Pin
		if (!init) {
			Pin.instantiate (Pin.PinType.TransitionIn, Pin.calculatePinPosition (result), result.transform);
		}

		// Extra Outcome Pin : other outcomes will be created with transitions
		Pin pin = MCEditor_Proxy_Factory.instantiatePin(Pin.PinType.TransitionOut, Pin.calculatePinPosition(state, result.gameObject, true, 1), result.transform);
		result.ExtraPin = pin;

		result.checkPins ();

		return result;
	}

	public static Vector3 calculateStatePosition( Transform parent ){
		return new Vector3(UnityEngine.Random.Range(-5, 5),UnityEngine.Random.Range(-5, 5), parent.position.z);
	}

    private void calculatePinPosition()
    {
        float radius = this.transform.localScale.y / 2;
        int outPin = 1;

        if (abState.Name.ToLower() == "init")
        {
            foreach (Pin pin in Outcomes)
            {
                pin.transform.position = new Vector3(
                    this.transform.position.x + (radius * Mathf.Cos(outPin * (2 * Mathf.PI) / Mathf.Max(1, Outcomes.Count))),
                    this.transform.position.y + (radius * Mathf.Sin(outPin * (2 * Mathf.PI) / Mathf.Max(1, Outcomes.Count))),
                    this.transform.position.z
                );
                outPin++;
            }
        } else {
            foreach (Pin pin in Outcomes)
            {
                pin.transform.position = new Vector3(
                    this.transform.position.x + (radius * Mathf.Cos(outPin * (2 * Mathf.PI) / Mathf.Max(1, Outcomes.Count)/-2)),
                    this.transform.position.y + (radius * Mathf.Sin(outPin * (2 * Mathf.PI) / Mathf.Max(1, Outcomes.Count)/-2)),
                    this.transform.position.z
                );
                outPin++;
            }
        }
    }
	#endregion

	#region implemented abstract members of MCEditor_Proxy

	public override void doubleClick ()
	{
		// Not init
		if( this.AbState.Id != MCEditorManager.instance.AbModel.InitStateId ){
			Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
			MCEditor_DialogBoxManager.instance.instantiateStateName (this, pos);
		}
	}

	#endregion
}
