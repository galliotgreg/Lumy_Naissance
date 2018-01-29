using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceAction : MonoBehaviour {
    // Workaround for script enabling issues
    public bool activated;

    [SerializeField]
    private Vector3[] path;
    [SerializeField]
    private Color32 color;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!activated)
        {
            return;
        }
	}
}
