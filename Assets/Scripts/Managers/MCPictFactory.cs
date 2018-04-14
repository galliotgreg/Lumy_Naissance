using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCPictFactory : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static MCPictFactory instance = null;

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

    [SerializeField]
    private List<GameObject> pictos;
    [SerializeField]
    private List<string> pictosIds;

    public GameObject InstanciatePict(string id)
    {
        int pictIndex = pictosIds.IndexOf(id);
        if (pictIndex >= 0)
        {
            return Instantiate(pictos[pictIndex], Vector3.zero, pictos[pictIndex].transform.rotation);
        }

        return null;
    }
}
