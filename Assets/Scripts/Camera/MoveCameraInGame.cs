using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraInGame : MonoBehaviour {

    [SerializeField]
    private GameObject camera;


    [SerializeField]
    private float speedCamera = 10;


    [SerializeField]
    private float maxX = 27;
    [SerializeField]
    private float minX = 9;
    [SerializeField]
    private float maxY = -4;
    [SerializeField]
    private float minY = -38;
    

    private Vector3 cameraPos;

    //ZOOM
    private float currentZoom;
    [SerializeField]
    private Vector2 zoomRange = new Vector2(-10,10);
    [SerializeField]
    private Vector2 zoomAngleRange = new Vector2(20, 70); 
    [SerializeField]
    private float zoomSpeed = 10;
    private Vector3 initPos;
    private Vector3 initRotation;
    private float zoomRotation = 1;

    // Use this for initialization
    void Start () {
       
        initPos = gameObject.transform.position;
        initRotation = gameObject.transform.eulerAngles; 
    }

    // Update is called once per frame
    void Update () {
        cameraPos = gameObject.transform.position;

        //MoveCamera
        MoveCamera();

        //Scroll Camera
        zoomCamera(); 
     
    }

    #region CameraMovement
    private void zoomCamera()
    {
        currentZoom -= Input.GetAxisRaw("Mouse ScrollWheel") * Time.unscaledDeltaTime * 1000 * zoomSpeed;

        currentZoom = Mathf.Clamp(currentZoom, zoomRange.x, zoomRange.y);
        transform.position = new Vector3(transform.position.x, transform.position.y - (transform.position.y - (initPos.y + currentZoom)) * 0.1f, transform.position.z);

        float x = transform.eulerAngles.x - (transform.eulerAngles.x - (initRotation.x + currentZoom * zoomRotation)) * 0.1f;
        x = Mathf.Clamp(x, zoomAngleRange.x, zoomAngleRange.y);

        transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void MoveCamera()
    {
        if(InGameUIController.instance == null )
        {
            return; 
        }
        if (InGameUIController.instance.Self != null)
        {
            camera.transform.localPosition = new Vector3(InGameUIController.instance.Self.transform.position.x, cameraPos.y, InGameUIController.instance.Self.transform.position.z -15); 
        }
        else
        {
            //MOVING 
            bool up = Input.GetKey(KeyCode.UpArrow);
            bool down = Input.GetKey(KeyCode.DownArrow);
            bool right = Input.GetKey(KeyCode.RightArrow);
            bool left = Input.GetKey(KeyCode.LeftArrow);

            if (up && cameraPos.z < maxY)
            {
                camera.transform.Translate(Vector3.forward * Time.unscaledDeltaTime * speedCamera, Space.World);
            }
            if (down && cameraPos.z > minY)
            {
                camera.transform.Translate(-Vector3.forward * Time.unscaledDeltaTime * speedCamera, Space.World);
            }
            if (left && cameraPos.x > minX)
            {
                camera.transform.Translate(Vector3.left * Time.unscaledDeltaTime * speedCamera, Space.World);
            }
            if (right && cameraPos.x < maxX)
            {
                camera.transform.Translate(Vector3.right * Time.unscaledDeltaTime * speedCamera, Space.World);
            }
        }
    }
    #endregion
}
