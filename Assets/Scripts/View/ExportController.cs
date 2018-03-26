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
        string folder_path = AppContextManager.instance.ActiveSpecieFolderPath;
        if (folder_path!="")
        {       
            string[] path_cutted = folder_path.Split('/');
            string folder_name = path_cutted[path_cutted.Length - 2];

            string relative_folder_path = Application.dataPath + @"/Inputs/Species/" + folder_name;
            string[] filesName = Directory.GetFiles(folder_path, "*.csv", SearchOption.TopDirectoryOnly);
            string destination_path = FileBrowser.OpenSingleFolder("Choose your destination folder");
            if (destination_path == "")
            {
                destination_path = path;
            }
            Debug.Log(folder_name);
            ZipUtil.Zip(folder_name, destination_path, filesName);
        }
    }
}
