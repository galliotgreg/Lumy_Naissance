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
        LoadDatabase();
    }

    //Goes through the database and returns pointer on specified item
    public Help FetchHelpByID(int id)
   {
       for(int i=0; i< database.Count; i++)
       {
           if (database[i].ID == id)
               return database[i];
       }
       if (id >= database.Count)
        {
            return database[0];

        }
        return null;
   }
    public Help FetchHelpByTitle(string title)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].Title == title)
                return database[i];
        }
        //TODO RETURN SOMETHING WRONG
        return database[0];

    }
    public void LoadDatabase(string namefile = "parametres")
    {
        string path = Application.dataPath + @"/Inputs/HelpFiles/"+ namefile +".json";
        using (StreamReader stream = new StreamReader(path))
        {
            string json = stream.ReadToEnd();
            database = JsonConvert.DeserializeObject<List<Help>>(json);
            stream.Close();
        }
    }
    public int GetLength()
    {
        return database.Count;
    }
}
