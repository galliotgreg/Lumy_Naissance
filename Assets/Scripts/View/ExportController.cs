using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Zip_Tool;
using UnityEditor;

public class ExportController : MonoBehaviour {

    private string filePath = Directory.GetCurrentDirectory() + "\\Assets\\Inputs\\Species";

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ExportSpecie);
    }

    void ExportSpecie()
    {
        Debug.Log("Select your folder");
        string folder_path = EditorUtility.OpenFolderPanel("Name", filePath, "");
        string[] filesName = Directory.GetFiles(folder_path, "*.csv",SearchOption.TopDirectoryOnly);
        Debug.Log("Select your destination folder");
        string destination_path = EditorUtility.OpenFolderPanel("Name", filePath, "");
        ZipUtil.Zip(folder_path, folder_path, filesName);

        
    }
}
