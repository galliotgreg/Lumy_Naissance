using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour {
    [AttrName(Identifier = "pos")]
    [SerializeField]
    private Vector2 location;
    [AttrName(Identifier = "color")]
    [SerializeField]
    private Color32 color;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
