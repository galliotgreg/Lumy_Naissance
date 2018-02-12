using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceUnitGameObject : MonoBehaviour {

	Color32 color;

	public Color32 Color {
		get {
			return color;
		}
		set {
			color = value;
			this.GetComponent<MeshRenderer> ().material.color = value;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
