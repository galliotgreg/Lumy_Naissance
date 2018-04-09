using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;

[Serializable]
public class SubHelp
{
    
    public string SubTitle { get; set; }
    public string Content { get; set; }
    /*
    public string SubTitle;
    public string Content;

    public SubHelp(string subtitle, string content)
    {
        this.SubTitle = subtitle;
        this.Content = content;
    }*/
}

[Serializable]
public class Help
{
    
    public int ID { get; set; }
    public string Title { get; set; }
    public IList<SubHelp> Content { get; set; }
   // public string Content { get; set; }

    public string Image { get; set; }
    public string Video { get; set; }
    public bool IsSimple_Content = false;
/*
    public int ID;
    public string Title;
    public bool IsSimple_Content;
   // public IList<SubHelp> Content;

    public string Content;
    public string Image;
    public string Video;
    
    public Help(int id, string title, IList<SubHelp> content, string image, string video)
    {

         this.ID = id;
         this.Title = title;
         this.Content = content;
         this.Image = image;
         this.Video = video;
         this.IsSimple_Content = false;

    } 
    
    public Help(int id, string title, string content, string image, string video)
    {

        this.ID = id;
        this.Title = title;
        this.Content = content;
        this.Image = image;
        this.Video = video;
        this.IsSimple_Content = true;
    }
    
    public Help()
     {
         this.ID = -1;
     }
     */
    public string GetContentText()
    {
        string ContentText = "";
        if (this.IsSimple_Content == true)
        {
            return ContentText;
        }
        else
        {
            foreach (SubHelp subhelp in Content)
            {
                if (subhelp.SubTitle == "")
                {
                    ContentText += subhelp.Content + "\n\n";
                }
                else
                {
                    ContentText += "<b><size=28>" + subhelp.SubTitle + "</size></b>\n" + subhelp.Content + "\n\n";
                }
            }
        }
        return ContentText;
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

    public Help GetFirstfromList()
    {
        if(database[0] != null)
            return database[0];
        return null;
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
