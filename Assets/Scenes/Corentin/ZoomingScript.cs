using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomingScript : MonoBehaviour
{

    public float minSize = 1f;
    public float maxSize = 40f;
    private float sensitivity = 10f;
    private GameObject dialogBox;
    private bool isZoomable = true;

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("MCEditor_DialogBox"))
        {
            isZoomable = false; 
        }
        else isZoomable = true;

        if (isZoomable)
        {
            float prev_size = GameObject.Find("Camera").GetComponent<Camera>().orthographicSize;
            float new_size = prev_size - Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            new_size = Mathf.Clamp(new_size, minSize, maxSize);
            GameObject.Find("Camera").GetComponent<Camera>().orthographicSize = new_size;
        }

        /*dialogBox = GameObject.FindGameObjectWithTag("MCEditor_DialogBox");
        Vector3 posBox = dialogBox.GetComponent<RectTransform>().position;
        if (prev_size < new_size) //dezooming
        {
            posBox = new Vector3(posBox.x - (new_size - prev_size) / 10, posBox.y - (new_size - prev_size) / 10, 0.0f);
        }
        else //zooming
        {
            posBox = new Vector3(posBox.x + (prev_size - new_size) / 10, posBox.y + (prev_size - new_size) / 10, 0.0f);
        }

        dialogBox.GetComponent<RectTransform>().SetPositionAndRotation(posBox, new Quaternion() );*/
    }
}
