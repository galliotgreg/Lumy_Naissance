using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Zip_Tool;
using UnityEditor;
public class ImportController : MonoBehaviour {

    private string filePath = Directory.GetCurrentDirectory() + @"/Assets/Inputs/Species";

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ImportSpecie);
    }

    void ImportSpecie()
    {
        string path = EditorUtility.OpenFilePanel("Name", filePath, "");
        if (path != null){
            Debug.Log(path);
            string[] path_cutted = path.Split('/');
            string name = path_cutted[path_cutted.Length - 1];
            //Decompress(path);
            ZipUtil.Unzip(path, filePath);
        }

    }

}

