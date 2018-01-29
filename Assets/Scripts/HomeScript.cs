using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScript : MonoBehaviour {
    [AttrName(Identifier = "pos")]
    [SerializeField]
    private Vector2 location;
    [AttrName(Identifier = "bRes")]
    [SerializeField]
    private float redResAmout;
    [AttrName(Identifier = "rRes")]
    [SerializeField]
    private float greenResAmout;
    [AttrName(Identifier = "gRes")]
    [SerializeField]
    private float blueResAmout;
    [AttrName(Identifier = "population")]
    private Dictionary<string, int> population = new Dictionary<string, int>();

    public float RedResAmout
    {
        get
        {
            return redResAmout;
        }

        set
        {
            redResAmout = value;
        }
    }

    public float GreenResAmout
    {
        get
        {
            return greenResAmout;
        }

        set
        {
            greenResAmout = value;
        }
    }

    public float BlueResAmout
    {
        get
        {
            return blueResAmout;
        }

        set
        {
            blueResAmout = value;
        }
    }

    public Dictionary<string, int> Population
    {
        get
        {
            return population;
        }

        set
        {
            population = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        location.x = this.transform.position.x;
        location.y = this.transform.position.y;
    }
}
