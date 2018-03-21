using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Zip_Tool;
using Crosstales.FB;


public class ImportController : MonoBehaviour {

    //private string filePath = Application.dataPath + @"/Inputs/Species";

    // Use this for initialization
    void Start()
    {
        //this.gameObject.GetComponent<Button>().onClick.AddListener(ImportSpecie);
    }

    public static void ImportSpecie()
    {
        // TODO Remove EdiorUtility
        /* Application.dataPath + "/" + compoDataPath
        System.IO.StreamReader reader = new System.IO.StreamReader(path);
        return reader.ReadToEnd();*/

        // Debug.Log(Application.dataPath + "/");
        string filePath = Application.dataPath + @"/Inputs/Species";

        string path = FileBrowser.OpenSingleFile("Open Folder", "","zip");        
        if (path != null && path!= ""){
          
            string name = (Path.GetFileName(path)).Split('.')[0];
            
            string new_folder = filePath + "\\" + name;
            ZipUtil.Unzip(path, new_folder);            
        }        
    }

}

