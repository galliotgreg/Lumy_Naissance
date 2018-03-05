using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropScript : MonoBehaviour
{

    GameObject getTarget;
    bool isMouseDragging;
    Vector3 offsetValue;
    Vector3 positionScreen;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            getTarget = ReturnClickedObject(out hitInfo);
            if (getTarget != null && getTarget.name != "pin(Clone)" && getTarget.name != "pinOut(Clone)")
            {
                isMouseDragging = true;
                Debug.Log(getTarget);
                //convert world pos => screen pos
                //positionScreen = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(getTarget.transform.position);
                //offsetValue = getTarget.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            }
        }

        //Mouse Button Up
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
        }

        //Mouse moving
        if (isMouseDragging)
        {
            //Debug.Log("isMouseDragging = " + isMouseDragging);
            //tracking mouse pos
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -GameObject.Find("Camera").GetComponent<Camera>().transform.position.z);

            //converting screen pos => world pos with offset.
            Vector3 currentPosition = GameObject.Find("Camera").GetComponent<Camera>().ScreenToWorldPoint(currentScreenSpace);// + offsetValue;

            //update target current postion.
            getTarget.transform.position = currentPosition;
            //Debug.Log("nouvelle pos du node :" + getTarget.transform.position);
        }
    }

    //Method to Return Clicked Object
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = GameObject.Find("Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
}
