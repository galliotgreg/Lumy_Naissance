using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using Zip_Tool;

public class ImportController : MonoBehaviour {

    private string filePath = Directory.GetCurrentDirectory() + @"/Assets/Inputs/Species";

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ImportSpecie);
    }

    void CopyFolder(string sourcePath)
    {
        string pol = @"C:/Users/etu2017/Desktop/MaNuee7";
        string ol = @"C:/Users/etu2017/Desktop/TMP/test/Lumy_Naissance/Assets/Inputs/Species";
        FileUtil.CopyFileOrDirectory(pol, ol);
    }

    void ImportSpecie()
    {
        // System.Diagnostics.Process.Start("Explorer.exe", @"/select,""" + filePath);
        //System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
        //string defaultName = BuildDefaultName();
        //AppContextManager.instance.CreateSpecie(defaultName);
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

