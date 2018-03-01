using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin_Order : MonoBehaviour {

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
			// Deactivate the gameObject if the value <= 0
			if (orderPosition <= 0) {
				if (this.gameObject.activeSelf) {
					this.gameObject.SetActive (false);
				}
			} else {
				if (!this.gameObject.activeSelf) {
					this.gameObject.SetActive (true);
				}
			}
			orderPositionText.text = value;
		}
	}

	void Awake(){
		OrderPosition = 0;
	}
	void Start(){
		
	}
	void Update(){
		
	}
}
