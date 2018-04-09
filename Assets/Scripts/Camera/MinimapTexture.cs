using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTexture : MonoBehaviour {

    [SerializeField]
    private RenderTexture renderTextureMinimap;

	// Use this for initialization
	void Start () {
        transform.GetComponent<Camera>().targetTexture = renderTextureMinimap;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
