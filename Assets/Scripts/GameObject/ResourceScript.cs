﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour {
	// GameObject Identification
	[AttrName(Identifier = "key")]
	[SerializeField]
	private int key;

    [AttrName(Identifier = "pos")]
    [SerializeField]
	private Vector2 location;
    [AttrName(Identifier = "color")]
    [SerializeField]
    private Color32 color;

	public Vector2 Location {
		get {
			return location;
		}
		set {
			location = value;
		}
	}

    public Color32 Color
    {
        get
        {
            return color; 
        }

        set
        {
            color = value; 
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
}
