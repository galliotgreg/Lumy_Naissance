using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour {
	// GameObject Identification
	[AttrName(Identifier = "key")]
	[SerializeField]
	private int key;

    [AttrName(Identifier = "stock")]
    [SerializeField]
    private int stock;

    [AttrName(Identifier = "pos")]
    [SerializeField]
	private Vector2 location;
    [AttrName(Identifier = "color")]
    [SerializeField]
    private Color32 color;

	public Vector2 Location {
		get
		{
			return location;
		}
		set{
			this.location = value;
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

    public int Stock
    {
        get
        {
            return stock;
        }

        set
        {
            stock = value;
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
