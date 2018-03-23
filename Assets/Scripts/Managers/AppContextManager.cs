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

    private bool prysmeEdit = false;

    /// <summary>
    /// Path to the folder hosting all available species
    /// </summary>
    [SerializeField]
    private string player1FolderPath = "Inputs/Player1/";

    /// <summary>
    /// Path to the folder hosting all available species
    /// </summary>
    [SerializeField]
    private string playre2FolderPath = "Inputs/Player2/";

    /// <summary>
    /// Path to the folder hosting all available species
    /// </summary>
    [SerializeField]
    private string speciesFolderPath = "Inputs/Species/";

    /// <summary>
    /// Path to the folder hosting the specie template
    /// </summary>
    [SerializeField]
    private string templateFolderPath = "Inputs/SpecieTemplate/";

    /// <summary>
    /// Path to the folder hosting the specie template
    /// </summary>
    [SerializeField]
    private string TEMPLATE_SPECIE_FILE_NAME = "XXX_specie";

    /// <summary>
    /// Casts files suffix
    /// </summary>
    [SerializeField]
    public string CAST_FILES_SUFFIX = "_behavior";

    /// <summary>
    /// Casts files suffix
    /// </summary>
    [SerializeField]
    public string SPECIE_FILES_SUFFIX = "_specie";

    /// <summary>
    /// Default specie name radix
    /// </summary>
    [SerializeField]
    public string DEFAULT_SPECIE_NAME = "maNuee";

    /// <summary>
    /// File name of the prysme behavior file.
    /// </summary>
    [SerializeField]
    public string PRYSME_FILE_NAME = "prysme_behavior";

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
            return  activeSpecieFolderPath;
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

    public string Player1FolderPath
    {
        get
        {
            return Application.dataPath + "/" + player1FolderPath;
        }

        set
        {
            player1FolderPath = value;
        }
    }

    public string Playre2FolderPath
    {
        get
        {
            return Application.dataPath + "/" + playre2FolderPath;
        }

        set
        {
            playre2FolderPath = value;
        }
    }

    public string SpeciesFolderPath
    {
        get
        {
            return Application.dataPath + "/" + speciesFolderPath;
        }

        set
        {
            speciesFolderPath = value;
        }
    }

    public string TemplateFolderPath
    {
        get
        {
            return Application.dataPath + "/" + templateFolderPath;
        }

        set
        {
            templateFolderPath = value;
        }
    }

    public bool PrysmeEdit
    {
        get
        {
            return prysmeEdit;
        }

        set
        {
            prysmeEdit = value;
        }
    }

    public void SwitchActiveCast(string castName)
    {
        if (!activeSpecie.Casts.ContainsKey(castName))
        {
            Debug.LogError(castName + " Does not exists");
            return;
        }

        //Set Specie path
        activeCast = activeSpecie.Casts[castName];
        activeBehaviorPath = GetPathFromCastName(castName);
    }

    public void LoadPlayerSpecies(string player1SpecieName, string player2SpecieName)
    {
        //Create Dirs in needed
        if (!Directory.Exists(Player1FolderPath))
        {
            Directory.CreateDirectory(Player1FolderPath);
        }
        if (!Directory.Exists(Playre2FolderPath))
        {
            Directory.CreateDirectory(Playre2FolderPath);
        }

        //Free Folders
        DirectoryInfo di = new DirectoryInfo(Player1FolderPath);
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
         di = new DirectoryInfo(Playre2FolderPath);
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }

        //Copy files
        di = new DirectoryInfo(SpeciesFolderPath + player1SpecieName + "/");
        foreach (FileInfo file in di.GetFiles())
        {
            File.Copy(
            file.FullName,
            Player1FolderPath + file.Name);
        }
        di = new DirectoryInfo(SpeciesFolderPath + player2SpecieName + "/");
        foreach (FileInfo file in di.GetFiles())
        {
            File.Copy(
            file.FullName,
            Playre2FolderPath + file.Name);
        }
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
        string name = tokens[tokens.Length - 1].Replace(SPECIE_FILES_SUFFIX + CSV_EXT, "");
        name = Char.ToUpperInvariant(name[0]) + name.Substring(1);
        activeSpecie.Name = name;
    }

    private void SaveSpecie()
    {
        //Read old file
        StreamReader reader = new StreamReader(activeSpecieFilePath);
        List<string> lines = new List<string>();
        while (reader.Peek() >= 0)
        {
            lines.Add(reader.ReadLine());
        }
        reader.Close();

        //Build new file
        //Copy Header
        string content = lines[0] + "\n"  + lines[1] + "\n";
        //Write cast definitions
        content += "Name,Behavior,Head Size,Components List,\n";
        foreach (KeyValuePair<string, Cast> entry in activeSpecie.Casts)
        {
            Cast curCast = entry.Value;
            content += curCast.Name + "," + curCast.BehaviorModelIdentifier + "," + curCast.Head.Count + ",";
            foreach (ComponentInfo compoInfo in curCast.Head)
            {
                content += compoInfo.Id + ",";
            }
            foreach (ComponentInfo compoInfo in curCast.Tail)
            {
                content += compoInfo.Id + ",";
            }
            content += "\n";
        }
        //Write cast hierarchy
        content += "Cast, Parent,\n";
        foreach (KeyValuePair<string, Cast> entry in activeSpecie.Casts)
        {
            Cast curCast = entry.Value;

            if (curCast.Parent != null)
            {
                content += curCast.Name + "," + curCast.Parent.Name + ",";
            } else
            {
                content += curCast.Name + ",,";
            }
            content += "\n";
        }

        //Replace file
        File.Delete(activeSpecieFilePath);
        File.AppendAllText(activeSpecieFilePath, content);
    }

    public void SaveCast()
    {
        //Read old file
        StreamReader reader = new StreamReader(activeSpecieFilePath);
        List<string> lines = new List<string>();
        while (reader.Peek() >= 0)
        {
            lines.Add(reader.ReadLine());
        }
        reader.Close();

        //Re write only cast line
        string content = "";
        foreach (string line in lines)
        {
            string[] tokens = line.Split(',');
            if (tokens[0] == activeCast.Name && tokens[1] == activeCast.BehaviorModelIdentifier)
            {
                content += activeCast.Name + "," + activeCast.BehaviorModelIdentifier + "," + activeCast.Head.Count + ",";
                foreach (ComponentInfo compoInfo in activeCast.Head)
                {
                    content += compoInfo.Id + ",";
                }
                foreach (ComponentInfo compoInfo in activeCast.Tail)
                {
                    content += compoInfo.Id + ",";
                }
                content += "\n";
            } else
            {
                content += line + "\n";
            }
        }

        //Replace file
        File.Delete(activeSpecieFilePath);
        File.AppendAllText(activeSpecieFilePath, content);
    }

    public string[] GetSpeciesFolderNames()
    {
        return speciesFolderNames;
    }

    public string[] GetCastFileNames()
    {
        return castsFileNames;
    }

    public void CopySpecie(string specieName)
    {
        string specieFolderName = Char.ToUpperInvariant(specieName[0]) + specieName.Substring(1);

        //Check is specie already exists
        foreach (string curFolderName in speciesFolderNames)
        {
            if (specieFolderName == curFolderName)
            {
                Debug.LogError("Cannot create " + specieName + " because this name is already used !");
            }
        }

        // Create Folder
        Directory.CreateDirectory(GetFolderPathFromSpecieName(specieFolderName));

        //Update Data
        UpdateSpeciesFoldersNames();

        //Create Files 
        DirectoryInfo di = new DirectoryInfo(ActiveSpecieFolderPath);
        foreach (FileInfo file in di.GetFiles())
        {
            File.Copy(
            file.FullName,
            GetFolderPathFromSpecieName(specieFolderName) + file.Name);
        }
        File.Move(GetFolderPathFromSpecieName(specieFolderName) + ActiveSpecie.Name + SPECIE_FILES_SUFFIX + CSV_EXT,
            GetFolderPathFromSpecieName(specieFolderName) + specieName + SPECIE_FILES_SUFFIX + CSV_EXT);
    }

    public void CreateSpecie(string specieName)
    {
        string specieFolderName = Char.ToUpperInvariant(specieName[0]) + specieName.Substring(1);

        //Check is specie already exists
        foreach (string curFolderName in speciesFolderNames)
        {
            if (specieFolderName == curFolderName)
            {
                Debug.LogError("Cannot create " + specieName + " because this name is already used !");
            }
        }

        // Create Folder
        Directory.CreateDirectory(GetFolderPathFromSpecieName(specieFolderName));

        //Update Data
        UpdateSpeciesFoldersNames();

        //Create Files 
        DirectoryInfo di = new DirectoryInfo(TemplateFolderPath);
        foreach (FileInfo file in di.GetFiles())
        {
            File.Copy(
            file.FullName,
            GetFolderPathFromSpecieName(specieFolderName) + file.Name);
        }
        File.Move(GetFolderPathFromSpecieName(specieFolderName) + TEMPLATE_SPECIE_FILE_NAME + CSV_EXT,
            GetFolderPathFromSpecieName(specieFolderName) + specieName + SPECIE_FILES_SUFFIX + CSV_EXT);

        // Set created as active
        //CastesUIController.instance.CreateSwarmSelectionButons();
        //CastesUIController.instance.SelectActiveSwarm(specieFolderName);
    }

    public void DeleteCast()
    {
        File.Delete(ActiveSpecieFolderPath + activeCast.BehaviorModelIdentifier + CSV_EXT);        

        //Remove childs from specie
        activeSpecie.Casts.Remove(activeCast.Name);

        //Alter Specie file
        SaveSpecie();
    }

    public void DeleteSpecie()
    {
        Directory.Delete(ActiveSpecieFolderPath, true);
        UpdateSpeciesFoldersNames();
    }

    /* No more used since Lumy/Nuee Screen refacto
    */
    [Obsolete("UnforkCast is deprecated, please use DeleteCast instead.")]
    public void UnforkCast()
    {
        //Remove Behaviod files
        Cast child1 = activeCast.Childs[0];
        Cast child2 = activeCast.Childs[1];
        File.Delete(ActiveSpecieFolderPath + child1.BehaviorModelIdentifier + CSV_EXT);
        File.Delete(ActiveSpecieFolderPath + child2.BehaviorModelIdentifier + CSV_EXT);

        //Remove childs from specie
        activeSpecie.Casts.Remove(child1.Name);
        activeSpecie.Casts.Remove(child2.Name);
        activeCast.Childs.Clear();

        //Alter Specie file
        SaveSpecie();
    }

    public void CloneCast()
    {
        //Create childs
        Cast clone = activeCast.Clone();
        int iterator = 0;
        foreach(string key in  activeSpecie.Casts.Keys){
            iterator++;
        }
        iterator++;
        clone.Name = activeCast.Name +"("+ iterator + ")";

        clone.BehaviorModelIdentifier =
            activeCast.BehaviorModelIdentifier.Replace(CAST_FILES_SUFFIX, "")
            + "(" + iterator + ")" + CAST_FILES_SUFFIX;    

        //Add childs to specie
        activeSpecie.Casts.Add(clone.Name, clone);       

        //Copy Behavior files
        File.Copy(
            ActiveSpecieFolderPath + activeCast.BehaviorModelIdentifier + CSV_EXT,
            ActiveSpecieFolderPath + clone.BehaviorModelIdentifier + CSV_EXT);

        //Alter Specie file
        SaveSpecie();
    }

    public void CreateCast()
    {
        //Create childs
        Cast newCast = new Cast();
        int iterator = 0;
        newCast.Name = "Lumy" + '('+ iterator + ')';
        bool isAdd = false;
        while (!isAdd)
        {
            if (activeSpecie.Casts.ContainsKey(newCast.Name))
            {
                iterator++;
                newCast.Name = "Lumy" + '(' + iterator + ')';
            } else
            {
                isAdd = true;
            }            
        }
        newCast.Head = new List<ComponentInfo>();
        newCast.Tail = new List<ComponentInfo>();

        newCast.Head.Add(ComponentFactory.instance.CreateComponent(1));
        newCast.Tail.Add(ComponentFactory.instance.CreateComponent(2));

        newCast.BehaviorModelIdentifier = "Lumy"
            + "(" + iterator + ")" + CAST_FILES_SUFFIX;

        //Add childs to specie
        activeSpecie.Casts.Add(newCast.Name, newCast);

        //Copy Behavior files
        File.Create(ActiveSpecieFolderPath + newCast.BehaviorModelIdentifier + CSV_EXT);
        
        //Alter Specie file
        SaveSpecie();
    }

    /* No more used since Lumy/Nuee Screen refacto
     */
    [Obsolete("ForkCast is deprecated, please use CloneCast instead.")]
    public void ForkCast()
    {
        //Create childs
        Cast child1 = activeCast.Clone();
        Cast child2 = activeCast.Clone();
        
        child1.Name = activeCast.Name + "_Left";
        child2.Name = activeCast.Name + "_Right";
        child1.BehaviorModelIdentifier = 
            activeCast.BehaviorModelIdentifier.Replace(CAST_FILES_SUFFIX, "")
            + "_Left" + CAST_FILES_SUFFIX;
        child2.BehaviorModelIdentifier =
            activeCast.BehaviorModelIdentifier.Replace(CAST_FILES_SUFFIX, "")
            + "_Right" + CAST_FILES_SUFFIX;


        //Link Childs
        child1.Childs.Clear();
        child2.Childs.Clear();
        child1.Parent = activeCast;
        child2.Parent = activeCast;
        activeCast.Childs.Add(child1);
        activeCast.Childs.Add(child2);

        //Add childs to specie
        activeSpecie.Casts.Add(child1.Name, child1);
        activeSpecie.Casts.Add(child2.Name, child2);

        //Copy Behavior files
        File.Copy(
            ActiveSpecieFolderPath + activeCast.BehaviorModelIdentifier + CSV_EXT,
            ActiveSpecieFolderPath + child1.BehaviorModelIdentifier + CSV_EXT);
        File.Copy(
            ActiveSpecieFolderPath + activeCast.BehaviorModelIdentifier + CSV_EXT,
            ActiveSpecieFolderPath + child2.BehaviorModelIdentifier + CSV_EXT);

        //Alter Specie file
        SaveSpecie();
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
        activeBehaviorPath = SpeciesFolderPath + activeSpecie.Name + "/" +
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
        return SpeciesFolderPath + activeSpecie.Name + "/" 
            + activeSpecie.Casts[name].BehaviorModelIdentifier + CSV_EXT;
    }

    private string GetFilePathFromSpecieName(string name)
    {
        string filename = Char.ToLowerInvariant(name[0]) + name.Substring(1) + SPECIE_FILES_SUFFIX + CSV_EXT;
        return SpeciesFolderPath + name + "/" + filename;
    }

    private string GetFolderPathFromSpecieName(string name)
    {
        return SpeciesFolderPath + name + "/";
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
            string name = castFileNamesList[i].Replace(CAST_FILES_SUFFIX + CSV_EXT, "");
            castsFileNames[i] = name;
        }
    }

    private bool IsCastFile(string name)
    {
        //Check if is behavior file
        string pat = @".+" + CAST_FILES_SUFFIX + CSV_EXT;
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
        DirectoryInfo info = new DirectoryInfo(SpeciesFolderPath);
        DirectoryInfo[] dirsInfo = info.GetDirectories();
        speciesFolderNames = new string[dirsInfo.Length];
        for (int i = 0; i < dirsInfo.Length; i++)
        {
            speciesFolderNames[i] = dirsInfo[i].Name;
        }
    }
}
