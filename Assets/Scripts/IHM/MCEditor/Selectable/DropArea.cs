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

	public void MouseEnter(){
		hover = true;
	}
	public void MouseExit(){
		hover = false;
	}
}
