using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Zip_Tool;
using Crosstales.FB;


public class ImportController : MonoBehaviour {
    public static string filePath = Application.dataPath + @"/Inputs/Species";


    // Use this for initialization
    void Start()
    {
        //filePath = Application.dataPath + @"/Inputs/Species";
        //this.gameObject.GetComponent<Button>().onClick.AddListener(ImportSpecie);
    }
    public static bool SwarmIsExisting(string name)
    {
        string checking_folder = filePath + "\\" + name;
        if (Directory.Exists(checking_folder))
        {
            return true;
        }
        return false;
    }

    public static void ImportSpecie()
    {
             //string filePath = Application.dataPath + @"/Inputs/Species";

        string name = "";
        string path = FileBrowser.OpenSingleFile("Open Folder", "","zip");        
        if (path != null && path!= ""){
          
            name = (Path.GetFileName(path)).Split('.')[0];
            if (SwarmIsExisting(name))
            {
                SwarmEditUIController.instance.ToggleImportSwarmConfirmationPanel();
                return;
            } 
            string new_folder = filePath + "\\" + name;
            ZipUtil.Unzip(path, new_folder);
            AppContextManager.instance.UpdateSpeciesFoldersNames();
            SwarmEditUIController.instance.SelectSwarm(name);
        }
    }
    public static void ResetSpecie()
    {
        string filePath = Application.dataPath + @"/Inputs/Species";
        string name = "";

        //Free Specie folder
        string[] dirs = Directory.GetDirectories(filePath);
        foreach (string dir in dirs)
        {
            Directory.Delete(dir, true);
        }

        //Extract files
        string backupPath = Application.dataPath + @"/Inputs/Backup";
        string[] filesPath = Directory.GetFiles(backupPath, "*.zip", SearchOption.TopDirectoryOnly);
        foreach (string filepath in filesPath)
        {
            if (filepath != null && filepath != "")
            {

                name = (Path.GetFileName(filepath)).Split('.')[0];

                string new_folder = filePath + "\\" + name;
                ZipUtil.Unzip(filepath, new_folder);
            }
        }

        name = (Path.GetFileName(filesPath[0])).Split('.')[0];
        AppContextManager.instance.UpdateSpeciesFoldersNames();
        SwarmEditUIController.instance.SelectSwarm(name);
    }
}

