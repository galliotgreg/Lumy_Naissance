using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumyPictFactory : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static LumyPictFactory instance = null;

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
    private List<GameObject> pictPrefabs;

    public GameObject InstanciatePict(int id)
    {
        return Instantiate(pictPrefabs[id], Vector3.zero, Quaternion.identity);
    }

    public GameObject[] InstanciateAllPicts()
    {
        GameObject[] allPicts = new GameObject[pictPrefabs.Count];
        int i = 0;
        foreach (GameObject prefab in pictPrefabs)
        {
            allPicts[i] = InstanciatePict(i);
            i++;
        }
        return allPicts;
    }
}
