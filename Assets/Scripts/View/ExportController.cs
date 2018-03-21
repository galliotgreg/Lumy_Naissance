using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Zip_Tool;
using Crosstales.FB;

public class ExportController : MonoBehaviour {

    //private string filePath = Directory.GetCurrentDirectory() + @"/Inputs/Species";

    // Use this for initialization
    void Start()
    {
        //this.gameObject.GetComponent<Button>().onClick.AddListener(ExportSpecie);
    }

    public static void ExportSpecie()
    {
        string path = Application.dataPath + @"/Inputs/Species";
        string folder_path = FileBrowser.OpenSingleFolder("Select your file", path);
        
        string[] path_cutted = folder_path.Split('\\');
        string name_folder = path_cutted[path_cutted.Length - 1];

        string relative_folder_path = Application.dataPath + @"/Inputs/Species/" + name_folder;
        string[] filesName = Directory.GetFiles(folder_path, "*.csv",SearchOption.TopDirectoryOnly);
        string destination_path = FileBrowser.OpenSingleFolder("Choose your destination folder");

        ZipUtil.Zip(relative_folder_path, destination_path, filesName);

    }
}
