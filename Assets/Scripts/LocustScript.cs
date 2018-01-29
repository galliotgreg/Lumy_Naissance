using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocustScript : MonoBehaviour {
    [AttrName(Identifier = "pos")]
    [SerializeField]
    private Vector2 location;
    // Use this for initialization
    void Start () {
        location.x = this.transform.position.x;
        location.y = this.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        location.x = this.transform.position.x;
        location.y = this.transform.position.y;
    }
}
