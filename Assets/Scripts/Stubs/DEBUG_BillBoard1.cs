using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_BillBoard1 : MonoBehaviour {

    private Camera my_camera;
    // Use this for initialization

    private void Start() {    
        my_camera = NavigationManager.instance.GetCurrentCamera();
        

    }
    // Update is called once per frame
    void Update() {
        //transform.LookAt(my_camera.transform);
        if (my_camera.name == "Main Camera") {
            my_camera = NavigationManager.instance.GetCurrentCamera(); 
        }


        transform.LookAt(transform.position + my_camera.transform.rotation * Vector3.forward, my_camera.transform.rotation * Vector3.up);
    }
}
