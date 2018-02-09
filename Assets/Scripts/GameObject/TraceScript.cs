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

	[SerializeField]
	private float lifeTime;

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
			return this.location;
		}
		set{
			this.location = value;
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
		this.lifeTime = 10;
	}
	
	// Update is called once per frame
	void Update () {
		this.Location = positionFromTransform ();

		this.lifeTime -= Time.deltaTime;
		if( lifeTime <= 0 ){
			Unit_GameObj_Manager.instance.destroyTrace (this);
		}
	}

	Vector2 positionFromTransform(){
		return new Vector2(transform.position.x, transform.position.z);
	}

	void changeColor(){}
}
