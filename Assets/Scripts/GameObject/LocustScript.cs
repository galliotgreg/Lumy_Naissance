using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocustScript : MonoBehaviour {
	// GameObject Identification
	[AttrName(Identifier = "key")]
	[SerializeField]
	private int key;

    [AttrName(Identifier = "pos")]
    [SerializeField]
	private Vector2 location;

	public Vector2 Location {
		get {
			return location;
		}
		set {
			location = value;
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
		this.Location = positionFromTransform ();
	}

	// Update is called once per frame
	void Update () {
		this.Location = positionFromTransform ();
	}

	Vector2 positionFromTransform(){
		return new Vector2(transform.position.x, transform.position.z);
	}
}
