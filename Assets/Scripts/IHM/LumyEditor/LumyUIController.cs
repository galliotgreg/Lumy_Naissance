using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumyUIController : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static LumyUIController instance = null;

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        LumyEditorManager.instance.EditedLumy.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        LumyEditorManager.instance.EditedLumy.SetActive(false);
    }
}
