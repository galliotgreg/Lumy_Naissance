using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkCastScript : MonoBehaviour {

    private LineRenderer lineRenderer;

    public float margin = 0.2f;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject parent = transform.parent.gameObject;
        if (parent.GetComponent<LinkCastScript>() == null)
        {
            parent = null;
        }

        if (parent != null)
        {
            Vector3 A = this.transform.position + new Vector3(0f, 0f, -0.1f);
            Vector3 B = parent.transform.position + new Vector3(0f, 0f, -0.1f);
            lineRenderer.SetPosition(0, Vector3.Lerp(A, B, margin));
            lineRenderer.SetPosition(1, Vector3.Lerp(A, B, 1f - margin));
        } 
	}
}
