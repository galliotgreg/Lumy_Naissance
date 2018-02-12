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
    private string activeSpecieFolderPath;
    [SerializeField]
    private string activeSpecieFilePath;
    [SerializeField]
    private string activeBehaviorPath;
    [SerializeField]
    private string[] speciesFolderNames;
    [SerializeField]
    private string[] castsFileNames;

    private Specie activeSpecie;
    private Cast activeCast;

    /// <summary>
    /// Path to the folder hosting all available species
    /// </summary>
    [SerializeField]
    private string SPECIES_FOLDER_PATH = "Assets/Inputs/Species/";
    /// <summary>
    /// Casts files suffix
    /// </summary>
    [SerializeField]
    public string CAST_FILES_SUFFIX = "_behavior.csv";

    /// <summary>
    /// Casts files suffix
    /// </summary>
    [SerializeField]
    public string SPECIE_FILES_SUFFIX = "_specie.csv";

    /// <summary>
    /// Casts files suffix
    /// </summary>
    [SerializeField]
    public string CSV_EXT = ".csv";

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

    public Specie ActiveSpecie
    {
        get
        {
            return activeSpecie;
        }

        set
        {
            activeSpecie = value;
        }
    }

    public string ActiveSpecieFolderPath
    {
        get
        {
            return activeSpecieFolderPath;
        }

        set
        {
            activeSpecieFolderPath = value;
        }
    }

    public string ActiveSpecieFilePath
    {
        get
        {
            return activeSpecieFilePath;
        }

        set
        {
            activeSpecieFilePath = value;
        }
    }

    public Cast ActiveCast
    {
        get
        {
            return activeCast;
        }

        set
        {
            activeCast = value;
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
        activeCast = activeSpecie.Casts[castName];
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
        activeSpecieFilePath = GetFilePathFromSpecieName(specieName);
        activeSpecieFolderPath = GetFolderPathFromSpecieName(specieName);
        ParseSpecie();

        //Set default cast
        UpdateCastsFilesNames();
        SetupDefaultActiveCast();
    }

    private void ParseSpecie()
    {
        //Read file
        StreamReader reader = new StreamReader(activeSpecieFilePath);
        List<string> lines = new List<string>();
        while (reader.Peek() >= 0)
        {
            lines.Add(reader.ReadLine());
        }
        reader.Close();
        //Parse file
        SpecieParser parser = new SpecieParser();
        activeSpecie = parser.Parse(lines);
        string[] tokens = activeSpecieFilePath.Split('/');
        string name = tokens[tokens.Length - 1].Replace(SPECIE_FILES_SUFFIX, "");
        name = Char.ToUpperInvariant(name[0]) + name.Substring(1);
        activeSpecie.Name = name;
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
        ParseSpecie();

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
        List<string> keyList = new List<string>(this.activeSpecie.Casts.Keys);
        string castName = keyList[0];
        activeCast = activeSpecie.Casts[castName];
        activeBehaviorPath = SPECIES_FOLDER_PATH + activeSpecie.Name + "/" +
            activeCast.BehaviorModelIdentifier + CSV_EXT;
    }

    private void SetupDefaultActiveSpecie()
    {
        //Setup active specie
        //Check player preferences
        //TODO
        //If player preferences are empty
        activeSpecieFilePath = GetFilePathFromSpecieName(speciesFolderNames[0]);
        activeSpecieFolderPath = GetFolderPathFromSpecieName(speciesFolderNames[0]);
    }

    private string GetPathFromCastName(string name)
    {
        return SPECIES_FOLDER_PATH + name + CAST_FILES_SUFFIX;
    }

    private string GetFilePathFromSpecieName(string name)
    {
        string filename = Char.ToLowerInvariant(name[0]) + name.Substring(1) + SPECIE_FILES_SUFFIX;
        return SPECIES_FOLDER_PATH + name + "/" + filename;
    }

    private string GetFolderPathFromSpecieName(string name)
    {
        return SPECIES_FOLDER_PATH + name + "/";
    }

    private void UpdateCastsFilesNames()
    {
        //Retrieve all directory content
        DirectoryInfo info = new DirectoryInfo(activeSpecieFolderPath);
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
