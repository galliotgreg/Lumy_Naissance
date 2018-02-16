using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour {
	bool hover = false;

	public bool CanDrop {
		get {
			return hover;
		}
	}

	void Start(){
	}
	void Update(){
	}

	void OnMouseEnter(){
		hover = true;
	}
	void OnMouseExit(){
		hover = false;
	}
}
