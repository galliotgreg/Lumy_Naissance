using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceScript : MonoBehaviour {

	// GameObject Identification
	[AttrName(Identifier = "key")]
	[SerializeField]
	private int key;

	[AttrName(Identifier = "color")]
	[SerializeField]
	Color32 color;

	[AttrName(Identifier = "pos")]
	[SerializeField]
	private Vector2 location;

	public Color32 Color {
		get {
			return color;
		}
		set {
			color = value;
			changeColor();
		}
	}

	public Vector2 Location {
		get {
			return new Vector2(this.transform.position.x, this.transform.position.z);
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
