using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour {
    // GameObject Identification
    #region attributes
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

    private List<Transform> spawnPoints;

    #endregion
    #region Accesseur
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
    #endregion

    void Awake(){
		key = this.GetHashCode();
	}

    // Use this for initialization
    void Start () {
        randomizeLoccation(); 
		this.Location = positionFromTransform ();
	}
	
	// Update is called once per frame
	void Update () {
		this.Location = positionFromTransform ();
        if(Stock <=0 )
        {
            Destroy(gameObject); 
        }
	}

	Vector2 positionFromTransform(){
		return new Vector2(transform.position.x, transform.position.z);
	}

    private void randomizeLoccation()
    { 
        GameObject[] go = GameObject.FindGameObjectsWithTag("SpawnPoint");
        if(go.Length <=0)
        {
            return; 
        }
        for(int i=0; i<go.Length; i++)
        {
            if(go[i].transform.IsChildOf(this.transform))
            {
                spawnPoints.Add(go[i].transform);
            }     
        }
        int index = Random.Range(0, spawnPoints.Count - 1);
        this.transform.position = spawnPoints[index].position; 
    }
}
