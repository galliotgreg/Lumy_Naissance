using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin_Order : MonoBehaviour {

	[SerializeField]
	int orderPosition;
	[SerializeField]
	UnityEngine.UI.Text orderPositionText;

	Vector3 transitionDirection;
	RectTransform thisRect;

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

	public Vector3 TransitionDirection {
		get {
			return transitionDirection;
		}
		set {
			transitionDirection = value;

			// define Panel position
			if (Mathf.Abs (value.x) < Mathf.Abs (value.y)) {
				// TOP DOWN
				if( value.y > 0 ){
					// TOP
					thisRect.localPosition = new Vector3(0,1, thisRect.localPosition.z);
				}else{
					// DOWN
					thisRect.localPosition = new Vector3(0,-1, thisRect.localPosition.z);
				}
			} else {
				// RIGHT LEFT
				if( value.x > 0 ){
					// RIGHT
					thisRect.localPosition = new Vector3(1,0, thisRect.localPosition.z);
				}else{
					// LEFT
					thisRect.localPosition = new Vector3(-1,0, thisRect.localPosition.z);
				}
			}
		}
	}

	void Awake(){
		OrderPosition = 0;
		thisRect = this.gameObject.GetComponent<RectTransform> ();
	}
	void Start(){
		
	}
	void Update(){
		
	}
}
