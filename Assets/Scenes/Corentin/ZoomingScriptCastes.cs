using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoomingScriptCastes : MonoBehaviour
{

    public float minFoV = 1f;
    public float maxFoV = 150f;
    private float sensitivity = 10f;

    void Update()
    {
        if (SceneManager.GetSceneByName("CastesScene").isLoaded)
        {
            float prev_size = NavigationManager.instance.GetCurrentCamera().fieldOfView;
            float new_size = prev_size + Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            new_size = Mathf.Clamp(new_size, minFoV, maxFoV);
            NavigationManager.instance.GetCurrentCamera().fieldOfView = new_size;
        } 
    }
}
