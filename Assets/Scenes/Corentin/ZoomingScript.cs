using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomingScript : MonoBehaviour {

    private float minFov = 15f;
    private float maxFov = 90f;
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
            float fov = Camera.main.fieldOfView;
            fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            Camera.main.fieldOfView = fov;
        //}
    }
}
