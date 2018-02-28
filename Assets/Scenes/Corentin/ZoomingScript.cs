using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomingScript : MonoBehaviour
{

    public float minSize = 1f;
    public float maxSize = 40f;
    private float sensitivity = 10f;
    private bool isZoomable = false;

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            isZoomable = !isZoomable;
        }

        if (isZoomable)
        {*/
        float size = GameObject.Find("Camera").GetComponent<Camera>().orthographicSize;
        size += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        size = Mathf.Clamp(size, minSize, maxSize);
        GameObject.Find("Camera").GetComponent<Camera>().orthographicSize = size;
        //}
    }
}
