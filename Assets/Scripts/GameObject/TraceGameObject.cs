using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceGameObject : MonoBehaviour {

	// GameObject Identification
	[AttrName(Identifier = "key")]
	[SerializeField]
	private int key;

	[SerializeField]
	Color32 color;

	public Color32 Color {
		get {
			return color;
		}
		set {
			color = value;
			changeColor();
		}
	}
	public int Key {
		get {
			return key;
		}
	}

	void Awake(){
		key = this.GetHashCode();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void changeColor(){}
}
