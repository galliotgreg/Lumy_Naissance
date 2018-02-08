using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class AppContextManager : MonoBehaviour
{
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
    private string activeSpeciePath;
    [SerializeField]
    private string activeBehaviorPath;
    [SerializeField]
    private string[] speciesFolderNames;
    [SerializeField]
    private string[] castsFileNames;

    /// <summary>
    /// Path to the folder hosting all available species
    /// </summary>
    [SerializeField]
    private string SPECIES_FOLDER_PATH = "Assets/Inputs/Species/";
    /// <summary>
    /// Casts files suffix
    /// </summary>
    [SerializeField]
    private string CAST_FILES_SUFFIX = "_behavior.csv";

    public string ActiveSpeciePath
    {
        get
        {
            return activeSpeciePath;
        }

        set
        {
            activeSpeciePath = value;
        }
    }

    public string ActiveBehaviorPath
    {
        get
        {
            return activeBehaviorPath;
        }

        set
        {
            activeBehaviorPath = value;
        }
    }

    public void SwitchActiveCast(string castName)
    {
        //Check if specie existe
        bool found = false;
        foreach (string name in castsFileNames)
        {
            if (name == castName)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            Debug.LogError(castName + " Does not exists");
        }

        //Set Specie path
        activeBehaviorPath = GetPathFromCastName(castName);
    }

    public void SwitchActiveSpecie(string specieName)
    {
        //Check if specie existe
        bool found = false;
        foreach (string name in speciesFolderNames)
        {
            if (name == specieName)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            Debug.LogError(specieName + " Does not exists");
        }

        //Set Specie path
        activeSpeciePath = GetPathFromSpecieName(specieName);

        //Set default cast
        UpdateCastsFilesNames();
        SetupDefaultActiveCast();
    }

    public string[] GetSpeciesFolderNames()
    {
        return speciesFolderNames;
    }

    public string[] GetCastFileNames()
    {
        return castsFileNames;
    }

    public void CreateSpecie(string specieName)
    {
        throw new NotImplementedException();
    }

    public void ForkCast(string specieName)
    {
        throw new NotImplementedException();
    }

    void Start()
    {
        //Retrieve Specie Folders Names
        UpdateSpeciesFoldersNames();
        SetupDefaultActiveSpecie();

        //Retrieve Cast Files Names
        UpdateCastsFilesNames();
        SetupDefaultActiveCast();
    }

    private void SetupDefaultActiveCast()
    {
        //Setup active cast
        //Check player preferences
        //TODO
        //If player preferences are empty
        activeBehaviorPath = GetPathFromCastName(castsFileNames[0]);
    }

    private void SetupDefaultActiveSpecie()
    {
        //Setup active specie
        //Check player preferences
        //TODO
        //If player preferences are empty
        activeSpeciePath = GetPathFromSpecieName(speciesFolderNames[0]);
    }

    private string GetPathFromCastName(string name)
    {
        return SPECIES_FOLDER_PATH + name + CAST_FILES_SUFFIX + "/";
    }

    private string GetPathFromSpecieName(string name)
    {
        return SPECIES_FOLDER_PATH + name + "/";
    }

    private void UpdateCastsFilesNames()
    {
        //Retrieve all directory content
        DirectoryInfo info = new DirectoryInfo(activeSpeciePath);
        FileInfo[] filesInfo = info.GetFiles();
        IList<string> castFileNamesList = new List<string>();

        //Filter behavior files
        for (int i = 0; i < filesInfo.Length; i++)
        {
            if (IsCastFile(filesInfo[i].Name))
            {
                castFileNamesList.Add(filesInfo[i].Name);
            }
        }

        //Set attribute
        castsFileNames = new string[castFileNamesList.Count];
        for (int i = 0; i < castFileNamesList.Count; i++)
        {
            string name = castFileNamesList[i].Replace(CAST_FILES_SUFFIX, "");
            castsFileNames[i] = name;
        }
    }

    private bool IsCastFile(string name)
    {
        //Check if is behavior file
        string pat = @".+" + CAST_FILES_SUFFIX;
        Regex r = new Regex(pat);
        Match m = r.Match(name);
        if (!(m.Length > 0))
        {
            return false;
        }

        //Check if is meta file
        pat = @".+meta";
        r = new Regex(pat);
        m = r.Match(name);
        if (m.Length > 0)
        {
            return false;
        }

        return true;
    }

    private void UpdateSpeciesFoldersNames()
    {
        DirectoryInfo info = new DirectoryInfo(SPECIES_FOLDER_PATH);
        DirectoryInfo[] dirsInfo = info.GetDirectories();
        speciesFolderNames = new string[dirsInfo.Length];
        for (int i = 0; i < dirsInfo.Length; i++)
        {
            speciesFolderNames[i] = dirsInfo[i].Name;
        }
    }
}
