using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppContextManager : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static AppContextManager instance = null;

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
    private string curSpecieFolderName;
    [SerializeField]
    private List<string> speciesFolderNames = new List<string>();

    /// <summary>
    /// The selected Swarm definition
    /// </summary>
    private Specie currentSpecie;

    /// <summary>
    /// The selected Cast definition
    /// </summary>
    private Cast currentCast;

    public Specie CurrentSpecie
    {
        get
        {
            return currentSpecie;
        }

        set
        {
            currentSpecie = value;
        }
    }

    public Cast CurrentCast
    {
        get
        {
            return currentCast;
        }

        set
        {
            currentCast = value;
        }
    }

    public List<string> SpeciesFolderNames
    {
        get
        {
            return speciesFolderNames;
        }
    
        set
        {
            speciesFolderNames = value;
        }
    }

    public string CurSpecieFolderName
    {
        get
        {
            return curSpecieFolderName;
        }

        set
        {
            curSpecieFolderName = value;
        }
    }

    public void SwitchSpecie(string specieName)
    {
        //Read file
        StreamReader reader = new StreamReader(curSpecieFolderName);
        List<string> lines = new List<string>();
        while (reader.Peek() >= 0)
        {
            lines.Add(reader.ReadLine());
        }

        SpecieParser parser = new SpecieParser();
        currentSpecie = parser.Parse(lines);
    }

    void Start()
    {
        //Use player preferences

        //If player preferences are empty

    }
}
