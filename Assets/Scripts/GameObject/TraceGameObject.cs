using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceGameObject : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void changeColor(){}
}
