using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraInGame : MonoBehaviour {

    [SerializeField]
    private GameObject camera;


    [SerializeField]
    private float zoomSpeed = 10; 



    [SerializeField]
    private float maxX = 27;
    [SerializeField]
    private float minX = 9;
    [SerializeField]
    private float maxY = -4;
    [SerializeField]
    private float minY = -38;

    [SerializeField]
    private float minZoom = 26;
    [SerializeField]
    private float maxZoom = 40; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 cameraPos = gameObject.transform.position; 
       
        //Right
        if (xAxisValue < 0 && cameraPos.x < minX)
        {
            xAxisValue = 0.0f;
        }
        //Left 
        else if (xAxisValue > 0 && cameraPos.x > maxX)
        {
            xAxisValue = 0.0f;
        }
        //Down
        if (zAxisValue < 0 && cameraPos.z < minY)
        {
            zAxisValue = 0.0f;
        }
        //Up
        if (zAxisValue > 0 && cameraPos.z > maxY)
        {
            zAxisValue = 0.0f;
        }

        camera.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue), Space.World);

        //Y min 26
        //Y max 40; 
        if (cameraPos.y < minZoom && scroll > 0)
        {
            scroll = 0; 
        }
        if (cameraPos.y > maxZoom && scroll < 0)
        
        {
            scroll = 0;
        }
        camera.transform.Translate(0.0f, 0.0f, scroll * zoomSpeed);

            /*
            if (gameObject.transform.position.x < 9 && xAxisValue > 0)
            {
                gameObject.transform.Translate(new Vector3(xAxisValue, 0.0f, 0.0f), Space.World);
            }
            else if(gameObject.transform.position.x < 9 && xAxisValue < 0)
            {
                gameObject.transform.Translate(new Vector3(0.0f, 0.0f, 0.0f), Space.World);
            }
            else if (gameObject.transform.position.x > 27 && xAxisValue < 0)
            {
                gameObject.transform.Translate(new Vector3(xAxisValue, 0.0f, 0.0f), Space.World);
            }
            else if(gameObject.transform.position.x > 27 && xAxisValue >0)
            {
                gameObject.transform.Translate(new Vector3(0.0f, 0.0f, 0.0f), Space.World);
            }
            Debug.Log("XAXIS : " + xAxisValue + " Position X : " + gameObject.transform.position.x);
            if (gameObject.transform.position.z < -8 && zAxisValue > 0)
            {
                gameObject.transform.Translate(new Vector3(0.0f, 0.0f, zAxisValue), Space.World);
            }
            else if (gameObject.transform.position.z < -8 && zAxisValue < 0)
            {
                gameObject.transform.Translate(new Vector3(0.0f, 0.0f,0.0f), Space.World);
            }
            else if (gameObject.transform.position.z > -37 && zAxisValue < 0)
            {
                gameObject.transform.Translate(new Vector3(0.0f, 0.0f, zAxisValue), Space.World);
            }
            else if (gameObject.transform.position.z > -37 && zAxisValue > 0)
            {
                gameObject.transform.Translate(new Vector3(0.0f, 0.0f, 0.0f), Space.World);
            }
            */
            //float yPos = gameObject.transform.position.y; 
            //gameObject.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue),Space.World);

        
    }
}
