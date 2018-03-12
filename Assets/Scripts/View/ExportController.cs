using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Zip_Tool;

public class ExportController : MonoBehaviour {

    private string filePath = Directory.GetCurrentDirectory() + "\\Assets\\Inputs\\Species";

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ExportSpecie);
    }

    void ExportSpecie()
    {
        Debug.LogError("Export not implemented yet !");
        return;
        //string path = EditorUtility.OpenFolderPanel("Name", filePath, "");
        //Debug.Log(path);
        //string[] filesName = Directory.GetFiles(path, "*.csv",SearchOption.TopDirectoryOnly);

        //ZipUtil.Zip(path, filesName);
        
    }
}
