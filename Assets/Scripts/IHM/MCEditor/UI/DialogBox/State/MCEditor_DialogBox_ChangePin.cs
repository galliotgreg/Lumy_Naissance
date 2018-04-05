using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_ChangePin : MCEditor_DialogBox_Proxy {
	[SerializeField]
	UnityEngine.UI.InputField value;

	ABState state;
	ProxyABTransition transition;
	Pin pin;

	// Use this for initialization
	void Start () {
		base.Start ();
		value.ActivateInputField ();
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region implemented abstract members of MCEditor_DialogBox_Proxy

	protected override string dialogTitle ()
	{
		return "Nombre de la Transition";
	}

	protected override void confirmProxy ()
	{
		// Check value
		try{
			int v = int.Parse (value.text);
			if( v > 0 && v <= this.state.Outcomes.Count ){
				// Change Model
				int previousIndex = this.state.Outcomes.IndexOf(this.transition.Transition);
				ABTransition previousTransition = this.state.Outcomes[ v-1 ];
				this.state.Outcomes[ v-1 ] = this.transition.Transition;
				this.state.Outcomes[ previousIndex ] = previousTransition;

				// Change View
				// search for the previousTransition and change it
				foreach( Pin p in this.proxy.getPins(Pin.PinType.TransitionOut) ){
					if( p.Pin_order.OrderPosition == v ){
						p.Pin_order.OrderPosition = this.pin.Pin_order.OrderPosition;
						break;
					}
				}
				// Change value into the current pin
				this.pin.Pin_order.OrderPosition = v;

				MCEditorManager.instance.exchangeTransitionPosition( this.transition.Transition, previousTransition );
			}
		}catch(System.Exception ex){
			Debug.LogError ("Value is not a valid");
		}
	}

	protected override void deactivateProxy ()
	{
	}
	#endregion

	#region INSTANTIATE
	public static MCEditor_DialogBox_ChangePin instantiate ( Pin pin, MCEditor_DialogBox_ChangePin prefab, Vector3 position, Transform parent ){
		if (pin.ProxyParent is ProxyABAction || pin.ProxyParent is ProxyABState) {
			MCEditor_DialogBox_ChangePin result = (MCEditor_DialogBox_ChangePin)MCEditor_DialogBox_Proxy.instantiate (pin.ProxyParent, prefab, position, parent);
			result.config ( pin );

			return result;
		}
		return null;
	}

	private void config( Pin pin ){
		this.pin = pin;
		this.transition = pin.AssociatedTransitions[0];

		if (pin.ProxyParent is ProxyABState) {
			this.state = ((ProxyABState)pin.ProxyParent).AbState;
		}else if (pin.ProxyParent is ProxyABAction) {
			this.state = ((ProxyABAction)pin.ProxyParent).AbState;
		}

		// Update view
		value.text = pin.Pin_order.OrderPosition.ToString();
	}
	#endregion
}
