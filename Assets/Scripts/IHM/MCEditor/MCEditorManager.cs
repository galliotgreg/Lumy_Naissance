using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MCEditorManager : MonoBehaviour {

    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static MCEditorManager instance = null;

    //Templates & Prefabs
    [SerializeField]
    private GameObject statePrefab;
    [SerializeField]
    private GameObject transitionPrefab;
    [SerializeField]
    private GameObject pinPrefab;
    [SerializeField]
    private GameObject operatorPrefab;
    [SerializeField]
    private GameObject parameterPrefab;
    [SerializeField]
    private GameObject actionPrefab;
    [SerializeField]
    private GameObject[] mcTemplates;

    //Path
    public string SPECIE_FILE_SUFFIX = "specie.csv";
    public string INPUTS_FOLDER_PATH = "Assets/Inputs/";
    public string PLAYER1_SPECIE_FOLDER = "Player1/";

    private Specie p1_specie;

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


    private ProxyABModel proxyABModel;

    // Use this for initialization
    // Use this for initialization
    void Start()
    {
        SetupCast();
    }

    private void SetupCast()
    {

    }


  /*  ProxyABModel Load_MC()
    {

    }*/

    void Save_MC()
    {

    }

    void Select()
    {

    }

    void DeleteTransition()
    {

    }

    void Move()
    {

    }
     void Copy()
    {

    }

    void Cut()
    {

    }

    void Undo()
    {

    }

    void Redo()
    {

    }

}
