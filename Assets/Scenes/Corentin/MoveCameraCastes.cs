using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCameraCastes : MonoBehaviour
{
    public float speed = 10.0f;
    private bool isMovable = false;
    private bool noDialogBox = true;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1) && SceneManager.GetSceneByName("CastesScene").isLoaded)
        {
            isMovable = !isMovable;
        }

        if (isMovable)
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed,
                                           Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0.0f);
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed,
                                            Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0.0f);
            }
            if (Input.GetMouseButtonUp(1))
            {
                isMovable = !isMovable;
            }
        }
    }

}
