using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;

[Serializable]
public class Help
{
    public int ID;
    public string Title;
    public string Content;

    public Help(int id, string title, string content)
    {
        this.ID = id;
        this.Title = title;
        this.Content = content;
    }
    public Help()
    {
        this.ID = -1;
    }
}
 
[Serializable]
public class HelpDatabase : MonoBehaviour
{
    private List<Help> database = new List<Help>();

    // Use this for initialization
    private void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(LoadDatabase);

    }

    //Goes through the database and returns pointer on specified item
    public Help FetchItemByID(int id)
   {
       for(int i=0; i< database.Count; i++)
       {
           if (database[i].ID == id)
               return database[i];
       }
       return null;
   }

    void LoadDatabase()
    {
        string path = Application.dataPath + "//Scenes//Xuan//Help.json";
        using (StreamReader stream = new StreamReader(path))
        {
            string json = stream.ReadToEnd();
            database = JsonConvert.DeserializeObject<List<Help>>(json);
        }
    }
}
