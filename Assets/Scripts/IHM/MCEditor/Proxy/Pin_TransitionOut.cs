using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin_TransitionOut : Pin {

	[SerializeField]
	int orderPosition;
	[SerializeField]
	UnityEngine.UI.Text orderPositionText;

	public int OrderPosition {
		get {
			return orderPosition;
		}
		set {
			orderPosition = value;
			OrderPositionText = value.ToString();
		}
	}

	protected string OrderPositionText {
		get {
			return orderPositionText.text;
		}
		set {
			orderPositionText.text = value;
		}
	}

	public static Pin_TransitionOut instantiate( int orderPosition, Vector3 position, Transform parent ){
		Pin_TransitionOut result = (Pin_TransitionOut) Pin.instantiate (PinType.TransitionOut, position, parent);
		result.orderPosition = orderPosition;
		return result;
	}
}
